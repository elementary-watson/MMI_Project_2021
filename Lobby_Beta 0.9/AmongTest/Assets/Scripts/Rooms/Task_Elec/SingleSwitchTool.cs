using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SingleSwitchTool : MonoBehaviour
{
    public Sprite on_Tool;
    public Sprite off_Tool;
    public Image img_Button;

    private bool isActive;
    [SerializeField] Button btn;
    [SerializeField] private MainClickCabinet mainobject;
    public AudioSource switchfin_sound;

    public void btnturntool()
    {
        if (isActive == false)
        {
            img_Button.sprite = on_Tool;
            switchfin_sound.Play();

            btn.interactable = false;
            mainobject.switchtool(1);
            isActive = true;
        }
    }

    public void setup()
    {
        isActive = false;
        btn.interactable = true;
        img_Button.sprite = off_Tool;
    }

}
