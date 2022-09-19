using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    [SerializeField] GameObject gameManagerObj;
    public GameObject[] allPlayers;

    /*public List<GameObject[]> TeamUnitArrays = new List<GameObject[]>();

    private GameObject[] _team1;
    private GameObject[] _team2;
    private GameObject[] _team3;
    private GameObject[] _team4;*/

    //private GameObject[][] _allTeams = new GameObject[][4];

    private GameManager _gameMan;

    void Awake()
    {
        _gameMan = gameManagerObj.GetComponent<GameManager>();

        allPlayers = new GameObject[_gameMan.numberOfPlayer];

        /*_team1 = new GameObject[_gameMan.numberOfUnits];
        _team2 = new GameObject[_gameMan.numberOfUnits];
        _team3 = new GameObject[_gameMan.numberOfUnits];
        _team4 = new GameObject[_gameMan.numberOfUnits];

        _allTeams[0] = _team1;
        _allTeams[1] = _team2;
        _allTeams[2] = _team3;
        _allTeams[3] = _team4;*/
    }
    
    void Start()
    {
        /*for (int i = 0; i < _gameMan.numberOfUnits; i++)
        {
            TeamUnitArrays.Add(_allTeams[i]);
        }*/
        
        for (int i = 0; i < _gameMan.numberOfPlayer; i++)
        {
            GameObject newPlayer = Instantiate(_gameMan.playerPrefabs[i], new Vector3(i * 3, 2, 0), Quaternion.identity);
            allPlayers[i] = newPlayer;
            
            /*for (int j = 0; j < _gameMan.numberOfUnits; j++)
            {
                GameObject newUnit = Instantiate(_gameMan.playerPrefabs[i], new Vector3(newPlayer.transform.position.x, newPlayer.transform.position.y, newPlayer.transform.position.z + 3 + (j * 3)), Quaternion.identity);
                TeamUnitArrays[i][j] = newUnit;
            }*/
        }
        
    }
    
}
