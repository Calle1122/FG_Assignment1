using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public int numberOfPlayer;
    public int numberOfUnits;

    [SerializeField] private GameObject specificPlayerManager;
    [SerializeField] private GameObject playerPrefab;

    void Start()
    {
        for (int i = 0; i < numberOfPlayer; i++)
        {
            GameObject newPlayerManager = Instantiate(specificPlayerManager, Vector3.zero, Quaternion.identity);
            
            for (int j = 0; j < numberOfUnits; j++)
            {
                //Add a list with all player units.
            }
        }
    }
    
    void Update()
    {
        
    }
}
