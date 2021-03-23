using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainComputer : MonoBehaviour
{
    string tenNumbers;
    List<int> randomNumberList;
    List<int> randomNumberforTaskList;
    List<int> tenNumberList;
    int currentPosition;
    private int fixedcounter = 10;
    private int count = 0;

    public AudioSource taskfin_sound;
    public GameObject win_message;

    public TextMeshProUGUI infoBoxText;
    [SerializeField] GameObject ComputerPanel;
    [SerializeField] SingleComputer[] s_numbercomputer = new SingleComputer[0];
    [SerializeField] private TextMeshProUGUI[] randomNumberText = new TextMeshProUGUI[0];
    [SerializeField] Network _network;

    // Start is called before the first frame update
    void Start()
    {
        setup();
    }

    public void setup()
    {
        fixedcounter = 10;
        count = 0;
        currentPosition = 0;
        win_message.SetActive(false);
        for (int i = 0; i < 30; i++)
        {
            s_numbercomputer[i].setup2();    
        }

        tenNumberList = new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        setup2();
    }

    private void setup2()
    {
        RandomNumber();
        for (int i = 0; i < 30; i++)
        {
            int number = randomNumberList[i];
            s_numbercomputer[i].setup(number);
        }
    }

    private void RandomNumber()
    {
        List<int> RandomNumberList = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30 };

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
        RandomNumberForTask();
    }

    private void RandomNumberForTask()
    {
        List<int> randomNumberforTaskList = new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        int tmp = Random.Range(10, 30);

        for (int i = 0; i < 10; i++)
        {
            randomNumberforTaskList[i] = tmp - i;
        }
        this.randomNumberforTaskList = randomNumberforTaskList;
        setText();
    }

    public bool checkPositionNumber(int number)
    {
        if (tenNumberList[currentPosition] == number)
        {
            currentPosition += 1;
            return true;
        }
        return false;
    }

    private void setText()
    {
        for (int i = 0; i < 30; i++)
        {
            randomNumberText[i].text = "" + randomNumberList[i];
        }


        for (int i = 0; i < 10; i++)
        {
            tenNumbers = tenNumbers + " " + randomNumberforTaskList[i];
            tenNumberList[i] = randomNumberforTaskList[i];
        }
        infoBoxText.text = "Klicke alle Zahlen von " + randomNumberforTaskList[0] + " bis " + randomNumberforTaskList[9] + " an";
    }

    public void checkCounter(int cp)
    {
        count = count + cp;
        if (count == fixedcounter)
        {
            taskfin_sound.Play();
            win_message.SetActive(true);
            Invoke("taskfinished", 3);
            //_network.incrementTaskprogress(10);
        }
    }

    private void taskfinished()
    {
        setup();
        _network.incrementTaskprogress();
        ComputerPanel.SetActive(false);
    }
}
