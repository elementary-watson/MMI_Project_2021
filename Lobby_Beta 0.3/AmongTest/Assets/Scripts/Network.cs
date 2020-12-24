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

    private void SpawnPlayer(string PlayerColor)
    {
        playerCamera.target = PhotonNetwork.Instantiate(PlayerColor,
                new Vector3(
                    Random.Range(-4, 4),
                    Random.Range(-4, 4),
                    0), Quaternion.identity).transform;
    }

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

        switch (PhotonNetwork.CurrentRoom.PlayerCount)
        {
            case 1:
                SpawnPlayer("PlayerBlack");
                break;
            case 2:
                SpawnPlayer("PlayerBlue");
                break;
            case 3:
                SpawnPlayer("PlayerBrown");
                break;
            case 4:
                SpawnPlayer("PlayerCyan");
                break;
            case 5:
                SpawnPlayer("PlayerGreen");
                break;
            case 6:
                SpawnPlayer("PlayerOrange");
                break;
            case 7:
                SpawnPlayer("PlayerPurple");
                break;
            case 8:
                SpawnPlayer("PlayerRed");
                break;
            case 9:
                SpawnPlayer("PlayerWhite");
                break;
            case 10:
                SpawnPlayer("PlayerYellow");
                break;
            default:
                print("Das darf gar nicht passieren");
                break;
        }

        /*if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
        {
            SpawnPlayer("PlayerBlack");

        }
        else if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            SpawnPlayer("PlayerBlue");

        }
        else if (PhotonNetwork.CurrentRoom.PlayerCount == 3)
        {
            SpawnPlayer("PlayerBrown");

        }
        else if (PhotonNetwork.CurrentRoom.PlayerCount == 4)
        {
            SpawnPlayer("PlayerCyan");

        }
        else if (PhotonNetwork.CurrentRoom.PlayerCount == 5)
        {
            SpawnPlayer("PlayerGreen");

        }
        else if (PhotonNetwork.CurrentRoom.PlayerCount == 6)
        {
            SpawnPlayer("PlayerOrange");

        }
        else if (PhotonNetwork.CurrentRoom.PlayerCount == 7)
        {
            SpawnPlayer("PlayerPurple");

        }
        else if (PhotonNetwork.CurrentRoom.PlayerCount == 8)
        {
            SpawnPlayer("PlayerRed");

        }
        else if (PhotonNetwork.CurrentRoom.PlayerCount == 9)
        {
            SpawnPlayer("PlayerWhite");

        }
        else if (PhotonNetwork.CurrentRoom.PlayerCount == 10)
        {
            SpawnPlayer("PlayerYellow");

        }*/

        //print("Current Player in the Room: " + PhotonNetwork.CurrentRoom.PlayerCount);
        statusPlayerNumber.text = "("+ PhotonNetwork.CurrentRoom.PlayerCount + "/10)";
    }
}
