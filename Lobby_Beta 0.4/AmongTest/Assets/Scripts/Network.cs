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
    [SerializeField] private TMP_Text[] playerNameTexts = new TMP_Text[4];
    [SerializeField] private TMP_Text[] playerReadyTexts = new TMP_Text[4];
    [SerializeField] private Image[] playerImageContainer = new Image[4];
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
        playerImageContainer[0].enabled = false;
        playerImageContainer[1].enabled = false;
        playerImageContainer[2].enabled = false;
        playerImageContainer[3].enabled = false;
    }

    [PunRPC]
    public void RefreshPlayerNumberLogical()
    {
        statusPlayerNumber.text = "(" + PhotonNetwork.CurrentRoom.PlayerCount + "/10)";
    }

    [PunRPC]
    public void SyncPlayersTextfiled()
    {
        switch (PhotonNetwork.CurrentRoom.PlayerCount)
        {
            case 1:
                SpawnPlayer(RandomColor());
                playerImageContainer[0].enabled = true;
                playerNameTexts[0].SetText(PhotonNetwork.NickName);                
                playerReadyTexts[0].SetText("LET IT RIP");

                break;
            case 2:
                SpawnPlayer(RandomColor());
                playerImageContainer[1].enabled = true;
                playerNameTexts[1].SetText(PhotonNetwork.NickName);
                playerReadyTexts[1].SetText("LET IT RIP");
                break;
            case 3:
                SpawnPlayer(RandomColor());
                playerImageContainer[2].enabled = true;
                playerNameTexts[2].SetText(PhotonNetwork.NickName);
                playerReadyTexts[2].SetText("LET IT RIP");
                break;
            case 4:
                SpawnPlayer(RandomColor());
                playerImageContainer[3].enabled = true;
                playerNameTexts[3].SetText(PhotonNetwork.NickName);
                playerReadyTexts[3].SetText("LET IT RIP");
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

    }

    public void CreateLobby()
    {
        statusText.text = "Connecting";
        PhotonNetwork.NickName = "Player" + Random.Range(0, 1000);
        
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        statusText.text = "Joining room";
        PhotonNetwork.JoinOrCreateRoom(Lobby_Room_Name, new RoomOptions() { MaxPlayers = 10 }, null);
    }

    public override void OnJoinedRoom()
    {
        //Dictionary<int, string> openWith = PhotonNetwork.CurrentRoom.;
        //Player playerList[] = new playerList[PhotonNetwork.PlayerList];

        foreach (Player player in PhotonNetwork.PlayerList) 
        {
            print("ID: "+ player.ActorNumber + "\n" + "Nickname: " + player.NickName);
        }

        Debug.Log("DEBUG: Name of Player: " + PhotonNetwork.NickName);
        statusText.text = "Connected to Lobby: " + Lobby_Room_Name;
        photonView = gameObject.GetComponent<PhotonView>();
        photonView.RPC("RefreshPlayerNumberLogical", RpcTarget.All);
        photonView.RPC("SyncPlayersTextfiled", RpcTarget.AllBuffered);
        //statusPlayerNumber.text = "("+ PhotonNetwork.CurrentRoom.PlayerCount + "/10)";
    }
}
