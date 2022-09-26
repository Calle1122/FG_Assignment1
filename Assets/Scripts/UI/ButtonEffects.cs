using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ButtonEffects : MonoBehaviour
{
    private void Start()
    {
        DOTween.Init();
    }

    public void ExpandBtn()
    {
        transform.DOScale(new Vector2(1.2f, 1.2f), 0.3f);
    }

    public void UnExpandBtn()
    {
        transform.DOScale(new Vector2(1f, 1f), 0.3f);
    }
}
