using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthbarManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nameTxt, healthTxt;
    [SerializeField] private Image barBackground;

    private CameraController _camController;

    void Start()
    {
        _camController = GameObject.Find("CameraController").GetComponent<CameraController>();
    }

    void Update()
    {
        barBackground.transform.LookAt(_camController.activeCamera.transform.position);
    }
    
    public void SetName(string newName)
    {
        nameTxt.text = newName;
    }
    
    public void UpdateHealth(int newHealth)
    {
        healthTxt.text = "Health: " + newHealth;
    }
}
