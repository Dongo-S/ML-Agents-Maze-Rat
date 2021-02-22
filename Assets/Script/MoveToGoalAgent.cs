using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class MoveToGoalAgent : Agent
{

    [SerializeField] private Transform targetTransform;

    public Material winMaterial;
    public Material loseMaterial;
    public Renderer floorRenderer;

    [SerializeField]float moveSpeed = 1f;


    public override void OnEpisodeBegin()
    {
        //transform.localPosition = Vector3.zero;
        transform.localPosition = new Vector3(Random.Range(-9f, 0f), 1.5f, Random.Range(-9f, -9f));
        targetTransform.localPosition = new Vector3(Random.Range(0f, 9f), 1.5f, Random.Range(-9f, 9f));
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.localPosition);
        sensor.AddObservation(targetTransform.transform.localPosition);
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        //Debug.Log(actions.DiscreteActions[0]);

        float moveX = actions.ContinuousActions[0];
        float moveZ = actions.ContinuousActions[1];


        transform.localPosition += new Vector3(moveX, 0f, moveZ)*Time.deltaTime*moveSpeed;
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<float> continuousAction = actionsOut.ContinuousActions;
        continuousAction[0] = Input.GetAxisRaw("Horizontal");
        continuousAction[1] = Input.GetAxisRaw("Vertical");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Goal>(out Goal goal))
        {
            SetReward(1f);
            floorRenderer.material = winMaterial;
            EndEpisode();
        }
        if (other.TryGetComponent<Wall>(out Wall wall))
        {
            SetReward(-1f);
            floorRenderer.material = loseMaterial;
            EndEpisode();
        }
    }
}
