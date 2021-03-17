using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SingleSkriptGame : MonoBehaviour
{
    string letter;
    [SerializeField] private MainSkriptGame mainobject;
    [SerializeField] private GameObject img_thumb;
    public Sprite thumbs_up;
    public Sprite thumbs_down;
    public GameObject thisObject;
    public AudioSource switchfin_sound;
    public void btn_switch()
    {
        bool isCorrect = mainobject.checkPositionLetter(letter);
        if (isCorrect == true)
        {
            switchfin_sound.Play();
            img_thumb.SetActive(true);
            Image[] temp = img_thumb.GetComponentsInChildren<Image>();
            temp[1].sprite = thumbs_up;
            thisObject.GetComponent<Button>().interactable = false;
            mainobject.isLast();
        }
        else
        {
            img_thumb.SetActive(true);
            Image[] temp = img_thumb.GetComponentsInChildren<Image>();
            temp[1].sprite = thumbs_down;
            thisObject.GetComponent<Button>().interactable = false;
            Invoke("setBack",1);
        }
    }
    public void setBack()
    {
        img_thumb.SetActive(false);

        thisObject.GetComponent<Button>().interactable = true;
    }
    public void setup(string letter)
    {
        this.letter = letter;
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

}
