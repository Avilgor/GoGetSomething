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

    playerState currentState;

    void Start()
    {
        currentState = playerState.idle;
    }

    
    void Update()
    {
        inputRead();
        switch (currentState)
        {
            case playerState.idle:
                break;
            case playerState.death:
                break;
            case playerState.walkUp:
                break;
            case playerState.walkDown:
                break;
            case playerState.walkLeft:
                break;
            case playerState.walkRight:
                break;
            case playerState.punch:
                break;
        }
    }

    private void inputRead()
    {
        if (!Input.anyKey) { currentState = playerState.idle; }
        else
        {
            if (Input.GetKeyDown(KeyCode.W)) { currentState = playerState.walkUp; }
            if (Input.GetKeyDown(KeyCode.S)) { currentState = playerState.walkDown; }
            if (Input.GetKeyDown(KeyCode.A)) { currentState = playerState.walkLeft; }
            if (Input.GetKeyDown(KeyCode.D)) { currentState = playerState.walkRight; }
        }
    }

}
