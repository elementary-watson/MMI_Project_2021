using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial_Scrip : MonoBehaviour
{
    [SerializeField] private Image[] img_tutorial = new Image[0];
    int counter;

    void Start()
    {
        counter = 0;
        
    }

    public void btn_next()
    {
        if (counter == 6) { }

        else
        { 
            img_tutorial[counter].enabled = false;
            counter += 1;
            img_tutorial[counter].enabled = true;
        }
    }

    public void btn_previous()
    {
        if(counter==0)
        { 
            
        }
        else
        { 
            img_tutorial[counter].enabled = false;
            counter -= 1;
            img_tutorial[counter].enabled = true;
        }
    }

}
