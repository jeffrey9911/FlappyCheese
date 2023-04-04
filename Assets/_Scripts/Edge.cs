using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Edge : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "TopRock" || collision.tag == "BotRock" || collision.tag == "Goal") Destroy(collision.gameObject);
    }
}
