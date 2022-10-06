using System;
using TMPro;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour
{
    [SerializeField] private Image[] facesLastToFirst;
    [SerializeField] private TextMeshProUGUI[] namesLastToFirst;
    
    [SerializeField] private Image p3Robot, p4Robot, nr3, nr4;
    [SerializeField] private Sprite nonActiveRbt, activeRbt;

    private void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        
        SetGameOverInfo();
    }

    public void BackToMain()
    {
        GameSettings.GameSettingsInstance.playerToDisplay = -1;
        GameSettings.GameSettingsInstance.ClearQueues();
        SceneManager.LoadScene(0);
    }

    void SetGameOverInfo()
    {
        switch (GameSettings.GameSettingsInstance.numberOfPlayers)
        {
            case 4:
                
                nr3.gameObject.SetActive(true);
                nr4.gameObject.SetActive(true);
                
                facesLastToFirst[0].gameObject.SetActive(true);
                facesLastToFirst[1].gameObject.SetActive(true);

                namesLastToFirst[0].gameObject.SetActive(true);
                namesLastToFirst[1].gameObject.SetActive(true);
                
                p3Robot.sprite = activeRbt;
                p4Robot.sprite = activeRbt;
                
                for (int i = 0; i < 4; i++)
                {
                    facesLastToFirst[i].sprite = GameSettings.GameSettingsInstance.DeadFaceQueue.Dequeue();
                    namesLastToFirst[i].text = GameSettings.GameSettingsInstance.DeadNameQueue.Dequeue();
                }
                
                break;
            
            case 3:

                nr3.gameObject.SetActive(true);
                nr4.gameObject.SetActive(false);
                
                p3Robot.sprite = activeRbt;
                p4Robot.sprite = nonActiveRbt;
                
                facesLastToFirst[0].gameObject.SetActive(false);
                facesLastToFirst[1].gameObject.SetActive(true);

                namesLastToFirst[0].gameObject.SetActive(false);
                namesLastToFirst[1].gameObject.SetActive(true);

                for (int i = 1; i < 4; i++)
                {
                    facesLastToFirst[i].sprite = GameSettings.GameSettingsInstance.DeadFaceQueue.Dequeue();
                    namesLastToFirst[i].text = GameSettings.GameSettingsInstance.DeadNameQueue.Dequeue();
                }
                
                break;
            
            case 2:
                
                nr3.gameObject.SetActive(false);
                nr4.gameObject.SetActive(false);
                
                p3Robot.sprite = nonActiveRbt;
                p4Robot.sprite = nonActiveRbt;
                
                facesLastToFirst[0].gameObject.SetActive(false);
                facesLastToFirst[1].gameObject.SetActive(false);

                namesLastToFirst[0].gameObject.SetActive(false);
                namesLastToFirst[1].gameObject.SetActive(false);
                

                for (int i = 2; i < 4; i++)
                {
                    facesLastToFirst[i].sprite = GameSettings.GameSettingsInstance.DeadFaceQueue.Dequeue();
                    namesLastToFirst[i].text = GameSettings.GameSettingsInstance.DeadNameQueue.Dequeue();
                }
                
                break;
        }
    }
}
