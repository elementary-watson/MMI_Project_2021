using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Single_Fillgauge_Task : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] Main_Fillgauge_Task mft_object;
    public Image img_fillGauge;
    [SerializeField] Image[] img_randomVisuals = new Image[0];
    [SerializeField] float current;
    [SerializeField] float maximum;
    bool isInteractable;
    bool pointerDown;
    public UnityEvent onHoldClick;
    float start = 0.15f;
    float end = 0.90f;
    int randomValue;
    public AudioSource switchfin_sound;
    public void OnPointerDown(PointerEventData eventData)
    {
        pointerDown = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (isInteractable) 
        {
            print("pointup");
            print(randomValue);
            print("Current: " + current);
            if (current > 0.30f && current < 0.40f && randomValue == 0)
            {
                isInteractable = false; disableBtn(); switchfin_sound.Play(); mft_object.setCurrent();
            }
            else if (current > 0.55f && current < 0.65f && randomValue == 1)
            {
                isInteractable = false; disableBtn(); switchfin_sound.Play(); mft_object.setCurrent();
            }
            else if (current > 0.80f && current < 0.90f && randomValue == 2)
            {
                isInteractable = false; disableBtn(); switchfin_sound.Play(); mft_object.setCurrent();
            }
        }
        Reset();        
    }
    public void Reset() 
    {
        if (isInteractable)
        {
            pointerDown = false;
            current = start;
            img_fillGauge.fillAmount = current / maximum;
        }
    }
    public void setup()
    {

        pointerDown = false;
        img_randomVisuals[0].enabled = false;
        img_randomVisuals[1].enabled = false;
        img_randomVisuals[2].enabled = false;

        randomValue = Random.Range(0,3);
        print("Random " + randomValue);
        if (randomValue == 0) img_randomVisuals[0].enabled = true;
        if (randomValue == 1) img_randomVisuals[1].enabled = true;
        if (randomValue == 2) img_randomVisuals[2].enabled = true;

        isInteractable = true;
        img_fillGauge.fillAmount = start;
        current = start;
        maximum = end;
    }
    void Start()
    {
        setup();
    }
    void Update()
    {
        if (isInteractable) 
        {
            if (pointerDown)
            {
                current += (Time.deltaTime)/3;
                //print("Current1: " + current);
                if (current >= maximum)
                {
                    print("Current2: " +current);
                    if (onHoldClick != null)
                    {
                        //isInteractable = false;
                        onHoldClick.Invoke(); 
                    }
                }
                img_fillGauge.fillAmount = current / maximum;
            }
        }        
    }
    public void disableBtn()
    {
        if (isInteractable)
        {
            isInteractable = false;
            print("btncall");
        }
    }

}
