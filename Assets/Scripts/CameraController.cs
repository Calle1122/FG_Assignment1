using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    [SerializeField] private float cameraSensitivity = 5f;
    [SerializeField] private GameObject camHolderObj;

    public Camera activeCamera;
    
    private CameraHolderScript _camHolderMan;

    public Camera thirdPersonCam, firstPersonCam;

    private float _rotationX, _rotationY;
    private Transform _playerTransform;
    [SerializeField] public Transform thirdPersonRotator;

    void Awake()
    {
        _camHolderMan = camHolderObj.GetComponent<CameraHolderScript>();
    }
    
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        firstPersonCam.enabled = false;
        activeCamera = thirdPersonCam;
    }

    void Update()
    {
        ChangeCamera();
        
        if (thirdPersonCam.enabled)
        {
            MoveThirdPersonCamera();
        }
        
        if (firstPersonCam.enabled)
        {
            MoveFirstPersonCamera();
        }
    }

    void ChangeCamera()
    {
        if (Input.GetMouseButtonDown(1))
        {
            firstPersonCam.enabled = true;
            thirdPersonCam.enabled = false;

            activeCamera = firstPersonCam;
        }

        if (Input.GetMouseButtonUp(1))
        {
            thirdPersonCam.enabled = true;
            firstPersonCam.enabled = false;

            activeCamera = thirdPersonCam;
        }
    }
    
    void MoveThirdPersonCamera()
    {
        float xMouse = Input.GetAxisRaw("Mouse X") * cameraSensitivity;
        float yMouse = Input.GetAxisRaw("Mouse Y") * cameraSensitivity;

        _rotationX -= yMouse;
        _rotationY += xMouse;

        //Make sure player can't look up or down more than 90 degrees.
        _rotationX = Mathf.Clamp(_rotationX, -90f, 90f);
        
        //Rotate camera and player
        thirdPersonRotator.transform.rotation = Quaternion.Euler(_rotationX, _rotationY, 0);
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
        firstPersonCam.transform.rotation = Quaternion.Euler(_rotationX, _rotationY, 0);
        _playerTransform.rotation = Quaternion.Euler(0, _rotationY, 0);
    }

    public void UpdateCameraTarget(GameObject activePlayer)
    {
        _playerTransform = activePlayer.transform;
        _camHolderMan.SetCameraFocus();
    }
}
