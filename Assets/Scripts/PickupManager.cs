
using System;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class PickupManager : MonoBehaviour
{
    [SerializeField] private GameObject[] pickUps;

    public bool hasPickup = false;

    private void Awake()
    {
        Actions.OnTurnEnd += SpawnPickup;
    }

    public void DestroyChild()
    {
        Destroy(transform.GetChild(0).gameObject);
    }
    
    private void SpawnPickup()
    {
        if (!hasPickup)
        {
            int spawnDecider = Random.Range(0, 20);

            if (spawnDecider == 0)
            {
                int pickPicker = Random.Range(0, pickUps.Length);
                GameObject pickup = pickUps[pickPicker];

                GameObject newPickup = Instantiate(pickup, transform.position, Quaternion.identity);
                newPickup.transform.parent = this.transform;
                
                hasPickup = true;
            }
        }
    }

    private void OnDestroy()
    {
        Actions.OnTurnEnd -= SpawnPickup;
    }
}
