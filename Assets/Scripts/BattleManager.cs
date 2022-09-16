using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    [SerializeField] GameObject gameManagerObj;
    public GameObject[] allPlayers;
    
    private GameManager _gameMan;

    void Awake()
    {
        _gameMan = gameManagerObj.GetComponent<GameManager>();

        allPlayers = new GameObject[_gameMan.numberOfPlayer];
    }
    
    void Start()
    {
        for (int i = 0; i < _gameMan.numberOfPlayer; i++)
        {
            GameObject newPlayer = Instantiate(_gameMan.playerPrefabs[i], new Vector3(i * 3, 2, 0), Quaternion.identity);
            allPlayers[i] = newPlayer;
        }
    }
    
}
