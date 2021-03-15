using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SingleElecBox : MonoBehaviour
{
    public Sprite on_Tool;
    public Image img_Button;

    private bool isActive;
    [SerializeField] Button btn;
    [SerializeField] private MainElecBox mainlogic;
    public AudioSource switchfin_sound;


    public void btnturntool()
    {
        if (isActive == false)
        {
            img_Button.sprite = on_Tool;
            switchfin_sound.Play();
            btn.interactable = false;
            mainlogic.switchtool(1);
            isActive = true;
        }
    }
}
