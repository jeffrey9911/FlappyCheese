using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class PenguinAgent : Agent
{

    [SerializeField] private Transform cheeseTransform;

    public override void OnEpisodeBegin()
    {
        this.transform.localPosition = new Vector3(-2, 0, 0);
        this.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(this.transform.position);
        sensor.AddObservation(cheeseTransform.position);
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
        discreteAction[0] = Input.GetKeyDown(KeyCode.LeftShift) ? 1 : 0;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "Cheese")
        {
            Debug.Log("Penguin Win!");
            SetReward(1f);
            cheeseTransform.GetComponent<CheeseAgent>().EndByPenguin();
            EndEpisode();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "LeftEdge")
        {
            Debug.Log("Penguin Lose!");
            SetReward(-1f);
            cheeseTransform.GetComponent<CheeseAgent>().EndByPenguin();
            EndEpisode();
        }
    }

}
