using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    float moveSpeed = 0;

    float posY = 0;

    private void Update()
    {
        if (moveSpeed != 0) this.transform.localPosition = new Vector2(this.transform.localPosition.x - (Time.deltaTime * moveSpeed), posY);
    }

    public void SetSpeed(float speed, float pos)
    {
        moveSpeed = speed;
        posY = pos;
    }
}
