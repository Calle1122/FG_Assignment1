using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ButtonEffects : MonoBehaviour
{

    [SerializeField] private AudioClip enterSound, exitSound;
    
    private void Start()
    {
        DOTween.Init();
    }

    public void ExpandBtn()
    {
        transform.DOScale(new Vector2(1.2f, 1.2f), 0.3f);
        SoundManager.SoundManagerInstance.PlaySound(enterSound);
    }

    public void UnExpandBtn()
    {
        transform.DOScale(new Vector2(1f, 1f), 0.3f);
        SoundManager.SoundManagerInstance.PlaySound(exitSound);
    }
}
