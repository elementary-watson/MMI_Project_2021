using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainLeverScript : MonoBehaviour
{
    int fincounter = 5;
    int counter;
    [SerializeField] Network _network;
    public AudioSource taskfin_sound;
    public GameObject win_message;

    [SerializeField] GameObject Lever_panel;
    [SerializeField] SingleLeverScript[] s_leverobject = new SingleLeverScript[0];

    // Start is called before the first frame update
    void Start()
    {
        setup();    
    }

    public void setup()
    {
        counter = 0;
        fincounter = 5;
        for (int i = 0; i < 5; i++)
        {
            s_leverobject[i].setup();
        }
        win_message.SetActive(false);
    }

    public void checkCounter()
    {
        counter = counter + 1;
        if (counter == fincounter)
        {
            _network.incrementTaskprogress();
            taskfin_sound.Play();
            win_message.SetActive(true);
            Invoke("taskfinished", 3);
        }
    }
    public void taskfinished()
    {
        setup();
        Lever_panel.SetActive(false);
    }
}
