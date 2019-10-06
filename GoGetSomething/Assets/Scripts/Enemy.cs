/**
 * Enemy.cs
 * Created by Akeru on 06/10/2019
 */

using UnityEngine;
using UnityEngine.AI;

enum LookPoint
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
    private LookPoint directionPointer;

    private CombatZone _parentCombatZone;

    #endregion

    #region MonoBehaviour Functions
    private void Start()
    {
        InitSetup();
        directionPointer = LookPoint.downRight;
    }

    private void Update()
    {
        FindUpdate();
        directionPointer = CalculateAngleDirection();
        switch (directionPointer)
        {
            case LookPoint.upLeft:
                _anim.SetFloat("Blend",1f);
                break;
            case LookPoint.upRight:
                _anim.SetFloat("Blend",0.66f);
                break;
            case LookPoint.downRight:
                _anim.SetFloat("Blend",0.33f);
                break;
            case LookPoint.downLeft:
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

    private LookPoint CalculateAngleDirection()
    {
        var v2 = transform.position - _target.transform.position;
        var angle = Mathf.Atan2(v2.y, v2.x) * Mathf.Rad2Deg;
        if (angle >= -180 && angle <-90) return LookPoint.upRight;
        if (angle < 0 && angle > -91) return LookPoint.upLeft;
        if (angle > 0 && angle < 90) return LookPoint.downLeft;
        if (angle > 90 && angle <= 180) return LookPoint.downRight;
        return LookPoint.downRight;
    }

    private void Die()
    {
        if(_parentCombatZone != null) _parentCombatZone.EnemyKilled(this);
    }

    #endregion
}
