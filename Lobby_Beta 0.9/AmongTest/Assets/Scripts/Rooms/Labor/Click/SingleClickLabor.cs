using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SingleClickLabor : MonoBehaviour
{
    string color;

    public Sprite on_TestTube;
    public Image img_button;
    public AudioSource switchfin_sound;

    [SerializeField] Button btn;
    [SerializeField] private MainClickLabor mainobject;


    public void btnturntool()
    {
        print(color);
        bool isCorrect = mainobject.checkPositionNumber(color);

        if (isCorrect == true)
        {
            img_button.sprite = on_TestTube;
            switchfin_sound.Play();
            btn.interactable = false;
            mainobject.checkCounter(1);
        }
    }

    public void setup(string color)
    {
        this.color = color;
    }
}
