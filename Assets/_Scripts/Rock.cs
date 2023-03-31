using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    float moveSpeed = 0;

    float posY = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if(collision.tag == "Penguin" || collision.tag == "Cheese") collision.transform.GetComponent<PlayerController>().PlayerOnHit();
    }

    private void Update()
    {
        if(moveSpeed != 0) this.transform.position = new Vector2(this.transform.position.x - (Time.deltaTime * moveSpeed), posY);
    }

    public void SetSpeed(float speed, float pos)
    {
        moveSpeed = speed;
        posY = pos;
    }
}
