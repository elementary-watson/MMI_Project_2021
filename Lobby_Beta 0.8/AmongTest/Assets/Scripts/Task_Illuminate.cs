using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Task_Illuminate : Interactable
{
    public Sprite passive_state;
    public Sprite active_state;
    public GameObject task;
    private SpriteRenderer sr;
    private bool isOpen;

    public override void Interact()
    {
        if (isOpen) { 
            sr.sprite = active_state;
            task.SetActive(true);
            RectTransform rt = (RectTransform)task.transform;
            float xValue = (float)(Screen.width * 0.5 - rt.rect.width * 0.5);
            float yValue = (float)(Screen.height * 0.5 + rt.rect.width * 0.5);
            task.transform.position = new Vector2(xValue, yValue);

        }
        else
            sr.sprite = passive_state;
        isOpen = !isOpen;
    }

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = active_state;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
