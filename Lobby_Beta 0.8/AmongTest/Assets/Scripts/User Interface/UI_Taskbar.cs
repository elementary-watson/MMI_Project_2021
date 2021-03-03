using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UI_Taskbar : MonoBehaviour
{
    //public Slider slider;
    public int maximum;
    public int current;
    [SerializeField] private Image img_progressbar;
    [SerializeField] private TMP_Text tmp_progressValue;
    [SerializeField] private GameObject Panel_Voting;
    [SerializeField] private GameObject thisMain_Progressbar;

    void getCurrentFill()
    {
        float fillamount = (float)current / (float)maximum;
        img_progressbar.fillAmount = fillamount;   
        if(current == 100)
        {
            Panel_Voting.SetActive(true);
            thisMain_Progressbar.SetActive(false);
        }
    }
    private void Start()
    {
        current = 0;
    }
    private void Update()
    {
        getCurrentFill();
        tmp_progressValue.text = ((float)current / (float)maximum) * 100 + " %";
    }
    public void setMaxTaskbar(int max)
    {
        //slider.maxValue = max;
    }
    public void setTaskprogress(int increment)
    {
        current += increment*5;
    }

}
