using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager SoundManagerInstance;

    [SerializeField] private AudioSource musicSource, effectSource;
    [SerializeField] private AudioClip mainMusic, battleMusic;
    
    private void Awake()
    {
        if (SoundManagerInstance == null)
        {
            SoundManagerInstance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlaySound(AudioClip sound)
    {
        effectSource.PlayOneShot(sound);
    }

    public void ChangeMasterVolume(float volume)
    {
        AudioListener.volume = volume;
    }

    public void ChangeMusicVolume(float volume)
    {
        musicSource.volume = volume;
    }
    
    public void ChangeEffectVolume(float volume)
    {
        effectSource.volume = volume;
    }

    public void SetMenuMusic()
    {
        musicSource.clip = mainMusic;
        musicSource.Play();
    }

    public void SetBattleMusic()
    {
        musicSource.clip = battleMusic;
        musicSource.Play();
    }
}
