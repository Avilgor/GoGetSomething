using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    enum playerState
    {
        idle = 0,
        death,
        walkRight,
        walkLeft,
        walkUp,
        walkDown,
        punch,

    }

    Rigidbody2D rb;
    playerState currentState,newState;
    private float velocity;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        currentState = playerState.idle;
        velocity = 0.08f;
    }

    
    void Update()
    {
        inputRead();
        if (currentState != newState)
        {
            switch (newState)
            {
                case playerState.idle:
                    rb.velocity = new Vector2(0, 0);
                    break;
                case playerState.death:
                    gameObject.SetActive(false);
                    break;
                case playerState.walkUp:
                    /*rb.velocity += new Vector2(0, 0);
                    if (rb.velocity.y < 4)
                        rb.velocity = new Vector2(0, velocity);*/
                    break;
                case playerState.walkDown:
                    /*rb.velocity += new Vector2(0, 0);
                    if (rb.velocity.y > -4)
                        rb.velocity = new Vector2(0, -velocity);*/
                    break;
                case playerState.walkLeft:
                    /*rb.velocity += new Vector2(0, 0);
                    if (rb.velocity.x > -4)
                        rb.velocity = new Vector2(-velocity, 0);*/
                    break;
                case playerState.walkRight:
                    /*rb.velocity = new Vector2(0, 0);
                    if (rb.velocity.x < 4)
                        rb.velocity += new Vector2(velocity, 0);*/
                    break;
                case playerState.punch:
                    break;
            }
            currentState = newState;
        }
    }

    private void inputRead()
    {
        
        if (!Input.anyKey) { newState = playerState.idle; }
        else
        {
           /* switch (Input.inputString)
            {
                case "w":
                    newState = playerState.walkUp;
                    break;
                case "s":
                    newState = playerState.walkDown;
                    break;
                case "a":
                    newState = playerState.walkLeft;
                    break;
                case "d":
                    newState = playerState.walkRight;
                    break;
            }*/
            if (Input.GetKey(KeyCode.W))
            {
                newState = playerState.walkUp;
                transform.position += new Vector3(0, velocity, 0);
            }
            if (Input.GetKey(KeyCode.S))
            {
                newState = playerState.walkDown;
                transform.position += new Vector3(0, -velocity, 0);
            }
            if (Input.GetKey(KeyCode.A))
            {
                newState = playerState.walkLeft;
                transform.position += new Vector3(-velocity, 0, 0);
            }
            if (Input.GetKey(KeyCode.D))
            {
                newState = playerState.walkRight;
                transform.position += new Vector3(velocity,0,0);
            }
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Enemy"))
        {
            gameObject.SetActive(false);
        }       
    }
}
