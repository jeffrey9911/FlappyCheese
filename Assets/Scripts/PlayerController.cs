using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] float jumpForce = 100;

    [SerializeField] private int isJump = 0;

    enum PlayMode
    {
        Penguin,
        Cheese
    }

    [SerializeField] private PlayMode mode = new PlayMode(); 

    private void Start()
    {
        rb = this.transform.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {

        if(mode == PlayMode.Penguin)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift)) isJump = 1;
        }

        if (mode == PlayMode.Cheese)
        {
            if (Input.GetKeyDown(KeyCode.RightShift)) isJump = 1;
        }

        if (isJump == 1)
        {
            JumpOnClick();
            isJump = 0;
        }
    }

    private void FixedUpdate()
    {
        
    }

    private void JumpOnClick()
    {
        rb.velocity = new Vector2(0, jumpForce);
    }
}
