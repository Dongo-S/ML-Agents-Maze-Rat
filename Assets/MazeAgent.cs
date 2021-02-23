using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;


public class MazeAgent : Agent
{

    [SerializeField] float moveSpeed;

    public Material winMaterial;
    public Material loseMaterial;
    Material original;
    public Renderer floorRenderer;
    [SerializeField] Generator mazeGenerator;
    [SerializeField] GameObject goal;



    public override void OnEpisodeBegin()
    {
        mazeGenerator.GenerateNewMaze();



        //transform.localPosition = Vector3.zero;
        transform.localPosition = mazeGenerator.startDummie.transform.localPosition;
        //targetTransform.localPosition = new Vector3(Random.Range(0f, 9f), 1.5f, Random.Range(-9f, 9f));
    }




    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.localPosition);
        sensor.AddObservation(mazeGenerator.endDummie.transform.localPosition);
        sensor.AddObservation(transform.rotation);
        //sensor.AddObservation(targetTransform.transform.localPosition);
    }


    public override void OnActionReceived(ActionBuffers actions)
    {
        //Debug.Log(actions.DiscreteActions[0]);

        float moveX = actions.ContinuousActions[0];
        float moveZ = actions.ContinuousActions[1];
        float rotation = actions.ContinuousActions[2];


        if (StepCount == MaxStep - 1)
        {
            floorRenderer.material = loseMaterial;
        }

        if ((gameObject.transform.rotation.y < 0.25f && rotation > 0f) ||
        (gameObject.transform.rotation.y > -0.25f && rotation < 0f))
        {
            gameObject.transform.Rotate(new Vector3(0, 1, 0), rotation);
        }

        transform.localPosition += new Vector3(moveX, 0f, moveZ) * Time.deltaTime * moveSpeed;
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
