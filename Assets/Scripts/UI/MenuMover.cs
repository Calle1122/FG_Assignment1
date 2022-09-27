using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MenuMover : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject playMenu;
    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private GameObject lobbyMenu;

    [SerializeField] private GameObject upPos;
    [SerializeField] private GameObject downPos;
    [SerializeField] private GameObject leftPos;
    [SerializeField] private GameObject rightPos;

    private Vector3 _middlePos;

    private void Awake()
    {
        _middlePos = mainMenu.transform.position;

        playMenu.transform.position = rightPos.transform.position;
        settingsMenu.transform.position = leftPos.transform.position;
        lobbyMenu.transform.position = downPos.transform.position;
    }

    private void Start()
    {
        DOTween.Init();
    }

    public void MainToPlay()
    {
        mainMenu.transform.DOMove(leftPos.transform.position, 1f);
        playMenu.transform.DOMove(_middlePos, 1f);
    }

    public void PlayToMain()
    {
        mainMenu.transform.DOMove(_middlePos, 1f);
        playMenu.transform.DOMove(rightPos.transform.position, 1f);
    }

    public void PlayToLobby()
    {
        playMenu.transform.DOMove(leftPos.transform.position, 1f);
        lobbyMenu.transform.DOMove(_middlePos, 1f);
    }

    public void LobbyToPlay()
    {
        playMenu.transform.DOMove(_middlePos, 1f);
        lobbyMenu.transform.DOMove(downPos.transform.position, 1f);
    }

    public void MainToSettings()
    {
        mainMenu.transform.DOMove(rightPos.transform.position, 1f);
        settingsMenu.transform.DOMove(_middlePos, 1f);
    }

    public void SettingsToMain()
    {
        mainMenu.transform.DOMove(_middlePos, 1f);
        settingsMenu.transform.DOMove(leftPos.transform.position, 1f);
    }
}
