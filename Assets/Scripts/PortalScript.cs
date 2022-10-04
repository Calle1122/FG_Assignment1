
using System;
using UnityEngine;
using TMPro;

public class PortalScript : MonoBehaviour
{
    public int cooldown = 0;
    
    [SerializeField] private GameObject exitPortal;
    private PortalScript _exitPortalScript;
    
    public TextMeshProUGUI screenTxt;

    private void Awake()
    {
        Actions.OnTurnEnd += ReduceCooldown;
    }

    private void Start()
    {
        screenTxt.color = Color.green;
        _exitPortalScript = exitPortal.GetComponent<PortalScript>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && cooldown == 0)
        {
            cooldown = 2;
            _exitPortalScript.cooldown = 2;
            SetPortalCooldownTexts();
            other.transform.position = exitPortal.transform.position;
        }
    }

    //Called on all portals at the end of each turn
    public void ReduceCooldown()
    {
        cooldown--;

        if (cooldown <= 0)
        {
            cooldown = 0;
            screenTxt.text = "PORTAL READY";
            screenTxt.color = Color.green;
        }

        else
        {
            SetPortalCooldownTexts();
        }
    }

    private void SetPortalCooldownTexts()
    {
        screenTxt.text = "COOLDOWN FOR: " + cooldown + " TURN";
        screenTxt.color = Color.red;
        _exitPortalScript.screenTxt.text = "COOLDOWN FOR: " + cooldown + " TURN";
        _exitPortalScript.screenTxt.color = Color.red;
    }
}
