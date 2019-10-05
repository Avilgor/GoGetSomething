using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

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

    [HideInInspector] public string CurrentZoneId;

    [SerializeField] private Rigidbody2D _rb;
    private PlayerState _currentState, _newState;
    private float velocity;
    private bool _dontMove;

    private Zone _currentZoneId;

    #endregion

    #region MonoBehaviour Functions

    void Start()
    {
        _currentState = PlayerState.idle;
        velocity = 0.08f;
    }


    void Update()
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
                    /*_rb.velocity += new Vector2(0, 0);
                    if (_rb.velocity.y < 4)
                        _rb.velocity = new Vector2(0, velocity);*/
                    break;
                case PlayerState.walkDown:
                    /*_rb.velocity += new Vector2(0, 0);
                    if (_rb.velocity.y > -4)
                        _rb.velocity = new Vector2(0, -velocity);*/
                    break;
                case PlayerState.walkLeft:
                    /*_rb.velocity += new Vector2(0, 0);
                    if (_rb.velocity.x > -4)
                        _rb.velocity = new Vector2(-velocity, 0);*/
                    break;
                case PlayerState.walkRight:
                    /*_rb.velocity = new Vector2(0, 0);
                    if (_rb.velocity.x < 4)
                        _rb.velocity += new Vector2(velocity, 0);*/
                    break;
                case PlayerState.punch:
                    break;
            }
            _currentState = _newState;
        }
    }

    private void OnEnabled()
    {
        EventManager.ZoneEntered += ZoneEntered;
    }

    private void OnDisabled()
    {
        EventManager.ZoneEntered -= ZoneEntered;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            gameObject.SetActive(false);
        }
    }

    #endregion

    #region Other Functions

    public void MoveTo(Vector2 position, float time)
    {
        _dontMove = true;
        _rb.DOMove(position, 1).SetEase(Ease.OutSine).OnComplete(()=> _dontMove = false);
    }

    private void ZoneEntered(Zone id)
    {
        _currentZoneId = id;
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
                transform.position += new Vector3(0, velocity, 0);
            }
            if (Input.GetKey(KeyCode.S))
            {
                _newState = PlayerState.walkDown;
                transform.position += new Vector3(0, -velocity, 0);
            }
            if (Input.GetKey(KeyCode.A))
            {
                _newState = PlayerState.walkLeft;
                transform.position += new Vector3(-velocity, 0, 0);
            }
            if (Input.GetKey(KeyCode.D))
            {
                _newState = PlayerState.walkRight;
                transform.position += new Vector3(velocity, 0, 0);
            }
        }
    }

    #endregion
}