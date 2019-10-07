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
    Nightmare1 = 100, Nightmare2, Nightmare3, Nightmare4,
    //etc.
}

public class Enemy : MonoBehaviour
{
    #region Fields
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private Animator _anim;
    [SerializeField] private GameObject _deathParticles,_deathDrop;
    [SerializeField] public AudioClip _deathSound,_attackSound;
    [SerializeField] private int _attackDmg;
    [SerializeField] private int _health;

    public int AttackDamage => _attackDmg;

    public bool _attacking;
    private bool _stop;
    private GameObject _target;
    private LookPoint directionPointer;

    [HideInInspector] public CombatZone ParentCombatZone;

    #endregion

    #region MonoBehaviour Functions
    private void Start()
    {
        InitSetup();
        directionPointer = LookPoint.downRight;
        _attacking = false;
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
            _anim.SetBool("attack", true);
            _attacking = true;
        }
        else { _anim.SetBool("attack", false); }
        if (!_attacking)
        {
            _agent.SetDestination(_target.transform.position);
            Navigate.DebugDrawPath(_agent.path.corners);
        }

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
        if(ParentCombatZone != null) ParentCombatZone.EnemyKilled(this);

        EventManager.OnEnemyDied();

        Instantiate(_deathParticles, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("PlayerWeap"))
        {
            Hit((int)PlayerController.I.Damage);
            GetComponent<AudioSource>().PlayOneShot(_deathSound);
            Instantiate(_deathDrop, transform.position, Quaternion.identity);
            Instantiate(_deathParticles, transform.position, Quaternion.identity);
        }
    }

    private void Hit(int dmg)
    {
        _health -= dmg;
        Debug.Log("<color=yellow>Hit for </color><color=white>"+ dmg + " (" + _health + ")</color><color=yellow> damage</color>");
        if(_health <= 0) Die();
    }

    #endregion
}
