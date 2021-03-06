using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main_Sink_Task:MonoBehaviour
{
    public Network _network;
    public GameObject winText;
    int currentPoints;
    int maxPoints = 12;
    public Single_Sink_Task[] sst = new Single_Sink_Task[0];
    [SerializeField] 
    // Start is called before the first frame update
    void Start()
    {
        currentPoints = 0;
        SetupValves();
    }

    public void ValveSwitch(int points)
    {
        print(points + " was added!");
        currentPoints += points;
        if(maxPoints == currentPoints)
        {
            winText.SetActive(true);
            Invoke("taskFinished", 1);
            _network.incrementTaskprogress(10);
        }
        else
        {
            winText.SetActive(false);
        }
    }
    public void SetupValves()
    {
        sst[0].setup(1);
        for (int i = 1; i < maxPoints; i++)
        {
            float temp = Random.value;
            print("Value "+ i + ": " + temp);
            if (temp >= 0.75f)
                currentPoints += 1;
            sst[i].setup(temp);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
