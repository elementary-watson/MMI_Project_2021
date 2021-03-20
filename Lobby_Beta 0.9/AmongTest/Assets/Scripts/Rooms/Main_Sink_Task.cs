using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main_Sink_Task:MonoBehaviour
{
    public Network _network;
    public GameObject win_message;
    public AudioSource taskfin_sound;

    int currentPoints;
    int maxPoints = 12;
    public Single_Sink_Task[] sst = new Single_Sink_Task[0];
    [SerializeField] GameObject SinkPanel;
    // Start is called before the first frame update
    void Start()
    {
        setup();
    }

    public void setup()
    {
        currentPoints = 0;
        SetupValves();
    }
    public void ValveSwitch(int points)
    {
        currentPoints += points;
        if(maxPoints == currentPoints)
        {
            taskfin_sound.Play();
            win_message.SetActive(true);
            Invoke("taskfinished", 3);
            _network.incrementTaskprogress();
        }
    }

    public void SetupValves()
    {
        sst[0].setup(0);
        for (int i = 1; i < maxPoints; i++)
        {
            float temp = Random.value;
            if (temp >= 0.75f)
                currentPoints += 1;
            sst[i].setup(temp);
        }
    }
    private void taskfinished()
    {
        setup();
        win_message.SetActive(false);
        SinkPanel.SetActive(false);
    }
}
