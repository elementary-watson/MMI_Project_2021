using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainClickLabor : MonoBehaviour
{
    [SerializeField] Network _network;
    List<string> randomTestTubeList;
    List<string> randomTestTubeforTaskList;
    List<string> TestTubeList; 
    public TextMeshProUGUI infotext;
    private int fixedcounter = 12;
    private int count = 0;
    int currentPosition;

    public AudioSource taskfin_sound;
    public GameObject win_message;

    [SerializeField] GameObject LaborTubePanel;
    [SerializeField] SingleClickLabor[] s_clicklabor = new SingleClickLabor[0];

    //1 = Blau, 2 = Grün, 3 = Gelb, 4=Pink, 5= Braun 6=Orange

    void Start()
    {
        setup();
        
    }

    public void setup()
    {
        currentPosition = 0;
        fixedcounter = 12;
        count = 0;
        win_message.SetActive(false);
        for (int i = 0; i < 24; i++)
        {
            s_clicklabor[i].setup2();
        }

        TestTubeList = new List<string> { "Blau", "Grün", "Gelb", "Lila", "Braun", "Weiß", "Blau", "Grün", "Gelb", "Lila", "Braun", "Weiß", "Blau", "Grün", "Gelb", "Lila", "Braun", "Weiß", "Blau", "Grün", "Gelb", "Lila", "Braun", "Weiß" };
        setup2();
    }

    private void setup2()
    {
        RandomTestTube();
        for (int i = 0; i < 24; i++)
        {
            string color = TestTubeList[i];
            s_clicklabor[i].setup(color);
        }
    }

    public void RandomTestTube()
    {
        List<string> RandomTestTubeList = new List<string> { "Blau", "Grün", "Gelb", "Lila", "Braun", "Weiß" };

        var count = RandomTestTubeList.Count;
        var last = count - 1;
        for (var i = 0; i < last; i++)
        {
            var r = UnityEngine.Random.Range(i, count);
            var tmp = RandomTestTubeList[i];
            RandomTestTubeList[i] = RandomTestTubeList[r];
            RandomTestTubeList[r] = tmp;
        }

        this.randomTestTubeList = RandomTestTubeList;
        RandomTestTubeForTask();
    }

    public void RandomTestTubeForTask()
    {
        List<string> randomTestTubeforTaskList = new List<string> { "","","","","","","","","","","",""};

        for (int i = 0; i < 4; i++)
        {
            randomTestTubeforTaskList[i] = randomTestTubeList[0];
        }

        for (int j = 4; j < 8; j++)
        {
            randomTestTubeforTaskList[j] = randomTestTubeList[1];
        }

        for (int x = 8; x < 12; x++)
        {
            randomTestTubeforTaskList[x] = randomTestTubeList[2];
        }

        this.randomTestTubeforTaskList = randomTestTubeforTaskList;
        //setText();
        infotext.text = "Wähle zuerst alle Reagenzgläser mit der Farbe '" + randomTestTubeList[0] + "' aus";
    }

    public bool checkPositionNumber(string color)
    {
        if (randomTestTubeforTaskList[currentPosition] == color)
        {
            print(randomTestTubeforTaskList[currentPosition]+ "/" + color);
            currentPosition += 1;
            return true;
        }
        return false;
    }

    public void checkCounter(int cp)
    {
        count = count + cp;
        if (count == fixedcounter)
        {
            taskfin_sound.Play();
            win_message.SetActive(true);
            Invoke("taskfinished", 3);
            _network.incrementTaskprogress();
        }

        if (count == 4)
        {
            infotext.text = "Jetzt alle mit der Farbe '" + randomTestTubeList[1] + "'";
        }

        if (count == 8)
        {
            infotext.text = "Zum Schluss alle mit der Farbe '" + randomTestTubeList[2] + "'";
        }
    }

    private void setText()
    {
        //infotext.text = "Die Farben '" +randomTestTubeList[0] + " - "+ randomTestTubeList[1] + " - " + randomTestTubeList[2] + "' nach der Reihe drücken";
    }

    private void taskfinished()
    {
        setup();
        LaborTubePanel.SetActive(false);
    }
}
