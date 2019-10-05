using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IA : MonoBehaviour
{
    enum enemyStates
    {
        idle = 0,
        move,
        die,
        attack
    }

    public Transform target;
    public float velocity;
    enemyStates currentState, newState;

    void Start()
    {
        currentState = enemyStates.idle;
    }


    void Update()
    {
        switch (currentState)
        {
            case enemyStates.idle:
                break;
            case enemyStates.move:
                Vector3 diff = target.position - transform.position;
                diff.Normalize();
                float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);

                transform.position = Vector3.MoveTowards(transform.position, target.position, velocity);
                break;
            case enemyStates.die:
                break;
            case enemyStates.attack:
                break;

        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag.Equals("Player"))
        {
            currentState = enemyStates.move;
        }        
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag.Equals("Player"))
        {
            currentState = enemyStates.idle;
        }
    }
}
