using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SingleComputer : MonoBehaviour
{
    int number;
    [SerializeField] private MainComputer mainobject;
    [SerializeField] private Image img_button;
    public Sprite green;
    public Sprite red;
    private Sprite white;
    public GameObject thisObject;
    public AudioSource switchfin_sound;


    public void btn_switch()
    {
        bool isCorrect = mainobject.checkPositionNumber(number);
        if (isCorrect == true)
        {
            img_button.sprite = green;
            thisObject.GetComponent<Button>().interactable = false;
            switchfin_sound.Play();
            mainobject.checkCounter(1);
        }
        else
        {
            img_button.sprite = red;
            Invoke("setBack", 1);
        }
    }

    public void setBack()
    {
        img_button.sprite = white;
    }

    public void setup(int number)
    {
        this.number = number;
        img_button.sprite = white;
    }
}
