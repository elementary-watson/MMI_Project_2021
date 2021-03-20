using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Countdown : MonoBehaviour
{
    float currentTime = 0f;
    float startingTime = 8f;

    [SerializeField] Text txt_countdownText;
    [SerializeField] Network _network;
    int isFin;
    void Start()
    {
        isFin = 0;
        currentTime = startingTime;    
    }

    // Update is called once per frame
    void Update()
    {
        if (isFin == 0)
        {
            if (currentTime <= 0)
            {
                currentTime = 0;
                isFin = 1;
            }
            else
            {
                currentTime -= 1 * Time.deltaTime;
                txt_countdownText.text = "Starting in " + currentTime.ToString("0");
            }

            if (currentTime <= 1)
            {
                txt_countdownText.text = "Game starts now...";
            }
        }

        if (isFin == 1)
        {
            isFin = 2;
            _network.RPCStartFading();
        }       
        
    }
}
