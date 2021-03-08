using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainClickMediKit : MonoBehaviour
{
    private int fixedcounter = 7;
    private int count = 0;

    public GameObject winText;
    public Network _network;
    [SerializeField] GameObject MediKitPanel;

    public void switchtool(int cp)
    {
        count = count + cp;
        if (count == fixedcounter)
        {
            winText.SetActive(true);
            Invoke("taskfinished", 1);
            //_network.incrementTaskprogress(10);
        }
    }


    private void taskfinished()
    {
        MediKitPanel.SetActive(false);
    }
}
