using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public bool isActive = false;
    public Transform cameraPosTransform;

    private void Awake()
    {
        cameraPosTransform = transform.GetChild(0);
    }
}
