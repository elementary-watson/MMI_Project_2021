using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] private TMP_Text[] playerNameTexts = new TMP_Text[4];
    [SerializeField] private TMP_Text[] playerReadyTexts = new TMP_Text[4];
    [SerializeField] private Image[] playerImageContainer = new Image[4];
    //[SerializeField]
    //Network _network;
    /*void OnApplicationQuit()
    {
        Network _network = new Network();
        _network.printMe();
        print("DEBUG: Player left Application EXTRA");
        PhotonNetwork.SendAllOutgoingCommands(); // send it right now
        this.SendQuitEvent();
    }
    void SendQuitEvent()
    {
        // send event, add your code here
        print("DEBUG: We entered SendQuit");
    }*/
}
