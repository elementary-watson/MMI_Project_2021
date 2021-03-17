using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SingleTubeScript : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    bool pointerDown;
    bool isInteractable;
    float start = 0.04f;
    float end = 0.957f;
    [SerializeField] MainSingleTubeScript mainobject;
    public Image img_fillGauge;
    [SerializeField] float current;
    [SerializeField] float maximum;

    public UnityEvent onHoldClick;

    void Start()
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
        if (current > 0.923f && current < 0.957f)
        {
            isInteractable = false; disableBtn(); mainobject.checkCounter();
        }
        Reset();
    }

    public void Reset()
    {
        if (isInteractable)
        {
            pointerDown = false;
            current = start;
            img_fillGauge.fillAmount = current;// / maximum;
        }
    }

    public void disableBtn()
    {
        if (!isInteractable)
        {
            isInteractable = false;
            print("btncall");
        }
    }

    void Update()
    {
        if (isInteractable)
        {
            if (pointerDown)
            {
                current += (Time.deltaTime) / (9/2);
                if (current >= maximum)
                {
                    if (onHoldClick != null)
                    {
                        onHoldClick.Invoke();
                    }
                }
                img_fillGauge.fillAmount = current; // / maximum;
            }
        }
    }
}
