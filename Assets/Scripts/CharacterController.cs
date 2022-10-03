using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class CharacterController : MonoBehaviour
{
    private GameObject _cameraConObj;
    private BattleUIController _battleUICon;
    
    public float speed = 2.5f;
    public float jumpForce = 4f;
    public float maxChargeTime = 1f;

    private float _jumpMultiTimer = 0f;
    private bool _isCharging = false;
    private bool _countUp = true;

    public LayerMask groundLayer;
    public float groundRayDis = 1.25f;

    public bool canMove = false;

    private CameraController _cameraCon;
    public Rigidbody playerRb;

    [SerializeField] private float rotationSpeed;

    private float _inputX;
    private float _inputZ;

    private float _actualJumpForce;

    private Vector3 _forward;
    private Vector3 _right;

    public bool jump;

    private float _wobbleAmount = 3.5f;
    private float _wobbleTimer = .4f;
    private int _wobbleSwitch = 0;
    
    
    void Awake()
    {
        _cameraConObj = GameObject.Find("CameraController");
        _cameraCon = _cameraConObj.GetComponent<CameraController>();
        playerRb = GetComponent<Rigidbody>();
        _battleUICon = GameObject.Find("BattleUIController").GetComponent<BattleUIController>();
    }

    private void Start()
    {
        if (GameSettings.GameSettingsInstance.moonMode)
        {
            _actualJumpForce = jumpForce * 2.5f;
        }

        else
        {
            _actualJumpForce = jumpForce;
        }
    }

    void Update()
    {

        if (_isCharging)
        {
            ChargeTime();
        }
        
        RaycastHit hit;

        if (Physics.SphereCast(transform.position, .2f, Vector3.down, out hit, groundRayDis, groundLayer))
        {
            jump = false;
        }
        else
        {
            jump = true;
        }

        if (canMove)
        {
            if (Input.GetKeyDown(KeyCode.Space) && jump == false)
            {
                _jumpMultiTimer = 0;
                
                _battleUICon.jumpSliderHolder.SetActive(true);
                _isCharging = true;
            }
            
            if (Input.GetKeyUp(KeyCode.Space) && jump == false)
            {
                _battleUICon.jumpSliderHolder.SetActive(false);
                _isCharging = false;
                playerRb.AddForce(Vector3.up * _actualJumpForce * (_jumpMultiTimer + 1), ForceMode.Impulse);
            }
        }

    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            GetInput();
            Move();
        }

        if (!canMove)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, 0);
        }
    }

    void GetInput()
    {
        _inputZ = (Input.GetAxis("Vertical"));
        _inputX = (Input.GetAxis("Horizontal"));

        if(_cameraCon.activeCamera == _cameraCon.firstPersonCam)
        {
            _forward = _cameraCon.activeCamera.transform.forward.normalized;
            _right = _cameraCon.activeCamera.transform.right.normalized;
        }
        
        else if (_cameraCon.activeCamera == _cameraCon.thirdPersonCam)
        {
            _forward = _cameraCon.thirdPersonRotator.forward.normalized;
            _right = _cameraCon.thirdPersonRotator.right.normalized;
        }

        _forward.y = 0f;
        _right.y = 0f;
        _forward = _forward.normalized;
        _right = _right.normalized;
    }

    void ChargeTime()
    {
        switch(_countUp)
        {
            case true:
                _jumpMultiTimer += Time.deltaTime;
                if (_jumpMultiTimer > maxChargeTime)
                {
                    _jumpMultiTimer = maxChargeTime;
                    _countUp = false;
                }
                break;
                
            case false:
                _jumpMultiTimer -= Time.deltaTime;
                if (_jumpMultiTimer < 0)
                {
                    _jumpMultiTimer = 0;
                    _countUp = true;
                }
                break;
        }

        _battleUICon.jumpSlider.value = _jumpMultiTimer / maxChargeTime;
    }
    
    void Move()
    {
        Vector3 inputZX = new Vector3(_inputX, 0, _inputZ).normalized;
        
        Vector3 xDir = (_right * _inputX).normalized;
        Vector3 zDir = (_forward * _inputZ).normalized;
        Vector3 combinedDir = (zDir + xDir).normalized;
        
        Vector3 moveDir = new Vector3(combinedDir.x, 0, combinedDir.z).normalized;

        Vector3 rayDir = moveDir * 10;
        Debug.DrawRay(transform.position, rayDir, Color.green);
        
        if (inputZX.magnitude > .1f)
        {
            float yVel = playerRb.velocity.y;
            
            playerRb.AddForce(moveDir * speed, ForceMode.VelocityChange);
            playerRb.velocity = playerRb.velocity.normalized * speed;

            playerRb.velocity = new Vector3(playerRb.velocity.x, yVel, playerRb.velocity.z);
            
            //Wobble
            _wobbleTimer -= Time.deltaTime;
            
            if (_wobbleTimer <= 0)
            {
                if (_wobbleSwitch == 0)
                {
                    _wobbleSwitch = 1;
                }
                
                else if (_wobbleSwitch == 1)
                {
                    _wobbleSwitch = 0;
                }

                switch (_wobbleSwitch)
                {
                    case 0:
                        _wobbleTimer = .4f;
                        _wobbleAmount = 3.5f;
                        break;
                    
                    case 1:
                        _wobbleTimer = .4f;
                        _wobbleAmount = -3.5f;
                        break;
                }
            }

            //Rotate towards moveDir + wobble
            float targetAngle = Mathf.Atan2(_inputX, _inputZ) * Mathf.Rad2Deg + _cameraCon.activeCamera.transform.eulerAngles.y;
            Quaternion targetRotation = Quaternion.Euler(transform.rotation.x, targetAngle, _wobbleAmount);
            
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);

        }
    }

}
