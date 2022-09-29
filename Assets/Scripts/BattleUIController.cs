
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleUIController : MonoBehaviour
{
    public GameObject weaponsMenuHolder;
    public GameObject timerHolder;
    public GameObject betweenTurnHolder;
    public GameObject activeFacesHolder;
    public GameObject chargeHolder;
    
    [SerializeField] private TextMeshProUGUI turnTimerTxt;
    [SerializeField] private Button bazookaBtn, blasterBtn;
    [SerializeField] private TextMeshProUGUI turnPlayerTxt;
    [SerializeField] private Image turnPlayerFace;

    [SerializeField] private Image charge1, charge2, charge3;
    [SerializeField] private Image[] allFaces, allHeads;
    [SerializeField] private Sprite deadCharge, aliveCharge, nonActiveFace;
    
    [SerializeField] private GameObject activePlayerManager;
    private ActivePlayerController _activePlayerController;

    private Color _activeColor;

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
        activeFacesHolder.SetActive(false);
        chargeHolder.SetActive(false);

        _activePlayerController = activePlayerManager.GetComponent<ActivePlayerController>();
        
         _activeColor = allFaces[0].color;
        
        SetFaces();
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

    public void SetActiveFaces(int activePlayerIndex)
    {
        foreach (Image face in allFaces)
        {
            var tempColor = face.color;
            tempColor.a = .35f;
            face.color = tempColor;
        }

        foreach (Image head in allHeads)
        {
            var tempColor = head.color;
            tempColor.a = .35f;
            head.color = tempColor;
        }

        allFaces[activePlayerIndex].color = _activeColor;
        allHeads[activePlayerIndex].color = _activeColor;
    }
    
    void SetFaces()
    {
        for (int i = 0; i < 4; i++)
        {
            if (i < GameSettings.GameSettingsInstance.numberOfPlayers)
            {
                allFaces[i].sprite = GameSettings.GameSettingsInstance.playerFaces[i];
            }
            else
            {
                allFaces[i].sprite = nonActiveFace;
                allHeads[i].enabled = false;
            }
        }
    }
    
    public void SetChargeImgs(int currentCharges)
    {
        if (currentCharges == 0)
        {
            charge1.sprite = deadCharge;
            charge2.sprite = deadCharge;
            charge3.sprite = deadCharge;

        }
        else if (currentCharges == 1)
        {
            charge1.sprite = aliveCharge;
            charge2.sprite = deadCharge;
            charge3.sprite = deadCharge;
            
        }
        else if (currentCharges == 2)
        {
            charge1.sprite = aliveCharge;
            charge2.sprite = aliveCharge;
            charge3.sprite = deadCharge;
        }
        else
        {
            charge1.sprite = aliveCharge;
            charge2.sprite = aliveCharge;
            charge3.sprite = aliveCharge;
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
