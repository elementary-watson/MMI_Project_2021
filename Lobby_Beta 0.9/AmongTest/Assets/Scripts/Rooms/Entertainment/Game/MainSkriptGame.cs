using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainSkriptGame : MonoBehaviour
{
    string fourLetters;
    List<string> randomLetterList;
    List<string> randomLetterforTaskList;
    List<string> fourLetterList;

    public TextMeshProUGUI infoBoxText;
    [SerializeField] GameObject GamePanel;
    public GameObject winText;

    [SerializeField] private TextMeshProUGUI[] randomLetterText = new TextMeshProUGUI[0];
    [SerializeField] SingleSkriptGame[] s_skriptgame = new SingleSkriptGame[0];
    [SerializeField] Button[] button = new Button[0];
    [SerializeField] mid_button_script[] midButtons = new mid_button_script[0];
    int currentPosition;
    int currenPositionMid;
    List<int> switchorderMid;
    //part three
    [SerializeField] Image[] img_playerImages = new Image[0];
    [SerializeField] Single_ShootPlayer_Script[] playerButtonsObjects = new Single_ShootPlayer_Script[0];
    [SerializeField] GameObject [] img_spaceship = new GameObject[0];
    [SerializeField] GameObject[] img_laserbeam = new GameObject[0];
    [SerializeField] GameObject[] img_explosion = new GameObject[0];
    [SerializeField] GameObject minigameContainer;
    List<string> RandomColorList = new List<string>();
    public AudioSource taskfin_sound;
    public AudioSource switchfin_sound;
    public AudioSource shoot_sound;
    int targetPosition;
    [SerializeField] Network _network;
    // Start is called before the first frame update
    void Start()
    {

        setup();

    }

    private void setup()
    {
        fourLetters = "";
        infoBoxText.text = "";
        for (int i = 0; i < 12; i++) 
        { 
            s_skriptgame[i].setBack();
        }

        winText.SetActive(false);
        minigameContainer.SetActive(false);
        RandomLetter();
        for (int i = 0; i < 12; i++)
        {
            string letter = randomLetterList[i];
            s_skriptgame[i].setup(letter);
        }
        for (int i = 0; i < 3; i++)
        { midButtons[i].Reset(); }
        currentPosition = 0;
        minigameContainer.SetActive(false);
        
        for (int i = 0; i < 10; i++)
        {
            img_explosion[i].SetActive(false);
            img_laserbeam[i].SetActive(false);
            img_spaceship[i].SetActive(false);
        }
    }    
    private void setupPartTwo()
    {
        infoBoxText.text = "Die mittleren gruenen Knoepfe druecken";
        currenPositionMid = 0;
        List<int> nums= new List<int> {0,1,2};
        var count = nums.Count;
        var last = count - 1;
        for (var i = 0; i < last; ++i)
        {
            var r = UnityEngine.Random.Range(i, count);
            var tmp = nums[i];
            nums[i] = nums[r];
            nums[r] = tmp;
        }
        //currentMidPointer
        switchorderMid = nums;
        for (int i = 0; i < 3; i++)
        {
            midButtons[i].setup(switchorderMid[i]);
            print("Button " + i + " getPos: " + switchorderMid[i]);
        }
    }
    public void setupPartThree() 
    {
        infoBoxText.text = "Einen der erschienenen Gegner anklicken!";
        minigameContainer.SetActive(true);
        List<string> RandomColorList = new List<string> { "Black", "Blue", "Brown", "Pink", "Green", "Orange", "Purple", "Red", "White", "Yellow" };
        var count = RandomColorList.Count;
        var last = count - 1;
        for (var i = 0; i < last; ++i)
        {
            var r = UnityEngine.Random.Range(i, count);
            var tmp = RandomColorList[i];
            RandomColorList[i] = RandomColorList[r];
            RandomColorList[r] = tmp;
        }
        this.RandomColorList = RandomColorList;

        for (int i = 0; i< 10;i++)
        {
            string filename = "Player Color/" + RandomColorList[i] + "_Char";
            Sprite sp = Resources.Load<Sprite>(filename);
            img_playerImages[i].sprite = sp;
            playerButtonsObjects[i].setup(i,RandomColorList[i]);
        }
    }
    public void sendTarget(int targetPosition)
    {
        this.targetPosition = targetPosition;
        loadSpaceship();
        Invoke("loadLasebeam", 1);
        Invoke("loadExplosion", 1.5f);
        Invoke("taskfinished", 3);

    }
    public void loadSpaceship()
    {        
        img_spaceship[targetPosition].SetActive(true);
    }
    public void loadLasebeam()
    {
        shoot_sound.Play();
        img_laserbeam[targetPosition].SetActive(true);
    }    
    public void loadExplosion()
    {
        var tempColor = img_playerImages[targetPosition].color;
        tempColor.a = 1f;
        img_playerImages[targetPosition].color = tempColor;
        img_explosion[targetPosition].SetActive(true);
    }
    public void nextMidButton()//nächster grüner button
    {
        if(currenPositionMid == 2) 
        { 
            setupPartThree();
        }
        else 
        { 
            print("Current Index: " + currenPositionMid + " Pressed Button" + switchorderMid[currenPositionMid]);
            currenPositionMid += 1;
            int tmp = switchorderMid[currenPositionMid];
            print("NextIndex: " + currenPositionMid + " Next Button in Index: " + tmp);
            midButtons[tmp].activateMidPart(true);
        }
    }
    public void isLast() //wenn alle buchstaben gedrück wurden nächste task
    {
        if (currentPosition == 4)
        {
            for (int i = 0; i < 12; i++)
                button[i].interactable = false;
            currentPosition = 4; //um diese schleife zu überspringen
            setupPartTwo();
        }
    }

    #region first task
    private void RandomLetter()
    {
        List<string> RandomLetterList = new List<string> { "Q", "W", "E", "R", "A", "S", "D", "F", "Y", "X", "C", "V" };

        var count = RandomLetterList.Count;
        var last = count - 1;
        for (var i = 0; i < last; ++i)
        {
            var r = UnityEngine.Random.Range(i, count);
            var tmp = RandomLetterList[i];
            RandomLetterList[i] = RandomLetterList[r];
            RandomLetterList[r] = tmp;
        }
        this.randomLetterList = RandomLetterList;

        RandomLetterForTask();
    }

    private void RandomLetterForTask()
    {
        List<String> randomLetterforTaskList = new List<String>(randomLetterList);

        var count = randomLetterforTaskList.Count;
        var last = count - 1;
        for (var i = 0; i < last; ++i)
        {
            var r = UnityEngine.Random.Range(i, count);
            var tmp = randomLetterforTaskList[i];
            randomLetterforTaskList[i] = randomLetterforTaskList[r];
            randomLetterforTaskList[r] = tmp;
        }
        
        this.randomLetterforTaskList = randomLetterforTaskList;
        setText();
    }
     

    public bool checkPositionLetter(string letter)
    {
        if(fourLetterList[currentPosition] == letter)
        {
            currentPosition += 1;            
            return true;
        }
        return false;
    }

    private void setText()
    {
        for(int i=0; i < 12; i++)
        {
            randomLetterText[i].text = randomLetterList[i];
        }
        fourLetterList = new List<string> {"","","","" };
        for (int i = 0; i < 4; i++)
        {
            fourLetters = fourLetters + " " + randomLetterforTaskList[i];
            fourLetterList[i] = randomLetterforTaskList[i];
        }
        infoBoxText.text = "Druecke" + fourLetters ;
    }

    private void taskfinished()
    {
        winText.SetActive(true);
        _network.incrementTaskprogress();
        taskfin_sound.Play();
        setup();
        GamePanel.SetActive(false);
    }
    #endregion
}
