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
    SingleSwitchTool[] tools = new SingleSwitchTool[6];
    
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
        CabinetPanel.SetActive(false);
    }
}
