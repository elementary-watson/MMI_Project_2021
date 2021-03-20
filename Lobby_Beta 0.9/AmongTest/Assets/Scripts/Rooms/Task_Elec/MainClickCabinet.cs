using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainClickCabinet : MonoBehaviour
{
    private int fixedcounter = 6;
    private int count = 0;

    public Network _network;
    public GameObject win_message;
    public AudioSource taskfin_sound;

    [SerializeField] GameObject CabinetPanel;
    [SerializeField] SingleSwitchTool[] tools = new SingleSwitchTool[0];

    private void Start()
    {
        setup();
    }

    public void setup()
    {
       fixedcounter = 6;
       count = 0;
       win_message.SetActive(false);

       for (int i = 0; i < 6; i++)
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
        CabinetPanel.SetActive(false);
    }
}
