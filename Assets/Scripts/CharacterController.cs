using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    private GameObject _cameraConObj;
    private BattleUIController _battleUICon;
    
    public float speed = 2.5f;
    public float jumpForce = 10f;
    public float maxChargeTime = 1f;

    private float _jumpMultiTimer = 0f;
    private bool _isCharging = false;
    private bool _countUp = true;

    public LayerMask groundLayer;
    public float groundRayDis = 1.25f;

    public bool canMove = false;

    private CameraController _cameraCon;
    private Rigidbody _playerRb;
    
    private float _inputX;
    private float _inputZ;

    private Vector3 _forward;
    private Vector3 _right;

    private bool _jump;
    
    void Start()
    {
        _cameraConObj = GameObject.Find("CameraController");
        _cameraCon = _cameraConObj.GetComponent<CameraController>();
        _playerRb = GetComponent<Rigidbody>();
        _battleUICon = GameObject.Find("BattleUIController").GetComponent<BattleUIController>();
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
            _jump = false;
        }
        else
        {
            _jump = true;
        }

        if (canMove)
        {
            if (Input.GetKeyDown(KeyCode.Space) && _jump == false)
            {
                _jumpMultiTimer = 0;
                
                _battleUICon.jumpSliderHolder.SetActive(true);
                _isCharging = true;
            }
            
            if (Input.GetKeyUp(KeyCode.Space) && _jump == false)
            {
                _battleUICon.jumpSliderHolder.SetActive(false);
                _isCharging = false;
                _playerRb.AddForce(Vector3.up * jumpForce * (_jumpMultiTimer + 1), ForceMode.Impulse);
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
            float yVel = _playerRb.velocity.y;
            
            _playerRb.AddForce(moveDir * speed, ForceMode.VelocityChange);
            _playerRb.velocity = _playerRb.velocity.normalized * speed;

            _playerRb.velocity = new Vector3(_playerRb.velocity.x, yVel, _playerRb.velocity.z);
        }

    }

}
