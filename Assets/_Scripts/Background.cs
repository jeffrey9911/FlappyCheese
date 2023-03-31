using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{

    [SerializeField] private float moveSpeed = 1;
    private float bgOffset = 0;

    private void Update()
    {
        this.GetComponent<MeshRenderer>().material.mainTextureOffset = new Vector2(-bgOffset, 0);

        bgOffset += Time.deltaTime * moveSpeed;

        if (bgOffset >= 1) bgOffset -= 1;
    }

    public void SetSpeed(float speed)
    {
        moveSpeed = speed;
    }
}
