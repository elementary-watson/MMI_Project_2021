using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestConnect : MonoBehaviourPunCallbacks
{
    public string _nickName = "Der Amir ";
    // Start is called before the first frame update
    void Start()
    {
        print("Connecting to server...");
        //Hier wirde der Nickname des Spielers gesetzt und zum Server geschickt.
        //PhotonNetwork.NickName = MasterManager.GameSettings.NickName;
        PhotonNetwork.NickName = (_nickName + Random.Range(101,200));
        //Versionierung erlaubt nur Verbindung mit den selben Buildversion
        //PhotonNetwork.GameVersion = MasterManager.GameSettings.GameVersion;
        PhotonNetwork.GameVersion = "0.0.0";
        //Hier verbinden wir uns mit dem Server dabei kann man mit einstellungen die verbindungen einstellen.
        PhotonNetwork.ConnectUsingSettings();
        //PhotonNetwork.ConnectToMaster();

    }
    //Methode wird ausgeführt wenn der Client zum server verbunden ist und matchmaking möglich ist.
    public override void OnConnectedToMaster()
    {
        print("Connection to Server successful!");
        //hier holen wir den nicknamen nicht lokal sondern vom server ab
        print(PhotonNetwork.LocalPlayer.NickName);
        base.OnConnectedToMaster();
    }


    public override void OnDisconnected(DisconnectCause cause)
    {
        print("Disconnected from server. Reason: " + cause.ToString());
        base.OnDisconnected(cause);
    }
    // Update is called once per frame
    void Update()
    {

    }
}