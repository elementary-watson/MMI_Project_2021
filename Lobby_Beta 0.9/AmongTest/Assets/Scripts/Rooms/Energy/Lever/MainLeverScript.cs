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

    // Start is called before the first frame update
    void Start()
    {
        counter = 0; 
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
        Lever_panel.SetActive(false);
    }
}
