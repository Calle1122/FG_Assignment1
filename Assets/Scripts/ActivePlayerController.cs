
using System.Collections;
using UnityEngine;

public class ActivePlayerController : MonoBehaviour
{

    [SerializeField] private GameObject battleManObj;
    [SerializeField] private GameObject cameraContObj;
    [SerializeField] private GameObject battleUIObj;

    [SerializeField] private float secondsBeforeKinematic;
    private float _kinTimer;
    
    public PlayerManager[] allPlayerManagers;
    public GameObject activePlayer;
    
    public float turnTimer;
    public float currentTurnTimer;

    private CharacterController[] _allCharacterControllers;
    private WeaponManager[] _allWeaponManagers;
    
    private BattleManager _battleMan;
    private CameraController _cameraCont;
    private BattleUIController _uiCon;
    public int activePlayerIndex = 0;

    private bool _hasChosen;

    void Awake()
    {
        //used earliear: _battleMan.allPlayers.Length
        _battleMan = battleManObj.GetComponent<BattleManager>();
        allPlayerManagers = new PlayerManager[GameSettings.GameSettingsInstance.numberOfPlayers];
        _cameraCont = cameraContObj.GetComponent<CameraController>();
        _allCharacterControllers = new CharacterController[GameSettings.GameSettingsInstance.numberOfPlayers];
        _allWeaponManagers = new WeaponManager[GameSettings.GameSettingsInstance.numberOfPlayers];
        _uiCon = battleUIObj.GetComponent<BattleUIController>();
    }

    void Start()
    {
        currentTurnTimer = turnTimer;
        
        _kinTimer = 0;
        
        for (int i = 0; i < allPlayerManagers.Length; i++)
        {
            allPlayerManagers[i] = _battleMan.allPlayers[i].GetComponent<PlayerManager>();
            _allCharacterControllers[i] = _battleMan.allPlayers[i].GetComponent<CharacterController>();
            _allWeaponManagers[i] = _battleMan.allPlayers[i].GetComponent<WeaponManager>();
        }

        activePlayerIndex = GameSettings.GameSettingsInstance.numberOfPlayers - 1;
        activePlayer = allPlayerManagers[GameSettings.GameSettingsInstance.numberOfPlayers - 1].gameObject;
        _allCharacterControllers[GameSettings.GameSettingsInstance.numberOfPlayers - 1].canMove = true;
        _allCharacterControllers[GameSettings.GameSettingsInstance.numberOfPlayers - 1].playerRb.isKinematic = false;
        _allWeaponManagers[GameSettings.GameSettingsInstance.numberOfPlayers - 1].canShoot = true;
        
        _cameraCont.UpdateCameraTarget(activePlayer);

        currentTurnTimer = 0.1f;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            NewTurn();
        }
        
        NonActiveHandling();

        if (GameSettings.GameSettingsInstance.isPaused == false)
        {
            currentTurnTimer -= Time.deltaTime;
            if (currentTurnTimer <= 0)
            {
                NewTurn();
            }
        }

    }

    public void CanMove(bool movable)
    {
        if (movable)
        {
            _allCharacterControllers[activePlayerIndex].canMove = true;
        }

        else
        {
            _allCharacterControllers[activePlayerIndex].canMove = false;
        }
    }
    
    public void NewTurn()
    {
        BetweenTurn();
    }

    void BetweenTurn()
    {
        while (_hasChosen == false)
        {
            GameSettings.GameSettingsInstance.playerToDisplay += 1;
            if (GameSettings.GameSettingsInstance.playerToDisplay == GameSettings.GameSettingsInstance.numberOfPlayers)
            {
                GameSettings.GameSettingsInstance.playerToDisplay = 0;
            }

            if (allPlayerManagers[GameSettings.GameSettingsInstance.playerToDisplay] != null)
            {
                _hasChosen = true;
                Actions.OnTurnEnd();
            }
        }
        
        _uiCon.UpdateBetweenTurnsInfo();
        
        _uiCon.timerHolder.SetActive(false);
        _uiCon.activeFacesHolder.SetActive(false);
        _uiCon.chargeHolder.SetActive(false);
        _uiCon.sideButtonHolder.SetActive(false);

        _cameraCont.firstPersonCam.enabled = false;
        _cameraCont.thirdPersonCam.enabled = false;
        _cameraCont.skyCamera.enabled = true;
        
        _uiCon.betweenTurnHolder.SetActive(true);
        
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
            
        _allCharacterControllers[activePlayerIndex].canMove = false;
    }

    public void StartTurn()
    {
        _uiCon.betweenTurnHolder.SetActive(false);
        _uiCon.timerHolder.SetActive(true);
        _uiCon.activeFacesHolder.SetActive(true);
        _uiCon.chargeHolder.SetActive(true);
        _uiCon.sideButtonHolder.SetActive(true);

        _cameraCont.skyCamera.enabled = false;
        _cameraCont.thirdPersonCam.enabled = true;
        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        _allWeaponManagers[activePlayerIndex].charges = 3;
        
        currentTurnTimer = turnTimer;
        NextPlayerActive();
        _uiCon.SetChargeImgs(_allWeaponManagers[activePlayerIndex].charges);
        _uiCon.SetActiveFaces(activePlayerIndex);
        _hasChosen = false;
        
        SoundManager.SoundManagerInstance.PlaySound(allPlayerManagers[activePlayerIndex].voicePack.RoundStartSound());
    }

    private void NonActiveHandling()
    {
        if (activePlayer != null)
        {
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
        
        
    }
    
    public void ResetGroundTimer()
    {
        _kinTimer = secondsBeforeKinematic;
    }

    public void NextPlayerActive()
    {
        bool hasSelected = false;
        
        _allCharacterControllers[activePlayerIndex].canMove = false;
        //_allCharacterControllers[_activePlayerIndex].playerRb.isKinematic = true;
        _allWeaponManagers[activePlayerIndex].canShoot = false;
        _allWeaponManagers[activePlayerIndex].NoWeaponActive();

        while (hasSelected == false)
        {
            activePlayerIndex++;

            if (activePlayerIndex == _battleMan.allPlayers.Length)
            {
                activePlayerIndex = 0;
            }

            if (allPlayerManagers[activePlayerIndex] != null)
            {
                hasSelected = true;
            }
        }

        activePlayer = allPlayerManagers[activePlayerIndex].gameObject;
        
        _cameraCont.UpdateCameraTarget(activePlayer);
        
        _allCharacterControllers[activePlayerIndex].canMove = true;
        _allCharacterControllers[activePlayerIndex].playerRb.isKinematic = false;
        _allWeaponManagers[activePlayerIndex].canShoot = true;
    }

    public void EquipWeapon(Weapon toEquip)
    {
        _allWeaponManagers[activePlayerIndex].EquipWeapon(toEquip);
    }

    public void NoWeapon()
    {
        _allWeaponManagers[activePlayerIndex].NoWeaponActive();
    }

}
