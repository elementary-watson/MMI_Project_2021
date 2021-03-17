using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainWaterDispenser : MonoBehaviour
{
    [SerializeField] GameObject WaterDispenser_Panel;
    [SerializeField] SingleFillDispenser s_filldespenser;
    public TextMeshProUGUI infoBoxText;
    public TextMeshProUGUI fill_Text;
    public TextMeshProUGUI empty_Text;

    private int fixedcounter = 2;
    private int count = 0;

    public AudioSource taskfin_sound;
    public AudioSource switchfin_sound;
    public GameObject win_message;

    public void checkCounter(int cp)
    {
        count = count + cp;
        if (count == fixedcounter)
        {
            taskfin_sound.Play();
            win_message.SetActive(true);
            //_network.incrementTaskprogress(10);
            Invoke("taskfinished", 3);
        }
         
        if(count == 1)
        {
            switchfin_sound.Play();
            infoBoxText.text = "Fülle den Behälter jetzt wieder auf";
            fill_Text.text = "Füllen";
            empty_Text.text = "";
            s_filldespenser.setInteractable(true);
        }
    }

    public void taskfinished()
    {
        WaterDispenser_Panel.SetActive(false);
    }
}
