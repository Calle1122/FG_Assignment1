using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleUIController : MonoBehaviour
{
    public GameObject weaponsMenuHolder;

    [SerializeField] private Button bazookaBtn, blasterBtn;
    
    public GameObject jumpSliderHolder;
    public Slider jumpSlider;

    public GameObject shootSliderHolder;
    public Slider shootSlider;

    void Start()
    {
        weaponsMenuHolder.SetActive(false);
        jumpSliderHolder.SetActive(false);
        shootSliderHolder.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            weaponsMenuHolder.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        if (Input.GetKeyUp(KeyCode.E))
        {
            weaponsMenuHolder.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}
