
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    [SerializeField] GameObject gameManagerObj;
    public GameObject[] allPlayers;

    private GameManager _gameMan;

    [SerializeField] private GameObject[] spawnPoints;

    void Awake()
    {
        _gameMan = gameManagerObj.GetComponent<GameManager>();
        
        allPlayers = new GameObject[GameSettings.GameSettingsInstance.numberOfPlayers];
    }
    
    void Start()
    {
        for (int i = 0; i < _gameMan.numberOfPlayer; i++)
        {
            GameObject newPlayer = Instantiate(_gameMan.playerPrefabs[i], spawnPoints[i].transform.position, Quaternion.identity);
            allPlayers[i] = newPlayer;
            newPlayer.GetComponent<PlayerManager>().face.sprite = GameSettings.GameSettingsInstance.playerFaces[i];
            newPlayer.GetComponent<PlayerManager>().nameTxt.text = GameSettings.GameSettingsInstance.playerNames[i];
            newPlayer.GetComponent<PlayerManager>().voicePack = GameSettings.GameSettingsInstance.playerVoices[i];
        }
        
    }
    
}
