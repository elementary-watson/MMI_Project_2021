using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainClickMediKit : MonoBehaviour
{
    private int fixedcounter = 7;
    private int count = 0;

    public GameObject win_message;
    public AudioSource taskfin_sound;
    public Network _network;
    [SerializeField] GameObject MediKitPanel;
    [SerializeField] SingleTurnTool[] tools = new SingleTurnTool[0];

    private void Start()
    {
        setup();
    }

    public void setup()
    {
        fixedcounter = 7;
        count = 0;
        win_message.SetActive(false);

        for (int i = 0; i < 7; i++)
        {
            tools[i].setup();
        }
    }

    public void switchtool(int cp)
    {
        count = count + cp;
        if (count == fixedcounter)
        {
            taskfin_sound.Play();
            win_message.SetActive(true);
            Invoke("taskfinished", 3);
            _network.incrementTaskprogress();
        }
    }


    private void taskfinished()
    {
        setup();
        MediKitPanel.SetActive(false);
    }
}
