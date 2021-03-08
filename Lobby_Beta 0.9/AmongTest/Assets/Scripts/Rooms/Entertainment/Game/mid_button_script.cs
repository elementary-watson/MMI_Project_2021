using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mid_button_script : MonoBehaviour
{
    [SerializeField] Button btn_mid;
    [SerializeField] Image img_midBtn;

    [SerializeField] Sprite neutral;
    [SerializeField] Sprite neutral_pressed;
    [SerializeField] Sprite active;
    [SerializeField] Sprite active_pressed;
    [SerializeField] MainSkriptGame msg_object;
    SpriteState ss;
    bool isState;

    // Start is called before the first frame update
    public void setup(int order)
    {
        if (order > 0)
        {
            ss.disabledSprite = neutral;
            btn_mid.spriteState = ss;
            btn_mid.interactable = true;
            isState = false;
        }
        else
            activateMidPart(true);
    }
    public void disableButtons()
    {
        btn_mid.interactable = false;
    }
    public void btn_switch()
    {
        if (isState)
        {
            img_midBtn.sprite = active_pressed;
            btn_mid.interactable = false;
            msg_object.nextMidButton();
        }
        else
        {
            img_midBtn.sprite = neutral_pressed;
            Invoke("setBack", 1);
        }
    }
    void setBack()
    {
        img_midBtn.sprite = neutral;
    }
    public void activateMidPart(bool isNextBtn)
    {
        isState = isNextBtn;

        if (isState) {
            btn_mid.interactable = true;
            ss.disabledSprite = active_pressed;
            btn_mid.spriteState = ss;
            img_midBtn.sprite = active;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
