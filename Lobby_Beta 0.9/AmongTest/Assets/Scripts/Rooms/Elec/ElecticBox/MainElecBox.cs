using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MainElecBox : MonoBehaviour
{
    private int fixedcounter = 9;
    private int count = 0;
    [SerializeField] Network _network;

    [SerializeField] GameObject ElecBoxPanel;
    public TextMeshProUGUI infoBoxText;
    public GameObject win_message;
    public Image img_Light;

    public AudioSource taskfin_sound;

    void Start()
    {
        img_Light.enabled = false;
        infoBoxText.text = "Bringe den Strom zum fließen";
    }
    
    public void switchtool(int cp)
    {
        count = count + cp;
        if (count == fixedcounter)
        {
            taskfin_sound.Play();
            win_message.SetActive(true);
            img_Light.enabled = true;
            Invoke("taskfinished", 3);
            _network.incrementTaskprogress();
        }
    }


    private void taskfinished()
    {
        ElecBoxPanel.SetActive(false);
    }
}
