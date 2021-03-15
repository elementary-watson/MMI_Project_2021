using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main_Fillgauge_Task : MonoBehaviour
{
    int maxTasks = 4;
    int current = 0;
    [SerializeField] Single_Fillgauge_Task[] sft_object = new Single_Fillgauge_Task[0];
    // Start is called before the first frame update
    void Start()
    {
        current = 0;
    }

    public void setCurrent()
    {
        current = current + 1;
        if(current == maxTasks)
        {

        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
