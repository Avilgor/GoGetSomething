using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
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
        velocity = 4;
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
                    rb.velocity = new Vector2(0, 0);
                    if (rb.velocity.y < 4)
                        rb.velocity += new Vector2(0, velocity);
                    break;
                case playerState.walkDown:
                    rb.velocity = new Vector2(0, 0);
                    if (rb.velocity.y > -4)
                        rb.velocity += new Vector2(0, -velocity);
                    break;
                case playerState.walkLeft:
                    rb.velocity = new Vector2(0, 0);
                    if (rb.velocity.x > -4)
                        rb.velocity += new Vector2(-velocity, 0);
                    break;
                case playerState.walkRight:
                    rb.velocity = new Vector2(0, 0);
                    if (rb.velocity.x < 4)
                        rb.velocity += new Vector2(velocity, 0);
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
            if (Input.GetKeyDown(KeyCode.W)) { newState = playerState.walkUp; }
            if (Input.GetKeyDown(KeyCode.S)) { newState = playerState.walkDown; }
            if (Input.GetKeyDown(KeyCode.A)) { newState = playerState.walkLeft; }
            if (Input.GetKeyDown(KeyCode.D)) { newState = playerState.walkRight; }
        }
    }

}
