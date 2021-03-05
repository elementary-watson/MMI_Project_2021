using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Single_Switch : MonoBehaviour
{
    public Sprite off_Switch;
    public Sprite on_Switch;
    public Sprite light_on;
    public Sprite light_off;
    private bool isActive;
    public Image img_button;
    public Image img_lights;
    [SerializeField] private Lightswitch_Logic ll_object;
    

    private void Start()
    {
        //setup_Task();
    }
    public void btn_turnSwitch()
    {
        if(isActive == false)
        {
            img_button.sprite = on_Switch;
            img_lights.sprite = light_on;

            ll_object.SwitchChange(1);
            isActive = true;            
        }
        else
        {
            img_lights.sprite = light_off;
            /*var temp = GetComponentsInChildren<Image>();
            foreach (Image i in temp)
            {
                if (i.tag.Contains("lights"))
                {
                    i.sprite = light_off;
                }
            }*/
            ll_object.SwitchChange(-1);
            isActive = false;
        }
    }
    public void setup_Task(bool state)
    {
        print("DEBUG: Setup is called");
        if (state) 
        { 
            isActive = true;
            img_button.sprite = on_Switch;
            img_lights.sprite = light_on;

        }
        else
        { 
            isActive = false;
            img_button.sprite = off_Switch;
            img_lights.sprite = light_off;
        }
    }

}
