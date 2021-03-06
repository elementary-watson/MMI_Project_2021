using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Loader_FadeOverlay : MonoBehaviour
{
    [Header("Visual References")]
    public GameObject loadingPanel;
    public GameObject PanelMap;

    public Image progressBar;
    public Image fadeOverlay;
    public Image animImage;
    public Text info;
    public Text procentnumber;
    float procent = 0f;

    [Header("Settings")]
    public float waitAfterLoading = 1f;
    public float fadeDuration = 0.5f;
    public Color unfinishedColor;


    public void Start()                                                          
    {
        try {
            //fadeOverlay.enabled = true;
            loadingPanel.SetActive(true);
            fadeOverlay.canvasRenderer.SetAlpha(0.0f);
            FadeIn();

            progressBar.color = unfinishedColor;
            progressBar.fillAmount = 0f;

            info.text = "Ladevorgang ...";
        }
        catch (Exception e) { print("print Excpe" + e); }
            
        
    }


    void Update()
    {
        if (progressBar.fillAmount < 1f)
        {
            animImage.transform.Rotate(new Vector3(0, 0, 1), 90 * Time.deltaTime);
        }

        procent = progressBar.fillAmount*100;
        progressBar.fillAmount += 0.005f;

        if (progressBar.fillAmount >= 0.9f)
        {
            Invoke("ShowCompletion", 0.75f);
        }

        if (progressBar.fillAmount == 1f)
        {
            procentnumber.text = "100%";
        }
        else
        {
            procentnumber.text = "" + System.Math.Round(procent, 0) + "%";
        }
    }

    void ShowCompletion()
    {
        info.text = "Loading Map";
        //fadeOverlay.canvasRenderer.SetAlpha(1.0f);
        FadeOut();
        Invoke("closePanel", waitAfterLoading);
    }

    void FadeIn()
    {
        fadeOverlay.CrossFadeAlpha(1, 1, true);
    }

    void FadeOut()
    {
        fadeOverlay.CrossFadeAlpha(0, 1, true);
    }

    void closePanel()
    {
        loadingPanel.SetActive(false);
        PanelMap.SetActive(true);
    }
}
