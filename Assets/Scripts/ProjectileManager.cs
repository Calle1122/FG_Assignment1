using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    [SerializeField] private GameObject hitParticle;

    private float _damage;
    
    void Awake()
    {
        _damage = GameObject.Find("ActivePlayerController").GetComponent<ActivePlayerController>().activePlayer.GetComponent<WeaponManager>().activeWeapon.damage;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        GameObject newHitParticle = Instantiate(hitParticle, transform.position, hitParticle.transform.rotation);
        Destroy(newHitParticle, 1f);

        if (other.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerManager>().TakeDamage(_damage);
        }
        
        Destroy(this.gameObject);
    }
}
