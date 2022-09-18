using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public bool isActive = false;
    public Transform cameraPosTransform;

    private HealthbarManager _healthBar;
    
    public int maxHealth = 100;
    private int _health = 100;

    private void Awake()
    {
        cameraPosTransform = transform.GetChild(0);
        _healthBar = this.gameObject.GetComponent<HealthbarManager>();
    }

    public void TakeDamage(int dmg)
    {
        _health -= dmg;
        
        _healthBar.UpdateHealth(_health);
        
        if (_health >= 0)
        {
            Die();
        }
    }

    public void Heal(int hp)
    {
        _health += hp;
        
        _healthBar.UpdateHealth(_health);
        
        if (_health > maxHealth)
        {
            _health = maxHealth;
        }
    }
    
    private void Die()
    {
        Destroy(this.gameObject);
        Debug.Log(this.transform.name + " was killed.");
    }
}
