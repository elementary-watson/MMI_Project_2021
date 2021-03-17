﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainEnergyNumberScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject EnergyNumber_Panel;

    int currentPosition;
    List<int> randomNumberList;
    List<int> randomNumberforTaskList = new List<int> {1, 2, 3, 4, 5, 6};
    [SerializeField] SingleEnergyNumberScript[] s_energynumber = new SingleEnergyNumberScript[0];
    [SerializeField] SingleHandScript[] s_handscript = new SingleHandScript[0];
    [SerializeField] private TextMeshProUGUI[] randomNumberText = new TextMeshProUGUI[0];

    public TextMeshProUGUI infoBoxText;
    private int fixedcounter = 6;
    private int count = 0;

    public AudioSource taskfin_sound;
    public GameObject win_message;

    void Start()
    {
        RandomNumber();
        setup();
        currentPosition = 0;
    }

    private void setup()
    {
        for (int i = 0; i < 6; i++)
        {
            int number = randomNumberList[i];
            s_energynumber[i].setup(number);
        }
    }

    private void RandomNumber()
    {
        List<int> RandomNumberList = new List<int> { 1, 2, 3, 4, 5, 6};

        var count = RandomNumberList.Count;
        var last = count - 1;
        for (var i = 0; i < last; i++)
        {
            var r = UnityEngine.Random.Range(i, count);
            var tmp = RandomNumberList[i];
            RandomNumberList[i] = RandomNumberList[r];
            RandomNumberList[r] = tmp;
        }
        this.randomNumberList = RandomNumberList;
        setText();
    }

    private void setText()
    {
        for (int i = 0; i < 6; i++)
        {
            randomNumberText[i].text = "" + randomNumberList[i];
        }
        infoBoxText.text = "Klicke alle Zahlen von 1 bis 6 an";
    }

    public bool checkPositionNumber(int number)
    {
        if (randomNumberforTaskList[currentPosition] == number)
        {
            currentPosition += 1;
            return true;
        }
        return false;
    }

    public void checkCounterNumber(int cp)
    {
        count = count + cp;
        if (count == fixedcounter)
        {
            infoBoxText.text = "Drücke und Halte die Hand";
            s_handscript[0].setInteractable(true);
        }
    }

    public void checkCounter()
    {
        taskfin_sound.Play();
        win_message.SetActive(true);
        //_network.incrementTaskprogress(10);
        Invoke("taskfinished", 3);
    }

    public void taskfinished()
    {
        EnergyNumber_Panel.SetActive(false);
    }
}
