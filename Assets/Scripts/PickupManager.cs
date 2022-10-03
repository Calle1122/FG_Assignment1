
using UnityEngine;
public class PickupManager : MonoBehaviour
{
    [SerializeField] private Transform[] pickupPositions;
    [SerializeField] private GameObject[] pickUps;

    public void SpawnPickup()
    {
        int spawnDecider = Random.Range(0, 3);

        if (spawnDecider == 0)
        {
            int posPicker = Random.Range(0, pickupPositions.Length);
            Vector3 spawnPos = pickupPositions[posPicker].position;

            int pickPicker = Random.Range(0, pickUps.Length);
            GameObject pickup = pickUps[pickPicker];

            Instantiate(pickup, spawnPos, Quaternion.identity);
        }
    }

}
