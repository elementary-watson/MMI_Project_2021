﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Summary_Panel_Script : MonoBehaviour
{
    [SerializeField] GameObject GameOver_Panel;
    [SerializeField] Gameover_Panel_Script gameOverPanel_object;

    [SerializeField] GameObject Summary_Panel;
    [SerializeField] Network _network;
    [SerializeField] Multiplayer_Reference m_reference;
    [SerializeField] Time_Game_Script timeGame_object;
    [SerializeField] Game_Info_Script gInfo_script;
    [SerializeField] Image img_CaughtSaboteur;
    [SerializeField] Image img_escapedSaboteur;
    public AudioSource fin_sound;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void setNextMode(bool caught, bool final)
    {
        if (caught)
        {
            fin_sound.Play();
            img_escapedSaboteur.enabled = false;
            img_CaughtSaboteur.enabled = true;
            gameOverPanel_object.setup(caught, final);
            //musik abspielern jingle geschafft!
            //call Final
        }
        else if (!caught && final)
        {
            fin_sound.Play();
            img_escapedSaboteur.enabled = true;
            img_CaughtSaboteur.enabled = false;
            gameOverPanel_object.setup(caught, final);
        }
        else if(!caught && !final)
        {
            Invoke("turnmeoff", 7);
        }
    }
    public void turnmeoff()
    {
        Summary_Panel.SetActive(false);
        _network.setPlayerMovement(true);
        timeGame_object.beginTimer();
        // gInfo_script.shortNotification("isGhost");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}