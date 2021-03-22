using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SingleEmptyDispenser : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    bool pointerDown;
    bool isInteractable;
    float start = 1f;
    float end = 0f;
    [SerializeField] MainWaterDispenser mainobject;
    public Image img_fillGauge;
    [SerializeField] float current;
    [SerializeField] float maximum;
    public AudioSource switchfin_sound;

    public UnityEvent onHoldClick;

    void Start()
    {
        //setup();
    }
    public void setup()
    {
        isInteractable = true;
        img_fillGauge.fillAmount = start;
        current = start;
        maximum = end;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        pointerDown = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Reset();
    }

    public void Reset()
    {
        pointerDown = false;
    }

    public void disableBtn()
    {
        if (!isInteractable)
        {
            isInteractable = false;
        }
    }

    void Update()
    {
        if (isInteractable)
        {
            if (pointerDown)
            {
                current -= (Time.deltaTime) / 6;
                if (current >= maximum)
                {
                    if (onHoldClick != null)
                    {
                        onHoldClick.Invoke();
                    }
                }
                img_fillGauge.fillAmount = current; // / maximum;

                if (current < 0f)
                {
                    isInteractable = false; disableBtn(); mainobject.checkCounter(1);
                }
            }
        }
    }
}
