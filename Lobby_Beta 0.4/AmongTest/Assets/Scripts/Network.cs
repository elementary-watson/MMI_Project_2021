using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Network : MonoBehaviourPunCallbacks
{
    [SerializeField] private TMP_Text[] playerNameTexts = new TMP_Text[10];
    [SerializeField] private TMP_Text[] playerReadyTexts = new TMP_Text[10];
    [SerializeField] private Image[] playerImageContainer = new Image[10];
    public Text statusText;
    public CameraFollow playerCamera;
    public string Lobby_Room_Name;

    PhotonView photonView;
    public Text statusPlayerNumber;


    List<int> PlayerColor = new List<int>();

    private void SpawnPlayer(string PlayerColor)
    {
       /*playerCamera.target = PhotonNetwork.Instantiate(PlayerColor,
                new Vector3(
                    Random.Range(-4, 4),
                    Random.Range(-4, 4),
                    0), Quaternion.identity).transform;*/
    }

    private string RandomColor()
    {
        List<string> PlayerColorName = new List<string> { "PlayerBlack", "PlayerBlue", "PlayerBrown", "PlayerCyan", "PlayerGreen", "PlayerOrange", "PlayerPurple", "PlayerRed", "PlayerWhite", "PlayerYellow" };
        print(PlayerColorName);

        bool B1 = true;
        bool B2 = false;
        int randomNumberforColor = 0;

        while (B1)
        {
            randomNumberforColor = Random.Range(1, 10);

            for (int i = 0; i < PlayerColor.Count; i++)
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

    public void Awake()
    {
        Lobby_Room_Name = "LobbyA";
        for(int i = 0; i < 10; i++)
        {
            playerImageContainer[i].enabled = false;
        }
    }

    [PunRPC]
    public void RefreshPlayerNumberOnJoin()
    {
        statusPlayerNumber.text = "(" + PhotonNetwork.CurrentRoom.PlayerCount + "/10)";
    }
    [PunRPC]
    public void RefreshPlayerNumberOnLeave()
    {
        statusPlayerNumber.text = "(" + (PhotonNetwork.CurrentRoom.PlayerCount-1) + "/10)";
    }
    [PunRPC]
    public void SyncPlayersTextfiles()
    {
        int i = 0;
        for (int j = 0; j < 10; j++) 
        {
            playerImageContainer[j].enabled = false;
            playerNameTexts[j].SetText("");
            //playerReadyTexts[j].SetText("<color=red>Not Ready</color>");
        }
        foreach (Player player in PhotonNetwork.PlayerList)
        {
            print("ID: " + player.ActorNumber + "\n" + "Nickname: " + player.NickName);
            playerNameTexts[i].SetText(player.NickName);
            //playerReadyTexts[i].SetText("<color=green>Ready</color>");
            playerImageContainer[i].enabled = true;
            i++;
        }
    }
    [PunRPC]
    public void SyncPlayersLeave(string playerNickname)
    {
        int i = 0;
        for (int j = 0; j < 10; j++)
        {
            playerImageContainer[j].enabled = false;
            playerNameTexts[j].SetText("");
            //playerReadyTexts[j].SetText("<color=red>Not Ready</color>");
        }
        foreach (Player player in PhotonNetwork.PlayerList)
        {
            if (playerNickname != player.NickName) 
            {                
                print("ID: " + player.ActorNumber + "\n" + "Nickname: " + player.NickName);
                playerNameTexts[i].SetText(player.NickName);
                //playerReadyTexts[i].SetText("<color=green>Ready</color>");
                playerImageContainer[i].enabled = true;
                i++;
            }
        }
    }
    public void CreateLobby()
    {
        print("");
        print("Statistik: " + PhotonNetwork.CountOfPlayersInRooms);
        statusText.text = "Connecting";
        PhotonNetwork.NickName = "Player" + Random.Range(0, 1000);
        
        PhotonNetwork.ConnectUsingSettings();
    }

    //
    /*public void setPlayerReadyBtn()
    {
        if (readyState == false) readyState = true;
        else if (readyState == true) readyState = false;
        
        photonView.RPC("setPlayerReady", RpcTarget.All);
    }
    [PunRPC]
    private void setPlayerReady()
    {
        if (readyState == true) intPlayerReady++;
        else if (readyState == false) intPlayerReady--;
        //counterPlayerReady.text = "(" + intPlayerReady + "/10)";
    }*/

    public override void OnConnectedToMaster()
    {

        // PhotonNetwork.PlayerList is never empty

        foreach (Player player in PhotonNetwork.PlayerList)
        {
            print("NICHT IM RAUM: ID: " + player.ActorNumber + "\n" + "Nickname: " + player.NickName);
        }
        statusText.text = "Joining room";
        PhotonNetwork.JoinOrCreateRoom(Lobby_Room_Name, new RoomOptions() { MaxPlayers = 10}, null);
    }

    void OnApplicationQuit()
    {
        print("DEBUG: Player left Application");
        photonView.RPC("SyncPlayersLeave", RpcTarget.All, PhotonNetwork.NickName.ToString());
        this.SendQuitEvent();
    }
    void SendQuitEvent()
    {
        photonView.RPC("RefreshPlayerNumberOnLeave", RpcTarget.All);
        // send event, add your code here
        print("DEBUG: 1. SendQuitEvent reinkommen");
        //photonView.RPC("SyncPlayersLeave", RpcTarget.All);
        print("DEBUG: 2. SendQuitEvent RPC");
        PhotonNetwork.SendAllOutgoingCommands(); // send it right now
        print("DEBUG: 3. SendQuitEvent SendAllOutgoing");
    }

    //XOF Funktion der Methode unbekannt. Steht in Relation mit Lobbyraum verlassen
    //public void OnPhotonPlayerDisconnected(){}

    public override void OnJoinedRoom()
    {
        Debug.Log("DEBUG: Name of Player: " + PhotonNetwork.NickName);
        statusText.text = "Connected to Lobby: " + Lobby_Room_Name;
        photonView = gameObject.GetComponent<PhotonView>();
        photonView.RPC("RefreshPlayerNumberOnJoin", RpcTarget.All);
        photonView.RPC("SyncPlayersTextfiles", RpcTarget.All);
        //statusPlayerNumber.text = "("+ PhotonNetwork.CurrentRoom.PlayerCount + "/10)";
    }

    public override void OnLeftRoom()
    {
        print("1. I left the room");
        base.OnLeftRoom();
    }
    public override void OnLeftLobby()
    {
        print("2. I left the lobby");
        base.OnLeftLobby();
    }
    public override void OnDisconnected(DisconnectCause cause)
    {
        print("3. I Disconnected");
        base.OnDisconnected(cause);
    }
}