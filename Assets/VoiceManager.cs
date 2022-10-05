using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceManager : MonoBehaviour
{
    public VoicePack[] allVoices;
    private VoicePack _selectedVoice;
    
    public void SetVoice(int voiceNumber)
    {
        _selectedVoice = allVoices[voiceNumber - 1];
    }
    
    public void LogVoice(int playerNumber)
    {
        GameSettings.GameSettingsInstance.LogVoice(playerNumber, _selectedVoice);
    }
}
