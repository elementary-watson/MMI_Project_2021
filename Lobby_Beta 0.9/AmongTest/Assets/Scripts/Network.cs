﻿using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System;
//using System.Windows.Forms;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Network : MonoBehaviourPunCallbacks
{
    //Lobby Room
    [Header("LobbyRoom")]
    [SerializeField] private TMP_Text[] playerNameTexts = new TMP_Text[10];
    [SerializeField] private TMP_Text[] playerReadyTexts = new TMP_Text[10];
    [SerializeField] private Image[] playerImageContainer = new Image[10];
    public Text statusText;
    RoomOptions myRoomOptions;

    //User Interface
    [Header("User Interface")]
    public CameraFollow playerCamera;
    List<int> PlayerColor = new List<int>();    
    public Text txtCounterPlayersInRoom;
    public Text txtCurrentRoomName;
    public Text lobbyOrRoom;
    public Text countdown;
    //[SerializeField] GameObject GameMapPanel;
    [SerializeField] GameObject LobbyRoomPanel;
    [SerializeField] GameObject StartPanel;
    [SerializeField] GameObject FadeObject;
    [SerializeField] GameObject CounterObject;
    public GameObject useindicator;
    public UI_Taskbar prog_reference;

    //Photon
    [Header("Photon Chat")]
    [SerializeField] private GameObject chatWindow;

    [Header("Photon")]
    private TypedLobby customLobby = new TypedLobby("customLobby", LobbyType.Default);
    private Dictionary<string, RoomInfo> cachedRoomList = new Dictionary<string, RoomInfo>();
    public string lobby_Room_Name;
    PhotonView photonView;
    int lobbySwitch;
    // intern game logic
    List<string> randomColorList;
    private string myPlayerColorPrefab;
    private string myPlayerColorFilename;
    int roomMaxPlayerRef;
    //bool canJoin;
    public Multiplayer_Reference m_reference;
    [SerializeField] private Result_Voting_Panel result_vp;
    private void SpawnPlayer()
    {
        /*playerCamera.target = PhotonNetwork.Instantiate(myPlayerColorPrefab,
                 new Vector3(
                     UnityEngine.Random.Range(-4, 4),
                     UnityEngine.Random.Range(-4, 4),
                     0), Quaternion.identity).transform; */
        GameObject spawn = PhotonNetwork.Instantiate(myPlayerColorPrefab, new Vector3(
            UnityEngine.Random.Range(-4, 4),
            UnityEngine.Random.Range(-4, 4),
            0), Quaternion.identity);
        CharacterControl cc = spawn.GetComponent<CharacterControl>();
        cc.interactIcon = useindicator;
        //cc.resetPosition();
        spawn.GetComponent<CharacterControl>().interactIcon = useindicator;
        //spawn.GetComponent<CharacterControl>().resetPosition(); ;
        
        //useindicator = spawn.
        //spawn;
        playerCamera.target = spawn.transform;
        //useindicator.transform.position = new Vector2(999, -999);
    }

    private void RandomColor()
    {
        List<string> RandomColorList = new List<string> { "PlayerBlack", "PlayerBlue", "PlayerBrown", "PlayerPink", "PlayerGreen", "PlayerOrange", "PlayerPurple", "PlayerRed", "PlayerWhite", "PlayerYellow" };
        /*List<string> RandomColorList = new List<string> { "PlayerBlack", "PlayerBlack", "PlayerBlack", "PlayerBlack", "PlayerBlack", "PlayerBlack", "PlayerBlack", "PlayerBlack", "PlayerBlack", "PlayerBlack" };*/

        var count = RandomColorList.Count;
        var last = count - 1;
        for (var i = 0; i < last; ++i)
        {
            var r = UnityEngine.Random.Range(i, count);
            var tmp = RandomColorList[i];
            RandomColorList[i] = RandomColorList[r];
            RandomColorList[r] = tmp;
        }
        this.randomColorList = RandomColorList;
    }

    
    public void Awake()
    {
        // XOF PhotonNetwork.FetchServerTimestamp();
        lobbySwitch = 0;
        lobby_Room_Name = "LobbyRoom_A";
        for(int i = 0; i < 10; i++)
        {
            playerImageContainer[i].enabled = false;
            playerNameTexts[i].text = "";
            playerReadyTexts[i].text = "";
        }
        RandomColor();
        myRoomOptions = new RoomOptions() { MaxPlayers = 2, IsVisible = true, IsOpen = true, /*PlayerTtl = 10000,*/ EmptyRoomTtl=60000 };
        roomMaxPlayerRef = 2;
        //canJoin = true;
    }

    public void incrementTaskprogress(int increment)
    {
        photonView.RPC("incrementTaskprogressNetwork", RpcTarget.All,increment);
    }
    [PunRPC]
    void incrementTaskprogressNetwork(int increment)
    {
        prog_reference.setTaskprogress(increment);
    }

    public int getActorId()
    {
        return PhotonNetwork.LocalPlayer.ActorNumber;
    }
    public int getActorInRoom()
    {
        return PhotonNetwork.CurrentRoom.PlayerCount;
    }
    public string getPlayerColor()
    {
        return myPlayerColorFilename;
    }

    #region Chatfunctions

    public void callChatWindowRPC()
    {
        try {
            print("i was called");
            photonView.RPC("openChatWindow", RpcTarget.All);
        }
        catch (Exception e) 
        {
            print("Exception: " + e);
        }
    }
    #endregion

    public void callSubmitVote(bool isSubmitted, string playerColor, int photonActorID, int indexPosition)
    {
        photonView.RPC("submitAllPlayers", RpcTarget.All, isSubmitted, playerColor, photonActorID, indexPosition);
    }
    #region RPC
    [PunRPC]
    public void RefreshPlayerNumberOnJoin()
    {
        txtCounterPlayersInRoom.text = "(" + PhotonNetwork.CurrentRoom.PlayerCount + "/10)";
    }    
    [PunRPC]
    public void submitAllPlayers(bool isSubmitted, string playerColor, int photonActorID, int indexPosition)
    {
        if (isSubmitted)
            result_vp.submitVote( getActorId(), getPlayerColor(), playerColor, photonActorID, indexPosition);
        else
            result_vp.submitVote( getActorId(), getPlayerColor(), "", 0, -1);
    }
    [PunRPC]
    public void RefreshPlayerNumberOnLeave()
    {
        txtCounterPlayersInRoom.text = "(" + (PhotonNetwork.CurrentRoom.PlayerCount-1) + "/10)";
    }
    [PunRPC]
    public void RoomPlayerJoin()
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
    public void RoomPlayerLeave(string playerNickname)
    {
        m_reference.deleteplayer(PhotonNetwork.LocalPlayer.ActorNumber);
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
    [PunRPC]
    public void setColor(String idAndColor)
    {
        //XOF
        String[] parts = idAndColor.Split('-');
            if (idAndColor.Contains(PhotonNetwork.NickName))
            {
                myPlayerColorPrefab = parts[1];
            }
        List<string> colorFileList = new List<string> { "Black_Char", "Blue_Char", "Brown_Char", "Pink_Char", "Green_Char", "Orange_Char", "Purple_Char", "Red_Char", "White_Char", "Yellow_Char" };
        string temp = "";
        if (myPlayerColorPrefab != null)
            temp = myPlayerColorPrefab.Remove(0, 6);
        int i = 0;
        foreach (string item in colorFileList)
        {
            if (item.Contains(temp))
            {
                myPlayerColorFilename = colorFileList[i];
            }
            i++;
        }
        //PhotonNetwork.NickName;
        
        m_reference.addPlayer(int.Parse(parts[0]),parts[1].Remove(0,6));
    }
    [PunRPC]
    public void startGame()
    {
        //GameMapPanel.SetActive(true);
        LobbyRoomPanel.SetActive(false);
        SpawnPlayer();
    }

    [PunRPC]
    public void startFading()
    {
        FadeObject.SetActive(true);
    }

    [PunRPC]
    public void closeRoom()
    {

    }
    [PunRPC]
    public void openChatWindow()
    {
        chatWindow.SetActive(true);
    }
    #endregion
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
    private void Start()
    {
        //MessageBox.Show(Callback, "Hello World!", "Hello");
    }
    public void JoinLobby()
    {
        print("1. Join Lobby was called");
        //In PUN 2 you would have to deal with the Room List in another way, since it isn't cached internally any longer. 
        //PhotonNetwork.CurrentLobby.Name = "";
        PhotonNetwork.JoinLobby(customLobby);
        
        print(PhotonNetwork.CountOfPlayersOnMaster);
        try
        {
            if (PhotonNetwork.InLobby) print("We are in a lobby"); 
            print("LobbyName: " + PhotonNetwork.CurrentLobby.ToString());
            print("LobbyName: " + PhotonNetwork.CurrentLobby.Name);
        }
        catch (Exception e)
        {
            print("ERROR: " + e);
        }
    }
    //Not working because Photon ist ein Huenson
    public override void OnJoinedLobby()
    {
        if (PhotonNetwork.InLobby) print("We are in a lobby");
    }
    public override void OnConnected()
    {
        print("2. Onconnected We are connected");
        base.OnConnected();
    }
    public override void OnConnectedToMaster()
    {
        print("OnConnectedToMaster is called");
        // PhotonNetwork.PlayerList is never empty
        foreach (Player player in PhotonNetwork.PlayerList)
        {
            print("NICHT IM RAUM: ID: " + player.ActorNumber + "\n" + "Nickname: " + player.NickName);
        }
        statusText.text = "Joining room";

        pickLobbyRoom();
    }
    private void pickLobbyRoom()
    {
        if (lobbySwitch == 0) PhotonNetwork.JoinOrCreateRoom("LobbyRaum_A", myRoomOptions, TypedLobby.Default);
        if (lobbySwitch == 1) PhotonNetwork.JoinOrCreateRoom("LobbyRaum_B", myRoomOptions, TypedLobby.Default);
        if (lobbySwitch == 2) PhotonNetwork.JoinOrCreateRoom("LobbyRaum_C", myRoomOptions, TypedLobby.Default);
    }
    public void btnJoinOrCreateRoom()
    {
        statusText.text = "Connecting";
        PhotonNetwork.NickName = "Player" + UnityEngine.Random.Range(0, 1000);
        print("Nickname was created: " + PhotonNetwork.NickName);
        PhotonNetwork.ConnectUsingSettings();
    }
    public void WebGLQuit()
    {
        this.SendQuitEvent();
    }
    void OnApplicationQuit()
    {
        print("DEBUG: Player left Application");
        this.SendQuitEvent();
    }


    /*unityInstance.Quit(function() {
        console.log("done!");
    });
    unityInstance = null;*/

    void SendQuitEvent()
    {
        try {
            photonView.RPC("RoomPlayerLeave", RpcTarget.All, PhotonNetwork.NickName.ToString());
            photonView.RPC("RefreshPlayerNumberOnLeave", RpcTarget.All);
            // send event, add your code here
            print("DEBUG: SendQuitEvent ausführen");
            PhotonNetwork.SendAllOutgoingCommands(); // send it right now
        }catch(Exception e) { print("ERROR: " + e); }

    }

    void RPCStartgame()
    {
        photonView.RPC("startGame", RpcTarget.All);
    }

    void RPCStartFading()
    {
        photonView.RPC("startFading", RpcTarget.All);
    }

    [PunRPC]
    public void RPCStartCounter()
    {
        CounterObject.SetActive(true);
    }

    //XOF Funktion der Methode unbekannt. Steht in Relation mit Lobbyraum verlassen
    public void OnPhotonPlayerDisconnected()
    {
        photonView.RPC("RoomPlayerLeave", RpcTarget.All, PhotonNetwork.NickName.ToString());
        photonView.RPC("RefreshPlayerNumberOnLeave", RpcTarget.All);
        // send event, add your code here
        print("DEBUG: OnPhotonPlayerDisconnected");
        PhotonNetwork.SendAllOutgoingCommands(); // send it right now
    }
    public override void OnJoinedRoom()
    {
        print("DEBUG: I joined room");
        try { 
            //else if (PhotonNetwork.CurrentRoom.PlayerCount == roomMaxPlayerRef+1) PhotonNetwork.LeaveRoom();
            if(PhotonNetwork.CurrentRoom.IsOpen == true)
            {
                print("XOF before nickname");
                PhotonNetwork.NickName = PhotonNetwork.LocalPlayer.ActorNumber + "";
                txtCurrentRoomName.text = PhotonNetwork.CurrentRoom.Name;
                print("Name of room: " + PhotonNetwork.CurrentRoom.Name);
                print("Player in current room: " + PhotonNetwork.CurrentRoom.PlayerCount + 
                    "\nAlle RaumStatistiken PlayerInRooms: " + PhotonNetwork.CountOfPlayersInRooms +
                    "\nDEBUG: (InRoom) Name of Player: " + PhotonNetwork.NickName);
                statusText.text = "Connected to Lobby: " + lobby_Room_Name;

                photonView = gameObject.GetComponent<PhotonView>();
                photonView.RPC("RefreshPlayerNumberOnJoin", RpcTarget.All);
                photonView.RPC("RoomPlayerJoin", RpcTarget.All);
                //txtCounterPlayersInRoom.text = "("+ PhotonNetwork.CurrentRoom.PlayerCount + "/10)";
                
                if (PhotonNetwork.CurrentRoom.PlayerCount == roomMaxPlayerRef)
                {
                    PhotonNetwork.CurrentRoom.IsOpen = false;
                    //Randomize colors
                    RandomColor();
                    //photonView = gameObject.GetComponent<PhotonView>();
                    int i = 0;
                    //setColor over RPC
                    foreach (Player player in PhotonNetwork.PlayerList)
                    {
                        photonView.RPC("setColor", RpcTarget.All, player.NickName + "-" + randomColorList[i]);
                        photonView.RPC("setColor", RpcTarget.OthersBuffered, player.NickName + "-" + randomColorList[i]);
                        i++;
                    }
                    print("XOF players");
                    m_reference.readPlayer();//print allplayer dictionary elements
                    photonView.RPC("RPCStartCounter", RpcTarget.All);
                    Invoke("RPCStartFading", 8);
                    Invoke("RPCStartgame", 10);

                    
                }

            }
            else { print("ERROR: Joining Room failed"); }
            
        }catch (Exception e) 
        {
            print("ERROR: " + e);
        }
    }
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        print("Joining Room failed");
        if (lobbySwitch == 0) lobbySwitch = 1;
        else if (lobbySwitch == 1) lobbySwitch = 2;
        else if (lobbySwitch == 2) lobbySwitch = 0;
        pickLobbyRoom();
        base.OnJoinRoomFailed(returnCode, message);
        //PhotonNetwork.LeaveRoom();
        
    }
    public override void OnLeftRoom()
    {
        print("1. I left the room");
        if (lobbySwitch == 0) lobbySwitch = 1;
        else if (lobbySwitch == 1) lobbySwitch = 2;
        else if (lobbySwitch == 2) lobbySwitch = 0;
        base.OnLeftRoom();
    }
    public override void OnDisconnected(DisconnectCause cause)
    {
        print("2. I Disconnected");
        base.OnDisconnected(cause);

    }
    //public override void OnPhoton
    public override void OnLeftLobby()
    {
        print("3. I left the lobby");
        base.OnLeftLobby();
    }
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        print("WE UPDATED THE LIST ROOMS");
        //base.OnRoomListUpdate(roomList);
        UpdateCachedRoomList(roomList);
    }
    private void UpdateCachedRoomList(List<RoomInfo> roomList)
    {
        print("UpdateCachedRoomList is called ");
        for (int i = 0; i < roomList.Count; i++)
        {
            RoomInfo info = roomList[i];
            if (info.RemovedFromList)
            {
                cachedRoomList.Remove(info.Name);
            }
            else
            {
                cachedRoomList[info.Name] = info;
                print("Name of LobbyRoom: " + info.Name);
                print("Count of Player: " + info.PlayerCount);
            }
        }
    }

}