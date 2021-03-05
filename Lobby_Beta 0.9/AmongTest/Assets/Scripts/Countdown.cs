using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Countdown : MonoBehaviour
{
    float currentTime = 0f;
    float startingTime = 8f;

    [SerializeField] Text txt_countdownText;

    void Start()
    {
        currentTime = startingTime;    
    }

    // Update is called once per frame
    void Update()
    {
        if (currentTime <= 0)
        {
            currentTime = 0;
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
}
