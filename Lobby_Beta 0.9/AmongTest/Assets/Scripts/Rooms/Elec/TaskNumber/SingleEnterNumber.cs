using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SingleEnterNumber : MonoBehaviour
{
    [SerializeField] Button btn;
    [SerializeField] private MainNumberBox mainobject;


    public void btn_switchup()
    {
        if (btn.tag == "Max1000")
        {
            mainobject.setboxnumber(100, 0);
        }

        if (btn.tag == "Min1000")
        {
            mainobject.setboxnumber(-100, 0);
        }

        if (btn.tag == "Max100")
        {
            mainobject.setboxnumber(10, 1);
        }

        if (btn.tag == "Min100")
        {
            mainobject.setboxnumber(-10, 1);
        }

        if (btn.tag == "Max10")
        {
            mainobject.setboxnumber(1, 2);
        }

        if (btn.tag == "Min10")
        {
            mainobject.setboxnumber(-1, 2);
        }

    }
}
