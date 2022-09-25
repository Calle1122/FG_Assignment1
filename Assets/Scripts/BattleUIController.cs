using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleUIController : MonoBehaviour
{
    public GameObject weaponsMenuHolder;

    [SerializeField] private TextMeshProUGUI turnTimerTxt;
    [SerializeField] private Button bazookaBtn, blasterBtn;

    [SerializeField] private GameObject activePlayerManager;
    private ActivePlayerController _activePlayerController;
    
    public GameObject crossHairObj;
    
    public GameObject jumpSliderHolder;
    public Slider jumpSlider;

    public GameObject shootSliderHolder;
    public Slider shootSlider;

    void Start()
    {
        weaponsMenuHolder.SetActive(false);
        jumpSliderHolder.SetActive(false);
        shootSliderHolder.SetActive(false);
        crossHairObj.SetActive(false);

        _activePlayerController = activePlayerManager.GetComponent<ActivePlayerController>();
    }

    void Update()
    {
        //Turn txt
        int currentTime = (int)_activePlayerController.currentTurnTimer;
        turnTimerTxt.text = currentTime.ToString();

        //Weapons Menu
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
