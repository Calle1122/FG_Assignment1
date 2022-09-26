using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TweenRobots : MonoBehaviour
{
    [SerializeField] private GameObject[] robots;
    [SerializeField] private Button createLobbyBtn;

    [SerializeField] private GameObject pos1;
    [SerializeField] private GameObject pos2;
    [SerializeField] private GameObject pos3;
    [SerializeField] private GameObject pos4;

    private void Start()
    {
        DOTween.Init();
    }

    public void ResetPlayMenu()
    {
        robots[0].transform.position = pos1.transform.position;
        robots[1].transform.position = pos2.transform.position;
        robots[2].transform.position = pos3.transform.position;
        robots[3].transform.position = pos4.transform.position;

        createLobbyBtn.interactable = false;
    }
    
    public void EnableTwo()
    {
        createLobbyBtn.interactable = true;
        
        robots[2].transform.DOMove(pos3.transform.position, .5f);
        robots[3].transform.DOMove(pos4.transform.position, .5f);

        robots[0].transform.DOMove(pos1.transform.position + new Vector3(0, 115f, 0), 0.5f);
        robots[1].transform.DOMove(pos2.transform.position + new Vector3(0, 115f, 0), 0.5f);
    }

    public void EnableThree()
    {
        createLobbyBtn.interactable = true;
        
        robots[3].transform.DOMove(pos4.transform.position, .5f);

        robots[0].transform.DOMove(pos1.transform.position + new Vector3(0, 115f, 0), 0.5f);
        robots[1].transform.DOMove(pos2.transform.position + new Vector3(0, 115f, 0), 0.5f);
        robots[2].transform.DOMove(pos3.transform.position + new Vector3(0, 115f, 0), 0.5f);
    }

    public void EnableFour()
    {
        createLobbyBtn.interactable = true;
        
        robots[0].transform.DOMove(pos1.transform.position + new Vector3(0, 115f, 0), 0.5f);
        robots[1].transform.DOMove(pos2.transform.position + new Vector3(0, 115f, 0), 0.5f);
        robots[2].transform.DOMove(pos3.transform.position + new Vector3(0, 115f, 0), 0.5f);
        robots[3].transform.DOMove(pos4.transform.position + new Vector3(0, 115f, 0), 0.5f);
    }
}
