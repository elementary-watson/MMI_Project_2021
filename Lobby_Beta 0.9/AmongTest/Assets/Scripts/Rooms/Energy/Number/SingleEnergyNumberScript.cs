using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SingleEnergyNumberScript : MonoBehaviour
{
    [SerializeField] MainEnergyNumberScript mainobject;

    int number;
    [SerializeField] private Image img_button;
    [SerializeField] private Button number_button;
    public Sprite green;
    public Sprite red;
    private Sprite white;

    public AudioSource switchfin_sound;


    public void btn_switch()
    {
        bool isCorrect = mainobject.checkPositionNumber(number);

        if (isCorrect == true)
        {
            img_button.sprite = green;
            number_button.interactable = false;
            switchfin_sound.Play();
            mainobject.checkCounterNumber(1);
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

    public void setup2()
    {
        number_button.interactable = true;
        //img_button.sprite = white;
    }
}
