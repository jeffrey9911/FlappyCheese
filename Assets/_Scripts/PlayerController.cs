using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] float jumpForce = 100;

    [SerializeField] AudioSource jumpSource;

    private int isJump = 0;

    private bool beHit = true;

    private float hitTimer = 0;

    private float flashTimer = 0;
    private bool isOrigColor = true;

    [SerializeField] private float xFixForce = 1.0f;

    enum PlayMode
    {
        Penguin,
        Cheese
    }

    [SerializeField] private PlayMode mode = new PlayMode();

    [SerializeField] private Sprite penguinUp;
    [SerializeField] private Sprite penguinDown;


    private void Start()
    {
        rb = this.transform.GetComponent<Rigidbody2D>();

        if(!ScoreManager.instance.isTraining)
        {
            switch (GameObject.FindGameObjectWithTag("Respawn").GetComponent<SceneDataManager>().gameMode)
            {
                case 0:
                    if (mode == PlayMode.Penguin)
                    {
                        this.GetComponent<PenguinAgent>().enabled = false;
                    }
                    break;

                case 1:
                    if (mode == PlayMode.Penguin)
                    {
                        this.GetComponent<PenguinAgent>().enabled = false;
                    }

                    if (mode == PlayMode.Cheese)
                    {
                        this.GetComponent<CheeseAgent>().enabled = false;
                    }
                    break;

                case 2:
                    if (mode == PlayMode.Cheese)
                    {
                        this.GetComponent<CheeseAgent>().enabled = false;
                    }
                    break;
                default:
                    break;
            }
        }
    }

    private void Update()
    {

        if(mode == PlayMode.Penguin)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift)) isJump = 1;

            this.GetComponent<SpriteRenderer>().sprite = rb.velocity.y > 0 ? penguinUp : penguinDown;
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

        this.transform.rotation = Quaternion.identity;

        FixHVelocity();
    }

    private void FixedUpdate()
    {
        
    }

    private void JumpOnClick()
    {
        jumpSource.Play();
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    public void PlayerOnHit()
    {
        if(beHit)
        {
            if(mode == PlayMode.Penguin)
            {
                rb.velocity += new Vector2(-0.5f, 0.0f);
            }
            else
            {
                rb.velocity += new Vector2(-0.8f, 0.0f);
            }
            
            beHit = false;
        }
    }

    private void FixHVelocity()
    {
        float vloX = rb.velocity.x;
        float vloY = rb.velocity.y;
        if(vloX != 0)
        {
            rb.velocity = new Vector2(vloX - (vloX * Time.deltaTime * xFixForce), vloY);
        }

        if(!beHit)
        {
            hitTimer += Time.deltaTime;
            flashTimer += Time.deltaTime;

            if (flashTimer >= 0.2f)
            {
                this.GetComponent<SpriteRenderer>().color = isOrigColor ? new Color(1f, 0.8f, 0.8f, 1f) : new Color(1f, 1f, 1f, 0.5f);
                isOrigColor = !isOrigColor;
                flashTimer -= 0.2f;
            }

            if (hitTimer >= 3)
            {
                this.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255);
                beHit = true;
                hitTimer = 0;
            }
        }
    }

    public void ActJump(int jump)
    {
        isJump = jump;
    }
}
