using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public Image face;
    public TextMeshProUGUI nameTxt;
    
    public bool isActive = false;
    public Transform cameraPosTransform;

    private HealthbarManager _healthBar;
    
    public float maxHealth = 100;
    private float _health = 100;

    public VoicePack voicePack;

    [SerializeField] private GameObject deadRobot;

    private void Awake()
    {
        cameraPosTransform = transform.GetChild(0);
        _healthBar = this.gameObject.GetComponent<HealthbarManager>();

        if (GameSettings.GameSettingsInstance.unhealthyMode)
        {
            maxHealth = 100;
        }

        else
        {
            maxHealth = 250;
        }
    }

    private void Start()
    {
        _health = maxHealth;
        _healthBar.UpdateHealth((int)_health);
    }

    public void TakeDamage(float dmg)
    {
        _health -= dmg;
        
        _healthBar.UpdateHealth((int)_health);
        
        if (_health <= 0)
        {
            Die();
        }

        else
        {
            SoundManager.SoundManagerInstance.PlaySound(voicePack.TakeDamageSound());
        }
    }

    public void Heal(float hp)
    {
        _health += hp;

        if (_health > maxHealth)
        {
            _health = maxHealth;
        }
        
        SoundManager.SoundManagerInstance.PlaySound(voicePack.healthUpSound);
        
        _healthBar.UpdateHealth((int)_health);
    }
    
    private void Die()
    {
        GameObject deadRobotThing = Instantiate(deadRobot, transform.position, transform.rotation);
        this.gameObject.SetActive(false);
        Destroy(deadRobotThing, 2f);

        if (this.gameObject == GameObject.Find("ActivePlayerController").GetComponent<ActivePlayerController>()
                .activePlayer)
        {
            GameObject.Find("ActivePlayerController").GetComponent<ActivePlayerController>().NewTurn();
        }

        GameSettings.GameSettingsInstance.DeadFaceQueue.Enqueue(face.sprite);
        GameSettings.GameSettingsInstance.DeadNameQueue.Enqueue(nameTxt.text);
        
        GameSettings.GameSettingsInstance.deadPlayers++;

        if (GameSettings.GameSettingsInstance.deadPlayers >= GameSettings.GameSettingsInstance.numberOfPlayers - 1)
        {
            Destroy(gameObject);
            GameSettings.GameSettingsInstance.EnqueueLastPlayer();

            //Load GameOver Scene
            SceneManager.LoadScene(2);
        }
        
        SoundManager.SoundManagerInstance.PlaySound(voicePack.deathSound);
        
        Destroy(gameObject);
    }
}
