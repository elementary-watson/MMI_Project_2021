using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class single_fillgauge_task : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Button btn_fill;
    public Image img_fillGauge;
    [SerializeField] float current;
    [SerializeField] float maximum;
    bool pointerUp;
    bool pointerDown;
    public UnityEvent onHoldClick;

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
        current = 0.13f;
        img_fillGauge.fillAmount = current / maximum;
    }
    // Start is called before the first frame update
    void Start()
    {
        img_fillGauge.fillAmount = 0.13f;
    }

    // Update is called once per frame
    void Update()
    {
        if (pointerDown)
        {
            current += Time.deltaTime;
            if(current >= maximum)
            {
                if (onHoldClick != null)
                    onHoldClick.Invoke();
                Reset();
            }
            img_fillGauge.fillAmount = current / maximum;
        }
    }
    public void btn_startFillgauge()
    {

        print("contact");
        if (Input.GetMouseButton(0))
        {
            print("inner if");
            if (maximum < 0.89f)
            {
                if (current == 0.5f) { btn_fill.enabled = false; }
                current += (.1f * Time.deltaTime);
                float fillamount = (float)current / (float)maximum;
                img_fillGauge.fillAmount = fillamount;
            }
        }

    }

}
