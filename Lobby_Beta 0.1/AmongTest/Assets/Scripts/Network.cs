using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Network : MonoBehaviourPunCallbacks
{
    public Text statusText;
    public CameraFollow playerCamera;
    public GameObject Lobby_Room_Name;

    public void CreateLobby()
    {
        statusText.text = "Connecting";
        PhotonNetwork.NickName = "Player" + Random.Range(0, 100);
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        statusText.text = "Joining room";
        PhotonNetwork.JoinOrCreateRoom(Lobby_Room_Name.GetComponent<Text>().text, new RoomOptions() { MaxPlayers = 10 }, null);
    }

    public override void OnJoinedRoom()
    {
        statusText.text = "Connected to Lobby: " + Lobby_Room_Name.GetComponent<Text>().text;
        playerCamera.target = PhotonNetwork.Instantiate("Player", 
            new Vector3(
                Random.Range(-4, 4), 
                Random.Range(-4, 4), 
                0), Quaternion.identity).transform;
    }
}
