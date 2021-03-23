using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSingleTubeScript : MonoBehaviour
{
    [SerializeField] GameObject SingleTube_panel;
    [SerializeField] SingleTubeScript s_tubeobject;
    [SerializeField] Network _network;
    public AudioSource taskfin_sound;
    public GameObject win_message;

    private void Start()
    {
        setup();
    }

    public void setup()
    {
        s_tubeobject.setup();
        win_message.SetActive(false);
    }

    public void checkCounter()
    {
        taskfin_sound.Play();
        win_message.SetActive(true);
        _network.incrementTaskprogress();
        Invoke("taskfinished", 3);
    }

    public void taskfinished()
    {
        setup();
        SingleTube_panel.SetActive(false);
    }
}
