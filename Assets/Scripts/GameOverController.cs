
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour
{
    private void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void BackToMain()
    {
        GameSettings.GameSettingsInstance.playerToDisplay = -1;
        SceneManager.LoadScene(0);
    }
}
