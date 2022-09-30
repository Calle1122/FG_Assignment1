
using System.Collections;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;

public class CameraController : MonoBehaviour
{

    [SerializeField] private float cameraSensitivity = 5f;
    [SerializeField] private GameObject camHolderObj;
    [SerializeField] private GameObject skyCamRotator;
    [SerializeField] private GameObject thirdPersonTarget;
    [SerializeField] private float thirdPersonCamDistance = 5f;

    [SerializeField] private GameObject battleUIObj;
    private BattleUIController _battleUICon;
    
    public Camera activeCamera;

    private CameraHolderScript _camHolderMan;

    public Camera thirdPersonCam, firstPersonCam, skyCamera;

    private float _rotationX, _rotationY;
    private Transform _playerTransform;
    [SerializeField] public Transform thirdPersonRotator;

    void Awake()
    {
        _battleUICon = battleUIObj.GetComponent<BattleUIController>();
        _camHolderMan = camHolderObj.GetComponent<CameraHolderScript>();
    }
    
    void Start()
    {
        DOTween.Init();
        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        firstPersonCam.enabled = false;
        skyCamera.enabled = false;
        activeCamera = thirdPersonCam;
    }

    void Update()
    {

        ChangeCamera();
        
        skyCamRotator.transform.Rotate(Vector3.up, 15f * Time.deltaTime);
        
        if (thirdPersonCam.enabled)
        {
            if (Cursor.lockState == CursorLockMode.Locked)
            {
                MoveThirdPersonCamera();
            }
        }
        
        if (firstPersonCam.enabled)
        {
            if (Cursor.lockState == CursorLockMode.Locked)
            {
                MoveFirstPersonCamera();
            }
        }
    }

    private void FixedUpdate()
    {
        if (thirdPersonCam.enabled)
        {
            RaycastHit cameraHit;
            Physics.SphereCast(_playerTransform.position, .75f, (thirdPersonTarget.transform.position - _playerTransform.position).normalized, out cameraHit, thirdPersonCamDistance);

            if (cameraHit.collider != null)
            {
                thirdPersonCam.transform.position = cameraHit.point;
            }
            else
            {
                thirdPersonCam.transform.position = _playerTransform.position + (thirdPersonTarget.transform.position - _playerTransform.position).normalized * (thirdPersonCamDistance * 0.9f);
            }
        }
    }

    void ChangeCamera()
    {
        if (Input.GetMouseButtonDown(1))
        {
            firstPersonCam.enabled = true;
            thirdPersonCam.enabled = false;

            _battleUICon.crossHairObj.SetActive(true);
            
            activeCamera = firstPersonCam;
        }

        if (Input.GetMouseButtonUp(1))
        {
            thirdPersonCam.enabled = true;
            firstPersonCam.enabled = false;

            _battleUICon.crossHairObj.SetActive(false);
            
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

        //Make sure player can't look up or down into self
        _rotationX = Mathf.Clamp(_rotationX, -65f, 75f);
        
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
