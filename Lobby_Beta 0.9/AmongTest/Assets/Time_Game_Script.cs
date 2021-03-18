using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Time_Game_Script : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI tmp_timer;
    [SerializeField] private TimeSpan timePlaying;
    [SerializeField] private float elapsedTime;
    [SerializeField] GameObject Progressbar_Panel;

    [SerializeField] Progressbar_Script pb_object;

    [SerializeField] bool timerGoing;
    // Start is called before the first frame update

    private void Awake()
    {
        timerGoing = false;
    }
    void Start()
    {
        tmp_timer.text = "0:00";
        timerGoing = true;
        beginTimer();
    }
    public void beginTimer()
    {
        timerGoing = true;
        elapsedTime = 180f;
    }
    public void stopTimer()
    {
        timerGoing = false;
    }
    void Update()
    {
        if (timerGoing)
        {
            if (elapsedTime > 0)
            {
                elapsedTime -= Time.deltaTime;
                DisplayTime(elapsedTime);
            }
            else
            {
                pb_object.stopProgressbar();
                Debug.Log("Time has run out!");
                elapsedTime = 0;
                timerGoing = false;

            }
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        tmp_timer.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
    /*public void delayCall()
    {
        StartCoroutine(UpdateTimer());
    }
    public void endTimer()
    {
        timerGoing = false;
    }
    private IEnumerator UpdateTimer()
    {
        while (timerGoing)
        {
            elapsedTime -= Time.deltaTime;
            timePlaying = TimeSpan.FromSeconds(elapsedTime);
            string timePlayingString = timePlaying.ToString("m':'ss");
            tmp_timer.text = timePlayingString;

            yield return null;
        }
    }*/
    // Update is called once per frame


}
