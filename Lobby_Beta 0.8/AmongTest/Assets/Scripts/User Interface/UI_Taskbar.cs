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
    public Image img_progressbar;
    private TMP_Text tmp_progressValue;

    void getCurrentFill()
    {
        float fillamount = (float)current / (float)maximum;
        img_progressbar.fillAmount = fillamount;        
    }
    private void Start()
    {
        tmp_progressValue = GetComponentInChildren<TMP_Text>();
    }
    private void Update()
    {
        getCurrentFill();
        tmp_progressValue.text = ((float)current / (float)maximum)*10 + " %";
    }
    public void setMaxTaskbar(int max)
    {
        //slider.maxValue = max;
    }
    public void setTaskprogress(int increment)
    {
        current += increment;
    }

}
