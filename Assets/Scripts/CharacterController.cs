using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    private GameObject cameraConObj;

    public float speed = 2.5f;
    public float jumpForce = 10f;

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
        cameraConObj = GameObject.Find("CameraController");
        _cameraCon = cameraConObj.GetComponent<CameraController>();
        _playerRb = GetComponent<Rigidbody>();
    }
    
    void Update()
    {
        
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
        
        _jump = Input.GetKeyDown(KeyCode.Space);
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
            _playerRb.AddForce(moveDir * speed, ForceMode.VelocityChange);
            _playerRb.velocity = _playerRb.velocity.normalized * speed;
        }

    }
}
