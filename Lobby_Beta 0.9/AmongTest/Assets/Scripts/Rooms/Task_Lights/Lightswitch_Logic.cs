using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightswitch_Logic : MonoBehaviour
{
    private int switchCount = 6;
    public GameObject winText;
    private int onCount = 0;
    [SerializeField] GameObject LightsPanel;
    public Network progress_reference;
    public Single_Switch[] switches = new Single_Switch[6];
    private void Start()
    {
        collectSwitches();
    }
    public void collectSwitches()
    {
        //true switch is turned on
        switches[0].setup_Task(false);//<- erstes immer false damit keine Fehler passieren

        for (int i = 1; i < 5; i++)
        {
            if (Random.value >= 0.5)
            {
                switches[i].setup_Task(true);
                onCount += 1;
            }
            else
            {
                switches[i].setup_Task(false);
            }
        }
    }
    public void SwitchChange(int points)//wenn button an oder aus gemacht wird diese methode gerufen
    {
        onCount = onCount + points;
        if (onCount == switchCount)
        {
            winText.SetActive(true);
            Invoke("taskFinished", 1);
            progress_reference.incrementTaskprogress(10);
        }
        else
        {
            winText.SetActive(false);
        }
    }

    private void taskFinished()
    {
        for(int i = 0; i<5; i++)
        {
            //switches[0].interac
        }
        LightsPanel.SetActive(false);
    }
}
