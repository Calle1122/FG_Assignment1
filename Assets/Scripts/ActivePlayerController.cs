using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivePlayerController : MonoBehaviour
{

    [SerializeField] private GameObject battleManObj;
    [SerializeField] private GameObject cameraContObj;
    
    public PlayerManager[] allPlayerManagers;
    public GameObject activePlayer;

    private CharacterController[] _allCharacterControllers;
    
    private BattleManager _battleMan;
    private CameraController _cameraCont;
    private int _activePlayerIndex = 0;

    void Awake()
    {
        _battleMan = battleManObj.GetComponent<BattleManager>();
        allPlayerManagers = new PlayerManager[_battleMan.allPlayers.Length];
        _cameraCont = cameraContObj.GetComponent<CameraController>();
        _allCharacterControllers = new CharacterController[_battleMan.allPlayers.Length];
    }

    void Start()
    {
        for (int i = 0; i < allPlayerManagers.Length; i++)
        {
            allPlayerManagers[i] = _battleMan.allPlayers[i].GetComponent<PlayerManager>();
            _allCharacterControllers[i] = _battleMan.allPlayers[i].GetComponent<CharacterController>();
        }

        activePlayer = allPlayerManagers[_activePlayerIndex].gameObject;
        _allCharacterControllers[_activePlayerIndex].canMove = true;
        
        _cameraCont.UpdateCameraTarget(activePlayer);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            NextPlayerActive();
        }
    }

    void NextPlayerActive()
    {
        _allCharacterControllers[_activePlayerIndex].canMove = false;
        
        _activePlayerIndex++;
        if (_activePlayerIndex == _battleMan.allPlayers.Length)
        {
            _activePlayerIndex = 0;
        }
        
        activePlayer = allPlayerManagers[_activePlayerIndex].gameObject;
        
        _cameraCont.UpdateCameraTarget(activePlayer);
        
        _allCharacterControllers[_activePlayerIndex].canMove = true;
    }

}
