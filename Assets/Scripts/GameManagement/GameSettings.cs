
using System.Collections.Generic;
using UnityEngine;


public class GameSettings : MonoBehaviour
{
    public static GameSettings GameSettingsInstance;

    public int numberOfPlayers;
    public bool shouldLogName = true;

    public bool isPaused = false;
    
    public int deadPlayers;

    public bool unhealthyMode = false;
    public bool moonMode = false;

    public int playerToDisplay;
    
    public string[] playerNames;
    public Sprite[] playerFaces;
    public VoicePack[] playerVoices;

    public Queue<string> DeadNameQueue;
    public Queue<Sprite> DeadFaceQueue;

    [SerializeField] private Sprite defaultFace;
    [SerializeField] private VoicePack defaultVoice;
    
    private void Awake()
    {
        DeadNameQueue = new Queue<string>();
        DeadFaceQueue = new Queue<Sprite>();
        
        playerToDisplay = -1;
        
        if (GameSettingsInstance == null)
        {
            GameSettingsInstance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void EnqueueLastPlayer()
    {
        ActivePlayerController gameCon = GameObject.Find("ActivePlayerController").GetComponent<ActivePlayerController>();

        foreach (PlayerManager playerMan in gameCon.allPlayerManagers)
        {
            if (playerMan != null && playerMan.health > 0)
            {
                DeadFaceQueue.Enqueue(playerMan.face.sprite);
                DeadNameQueue.Enqueue(playerMan.nameTxt.text);
            }
        }

        
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    
    public void SetupArrays()
    {
        playerFaces = new Sprite[numberOfPlayers];
        playerNames = new string[numberOfPlayers];
        playerVoices = new VoicePack[numberOfPlayers];
        
        for (int i = 0; i < playerFaces.Length; i++)
        {
            playerFaces[i] = defaultFace;
            playerVoices[i] = defaultVoice;
            playerNames[i] = "Unnamed Robot";
        }
    }

    public void ClearQueues()
    {
        DeadFaceQueue.Clear();
        DeadNameQueue.Clear();
    }
    
    public void LogFace(int playerNumber, Sprite face)
    {
        playerFaces[playerNumber - 1] = face;
    }

    public void LogName(int playerNumber, string playerName)
    {
        if (shouldLogName)
        {
            playerNames[playerNumber - 1] = playerName;
        }
    }

    public void LogVoice(int playerNumber, VoicePack voice)
    {
        playerVoices[playerNumber - 1] = voice;
        
        SoundManager.SoundManagerInstance.PlaySound(voice.hello);
    }

    public void SetMoonMode(bool isOn)
    {
        moonMode = isOn;
    }

    public void SetHealthMode(bool isOn)
    {
        unhealthyMode = isOn;
    }
}
