using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSingleTubeScript : MonoBehaviour
{
    [SerializeField] GameObject SingleTube_panel;

    public AudioSource taskfin_sound;
    public GameObject win_message;

    public void checkCounter()
    {
        taskfin_sound.Play();
        win_message.SetActive(true);
        //_network.incrementTaskprogress(10);
        Invoke("taskfinished", 3);
    }

    public void taskfinished()
    {
        SingleTube_panel.SetActive(false);
    }
}
