using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SingleSwitchTool : MonoBehaviour
{
    public Sprite on_Tool;
    public Image img_Button;

    private bool isActive;
    [SerializeField] Button btn;
    [SerializeField] private MainClickCabinet mainobject;

    public void btnturntool()
    {
        if (isActive == false)
        {
            img_Button.sprite = on_Tool;


            btn.interactable = false;
            mainobject.switchtool(1);
            isActive = true;
        }
     }

}
