
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFader : MonoBehaviour
{
    [SerializeField] private GameObject blackFade;
    
    private bool shouldFade = false;
    private RectMask2D screenFader;

    private void Start()
    {
        StartCoroutine(FadeOut());
    }

    private void Update()
    {
        if (shouldFade)
        {
            screenFader = this.gameObject.GetComponent<RectMask2D>();
            float value = Time.deltaTime * 2500f;
            screenFader.softness = new Vector2Int(1000, 1000);
            screenFader.padding = screenFader.padding + new Vector4(value, value, value, value);
        }
    }

    IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(1);

        shouldFade = true;

        yield return new WaitForSeconds(1);

        blackFade.SetActive(false);
        shouldFade = false;
        screenFader.enabled = false;
        
        yield return null;
    }
}
