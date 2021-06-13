using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainEnergyNumberScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject EnergyNumber_Panel;

    int currentPosition;
    List<int> randomNumberList;
    List<int> randomNumberforTaskList;
    [SerializeField] SingleEnergyNumberScript[] s_energynumber = new SingleEnergyNumberScript[0];
    [SerializeField] SingleHandScript[] s_handscript = new SingleHandScript[0];
    [SerializeField] private TextMeshProUGUI[] randomNumberText = new TextMeshProUGUI[0];
    [SerializeField] Network _network;
    public TextMeshProUGUI infoBoxText;
    private int fixedcounter = 6;
    private int count = 0;

    public AudioSource taskfin_sound;
    public GameObject win_message;

    void Start()
    {
        setup();
    }

    public void setup()
    {
        fixedcounter = 6;
        count = 0;
        currentPosition = 0;
        win_message.SetActive(false);

        for (int i = 0; i < 6; i++)
        {
            s_energynumber[i].setup2();
        }
        s_handscript[0].setup();

        randomNumberforTaskList = new List<int> { 1, 2, 3, 4, 5, 6 };
        setup2();
    }

    private void setup2()
    {
        RandomNumber();
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
        infoBoxText.text = "Click all numbers from 1 to 6";
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
            infoBoxText.text = "Squeeze and hold the hand symbol";
            s_handscript[0].setInteractable(true);
        }
    }

    public void checkCounter()
    {
        taskfin_sound.Play();
        win_message.SetActive(true);
        _network.incrementTaskprogress();
        Invoke("taskfinished", 3);
    }

    public void taskfinished()
    {
        setup();
        EnergyNumber_Panel.SetActive(false);
    }
}

