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

    List<int> PlayerColor = new List<int>();

    private void SpawnPlayer(string PlayerColor)
    {
        playerCamera.target = PhotonNetwork.Instantiate(PlayerColor,
                new Vector3(
                    Random.Range(-4, 4),
                    Random.Range(-4, 4),
                    0), Quaternion.identity).transform;
    }

    private string RandomColor()
    {
        List<string> PlayerColorName = new List<string>{"PlayerBlack", "PlayerBlue", "PlayerBrown", "PlayerCyan", "PlayerGreen", "PlayerOrange", "PlayerPurple", "PlayerRed", "PlayerWhite", "PlayerYellow"};
        print(PlayerColorName);
        
        bool B1 = true;
        bool B2 = false;
        int randomNumberforColor = 0;
              
        while(B1)
        {
            randomNumberforColor = Random.Range(1, 10);

            for (int i = 0; i< PlayerColor.Count; i++)
            {
                if (randomNumberforColor == PlayerColor[i])
                {
                    B2 = true;
                }
            }

            if (B2 == true)
            {
                B1 = true;
            }

            else
            {
                B1 = false;
                PlayerColor.Add(randomNumberforColor);
            }
        }
        return PlayerColorName[randomNumberforColor];
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
                SpawnPlayer(RandomColor());
                break;
            case 2:
                SpawnPlayer(RandomColor());
                break;
            case 3:
                SpawnPlayer(RandomColor());
                break;
            case 4:
                SpawnPlayer(RandomColor());
                break;
            case 5:
                SpawnPlayer(RandomColor());
                break;
            case 6:
                SpawnPlayer(RandomColor());
                break;
            case 7:
                SpawnPlayer(RandomColor());
                break;
            case 8:
                SpawnPlayer(RandomColor());
                break;
            case 9:
                SpawnPlayer(RandomColor());
                break;
            case 10:
                SpawnPlayer(RandomColor());
                break;
            default:
                print("Das darf gar nicht passieren");
                break;
        }
       
        statusPlayerNumber.text = "("+ PhotonNetwork.CurrentRoom.PlayerCount + "/10)";
    }
}
