using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivePlayerController : MonoBehaviour
{

    public List<GameObject> playerUnits;
    public int activePlayerIndex = 0;

    [SerializeField] private GameObject gameManager;

    void Start()
    {
        
    }
    
    void Update()
    {
        
    }

    public GameObject GetActiveUnit()
    {
        return playerUnits[activePlayerIndex];
    }
}
