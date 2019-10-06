using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : Singleton<PlayerController>
{
    #region Fields

    enum PlayerState
    {
        Idle = 0,
        Death,
        WalkRight,
        WalkLeft,
        WalkUp,
        WalkDown,
        Punch,
    }

    [Title("Setup")]
    [SerializeField] private float _velocity = 0.08f;

    [Title("References")]
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private BoxCollider2D _collider;
    [SerializeField] private Zone _currentZone;

    private PlayerState _currentState, _newState;
    private bool _automaticMove;

    public float Velocity => _velocity * Time.deltaTime;

    #endregion

    #region MonoBehaviour Functions

    void Start()
    {
//        _currentZone.EnterInitZone();
        _currentState = PlayerState.Idle;

        StartGame();
    }


    private void Update()
    {
        DebugControls();
        CheckStates();
    }

    private void DebugControls()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.R))
        {
            Death();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else if (Input.GetKeyDown(KeyCode.F)) User.Firedust++;
        else if (Input.GetKeyDown(KeyCode.G)) User.Firedust--;
#endif
    }
    private void FixedUpdate()
    {
        MovementUpdate();
    }

    private void LateUpdate()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
    }

    private void OnEnable()
    {
        EventManager.ZoneEntered += ZoneEntered;
        EventManager.ZoneReady += ZoneReady;
        EventManager.PopupOpened += PopupOpened;
        EventManager.PopupsClosed += PopupsClosed;
    }

    private void OnDisabled()
    {
        EventManager.ZoneEntered -= ZoneEntered;
        EventManager.ZoneReady -= ZoneReady;
        EventManager.PopupOpened -= PopupOpened;
        EventManager.PopupsClosed -= PopupsClosed;
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
        Debug.Log("Tag: "+col.gameObject.tag);

        if (col.CompareTag("SwitchZone"))
        {
            col.GetComponent<TriggerSwitchZone>().SwitchZoneTriggerEntered(_currentZone, this);
        }
        else if (col.CompareTag("Bonfire"))
        {
            Debug.Log("Bonfire");
            col.GetComponent<Bonfire>().Interact();
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Bonfire"))
        {
            Debug.Log("Bonfire Exit");
            col.GetComponent<Bonfire>().HidePopup();
        }
    }

    #endregion

    #region Other Functions

    private void PopupOpened()
    {
//        AutomaticMovement(true);
    }

    private void PopupsClosed()
    {
//        AutomaticMovement(false);
    }

    private void CheckStates()
    {
        if (_currentState != _newState)
        {
            switch (_newState)
            {
                case PlayerState.Idle:
                    _rb.velocity = new Vector2(0, 0);
                    break;
                case PlayerState.Death:
                    gameObject.SetActive(false);
                    break;
                case PlayerState.WalkUp:
                    /*_rb._velocity += new Vector2(0, 0);
                    if (_rb._velocity.y < 4)
                        _rb._velocity = new Vector2(0, _velocity);*/
                    break;
                case PlayerState.WalkDown:
                    /*_rb._velocity += new Vector2(0, 0);
                    if (_rb._velocity.y > -4)
                        _rb._velocity = new Vector2(0, -_velocity);*/
                    break;
                case PlayerState.WalkLeft:
                    /*_rb._velocity += new Vector2(0, 0);
                    if (_rb._velocity.x > -4)
                        _rb._velocity = new Vector2(-_velocity, 0);*/
                    break;
                case PlayerState.WalkRight:
                    /*_rb._velocity = new Vector2(0, 0);
                    if (_rb._velocity.x < 4)
                        _rb._velocity += new Vector2(_velocity, 0);*/
                    break;
                case PlayerState.Punch:
                    break;
            }
            _currentState = _newState;
        }
    }

    public void ChangeZone(Zone zone)
    {
        _currentZone = zone;
    }

    private void ZoneReady()
    {
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
            _automaticMove = true;
            _collider.enabled = false;
        }
        else
        {
            _automaticMove = false;
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
        if (_automaticMove) return;

        if (!Input.anyKey)
        {
            _newState = PlayerState.Idle;
        }
        else
        {
            /* switch (Input.inputString)
             {
                 case "w":
                     _newState = PlayerState.WalkUp;
                     break;
                 case "s":
                     _newState = PlayerState.WalkDown;
                     break;
                 case "a":
                     _newState = PlayerState.WalkLeft;
                     break;
                 case "d":
                     _newState = PlayerState.WalkRight;
                     break;
             }*/
            if (Input.GetKey(KeyCode.W))
            {
                _newState = PlayerState.WalkUp;
                transform.position += new Vector3(0, Velocity, 0);
            }
            if (Input.GetKey(KeyCode.S))
            {
                _newState = PlayerState.WalkDown;
                transform.position += new Vector3(0, -Velocity, 0);
            }
            if (Input.GetKey(KeyCode.A))
            {
                _newState = PlayerState.WalkLeft;
                transform.position += new Vector3(-Velocity, 0, 0);
            }
            if (Input.GetKey(KeyCode.D))
            {
                _newState = PlayerState.WalkRight;
                transform.position += new Vector3(Velocity, 0, 0);
            }
        }
    }

    private void Death()
    {
        Debug.Log("Player Died");
        User.ClearZonesQueued();
    }

    private void StartGame()
    {
        if (User.LastSavedPlayerPosition() != Vector2.zero) transform.position = User.LastSavedPlayerPosition();
    }

    #endregion
}