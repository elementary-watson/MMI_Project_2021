using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Single_Sink_Task : MonoBehaviour
{
    public Sprite cardinalCorrect;
    public Sprite cardinal_050;
    public Sprite cardinal_025;
    public Sprite cardinal_000;
    public Button btn_valve;
    public Image img_btn;

    float currentCardinal;
    public Main_Sink_Task mst;
    public AudioSource switchfin_sound;
    // Start is called before the first frame update
    void Start()
    {
        reset();
    }
    public void reset()
    {
        btn_valve.interactable = true;
    }
    public void btn_turnValve()
    {
        if(currentCardinal>= 0.5f)
        {
            img_btn.sprite = cardinalCorrect;
            btn_valve.interactable = false;
            /*SpriteState sst = new SpriteState();
            sst = btn_valve.spriteState;
            sst.disabledSprite = cardinalCorrect;
            sst.highlightedSprite = cardinalCorrect;
            btn_valve.spriteState = sst;*/
            //btn_valve.enabled = false;
            switchfin_sound.Play();
            mst.ValveSwitch(1);
        }
        else if (currentCardinal >= 0.25f)
        {
            currentCardinal = 0.5f;
            img_btn.sprite = cardinal_050;
        }
        else if (currentCardinal >= 0)
        {
            currentCardinal = 0.25f;
            img_btn.sprite = cardinal_025;
        }
        else
        {
            print("Unexpected Switch event");
        }
    }
    public void setup(float cardinalPoint)
    {
        reset();
        currentCardinal = cardinalPoint;
        if(cardinalPoint >= 0.75f)
        {
            img_btn.sprite = cardinalCorrect;
            //btn_valve.enabled = false;
            btn_valve.interactable = false;                        
        }
        else if (currentCardinal >= 0.5f)
        {
            img_btn.sprite = cardinal_050;
        }
        else if (currentCardinal >= 0.25f)
        {
            img_btn.sprite = cardinal_025;
        }      
        else if (currentCardinal >= 0f)
        {
            img_btn.sprite = cardinal_000;
        }
        else
        {
            print("Unexpected Setup event");
        }
    }
}