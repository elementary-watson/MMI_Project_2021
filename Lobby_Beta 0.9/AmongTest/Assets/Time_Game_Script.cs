using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Time_Game_Script : MonoBehaviour
{
    //[SerializeField] GameObject Progressbar_Panel;
    [SerializeField] TextMeshProUGUI tmp_timer;
    [SerializeField] private TimeSpan timePlaying;
    [SerializeField] private float elapsedTime;

    [SerializeField] Network _network;
    [SerializeField] Progressbar_Script pb_object;
    public AudioSource countdown_sound;
    [SerializeField] bool timerGoing;
    [SerializeField] GameObject Main_Introduction_Panel;
    [SerializeField] GameObject Introduction_Panel_Saboteur;
    [SerializeField] GameObject Introduction_Panel_Crewmate;
    int canPlay;
    // Start is called before the first frame update

    float start = 120f; //120f;

    private void Awake()
    {
        timerGoing = false;
    }
    void Start()
    {
        tmp_timer.text = "0:00";
        timerGoing = false;
        //Invoke("setup", 9);
    }
    public void setup()
    {
        Introduction_Panel_Saboteur.SetActive(false);
        Introduction_Panel_Crewmate.SetActive(false);
        Main_Introduction_Panel.SetActive(false);
        
        beginTimer();
    }
    public void beginTimer()
    {
        canPlay = 0;
        countdown_sound.Stop();
        timerGoing = true;
        elapsedTime = start;
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
                if (elapsedTime <= 15f)
                {
                    if (canPlay == 0)
                        canPlay = 1;
                }
                if (canPlay == 1)
                {
                    countdown_sound.Play();
                    canPlay++;
                }
            }
            else
            {
                countdown_sound.Stop();
                timerGoing = false;
                pb_object.stopProgressbar();
                pb_object.startMeeting(false);
                print("Time has run out!");
                elapsedTime = start;                
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
