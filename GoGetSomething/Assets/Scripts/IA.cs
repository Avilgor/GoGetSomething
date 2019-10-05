using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IA : MonoBehaviour
{
    private bool onRange;
    public Transform target;
    public float velocity;

    void Start()
    {
        onRange = false;
    }


    void Update()
    {
        if(onRange)
        {
            Vector3 diff = target.position - transform.position;
            diff.Normalize();
            float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);

            transform.position = Vector3.MoveTowards(transform.position, target.position, velocity);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag.Equals("Player"))
        {
            onRange = true;
        }        
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag.Equals("Player"))
        {
            onRange = false;
        }
    }
}
