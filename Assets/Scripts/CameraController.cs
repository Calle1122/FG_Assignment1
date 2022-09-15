using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    [SerializeField] private Camera thirdPersonCam, firstPersonCam;
    [SerializeField] private float cameraSensitivity;

    private float _rotationX, _rotationY;

    private Transform _playerTransform;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (thirdPersonCam.enabled)
        {
            MoveThirdPersonCamera();
        }
        
        else if (firstPersonCam.enabled)
        {
            MoveFirstPersonCamera();
        }
    }

    void MoveThirdPersonCamera()
    {
        
    }
    
    void MoveFirstPersonCamera()
    {
        float xMouse = Input.GetAxisRaw("Mouse X") * cameraSensitivity;
        float yMouse = Input.GetAxisRaw("Mouse Y") * cameraSensitivity;

        _rotationX -= yMouse;
        _rotationY += xMouse;

        //Make sure player can't look up or down more than 90 degrees.
        _rotationX = Mathf.Clamp(_rotationX, -90f, 90f);
        
        //Rotate camera and player
        transform.rotation = Quaternion.Euler(_rotationX, _rotationY, 0);
        _playerTransform.rotation = Quaternion.Euler(0, _rotationY, 0);
    }

    void UpdateCameraTarget(GameObject activePlayer)
    {
        
    }
}
