using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SingleHandScript : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    bool pointerDown;
    bool isInteractable;
    [SerializeField] MainEnergyNumberScript mainobject;
    float start1 = 0.07f;
    float end1 = 1f;
    public Image img_fillGauge1;
    [SerializeField] float current1;
    [SerializeField] float maximum1;

    float start2 = 0f;
    float end2 = 0.93f;
    public Image img_fillGauge2;
    [SerializeField] float current2;

    public UnityEvent onHoldClick;

    void Start()
    {
        pointerDown = false;
        isInteractable = false;
        img_fillGauge1.fillAmount = start1;
        current1 = start1;
        maximum1 = end1;

        img_fillGauge2.fillAmount = start2;
        current2 = start2;
    }

    public void setup()
    {
        isInteractable = false;
        pointerDown = false;
        start1 = 0.07f;
        end1 = 1f;
        start2 = 0f;
        end2 = 0.93f;
        img_fillGauge1.fillAmount = start1;
        current1 = start1;
        maximum1 = end1;
        img_fillGauge2.fillAmount = start2;
        current2 = start2;
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
        if (isInteractable)
        {
            current1 = start1;
            img_fillGauge1.fillAmount = current1;// / maximum;

            current2 = start2;
            img_fillGauge2.fillAmount = current2;
        }
    }

    public void disableBtn()
    {
        if (!isInteractable)
        {
            isInteractable = false;
        }
    }

    public void setInteractable (bool isInteractable)
    {
        this.isInteractable = isInteractable;
    }

    void Update()
    {
        if (isInteractable)
        {
            if (pointerDown)
            {
                current1 += (Time.deltaTime) / 7;
                current2 += (Time.deltaTime) / 7;
                if (current1 >= maximum1)
                {
                    if (onHoldClick != null)
                    {
                        onHoldClick.Invoke();
                    }
                }
                img_fillGauge1.fillAmount = current1; // / maximum;
                img_fillGauge2.fillAmount = current2;

                if (current1 > 1f)
                {
                    isInteractable = false; disableBtn(); mainobject.checkCounter();
                }
            }
        }
    }
}
