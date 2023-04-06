using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class CheeseAgent : Agent
{
    [SerializeField] private Transform penguinTransform;
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
        this.transform.localPosition = new Vector3(3.69f, 0, 0);
        this.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        SetReward(0);
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(this.GetComponent<Rigidbody2D>().velocity);

        if (isJump == 1)
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
        if (collision.transform.tag == "BotEdge" || collision.transform.tag == "TopEdge")
        {
            //Debug.Log("Cheese Hit Edge. Cheese Lose.");
            penguinTransform.GetComponent<PenguinAgent>().EndByCheese();
            AddReward(-10f);
            ScoreManager.instance.CheeseScore(-1);
            EndEpisode();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "LeftEdge")
        {
            AddReward(-10f);
            ScoreManager.instance.CheeseScore(-2);
            penguinTransform.GetComponent<PenguinAgent>().EndByCheese();
            EndEpisode();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "TopRock" || collision.tag == "BotRock")
        {
            //Debug.Log("Cheese Hit Rock!");
            AddReward(-5f);
        }

        if (collision.tag == "Goal")
        {
            //Debug.Log("Cheese Hit Goal!");
            AddReward(+1f);
        }
    }

    

    public void EndByPenguin()
    {
        EndEpisode();
    }
}
