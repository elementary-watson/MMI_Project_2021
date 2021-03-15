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
        setImageFalse();
    }

    public void SetRoomName(string roomtitle, int playerpos)
    {
        player_int = playerpos;
        roomname.text = roomtitle;
        print("EnterRoomName");
        if (!isActiveBigmap) 
        {
            print("minimap set called");
            if (playerpos == 10)
            {
                setImageFalse();
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
                setImageFalse();
            else
            {
                img_bigMapPlayerPos[player_int].enabled = true;
                print("Img big map was enabled");
            }
        }
        /*if (i == 5)
        {
            img_miniMapTargetRoom[0].enabled = true;
        }*/

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
            setImageFalse();
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
            setImageFalse();
            print(player_int);
            img_bigMapPlayerPos[player_int].enabled = true;
            MiniMap.SetActive(false);
        }
    }



    private void setImageFalse()
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
