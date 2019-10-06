using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : Singleton<PlayerController>
{
    #region Fields
    private enum forwardPointer
    {
        back=0,
        front,
        left,
        right
    }

    private enum weapon
    {
        nude = 0,
        bone
    }
    enum PlayerState
    {
        Idle = 0,
        Death,
        WalkRight,
        WalkLeft,
        WalkUp,
        WalkDown,
        BoneAttack,
    }

    [Title("Setup")]
    [SerializeField] private float _velocity = 0.08f;

    [Title("References")]
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private BoxCollider2D _collider;
    [SerializeField] private Zone _currentZone;
    [SerializeField] private Animator _anim;

    private PlayerState _currentState, _newState;
    private forwardPointer _aimDirection;
    private weapon _weaponEquiped;
    private bool _automaticMove;

    public float Velocity => _velocity * Time.deltaTime;

    #endregion

    #region MonoBehaviour Functions

    void Start()
    {
//        _currentZone.EnterInitZone();
        _aimDirection = forwardPointer.front;
        _currentState = PlayerState.Idle;
        _weaponEquiped = weapon.nude;
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
            Die();
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
                    switch (_weaponEquiped)
                    {
                        case weapon.nude:
                            _anim.SetBool("walk", false);
                            _anim.SetBool("withBone", false);
                            break;
                        case weapon.bone:
                            _anim.SetBool("walk", false);
                            _anim.SetBool("withBone", true);
                            break;
                    }
                    
                    break;
                case PlayerState.Death:
                    gameObject.SetActive(false);
                    break;
                case PlayerState.WalkUp:
                    _aimDirection = forwardPointer.back;
                    switch (_weaponEquiped)
                    {
                        case weapon.nude:
                            _anim.SetBool("walk", true);
                            _anim.SetBool("withBone", false);
                            break;
                        case weapon.bone:
                            _anim.SetBool("walk", true);
                            _anim.SetBool("withBone", true);
                            break;
                    }
                    
                    break;
                case PlayerState.WalkDown:
                    _aimDirection = forwardPointer.front;
                    switch (_weaponEquiped)
                    {
                        case weapon.nude:
                            _anim.SetBool("walk", true);
                            _anim.SetBool("withBone", false);
                            break;
                        case weapon.bone:
                            _anim.SetBool("walk", true);
                            _anim.SetBool("withBone", true);
                            break;
                    }               
                    break;
                case PlayerState.WalkLeft:
                    _aimDirection = forwardPointer.left;
                    switch (_weaponEquiped)
                    {
                        case weapon.nude:
                            _anim.SetBool("walk", true);
                            _anim.SetBool("withBone", false);
                            break;
                        case weapon.bone:
                            _anim.SetBool("walk", true);
                            _anim.SetBool("withBone", true);
                            break;
                    }
                    break;
                case PlayerState.WalkRight:
                    _aimDirection = forwardPointer.right;
                    switch (_weaponEquiped)
                    {
                        case weapon.nude:
                            _anim.SetBool("walk", true);
                            _anim.SetBool("withBone", false);
                            break;
                        case weapon.bone:
                            _anim.SetBool("walk", true);
                            _anim.SetBool("withBone", true);
                            break;
                    }
                    break;
                case PlayerState.BoneAttack:
                    _anim.SetTrigger("boneAttack");
                    break;
            }

            switch (_aimDirection)
            {
                case forwardPointer.back:
                    _anim.SetFloat("Blend", 0.66f);
                    break;
                case forwardPointer.front:
                    _anim.SetFloat("Blend", 0f);
                    break;
                case forwardPointer.right:
                    _anim.SetFloat("Blend", 0.33f);
                    break;
                case forwardPointer.left:
                    _anim.SetFloat("Blend", 1f);
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
            if (Input.GetKey(KeyCode.Space))
            {
                _newState = PlayerState.BoneAttack;                
            }
        }
    }

    public void Die()
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