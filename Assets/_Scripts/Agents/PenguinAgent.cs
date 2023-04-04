using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class PenguinAgent : Agent
{

    [SerializeField] private Transform cheeseTransform;
    [SerializeField] private Transform ceilingTransform;
    [SerializeField] private Transform bottomTransform;

    private AgentSensor sensorArea;

    private void Start()
    {
        sensorArea = this.transform.Find("Ring").Find("Sensor").GetComponent<AgentSensor>();
    }

    public override void OnEpisodeBegin()
    {
        this.transform.localPosition = new Vector3(-1.63f, -0.197f, 0);
        this.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        SetReward(0);
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(this.transform.position);
        sensor.AddObservation(ceilingTransform.position);
        sensor.AddObservation(bottomTransform.position);

        sensor.AddObservation(sensorArea.topRockC);
        sensor.AddObservation(sensorArea.topRockP);
        sensor.AddObservation(sensorArea.botRockC);
        sensor.AddObservation(sensorArea.botRockP);
        sensor.AddObservation(sensorArea.goalAreaC);
        sensor.AddObservation(sensorArea.goalAreaP);

        /*
        sensor.AddObservation(RockManager.instance.genInterval);
        sensor.AddObservation(RockManager.instance.moveSpeed);
        sensor.AddObservation(RockManager.instance.topRockHeight);
        sensor.AddObservation(RockManager.instance.botRockHeight);*/
        AddReward(0.5f);
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
            //Debug.Log("Penguin Win!");
            AddReward(10f);
            //cheeseTransform.GetComponent<CheeseAgent>().EndByPenguin();
            EndEpisode();
        }

        if (collision.transform.tag == "BotEdge" || collision.transform.tag == "TopEdge")
        {
            //Debug.Log("Penguin Hit Edge. Penguin Lose.");
            //cheeseTransform.GetComponent<CheeseAgent>().EndByPenguin();
            AddReward(-10f);
            EndEpisode();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "LeftEdge")
        {
            //Debug.Log("Penguin Lose!");
            AddReward(-10f);
            //cheeseTransform.GetComponent<CheeseAgent>().EndByPenguin();
            EndEpisode();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "TopRock" || collision.tag == "BotRock")
        {
            //Debug.Log("Penguin Hit Rock!");
            AddReward(-5f);
            //cheeseTransform.GetComponent<CheeseAgent>().EndByPenguin();
        }

        if(collision.tag == "Goal")
        {
            //Debug.Log("Penguin Hit Goal!");
            AddReward(1f);
        }
    }

    public void EndByCheese()
    {
        EndEpisode();
    }

}
