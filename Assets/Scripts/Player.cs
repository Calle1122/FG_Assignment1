using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float health;
    public Weapon[] usableWeapons;

    [SerializeField] private GameObject playerPrefab;
}
