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
    [SerializeField] private Lightswitch_Logic ll_object;
    

    private void Start()
    {
        //setup_Task();
    }
    public void btn_turnSwitch()
    {
        if(isActive == false)
        {
            GetComponent<Image>().sprite = on_Switch;
            var temp = GetComponentsInChildren<Image>();
            foreach (Image i in temp)
            {
                if (i.tag.Contains("lights"))
                {
                    print("Found a match");
                    i.sprite = light_on;
                }
            }
            ll_object.SwitchChange(1);
            isActive = true;            
        }
        else
        {
            GetComponent<Image>().sprite = off_Switch;
            var temp = GetComponentsInChildren<Image>();
            foreach (Image i in temp)
            {
                if (i.tag.Contains("lights"))
                {
                    print("Found a match");
                    i.sprite = light_off;
                }
            }
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
            GetComponent<Image>().sprite = on_Switch;
            var temp = GetComponentsInChildren<Image>();
            foreach (Image i in temp)
            {
                if (i.tag.Contains("lights"))
                {
                    print("Found a match");
                    i.sprite = light_on;
                }
            }
        }
        else
        { 
            isActive = false;
            GetComponent<Image>().sprite = off_Switch;
            var temp = GetComponentsInChildren<Image>();
            foreach (Image i in temp)
            {
                if (i.tag.Contains("lights"))
                {
                    print("Found a match");
                    i.sprite = light_off;
                }
            }
        }
    }

}
