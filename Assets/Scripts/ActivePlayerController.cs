using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivePlayerController : MonoBehaviour
{

    [SerializeField] private GameObject battleManObj;
    [SerializeField] private GameObject cameraContObj;

    [SerializeField] private float secondsBeforeKinematic;
    private float _kinTimer;
    
    public PlayerManager[] allPlayerManagers;
    public GameObject activePlayer;

    private CharacterController[] _allCharacterControllers;
    private WeaponManager[] _allWeaponManagers;
    
    private BattleManager _battleMan;
    private CameraController _cameraCont;
    private int _activePlayerIndex = 0;

    void Awake()
    {
        _battleMan = battleManObj.GetComponent<BattleManager>();
        allPlayerManagers = new PlayerManager[_battleMan.allPlayers.Length];
        _cameraCont = cameraContObj.GetComponent<CameraController>();
        _allCharacterControllers = new CharacterController[_battleMan.allPlayers.Length];
        _allWeaponManagers = new WeaponManager[_battleMan.allPlayers.Length];
    }

    void Start()
    {
        _kinTimer = 0;
        
        for (int i = 0; i < allPlayerManagers.Length; i++)
        {
            allPlayerManagers[i] = _battleMan.allPlayers[i].GetComponent<PlayerManager>();
            _allCharacterControllers[i] = _battleMan.allPlayers[i].GetComponent<CharacterController>();
            _allWeaponManagers[i] = _battleMan.allPlayers[i].GetComponent<WeaponManager>();
        }

        activePlayer = allPlayerManagers[_activePlayerIndex].gameObject;
        _allCharacterControllers[_activePlayerIndex].canMove = true;
        _allCharacterControllers[_activePlayerIndex].playerRb.isKinematic = false;
        _allWeaponManagers[_activePlayerIndex].canShoot = true;
        
        _cameraCont.UpdateCameraTarget(activePlayer);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            NextPlayerActive();
        }

        foreach (CharacterController charCon in _allCharacterControllers)
        {
            if (charCon != null)
            {
                
                //For all non-active players:
                
                if (charCon.gameObject != activePlayer)
                {
                    charCon.gameObject.transform.LookAt(activePlayer.transform.position, Vector3.up);
                    float yRot = charCon.gameObject.transform.rotation.eulerAngles.y;
                    charCon.gameObject.transform.rotation = Quaternion.Euler(0, yRot, 0);

                    if (charCon.jump)
                    {
                        charCon.playerRb.isKinematic = false;
                        _kinTimer = secondsBeforeKinematic;
                    }

                    else
                    {
                        _kinTimer -= Time.deltaTime;
                        if (_kinTimer < 0)
                        {
                            charCon.playerRb.isKinematic = true;
                        }
                    }
                }
            }
        }
    }

    public void ResetGroundTimer()
    {
        _kinTimer = secondsBeforeKinematic;
    }
    
    void NextPlayerActive()
    {
        bool hasSelected = false;
        
        _allCharacterControllers[_activePlayerIndex].canMove = false;
        //_allCharacterControllers[_activePlayerIndex].playerRb.isKinematic = true;
        _allWeaponManagers[_activePlayerIndex].canShoot = false;

        while (hasSelected == false)
        {
            _activePlayerIndex++;

            if (_activePlayerIndex == _battleMan.allPlayers.Length)
            {
                _activePlayerIndex = 0;
            }

            if (allPlayerManagers[_activePlayerIndex] != null)
            {
                hasSelected = true;
            }
        }

        activePlayer = allPlayerManagers[_activePlayerIndex].gameObject;
        
        _cameraCont.UpdateCameraTarget(activePlayer);
        
        _allCharacterControllers[_activePlayerIndex].canMove = true;
        _allCharacterControllers[_activePlayerIndex].playerRb.isKinematic = false;
        _allWeaponManagers[_activePlayerIndex].canShoot = true;
    }

    public void EquipWeapon(Weapon toEquip)
    {
        _allWeaponManagers[_activePlayerIndex].EquipWeapon(toEquip);
    }

    public void NoWeapon()
    {
        _allWeaponManagers[_activePlayerIndex].NoWeaponActive();
    }

}
