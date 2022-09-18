using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleUIController : MonoBehaviour
{
    public GameObject jumpSliderHolder;
    public Slider jumpSlider;

    public GameObject shootSliderHolder;
    public Slider shootSlider;

    void Start()
    {
        jumpSliderHolder.SetActive(false);
        shootSliderHolder.SetActive(false);
    }
}
