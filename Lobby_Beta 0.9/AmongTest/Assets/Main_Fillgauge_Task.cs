using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main_Fillgauge_Task : MonoBehaviour
{
    int maxTasks = 4;
    int current = 0;
    [SerializeField] GameObject info_panel;
    [SerializeField] GameObject win_panel;
    [SerializeField] GameObject Fillgauge_panel;
    [SerializeField] Single_Gauge_Task[] sft_object = new Single_Gauge_Task[0];
    [SerializeField] Network _network;
    public AudioSource taskfin_sound;

    // Start is called before the first frame update
    void Start()
    {
        setup();
    }
    public void setup()
    {
        win_panel.SetActive(false);
        current = 0;
        for(int i = 0; i < 4; i++)
        {
            sft_object[i].setup();
        }
    }
    public void setCurrent()
    {
        current = current + 1;
        if(current == maxTasks)
        {
            
            win_panel.SetActive(true);
            taskfin_sound.Play();
            Invoke("taskfinished",3);
            _network.incrementTaskprogress();
        }
    }

    public void taskfinished()
    {
        setup();
        Fillgauge_panel.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
