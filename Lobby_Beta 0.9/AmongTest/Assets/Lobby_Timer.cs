using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Lobby_Timer : MonoBehaviour
{

    [SerializeField] private float elapsedTime;
    [SerializeField] TextMeshProUGUI tmp_timer;
    [SerializeField] Network _network;
    public AudioSource countdown_sound;
    [SerializeField] bool timerGoing;
    int canPlay;
    int lobbyRoomPeople;

    float start = 180f;
    public void setup()
    {
        beginTimer();
    }
    public void setLobbyRoomPeople(bool isJoined)
    {
        if(isJoined)
            lobbyRoomPeople += 1;
        else
            lobbyRoomPeople -= 1;
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
        if (lobbyRoomPeople > 4)
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
                    elapsedTime = start;
                    _network.initiateStartGame();
                }
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
}
