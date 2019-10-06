/**
 * Enemy.cs
 * Created by Akeru on 06/10/2019
 */

using UnityEngine;
using UnityEngine.AI;

enum lookPoint
{
    upLeft=0,
    upRight,
    downRight,
    downLeft
}

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
    [SerializeField] private Animator _anim;

    private bool _stop;
    private GameObject _target;
    private lookPoint directionPointer;

    #endregion

    #region MonoBehaviour Functions
    private void Start()
    {
        InitSetup();
        directionPointer = lookPoint.downRight;
    }

    private void Update()
    {
        FindUpdate();
        directionPointer = CalculateAngleDirection();
        switch (directionPointer)
        {
            case lookPoint.upLeft:
                _anim.SetFloat("Blend",1f);
                break;
            case lookPoint.upRight:
                _anim.SetFloat("Blend",0.66f);
                break;
            case lookPoint.downRight:
                _anim.SetFloat("Blend",0.33f);
                break;
            case lookPoint.downLeft:
                _anim.SetFloat("Blend",0f);
                break;
        }
    }
    private void OnEnable()
    {
        EventManager.KillAllEnemies += Die;
    }

    private void OnDisable()
    {
        EventManager.KillAllEnemies -= Die;
    }
    private void Die()
    {

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

    private lookPoint CalculateAngleDirection()
    {
        Vector3 v2 = transform.position - _target.transform.position;
        float angle = Mathf.Atan2(v2.y, v2.x) * Mathf.Rad2Deg;
        if (angle >= -180 && angle <-90) return lookPoint.upRight;
        else if (angle < 0 && angle > -91) return lookPoint.upLeft;
        else if (angle > 0 && angle < 90) return lookPoint.downLeft;
        else if (angle > 90 && angle <= 180) return lookPoint.downRight;
        else return lookPoint.downRight;
    }

    #endregion
}
