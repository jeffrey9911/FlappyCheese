using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Edge : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Rock") Destroy(collision.gameObject);

        if (collision.tag == "Penguin") Debug.Log("Penguin Lose!");
    }
}
