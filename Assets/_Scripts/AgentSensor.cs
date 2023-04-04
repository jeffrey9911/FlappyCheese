using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentSensor : MonoBehaviour
{
    public Vector3 topRockP = new Vector3();
    public Vector3 botRockP = new Vector3();
    public Vector3 goalAreaP = new Vector3();
    
    public Vector3 topRockC = new Vector3();
    public Vector3 botRockC = new Vector3();
    public Vector3 goalAreaC = new Vector3();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "TopRock")
        {
            topRockP = topRockC;
            topRockC = collision.transform.position;
        }

        if(collision.tag == "BotRock")
        {
            botRockP = botRockC;
            botRockC = collision.transform.position;
        }

        if( collision.tag == "Goal")
        {
            goalAreaP = goalAreaC;
            goalAreaC = collision.transform.position;
        }
    }
}
