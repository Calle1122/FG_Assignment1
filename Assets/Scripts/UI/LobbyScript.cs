
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyScript : MonoBehaviour
{
    [SerializeField] private GameObject activeP3, activeP4;
    [SerializeField] private GameObject inActiveP3, inActiveP4;
    [SerializeField] private GameObject nameP3, nameP4;

    public void TwoActive()
    {
        activeP3.SetActive(false);
        activeP4.SetActive(false);
        
        inActiveP3.SetActive(true);
        inActiveP4.SetActive(true);
        
        nameP3.SetActive(false);
        nameP4.SetActive(false);
    }
    
    public void ThreeActive()
    {
        activeP3.SetActive(true);
        activeP4.SetActive(false);
        
        inActiveP3.SetActive(false);
        inActiveP4.SetActive(true);
        
        nameP3.SetActive(true);
        nameP4.SetActive(false);
    }

    public void FourActive()
    {
        activeP3.SetActive(true);
        activeP4.SetActive(true);
        
        inActiveP3.SetActive(false);
        inActiveP4.SetActive(false);
        
        nameP3.SetActive(true);
        nameP4.SetActive(true);
    }

    public void StartGame()
    {
        GameSettings.GameSettingsInstance.deadPlayers = 0;
        SceneManager.LoadScene(1);
    }

}
