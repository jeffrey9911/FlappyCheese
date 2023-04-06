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

    int isJump = 0;

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
        sensor.AddObservation(this.GetComponent<Rigidbody2D>().velocity);

        if(isJump == 1)
        {
            this.GetComponent<Rigidbody2D>().velocity = new Vector2(this.GetComponent<Rigidbody2D>().velocity.x, 10);
            isJump = 0;
        }

        AddReward(0.5f);
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        isJump = actions.DiscreteActions[0];
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "Cheese")
        {
            //Debug.Log("Penguin Win!");
            AddReward(10f);
            ScoreManager.instance.PenguinScore(10);
            cheeseTransform.GetComponent<CheeseAgent>().AddReward(-10f);
            cheeseTransform.GetComponent<CheeseAgent>().EndByPenguin();
            EndEpisode();
        }

        if (collision.transform.tag == "BotEdge" || collision.transform.tag == "TopEdge")
        {
            //Debug.Log("Penguin Hit Edge. Penguin Lose.");
            cheeseTransform.GetComponent<CheeseAgent>().EndByPenguin();
            AddReward(-10f);
            ScoreManager.instance.PenguinScore(-1);
            EndEpisode();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "LeftEdge")
        {
            //Debug.Log("Penguin Lose!");
            AddReward(-10f);
            ScoreManager.instance.PenguinScore(-2);
            cheeseTransform.GetComponent<CheeseAgent>().EndByPenguin();
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
