using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleUIController : MonoBehaviour
{
    public GameObject jumpSliderHolder;
    public Slider jumpSlider;

    void Start()
    {
        jumpSliderHolder.SetActive(false);
    }
}
