using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UIRotator : MonoBehaviour
{
    void Start()
    {
        DOTween.Init();
        
        DOTween.Sequence()
            .Append(transform.DORotate(new Vector3(0, 0, -1.5f), 1f))
            .Append(transform.DORotate(new Vector3(0, 0, 1.5f), 1f))
            .SetLoops(-1, LoopType.Restart).SetEase(Ease.Linear);
        
        DOTween.Sequence()
            .Append(transform.DOScale(new Vector2(1.05f, 1.05f), .5f))
            .Append(transform.DOScale(Vector2.one, .5f))
            .SetLoops(-1, LoopType.Restart).SetEase(Ease.Linear);
    }

    private void Update()
    {
        
    }
}
