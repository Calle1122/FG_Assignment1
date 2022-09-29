
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TweenRobots : MonoBehaviour
{
    [SerializeField] private GameObject[] robots;
    [SerializeField] private Button createLobbyBtn;

    [SerializeField] private GameObject pos1, pos2, pos3, pos4, pos1Up, pos2Up, pos3Up, pos4Up;

    private void Start()
    {
        DOTween.Init();
    }

    public void ResetPlayMenu()
    {
        robots[0].transform.position = pos1.transform.position;
        robots[1].transform.position = pos2.transform.position;
        robots[2].transform.position = pos3.transform.position;
        robots[3].transform.position = pos4.transform.position;

        createLobbyBtn.interactable = false;
    }
    
    public void EnableTwo()
    {
        GameSettings.GameSettingsInstance.numberOfPlayers = 2;
        GameSettings.GameSettingsInstance.SetupArrays();
        
        createLobbyBtn.interactable = true;
        
        robots[2].transform.DOMove(pos3.transform.position, .5f);
        robots[3].transform.DOMove(pos4.transform.position, .5f);

        robots[0].transform.DOMove(pos1Up.transform.position, 0.5f);
        robots[1].transform.DOMove(pos2Up.transform.position, 0.5f);
    }

    public void EnableThree()
    {
        GameSettings.GameSettingsInstance.numberOfPlayers = 3;
        GameSettings.GameSettingsInstance.SetupArrays();

        createLobbyBtn.interactable = true;
        
        robots[3].transform.DOMove(pos4.transform.position, .5f);

        robots[0].transform.DOMove(pos1Up.transform.position, 0.5f);
        robots[1].transform.DOMove(pos2Up.transform.position, 0.5f);
        robots[2].transform.DOMove(pos3Up.transform.position, 0.5f);
    }

    public void EnableFour()
    {
        GameSettings.GameSettingsInstance.numberOfPlayers = 4;
        GameSettings.GameSettingsInstance.SetupArrays();
        
        createLobbyBtn.interactable = true;
        
        robots[0].transform.DOMove(pos1Up.transform.position, 0.5f);
        robots[1].transform.DOMove(pos2Up.transform.position, 0.5f);
        robots[2].transform.DOMove(pos3Up.transform.position, 0.5f);
        robots[3].transform.DOMove(pos4Up.transform.position, 0.5f);
    }
}
