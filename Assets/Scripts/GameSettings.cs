

using System.Collections.Generic;
using UnityEngine;


public class GameSettings : MonoBehaviour
{
    public static GameSettings GameSettingsInstance;

    public int numberOfPlayers;
    public bool shouldLogName = true;

    public bool isPaused = false;
    
    public int deadPlayers;
    
    public int playerToDisplay;
    
    public string[] playerNames;
    public Sprite[] playerFaces;

    public Queue<string> DeadNameQueue;
    public Queue<Sprite> DeadFaceQueue;

    [SerializeField] private Sprite defaultFace;
    
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
        for (int i = 0; i < numberOfPlayers; i++)
        {
            if (DeadNameQueue.Contains(playerNames[i]) == false)
            {
                DeadNameQueue.Enqueue(playerNames[i]);
                DeadFaceQueue.Enqueue(playerFaces[i]);
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

            for (int i = 0; i < playerFaces.Length; i++)
            {
                playerFaces[i] = defaultFace;
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
}
