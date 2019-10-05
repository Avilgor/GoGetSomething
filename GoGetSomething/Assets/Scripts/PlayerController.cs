using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    #region Fields

    enum PlayerState
    {
        idle = 0,
        death,
        walkRight,
        walkLeft,
        walkUp,
        walkDown,
        punch,
    }

    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private BoxCollider2D _collider;
    [SerializeField] private float _velocity = 0.08f;
    private PlayerState _currentState, _newState;
    private bool _dontMove;

    [SerializeField] private Zone _currentZone;

    public float Velocity => _velocity * Time.fixedDeltaTime;

    #endregion

    #region MonoBehaviour Functions

    void Start()
    {
        _currentZone.EnterInitZone();
        _currentState = PlayerState.idle;
    }


    private void Update()
    {
        DebugControls();
    }

    private static void DebugControls()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
#endif
    }

    void FixedUpdate()
    {
        MovementUpdate();
        if (_currentState != _newState)
        {
            switch (_newState)
            {
                case PlayerState.idle:
                    _rb.velocity = new Vector2(0, 0);
                    break;
                case PlayerState.death:
                    gameObject.SetActive(false);
                    break;
                case PlayerState.walkUp:
                    /*_rb._velocity += new Vector2(0, 0);
                    if (_rb._velocity.y < 4)
                        _rb._velocity = new Vector2(0, _velocity);*/
                    break;
                case PlayerState.walkDown:
                    /*_rb._velocity += new Vector2(0, 0);
                    if (_rb._velocity.y > -4)
                        _rb._velocity = new Vector2(0, -_velocity);*/
                    break;
                case PlayerState.walkLeft:
                    /*_rb._velocity += new Vector2(0, 0);
                    if (_rb._velocity.x > -4)
                        _rb._velocity = new Vector2(-_velocity, 0);*/
                    break;
                case PlayerState.walkRight:
                    /*_rb._velocity = new Vector2(0, 0);
                    if (_rb._velocity.x < 4)
                        _rb._velocity += new Vector2(_velocity, 0);*/
                    break;
                case PlayerState.punch:
                    break;
            }
            _currentState = _newState;
        }
    }

    private void OnEnable()
    {
        EventManager.ZoneEntered += ZoneEntered;
        EventManager.ZoneReady += ZoneReady;
    }

    private void OnDisabled()
    {
        EventManager.ZoneEntered -= ZoneEntered;
        EventManager.ZoneReady -= ZoneReady;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            gameObject.SetActive(false);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log(col.gameObject.name + " : " + gameObject.name + " : " + Time.time);
        if (col.CompareTag("SwitchZone"))
        {
            col.GetComponent<TriggerSwitchZone>().SwitchZoneTriggerEntered(_currentZone, this);
        }
    }

    #endregion

    #region Other Functions

    public void ChangeZone(Zone zone)
    {
        _currentZone = zone;
    }

    private void ZoneReady()
    {
        Debug.Log("?");
        AutomaticMovement(false);
    }

    public void MoveTo(Vector2 position, float time)
    {
        AutomaticMovement(true);
        _rb.DOMove(position, 1).SetEase(Ease.OutSine);
    }

    private void AutomaticMovement(bool on)
    {
        if (on)
        {
            _dontMove = true;
            _collider.enabled = false;
        }
        else
        {
            _dontMove = false;
            _collider.enabled = true;
        }
    }

    private void ZoneEntered(Zone id)
    {
        Debug.Log("Zone entered");
        _currentZone = id;
    }


    private void MovementUpdate()
    {
        if (_dontMove) return;

        if (!Input.anyKey)
        {
            _newState = PlayerState.idle;
        }
        else
        {
            /* switch (Input.inputString)
             {
                 case "w":
                     _newState = PlayerState.walkUp;
                     break;
                 case "s":
                     _newState = PlayerState.walkDown;
                     break;
                 case "a":
                     _newState = PlayerState.walkLeft;
                     break;
                 case "d":
                     _newState = PlayerState.walkRight;
                     break;
             }*/
            if (Input.GetKey(KeyCode.W))
            {
                _newState = PlayerState.walkUp;
                transform.position += new Vector3(0, Velocity, 0);
            }
            if (Input.GetKey(KeyCode.S))
            {
                _newState = PlayerState.walkDown;
                transform.position += new Vector3(0, -Velocity, 0);
            }
            if (Input.GetKey(KeyCode.A))
            {
                _newState = PlayerState.walkLeft;
                transform.position += new Vector3(-Velocity, 0, 0);
            }
            if (Input.GetKey(KeyCode.D))
            {
                _newState = PlayerState.walkRight;
                transform.position += new Vector3(Velocity, 0, 0);
            }
        }
    }

    #endregion
}