using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public bool isActive = false;
    public Transform cameraPosTransform;

    private HealthbarManager _healthBar;
    
    public float maxHealth = 100;
    private float _health = 100;

    [SerializeField] private GameObject deadRobot;

    private void Awake()
    {
        cameraPosTransform = transform.GetChild(0);
        _healthBar = this.gameObject.GetComponent<HealthbarManager>();
    }

    public void TakeDamage(float dmg)
    {
        _health -= dmg;
        
        _healthBar.UpdateHealth((int)_health);
        
        if (_health <= 0)
        {
            Die();
        }
    }

    public void Heal(float hp)
    {
        _health += hp;

        if (_health > maxHealth)
        {
            _health = maxHealth;
        }
        
        _healthBar.UpdateHealth((int)_health);
    }
    
    private void Die()
    {
        GameObject deadRobotThing = Instantiate(deadRobot, transform.position, transform.rotation);
        this.gameObject.SetActive(false);
        Destroy(deadRobotThing, 2f);
        
        Destroy(this.gameObject);
    }
}
