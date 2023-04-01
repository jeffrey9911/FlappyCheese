using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class CheeseAgent : Agent
{
    [SerializeField] private Transform penguinTransform;

    public override void OnEpisodeBegin()
    {
        this.transform.localPosition = new Vector3(3, 0, 0);
        this.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(this.transform.position);
        sensor.AddObservation(penguinTransform.position);
        sensor.AddObservation(this.transform.GetChild(0).GetComponent<RayPerceptionSensorComponent2D>());
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        int actJump = actions.DiscreteActions[0];

        this.GetComponent<PlayerController>().ActJump(actJump);
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<int> discreteAction = actionsOut.DiscreteActions;
        discreteAction[0] = Input.GetKeyDown(KeyCode.RightShift) ? 1 : 0;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Penguin")
        {
            SetReward(-1f);
            EndEpisode();
        }
    }


    private void Update()
    {
        AddReward(Time.deltaTime * 0.1f);
    }


    public void EndByPenguin()
    {
        EndEpisode();
    }
}
