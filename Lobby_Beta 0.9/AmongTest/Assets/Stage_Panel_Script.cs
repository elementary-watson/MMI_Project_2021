using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stage_Panel_Script : MonoBehaviour
{
    [SerializeField] Sprite stageOn;
    [SerializeField] Sprite stageOff;
    [SerializeField] Image img_stage_prevote;
    [SerializeField] Image img_stage_chat;
    [SerializeField] Image img_stage_postvote;
    [SerializeField] Single_StagePanel_Script[] sss_object = new Single_StagePanel_Script[0];
    [SerializeField] Multiplayer_Reference m_reference;
    
    public AudioSource taskfin_sound;
    // Start is called before the first frame update
    void Start()
    {
        setup();
    }

    public void setup()
    {        
        img_stage_prevote.sprite = stageOn;
        img_stage_chat.sprite = stageOff;
        img_stage_postvote.sprite = stageOff;
        sss_object[0].setup();
        sss_object[1].setup();
    }

    public void switchOnNextstage()
    {
        if(m_reference.getCurrentStage() == 2)
        {
            img_stage_prevote.sprite = stageOff;
            sss_object[0].setCanStart(true);
        }
        else if(m_reference.getCurrentStage() == 3)
        {
            img_stage_chat.sprite = stageOff;
            sss_object[1].setCanStart(true);
        }

    }

    internal void switchSprite()
    {
        if (m_reference.getCurrentStage() == 2)
        {
            img_stage_chat.sprite = stageOn;
        }
        else if (m_reference.getCurrentStage() == 3)
        {
            img_stage_postvote.sprite = stageOn;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
