using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SingleLeverScript : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    [SerializeField] MainLeverScript mainobject;
    public Image img_fillGauge;

    int randomValue;
    [SerializeField] Image[] img_randomVisuals = new Image[0];

    bool isInteractable;
    bool pointerDown;
    float start = 0.15f;
    float end = 0.90f;
    [SerializeField] float current;
    [SerializeField] float maximum;
    private float barspeed;
    public AudioSource switchfin_sound;

    public UnityEvent onHoldClick;

    void Start()
    {
        randomValue = Random.Range(0, 2);
        print("Random " + randomValue);
        if (randomValue == 0) img_randomVisuals[0].enabled = true;
        if (randomValue == 1) img_randomVisuals[1].enabled = true;

        isInteractable = true;
        img_fillGauge.fillAmount = start;
        current = start;
        maximum = end;

        if (Random.value > 0.5f) barspeed = 0.75f;
        else barspeed = 2f;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        pointerDown = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (current >= 0.471f && current <= 0.551f && randomValue == 0)
        {
            isInteractable = false; disableBtn(); switchfin_sound.Play(); mainobject.checkCounter();
        }
        else if (current >= 0.744f && current <= 0.824f && randomValue == 1)
        {
            isInteractable = false; disableBtn(); switchfin_sound.Play(); mainobject.checkCounter();
        }
        Reset();
    }

    public void disableBtn()
    {
        if (!isInteractable)
        {
            isInteractable = false;
            print("btncall");
        }
    }

    public void Reset()
    {
        if (isInteractable)
        {
            pointerDown = false;
            current = start;
            img_fillGauge.fillAmount = current;
        }
    }

    void Update()
    {
        if (isInteractable)
        {
            if (pointerDown)
            {
                current += (Time.deltaTime) / barspeed;
                //print("Current1: " + current);
                if (current >= maximum)
                {
                    print("Current2: " + current);
                    if (onHoldClick != null)
                    {
                        //isInteractable = false;
                        onHoldClick.Invoke();
                    }
                }
                img_fillGauge.fillAmount = current;
            }
        }
    }
}
