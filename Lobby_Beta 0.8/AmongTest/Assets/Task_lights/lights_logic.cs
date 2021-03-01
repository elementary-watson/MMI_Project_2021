using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lights_logic : MonoBehaviour
{
    public int switchCount;
    public GameObject winText;
    private int onCount = 0;
    public UI_Taskbar prog_reference;

    public void SwitchChange(int points)
    {
        onCount = onCount + points;
        if(onCount == switchCount)
        {
            winText.SetActive(true);
            prog_reference.setTaskprogress(10);
        }
        else
        {
            winText.SetActive(false);
        }
    }
}
