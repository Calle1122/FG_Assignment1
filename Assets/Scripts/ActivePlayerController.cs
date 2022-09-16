using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivePlayerController : MonoBehaviour
{

    [SerializeField] private GameObject battleManObj;
    [SerializeField] private GameObject cameraContObj;
    
    public PlayerManager[] allPlayerManagers;
    public GameObject activePlayer;
    
    private BattleManager _battleMan;
    private CameraController _cameraCont;
    private int _activePlayerIndex = 0;

    void Awake()
    {
        _battleMan = battleManObj.GetComponent<BattleManager>();

        allPlayerManagers = new PlayerManager[_battleMan.allPlayers.Length];
        
        _cameraCont = cameraContObj.GetComponent<CameraController>();
    }

    void Start()
    {
        for (int i = 0; i < allPlayerManagers.Length; i++)
        {
            allPlayerManagers[i] = _battleMan.allPlayers[i].GetComponent<PlayerManager>();
        }

        activePlayer = allPlayerManagers[_activePlayerIndex].gameObject;
        
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
        _activePlayerIndex++;
        if (_activePlayerIndex == _battleMan.allPlayers.Length)
        {
            _activePlayerIndex = 0;
        }
        
        activePlayer = allPlayerManagers[_activePlayerIndex].gameObject;
        
        _cameraCont.UpdateCameraTarget(activePlayer);
    }

}
