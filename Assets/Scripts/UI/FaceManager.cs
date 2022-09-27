using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class FaceManager : MonoBehaviour
{
    [SerializeField] private int playerNumber;
    
    public Sprite[] faces;

    private Image _faceHolder;
    
    private Sprite _activeFace;
    private int _faceIndex = 0;
    
    void Start()
    {
        DOTween.Init();

        _activeFace = faces[_faceIndex];
        _faceHolder = GetComponent<Image>();

        _faceHolder.sprite = _activeFace;
    }

    public void NextFace()
    {
        _faceIndex++;
        if (_faceIndex == faces.Length)
        {
            _faceIndex = 0;
        }

        _activeFace = faces[_faceIndex];

        _faceHolder.sprite = _activeFace;
        
        GameSettings.GameSettingsInstance.LogFace(playerNumber, _activeFace);
    }

    public void PrevFace()
    {
        _faceIndex--;
        if (_faceIndex < 0)
        {
            _faceIndex = faces.Length - 1;
        }

        _activeFace = faces[_faceIndex];

        _faceHolder.sprite = _activeFace;
        
        GameSettings.GameSettingsInstance.LogFace(playerNumber, _activeFace);
    }

    public void ResetFace()
    {
        _faceIndex = 0;
        _activeFace = faces[_faceIndex];
        
        _faceHolder.sprite = _activeFace;
    }
}
