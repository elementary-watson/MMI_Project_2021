using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SingleChangeNumber : MonoBehaviour
{
    [SerializeField] Button btn;
    [SerializeField] private MainRadioNumber mainobject;

    public void btn_switchup()
    {
        if (btn.tag == "UpMax100")
        {
            mainobject.setradionumber(10, 0);
        }

        if (btn.tag == "DownMin100")
        {
            mainobject.setradionumber(-10, 0);
        }

        if (btn.tag == "UpMax50")
        {
            mainobject.setradionumber(5, 1);
        }

        if (btn.tag == "DownMin50")
        {
            mainobject.setradionumber(-5, 1);
        }

        if (btn.tag == "UpMax10")
        {
            mainobject.setradionumber(1, 2);
        }

        if (btn.tag == "DownMin10")
        {
            mainobject.setradionumber(-1, 2);
        }

    }
}
