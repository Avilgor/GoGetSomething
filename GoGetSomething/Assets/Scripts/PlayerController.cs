using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : Singleton<PlayerController>
{
    #region Fields
    public enum forwardPointer
    {
        back=0,
        front,
        left,
        right
    }

    private enum weapon
    {
        nude = 0,
        bone,
        porra
    }
    enum PlayerState
    {
        Idle = 0,
        Death,
        WalkRight,
        WalkLeft,
        WalkUp,
        WalkDown,
        Attack,
        damaged,
    }

    [Title("Player variables")]
    [SerializeField] private int _health = 100;

    [Title("Setup")]
    [SerializeField] private float _velocity = 0.08f;

    [Title("References")]
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private BoxCollider2D _collider;
    [SerializeField] private Zone _currentZone;
    [SerializeField] private Animator _anim;
    [SerializeField] private GameObject _DieParticles;

    private PlayerState _currentState, _newState;
    public forwardPointer _aimDirection;
    private weapon _weaponEquiped;
    private Vector2 kick = new Vector2(0,0);
    private bool _automaticMove,_forceCheck;
    private int _essencePower;
    public bool _attacking,_gotDamaged;

    public float Velocity => _velocity * Time.deltaTime;

    #endregion

    #region MonoBehaviour Functions

    void Start()
    {
//        _currentZone.EnterInitZone();
        _aimDirection = forwardPointer.front;
        _currentState = PlayerState.Idle;
        _weaponEquiped = weapon.nude;
        _attacking = false;
        _forceCheck = false;
        _gotDamaged = false;
        _essencePower = 0;
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
        if (collision.gameObject.CompareTag("Bone"))
        {
            _weaponEquiped = weapon.bone;
            collision.gameObject.SetActive(false);
            _forceCheck = true;
        }

        if (collision.gameObject.CompareTag("Porra"))
        {
            _weaponEquiped = weapon.porra;
            collision.gameObject.SetActive(false);
            _forceCheck = true;
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
        else if (col.CompareTag("Damagable"))
        {
            col.GetComponent<IDamagable>().Damage(10);
        }

        if (col.gameObject.CompareTag("Enemy"))
        {
            _newState = PlayerState.damaged;
 //           kick = transform.position - col.gameObject.transform.position;
 //           kick.Normalize();
            
        }
        if (col.gameObject.CompareTag("DeathEssence"))
        {
            _essencePower++;
            Destroy(col.gameObject);
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
        if (_currentState != _newState || _forceCheck)
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
                            _anim.SetBool("withPorra", false);
                            _anim.SetBool("withBone", true);
                            break;
                        case weapon.porra:
                            _anim.SetBool("withBone", false);
                            _anim.SetBool("walk", false);
                            _anim.SetBool("withPorra", true);
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
                            _anim.SetBool("withPorra", false);
                            _anim.SetBool("withBone", true);
                            break;
                        case weapon.porra:
                            _anim.SetBool("withBone", false);
                            _anim.SetBool("walk", true);
                            _anim.SetBool("withPorra", true);
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
                            _anim.SetBool("withPorra", false);
                            _anim.SetBool("withBone", true);
                            break;
                        case weapon.porra:
                            _anim.SetBool("withBone", true);
                            _anim.SetBool("walk", true);
                            _anim.SetBool("withPorra", true);
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
                            _anim.SetBool("withPorra", false);
                            _anim.SetBool("withBone", true);
                            break;
                        case weapon.porra:
                            _anim.SetBool("withBone", false);
                            _anim.SetBool("walk", true);
                            _anim.SetBool("withPorra", true);
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
                            _anim.SetBool("withPorra", false);
                            _anim.SetBool("withBone", true);                          
                            break;
                        case weapon.porra:
                            _anim.SetBool("withBone", false);
                            _anim.SetBool("walk", true);
                            _anim.SetBool("withPorra", true);
                            break;
                    }
                    break;
                case PlayerState.Attack:
                    switch (_weaponEquiped)
                    {
                        case weapon.nude:
                            _anim.SetBool("withPorra", false);
                            _anim.SetBool("withBone", false);
                            break;
                        case weapon.bone:
                            _anim.SetBool("withPorra", false);
                            _anim.SetBool("withBone", true);
                            break;
                        case weapon.porra:
                            _anim.SetBool("withPorra", true);
                            _anim.SetBool("withBone", false);
                            break;
                    }
                    _anim.SetTrigger("attack");
                    break;
                case PlayerState.damaged:
                    /*                    _gotDamaged = true;
                                        StartCoroutine(wait(0.8f));
                                        switch (_aimDirection)
                                        {
                                            case forwardPointer.back:
                                                kick = new Vector2(0, 40);
                                                break;
                                            case forwardPointer.front:
                                                kick = new Vector2(0, -40);
                                                break;
                                            case forwardPointer.right:
                                                kick = new Vector2(40, 0);

                                                break;
                                            case forwardPointer.left:
                                                kick = new Vector2(-40, 0);
                                                break;
                                        }
                                        _rb.AddForce(-kick*500);
                                        _DieParticles
                    */
                    _DieParticles.GetComponent<ParticleSystem>().Play();
                    Instantiate(_DieParticles, transform.position, Quaternion.identity);
                    gameObject.SetActive(false);
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
            _forceCheck = false;
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
            if (!_attacking && !_gotDamaged)
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
            }           
            if (Input.GetKey(KeyCode.Space))
            {
                _newState = PlayerState.Attack;
                _attacking = true;
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

    IEnumerator wait(float time)
    {
        yield return new WaitForSeconds(time);
        _gotDamaged = false;
    }
    #endregion
}