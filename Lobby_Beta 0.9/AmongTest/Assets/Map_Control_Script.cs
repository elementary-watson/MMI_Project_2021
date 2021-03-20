using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Map_Control_Script : MonoBehaviour
{
    private int player_int;
    public TextMeshProUGUI roomname;
    public GameObject BigMap;
    public GameObject MiniMap;

    public Image[] img_miniMapTargetRoom = new Image[0];
    public Image[] img_miniMapPlayerPos = new Image[0];

    public Image[] img_bigMapTargetRoom = new Image[0];
    public Image[] img_bigMapPlayerPos = new Image[0];
    bool isActiveBigmap;
    bool isStart;


    private void Start()
    {
        isStart = true;
        isActiveBigmap = false;
        //img_miniMapTargetRoom[0].enabled = false;
        setup();
    }

    public void SetRoomName(string roomtitle, int playerpos, string currentTask)
    {
        player_int = playerpos;
        roomname.text = roomtitle;
        print("EnterRoomName");
        if (!isActiveBigmap) 
        {
            setTargetRoom(currentTask);

            print("minimap set called");
            if (playerpos == 10)
            {
                setup();
            }
            else
            {                
                img_miniMapPlayerPos[playerpos].enabled = true;
            }
        }
        if (isActiveBigmap)//bigmap
        {
            print("bigmap set called");
            if (playerpos == 10)            
                setup();
            else
            {
                img_bigMapPlayerPos[player_int].enabled = true;
                print("Img big map was enabled");
            }
        }
    }
    public void setTargetRoom(string currenTask)
    {
        switch (currenTask)
        {
            case "Tag_NumberRadio":
                img_miniMapTargetRoom[0].enabled = true;
                img_bigMapTargetRoom[0].enabled = true;
                break;
            case "Tag_Game":
                img_miniMapTargetRoom[0].enabled = true;
                img_bigMapTargetRoom[0].enabled = true;
                break;
            case "Tag_WaterDispenser":
                img_miniMapTargetRoom[0].enabled = true;
                img_bigMapTargetRoom[0].enabled = true;
                break;
            case "Tag_NumberBox":
                img_miniMapTargetRoom[1].enabled = true;
                img_bigMapTargetRoom[1].enabled = true;
                break;
            case "Tag_ElectricBox":
                img_miniMapTargetRoom[1].enabled = true;
                img_bigMapTargetRoom[1].enabled = true;
                break;
            case "Tag_ClickCabinet":
                img_miniMapTargetRoom[1].enabled = true;
                img_bigMapTargetRoom[1].enabled = true;
                break;
            case "Tag_Fillgauge":
                img_miniMapTargetRoom[2].enabled = true;
                img_bigMapTargetRoom[2].enabled = true;
                break;
            case "Tag_EnergyNumber":
                img_miniMapTargetRoom[2].enabled = true;
                img_bigMapTargetRoom[2].enabled = true;
                break;
            case "Tag_LeverEnergy":
                img_miniMapTargetRoom[2].enabled = true;
                img_bigMapTargetRoom[2].enabled = true;
                break;
            case "Tag_ClickMediKit":
                img_miniMapTargetRoom[3].enabled = true;
                img_bigMapTargetRoom[3].enabled = true;
                break;
            case "Tag_Sink":
                img_miniMapTargetRoom[3].enabled = true;
                img_bigMapTargetRoom[3].enabled = true;
                break;
            case "Tag_Tablet":
                img_miniMapTargetRoom[3].enabled = true;
                img_bigMapTargetRoom[3].enabled = true;
                break;
            case "Tag_ComputerLabor":
                img_miniMapTargetRoom[4].enabled = true;
                img_bigMapTargetRoom[4].enabled = true;
                break;
            case "Tag_LaborSingleTube":
                img_miniMapTargetRoom[4].enabled = true;
                img_bigMapTargetRoom[4].enabled = true;
                break;
            case "Tag_ClickLabor":
                img_miniMapTargetRoom[4].enabled = true;
                img_bigMapTargetRoom[4].enabled = true;
                break;
            default:
                img_miniMapTargetRoom[5].enabled = true;
                img_bigMapTargetRoom[5].enabled = true;
                break;
        }
    }
    public void btn_bigmapClick() 
    {
        print("Close big mapp");
        if (isActiveBigmap)
        {
            print("Clean up big map");
            isActiveBigmap = false;
            MiniMap.SetActive(true);
            BigMap.SetActive(false);
            setup();
            print(player_int);
            img_miniMapPlayerPos[player_int].enabled = true;
        }
    }
    public void btn_minimapClick()
    {
        if (!isActiveBigmap) // map geht aus 
        {
            isActiveBigmap = true;
            BigMap.SetActive(true);
            setup();
            print(player_int);
            img_bigMapPlayerPos[player_int].enabled = true;
            MiniMap.SetActive(false);
        }
    }
    public void resetTargetImages()
    {
        for (int i = 0; i < 6; i++)
        {
            img_bigMapTargetRoom[i].enabled = false;
            img_miniMapTargetRoom[i].enabled = false;
        }
    }
    private void setup()
    {
        for (int i = 0; i < 10; i++)
        {
            if (!isStart)
            {
                if (!isActiveBigmap)
                    img_miniMapPlayerPos[i].enabled = false;
                if (isActiveBigmap)
                    img_bigMapPlayerPos[i].enabled = false;
            }
            else
            {
                isStart = false;
            }
        }
    }
}
