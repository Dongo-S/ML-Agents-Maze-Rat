using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;


public class MazeAgent : Agent
{

    [SerializeField] [Range(0.1f, 1f)]float moveSpeed = 0.5f;

    public Material winMaterial;
    public Material loseMaterial;
    public Material loseTimeMaterial;
    public Material CheckPointMaterial;
    Material originalMaterial;
    public Renderer floorRenderer;
    [SerializeField] Generator mazeGenerator;
    [SerializeField] Vector3 goalPosition;
    public bool regenerateMaze = true;

    Vector2 initialGoalPosition;
    float rewardPenalty;

    Rigidbody m_AgentRb;

    private void Start()
    {
        rewardPenalty = 1f / MaxStep;

        m_AgentRb = GetComponent<Rigidbody>();
    }

    public override void OnEpisodeBegin()
    {
        if (regenerateMaze)
            mazeGenerator.GenerateNewMaze();

        m_AgentRb.velocity = Vector3.zero;
        transform.rotation = Quaternion.Euler(new Vector3(0f, Random.Range(0, 360)));

        Vector2 randomV = Random.insideUnitCircle;
        //transform.localPosition = Vector3.zero;
        transform.localPosition = mazeGenerator.startDummie.transform.localPosition + new Vector3(randomV.x, 0f, randomV.y);
        randomV = Random.insideUnitCircle;
        goalPosition = mazeGenerator.endDummie.transform.localPosition + new Vector3(randomV.x, 0f, randomV.y);
        if (regenerateMaze)
            mazeGenerator.endDummie.transform.localPosition = goalPosition;
        //targetTransform.localPosition = new Vector3(Random.Range(0f, 9f), 1.5f, Random.Range(-9f, 9f));
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.localPosition);
        sensor.AddObservation(goalPosition);
        sensor.AddObservation(transform.InverseTransformDirection(m_AgentRb.velocity));

        //sensor.AddObservation(targetTransform.transform.localPosition);
    }


    public override void OnActionReceived(ActionBuffers actions)
    {
        //Debug.Log(actions.DiscreteActions[0]);

        //float moveX = actions.ContinuousActions[0];
        //float moveZ = actions.ContinuousActions[1];
        //float rotation = actions.ContinuousActions[2];

        AddReward(-rewardPenalty);

        if (StepCount == MaxStep - 1)
        {
            //SetReward(-1f);
            floorRenderer.material = loseTimeMaterial;
            //EndEpisode();
        }

        MoveAgent(actions.DiscreteActions);
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var discreteActionsOut = actionsOut.DiscreteActions;
        discreteActionsOut[0] = 0;
        if (Input.GetKey(KeyCode.D))
        {
            discreteActionsOut[0] = 3;
        }
        else if (Input.GetKey(KeyCode.W))
        {
            discreteActionsOut[0] = 1;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            discreteActionsOut[0] = 4;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            discreteActionsOut[0] = 2;
        }
    }


    IEnumerator GoalScoredSwapGroundMaterial(Material mat, float time)
    {
        originalMaterial = floorRenderer.material;
        floorRenderer.material = mat;
        yield return new WaitForSeconds(time); //wait for 2 sec
        floorRenderer.material = originalMaterial;
    }


    public void MoveAgent(ActionSegment<int> act)
    {
        var dirToGo = Vector3.zero;
        var rotateDir = Vector3.zero;

        var action = act[0];
        switch (action)
        {
            case 1:
                dirToGo = transform.forward * 1f;
                break;
            case 2:
                dirToGo = transform.forward * -1f;
                break;
            case 3:
                rotateDir = transform.up * 1f;
                break;
            case 4:
                rotateDir = transform.up * -1f;
                break;
        }
        transform.Rotate(rotateDir, Time.deltaTime * 100f);
        m_AgentRb.AddForce(dirToGo * moveSpeed, ForceMode.VelocityChange);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Goal goal))
        {
            switch (goal.value)
            {
                case ValueCheckpoint.Checkpoint:
                    other.gameObject.SetActive(false);
                    AddReward(rewardPenalty);
                    StartCoroutine(GoalScoredSwapGroundMaterial(CheckPointMaterial, 0.2f));
                    break;
                case ValueCheckpoint.Goal:
                    SetReward(2f);
                    floorRenderer.material = winMaterial;
                    EndEpisode();
                    break;
                default:
                    break;
            }

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Wall wall))
        {
            SetReward(-1f);
            floorRenderer.material = loseMaterial;
            EndEpisode();
        }
    }

}
