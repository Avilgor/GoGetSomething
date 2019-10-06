/**
 * Enemy.cs
 * Created by Akeru on 06/10/2019
 */

using UnityEngine;
using UnityEngine.AI;

public enum EnemyType
{
    Null = -1,
    Nightmare1 = 100, Nightmare2, Nightmare3,
    Other = 200
    //etc.
}

public class Enemy : MonoBehaviour
{
    #region Fields
    [SerializeField] private NavMeshAgent _agent;

    private bool _stop;
    private GameObject _target;

    #endregion

    #region MonoBehaviour Functions
    private void Start()
    {
        InitSetup();
    }

    private void Update()
    {
        FindUpdate();
    }

    #endregion

    #region Other Functions

    private void InitSetup()
    {
        _target = PlayerController.I.gameObject;

        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
    }

    private void FindUpdate()
    {
        if ((transform.position - _target.transform.position).magnitude < 1 && !_stop)
        {
            Debug.Log("Enemy Attack");
            //TODO Hit
        }
        _agent.SetDestination(_target.transform.position);
        Navigate.DebugDrawPath(_agent.path.corners);
    }

    #endregion
}
