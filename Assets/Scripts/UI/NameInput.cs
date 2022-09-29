
using UnityEngine;
using TMPro;

public class NameInput : MonoBehaviour
{
    [SerializeField] private int playerNumber;
    [SerializeField] private TMP_InputField nameInput;

    private void Start()
    {
        nameInput.onValueChanged.AddListener(newName => GameSettings.GameSettingsInstance.LogName(playerNumber, newName));
    }

    public void ResetName()
    {
        GameSettings.GameSettingsInstance.shouldLogName = false;
        nameInput.text = null;
        GameSettings.GameSettingsInstance.shouldLogName = true;
    }
}
