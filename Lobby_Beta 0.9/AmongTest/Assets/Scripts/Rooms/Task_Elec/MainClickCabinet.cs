using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainClickCabinet : MonoBehaviour
{
    private int fixedcounter = 6;
    private int count = 0;

    public GameObject winText;
    public Network _network;
    [SerializeField] GameObject CabinetPanel;
    SingleSwitchTool[] tools = new SingleSwitchTool[6];
    
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
        CabinetPanel.SetActive(false);
    }
}
