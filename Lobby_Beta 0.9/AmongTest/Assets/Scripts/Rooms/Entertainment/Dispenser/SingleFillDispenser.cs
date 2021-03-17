using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SingleFillDispenser : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    bool pointerDown;
    bool isInteractable;
    float start = 0.0f;
    float end = 1f;
    [SerializeField] MainWaterDispenser mainobject;
    public Image img_fillGauge;
    [SerializeField] float current;
    [SerializeField] float maximum;

    public UnityEvent onHoldClick;

    void Start()
    {
        pointerDown = false;
        isInteractable = false;
        //img_fillGauge.fillAmount = start;
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

    public void setInteractable(bool isInteractable)
    {
        this.isInteractable = isInteractable;
    }

    void Update()
    {
        if (isInteractable)
        {
            if (pointerDown)
            {
                current += (Time.deltaTime) / 6;
                if (current >= maximum)
                {
                    if (onHoldClick != null)
                    {
                        onHoldClick.Invoke();
                    }
                }
                img_fillGauge.fillAmount = current; // / maximum;

                if (current > 1f)
                {
                    isInteractable = false; disableBtn(); mainobject.checkCounter(1);
                }
            }
        }
    }
}
