using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LobbyScript : MonoBehaviour
{
    [SerializeField] private GameObject menuCanv;
    
    [SerializeField] private GameObject activeP3, activeP4;
    [SerializeField] private GameObject inActiveP3, inActiveP4;
    [SerializeField] private GameObject nameP3, nameP4;
    [SerializeField] private GameObject voiceP3, voiceP4;

    private bool shouldFade = false;
    
    private void Update()
    {
        if (shouldFade)
        {
            RectMask2D screenFader = menuCanv.GetComponent<RectMask2D>();
            float value = Time.deltaTime * 2500f;
            screenFader.softness = new Vector2Int(1000, 1000);
            screenFader.padding = screenFader.padding + new Vector4(value, value, value, value);
        }
    }

    public void TwoActive()
    {
        activeP3.SetActive(false);
        activeP4.SetActive(false);
        
        inActiveP3.SetActive(true);
        inActiveP4.SetActive(true);
        
        nameP3.SetActive(false);
        nameP4.SetActive(false);
        
        voiceP3.SetActive(false);
        voiceP4.SetActive(false);
    }
    
    public void ThreeActive()
    {
        activeP3.SetActive(true);
        activeP4.SetActive(false);
        
        inActiveP3.SetActive(false);
        inActiveP4.SetActive(true);
        
        nameP3.SetActive(true);
        nameP4.SetActive(false);
        
        voiceP3.SetActive(true);
        voiceP4.SetActive(false);
    }

    public void FourActive()
    {
        activeP3.SetActive(true);
        activeP4.SetActive(true);
        
        inActiveP3.SetActive(false);
        inActiveP4.SetActive(false);
        
        nameP3.SetActive(true);
        nameP4.SetActive(true);
        
        voiceP3.SetActive(true);
        voiceP4.SetActive(true);
    }

    public void StartGame()
    {
        GameSettings.GameSettingsInstance.deadPlayers = 0;

        shouldFade = true;

        StartCoroutine(SwapScene());
    }

    IEnumerator SwapScene()
    {
        yield return new WaitForSeconds(1);

        SceneManager.LoadScene(1);

        yield return null;
    }

}
