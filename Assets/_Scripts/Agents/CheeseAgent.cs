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

    public override void OnEpisodeBegin()
    {
        this.transform.localPosition = new Vector3(3.69f, 0, 0);
        this.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        float worldHeight = 10f;
        float heightNormalized = (this.transform.position.y + (worldHeight / 2f)) / worldHeight;
        sensor.AddObservation(heightNormalized);


        sensor.AddObservation(this.transform.position);
        sensor.AddObservation(penguinTransform.position);
        sensor.AddObservation(ceilingTransform.position);
        sensor.AddObservation(bottomTransform.position);
        sensor.AddObservation(RockManager.instance.genInterval);
        sensor.AddObservation(RockManager.instance.moveSpeed);
        sensor.AddObservation(RockManager.instance.topRockHeight);
        sensor.AddObservation(RockManager.instance.botRockHeight);
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

        if (collision.transform.tag == "BotEdge" || collision.transform.tag == "TopEdge")
        {
            Debug.Log("Cheese Hit Edge. Cheese Lose.");
            penguinTransform.GetComponent<PenguinAgent>().EndByCheese();
            SetReward(-1f);
            EndEpisode();
        }
    }


    private void Update()
    {
        AddReward(Time.deltaTime * 0.001f);
    }


    public void EndByPenguin()
    {
        EndEpisode();
    }
}
