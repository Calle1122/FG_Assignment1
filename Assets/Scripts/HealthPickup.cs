using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    [SerializeField] private float health;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerManager>().Heal(health);
            
            Destroy(this.gameObject);
        }
    }
}
