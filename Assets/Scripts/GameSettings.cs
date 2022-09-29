using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSettings : MonoBehaviour
{
    public static GameSettings GameSettingsInstance;

    public int numberOfPlayers;
    public bool shouldLogName = true;

    public int deadPlayers;
    
    public int playerToDisplay;
    
    public string[] playerNames;
    public Sprite[] playerFaces;

    [SerializeField] private Sprite defaultFace;

    private bool _hasInitialSetup = false;
    private void Awake()
    {
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

    public void SetupArrays()
    {
        /*if (_hasInitialSetup)
        {
            Sprite[] prevFaces = new Sprite[numberOfPlayers];
            string[] prevNames = new string[numberOfPlayers];

            for (int i = 0; i < numberOfPlayers - 1; i++)
            {
                if (i < playerFaces.Length)
                {
                    prevFaces[i] = playerFaces[i];
                }

                if (i < playerNames.Length)
                {
                    prevNames[i] = playerNames[i];
                }
            }*/
            
            playerFaces = new Sprite[numberOfPlayers];
            playerNames = new string[numberOfPlayers];

            for (int i = 0; i < playerFaces.Length; i++)
            {
                playerFaces[i] = defaultFace;
            }
            
            /*for (int i = 0; i < numberOfPlayers - 1; i++)
            {
                if (prevFaces[i] != null)
                {
                    playerFaces[i] = prevFaces[i];
                }

                if (prevNames[i] != null)
                {
                    playerNames[i] = prevNames[i];
                }
            }
        }
        else
        {
            playerFaces = new Sprite[numberOfPlayers];
            playerNames = new string[numberOfPlayers];

            _hasInitialSetup = true;
        }*/
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
