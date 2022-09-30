
using System;
using UnityEngine;
using DG.Tweening;

public class PauseCanvas : MonoBehaviour
{
    [SerializeField] private ActivePlayerController activeCon;
    [SerializeField] private GameObject pauseArea;
    [SerializeField] private GameObject leftPos;
    private Vector3 _middlePos;

    private bool _isActive = false;

    private void Awake()
    {
        _middlePos = pauseArea.transform.position;
    }

    private void Start()
    {
        DOTween.Init();
        pauseArea.transform.position = leftPos.transform.position;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            switch (_isActive)
            {
                case true:
                    
                    MenuOut();
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;

                    _isActive = false;
                    GameSettings.GameSettingsInstance.isPaused = false;
                    
                    activeCon.CanMove(true);
                    
                    break;
                
                case false:

                    MenuIn();
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;

                    _isActive = true;
                    GameSettings.GameSettingsInstance.isPaused = true;
                    
                    activeCon.CanMove(false);
                    
                    break;
            }
        }
    }

    public void MenuIn()
    {
        pauseArea.transform.DOMove(_middlePos, 1f);
    }

    public void MenuOut()
    {
        pauseArea.transform.DOMove(leftPos.transform.position, 1f);
    }
}


