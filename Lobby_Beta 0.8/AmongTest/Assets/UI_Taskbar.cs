using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UI_Taskbar : MonoBehaviour
{
    public Slider slider;

    public void setMaxTaskbar(int max)
    {
        slider.maxValue = max;
    }
    public void setTaskprogress(int increment)
    {
        slider.value = increment;
    }

}
