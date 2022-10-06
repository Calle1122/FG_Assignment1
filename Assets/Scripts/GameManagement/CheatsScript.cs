
using UnityEngine;
using UnityEngine.UI;

public class CheatsScript : MonoBehaviour
{
    [SerializeField] private Toggle moonToggle, healthToggle;

    public void SetMoon()
    {
        GameSettings.GameSettingsInstance.SetMoonMode(moonToggle.isOn);
    }

    public void SetHealth()
    {
        GameSettings.GameSettingsInstance.SetHealthMode(healthToggle.isOn);
    }
}
