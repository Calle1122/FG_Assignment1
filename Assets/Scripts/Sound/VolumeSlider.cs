using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    [SerializeField] private Slider slider;

    [SerializeField] private bool forMaster, forMusic, forEffect;

    private void Start()
    {
        if (forMaster)
        {
            SoundManager.SoundManagerInstance.ChangeMasterVolume(slider.value);
            slider.onValueChanged.AddListener(val => SoundManager.SoundManagerInstance.ChangeMasterVolume(val));
        }

        if (forMusic)
        {
            SoundManager.SoundManagerInstance.ChangeMusicVolume(slider.value);
            slider.onValueChanged.AddListener(val => SoundManager.SoundManagerInstance.ChangeMusicVolume(val));
        }

        if (forEffect)
        {
            SoundManager.SoundManagerInstance.ChangeEffectVolume(slider.value);
            slider.onValueChanged.AddListener(val => SoundManager.SoundManagerInstance.ChangeEffectVolume(val));
        }
    }
}
