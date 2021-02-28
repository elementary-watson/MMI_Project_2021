using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lights_logic : MonoBehaviour
{

    public int switchCount;
    public GameObject winText;
    private int onCount = 0;

    public void SwitchChange(int points)
    {
        onCount = onCount + points;
        if(onCount == switchCount)
        {
            winText.SetActive(true);
        }
        else
        {
            winText.SetActive(false);
        }
    }
}
