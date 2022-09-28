using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using TMPro.Examples;

public class BattleUIController : MonoBehaviour
{
    public GameObject weaponsMenuHolder;
    public GameObject timerHolder;
    public GameObject betweenTurnHolder;
    
    [SerializeField] private TextMeshProUGUI turnTimerTxt;
    [SerializeField] private Button bazookaBtn, blasterBtn;
    [SerializeField] private TextMeshProUGUI turnPlayerTxt;
    [SerializeField] private Image turnPlayerFace;

    [SerializeField] private GameObject activePlayerManager;
    private ActivePlayerController _activePlayerController;

    public GameObject crossHairObj;
    
    public GameObject jumpSliderHolder;
    public Slider jumpSlider;

    public GameObject shootSliderHolder;
    public Slider shootSlider;

    void Start()
    {
        betweenTurnHolder.SetActive(false);
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

    public void UpdateBetweenTurnsInfo()
    {
        //Set the between turn info
        turnPlayerTxt.text =
            GameSettings.GameSettingsInstance.playerNames[GameSettings.GameSettingsInstance.playerToDisplay] +
            "'S TURN";
        turnPlayerFace.sprite =
            GameSettings.GameSettingsInstance.playerFaces[GameSettings.GameSettingsInstance.playerToDisplay];
    }
}
