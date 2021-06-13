using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainWaterDispenser : MonoBehaviour
{
    [SerializeField] GameObject WaterDispenser_Panel;
    [SerializeField] SingleFillDispenser s_filldespenser;
    [SerializeField] SingleEmptyDispenser s_emptydespenser;
    public TextMeshProUGUI infoBoxText;
    public TextMeshProUGUI fill_Text;
    public TextMeshProUGUI empty_Text;
    [SerializeField] Network _network;

    private int fixedcounter = 2;
    private int count = 0;

    public AudioSource taskfin_sound;
    public AudioSource switchfin_sound;
    public GameObject win_message;

    public Image img_empty;
    public Image img_empty_black;
    public Image img_fill;
    public Image img_fill_black;

    public void checkCounter(int cp)
    {
        count = count + cp;
        if (count == fixedcounter)
        {
            taskfin_sound.Play();
            win_message.SetActive(true);
            _network.incrementTaskprogress();
            Invoke("taskfinished", 3);
        }
         
        if(count == 1)
        {
            switchfin_sound.Play();
            infoBoxText.text = "Now fill up the container again";
            fill_Text.text = "Fill";
            empty_Text.text = "";
            s_filldespenser.setInteractable(true);
            s_filldespenser.setup();
            img_fill.enabled = true;
            img_fill_black.enabled = true;
            img_empty.enabled = false;
            img_empty_black.enabled = false;
        }
    }
    private void Start()
    {
        setup();
    }
    public void setup()
    {
        s_emptydespenser.setup();
        s_filldespenser.setInteractable(false);
        infoBoxText.text = "Empty the container";
        fill_Text.text = "";
        empty_Text.text = "Empty";
        fixedcounter = 2;
        count = 0;
        img_fill.enabled = false;
        img_fill_black.enabled = false;
        img_empty.enabled = true;
        img_empty_black.enabled = true;
        win_message.SetActive(false);
    }
    public void taskfinished()
    {        
        setup();        
        WaterDispenser_Panel.SetActive(false);
    }
}
