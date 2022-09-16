using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    private GameObject cameraConObj;

    public float acceleration = 5f;
    public float maxSpeed = 10f;
    public float jumpForce = 10f;

    private CameraController _cameraCon;
    private Rigidbody _playerRb;
    
    private float _inputX;
    private float _inputZ;

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
        GetInput();
        Move();
    }

    void GetInput()
    {
        _inputZ = (Input.GetAxis("Vertical"));
        _inputX = (Input.GetAxis("Horizontal"));
        _jump = Input.GetKeyDown(KeyCode.Space);
    }
    
    void Move()
    {
        Vector3 _inputXZ = new Vector3(_inputX, 0, _inputZ);
        Vector3 _combinedDirection = this.transform.TransformDirection(_inputXZ);
        
    }
}
