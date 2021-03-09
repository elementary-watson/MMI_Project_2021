using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MainElecBox : MonoBehaviour
{
    private int fixedcounter = 9;
    private int count = 0;

    [SerializeField] GameObject ElecBoxPanel;
    public TextMeshProUGUI infoBoxText;
    public Image img_Light;


    void Start()
    {
        img_Light.enabled = false;
        infoBoxText.text = "Bringe die Schalter in die richtige Position";
    }
    
    public void switchtool(int cp)
    {
        count = count + cp;
        if (count == fixedcounter)
        {
            img_Light.enabled = true;
            infoBoxText.text = "Aufgabe erledigt";
            Invoke("taskfinished", 2);
            //_network.incrementTaskprogress(10);
        }
    }


    private void taskfinished()
    {
        ElecBoxPanel.SetActive(false);
    }
}
