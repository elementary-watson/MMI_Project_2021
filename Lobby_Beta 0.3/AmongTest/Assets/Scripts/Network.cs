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
    public Text statusPlayerNumber;
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
        //print("Current Player in the Lobby: " + PhotonNetwork.CountOfPlayersOnMaster);
    }

    public override void OnJoinedRoom()
    {
        /*byte PlayerNumber = PhotonNetwork.CurrentRoom.PlayerCount;
        int PlayerNumberInt = Convert.ToInt32(PlayerNumber);
        

        print(PlayerNumber);*/
        //print("Current Player in the Room: " + PhotonNetwork.CurrentRoom.PlayerCount);

        statusText.text = "Connected to Lobby: " + Lobby_Room_Name.GetComponent<Text>().text;

        if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
        {
            playerCamera.target = PhotonNetwork.Instantiate("PlayerBlack",
                new Vector3(
                    Random.Range(-4, 4),
                    Random.Range(-4, 4),
                    0), Quaternion.identity).transform;

        }
        else if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            playerCamera.target = PhotonNetwork.Instantiate("PlayerBlue",
                new Vector3(
                    Random.Range(-4, 4),
                    Random.Range(-4, 4),
                    0), Quaternion.identity).transform;
        }
        else if (PhotonNetwork.CurrentRoom.PlayerCount == 3)
        {
            playerCamera.target = PhotonNetwork.Instantiate("PlayerBrown",
                new Vector3(
                    Random.Range(-4, 4),
                    Random.Range(-4, 4),
                    0), Quaternion.identity).transform;
        }
        else if (PhotonNetwork.CurrentRoom.PlayerCount == 4)
        {
            playerCamera.target = PhotonNetwork.Instantiate("PlayerCyan",
                new Vector3(
                    Random.Range(-4, 4),
                    Random.Range(-4, 4),
                    0), Quaternion.identity).transform;
        }
        else if (PhotonNetwork.CurrentRoom.PlayerCount == 5)
        {
            playerCamera.target = PhotonNetwork.Instantiate("PlayerGreen",
                new Vector3(
                    Random.Range(-4, 4),
                    Random.Range(-4, 4),
                    0), Quaternion.identity).transform;
        }
        else if (PhotonNetwork.CurrentRoom.PlayerCount == 6)
        {
            playerCamera.target = PhotonNetwork.Instantiate("PlayerOrange",
                new Vector3(
                    Random.Range(-4, 4),
                    Random.Range(-4, 4),
                    0), Quaternion.identity).transform;
        }
        else if (PhotonNetwork.CurrentRoom.PlayerCount == 7)
        {
            playerCamera.target = PhotonNetwork.Instantiate("PlayerPurple",
                new Vector3(
                    Random.Range(-4, 4),
                    Random.Range(-4, 4),
                    0), Quaternion.identity).transform;
        }
        else if (PhotonNetwork.CurrentRoom.PlayerCount == 8)
        {
            playerCamera.target = PhotonNetwork.Instantiate("PlayerRed",
                new Vector3(
                    Random.Range(-4, 4),
                    Random.Range(-4, 4),
                    0), Quaternion.identity).transform;
        }
        else if (PhotonNetwork.CurrentRoom.PlayerCount == 9)
        {
            playerCamera.target = PhotonNetwork.Instantiate("PlayerWhite",
                new Vector3(
                    Random.Range(-4, 4),
                    Random.Range(-4, 4),
                    0), Quaternion.identity).transform;
        }
        else if (PhotonNetwork.CurrentRoom.PlayerCount == 10)
        {
            playerCamera.target = PhotonNetwork.Instantiate("PlayerYellow",
                new Vector3(
                    Random.Range(-4, 4),
                    Random.Range(-4, 4),
                    0), Quaternion.identity).transform;
        }


        //print("Current Player in the Room: " + PhotonNetwork.CurrentRoom.PlayerCount);
        statusPlayerNumber.text = "("+ PhotonNetwork.CurrentRoom.PlayerCount + "/10)";
    }
}
