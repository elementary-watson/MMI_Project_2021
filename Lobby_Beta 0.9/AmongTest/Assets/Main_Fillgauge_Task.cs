﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main_Fillgauge_Task : MonoBehaviour
{
    int maxTasks = 4;
    int current = 0;
    [SerializeField] GameObject info_panel;
    [SerializeField] GameObject win_panel;
    [SerializeField] GameObject Fillgauge_panel;
    [SerializeField] Single_Fillgauge_Task[] sft_object = new Single_Fillgauge_Task[0];
    Network _network;
    // Start is called before the first frame update
    void Start()
    {
        current = 0;
    }

    public void setCurrent()
    {
        current = current + 1;
        if(current == maxTasks)
        {
            _network.incrementTaskprogress(10);
            win_panel.SetActive(true);
            Invoke("taskfinished",3);
        }
    }

    public void taskfinished()
    {
        Fillgauge_panel.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}