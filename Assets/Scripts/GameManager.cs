using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public int numberOfPlayer;
    public int numberOfUnits;

    public GameObject[] playerPrefabs;

    //SetPlayerPrefabs() will be called from main menu after all players select their character.
    void SetPlayerPrefabs(GameObject[] allPlayerPrefabs)
    {
        playerPrefabs = allPlayerPrefabs;
    }
    
}
