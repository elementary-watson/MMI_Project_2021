﻿using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System;
//using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Network : MonoBehaviourPunCallbacks
{
    [Header("LobbyRoom")]
    [SerializeField] private TMP_Text[] playerNameTexts = new TMP_Text[10];
    [SerializeField] private TMP_Text[] playerReadyTexts = new TMP_Text[10];
    [SerializeField] private Image[] playerImageContainer = new Image[10];
    public Text statusText;
    RoomOptions myRoomOptions;

    [Header("User Interface")]
    public CameraFollow playerCamera;
    List<int> PlayerColor = new List<int>();
    public Text txtCounterPlayersInRoom;
    public Text txtCurrentRoomName;
    public Text countdown;
    [SerializeField] GameObject LobbyRoomPanel;
    [SerializeField] GameObject StartPanel;
    [SerializeField] GameObject FadeObject;
    [SerializeField] GameObject CounterObject;
    

    //Photon
    [Header("Photon Chat")]
    //[SerializeField] private GameObject chatWindow;

    [Header("Photon")]
    private TypedLobby customLobby = new TypedLobby("customLobby", LobbyType.Default);
    private Dictionary<string, RoomInfo> cachedRoomList = new Dictionary<string, RoomInfo>();
    [SerializeField] string lobby_Room_Name;
    PhotonView photonView;
    int lobbySwitch;
    int maxPlayersOfRoom;

    [Header("Character Control")]
    public GameObject useindicator;
    [SerializeField] Main_Console_Script mainConsole_object;
    [SerializeField] Game_Info_Script gInfoScript_object;
    public Map_Control_Script msc_object;

    [Header("Game logic")]
    [SerializeField] Camera cam;
    [SerializeField] Time_Game_Script tgs_object;
    [SerializeField] private Result_Voting_Panel result_vp;
    [SerializeField] TextMeshProUGUI myPlayerRole;
    [SerializeField] GameObject Introduction_Panel_Crewmate;
    [SerializeField] GameObject Introduction_Panel_Saboteur;
    [SerializeField] GameObject Introduction_Panel;
    [SerializeField] private bool isGameOver;
    private string RPC_timestamp; //XOF Zum Loggen der Daten
    public Progressbar_Script prog_reference;
    public Multiplayer_Reference m_reference;

    [Header("Spieler Variablen")]
    IDictionary<int, string> listOfSuspects = new Dictionary<int, string>();
    IDictionary<int, string> listOfVotekicks = new Dictionary<int, string>();
    private Vector3 spawnPositions;
    GameObject spawnedPlayerObject; // der Spieler Prefab
    List<string> randomColorList;
    private string myPlayerColorPrefab;
    private string myPlayerColorFilename;
    [SerializeField] private bool isSaboteur; //Wichtige Variable
    [SerializeField] private bool isGhost; //Wichtige Variable    
    [SerializeField] int playerScorepoints; //XOF Punktzahl der spieler
    [SerializeField] string myCurrentTask; //XOF meine Task


    private void Start()
    {
        //MessageBox.Show(Callback, "Hello World!", "Hello");
    }
    public void Awake()
    {
        // XOF PhotonNetwork.FetchServerTimestamp();
        isGameOver = false;
        lobbySwitch = 0;
        lobby_Room_Name = "LobbyRoom_A";
        isSaboteur = false;
        isGhost = false;
        for (int i = 0; i < 10; i++)
        {
            playerImageContainer[i].enabled = false;
            playerNameTexts[i].text = "";
            playerReadyTexts[i].text = "";
        }
        RandomColor();
        myRoomOptions = new RoomOptions() { MaxPlayers = 1, IsVisible = true, IsOpen = true, /*PlayerTtl = 10000, EmptyRoomTtl=60000*/ };
        maxPlayersOfRoom = 1;
        //canJoin = true;
    }

    #region getta/setta
    public int getActorId() { return PhotonNetwork.LocalPlayer.ActorNumber; }
    public int getActorsInRoom() { return PhotonNetwork.CurrentRoom.PlayerCount; }
    public string getPlayerColor() { return myPlayerColorFilename; }

    public void setIsGameOver(bool isGameOver) {this.isGameOver = isGameOver;}    
    public bool getIsGameOver() {return isGameOver;}
    public void addSuspectToList(int stage, int gameround, string playerColor)
    {
        string value = gameround + " " + playerColor;
        // Runde pre/post Brown/Rot/Blau
        listOfSuspects.Add(gameround, value);
    }
    public void addVotekickToList(int gameround, string playerColor)
    {
        string value = gameround + " " + playerColor;
        // Runde pre/post Brown/Rot/Blau
        listOfVotekicks.Add(gameround, value);
    }

    #endregion


    #region Chatfunctions

    public void callChatWindowRPC() // XOF kann gelöscht werden
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


    #region Joining Process

    public void btnJoinOrCreateRoom()
    {
        statusText.text = "Connecting";
        PhotonNetwork.NickName = "Player" + UnityEngine.Random.Range(0, 1000);
        print("Nickname was created: " + PhotonNetwork.NickName);
        PhotonNetwork.ConnectUsingSettings();
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
        if (lobbySwitch == 3) PhotonNetwork.JoinOrCreateRoom("LobbyRaum_D", myRoomOptions, TypedLobby.Default);
    }

    [PunRPC]
    public void RoomPlayerJoin()
    {
        int i = 0;
        for (int j = 0; j < 10; j++)
        {
            playerImageContainer[j].enabled = false;
            playerNameTexts[j].SetText("");
        }
        foreach (Player player in PhotonNetwork.PlayerList)
        {
            print("ID: " + player.ActorNumber + "\n" + "Nickname: " + player.NickName);
            playerNameTexts[i].SetText(player.NickName);
            playerImageContainer[i].enabled = true;
            i++;
        }
    }
    [PunRPC]
    public void RefreshPlayerNumberOnJoin()
    {
        txtCounterPlayersInRoom.text = "(" + PhotonNetwork.CurrentRoom.PlayerCount + "/10)";
    }
    #endregion

    #region Inside_Room/Configuration
    public override void OnJoinedRoom()
    {
        print("DEBUG: I joined room");
        //try { 
        //else if (PhotonNetwork.CurrentRoom.PlayerCount == roomMaxPlayerRef+1) PhotonNetwork.LeaveRoom();
        if (PhotonNetwork.CurrentRoom.IsOpen == true)
        {
            PhotonNetwork.NickName = PhotonNetwork.LocalPlayer.ActorNumber + "";
            txtCurrentRoomName.text = PhotonNetwork.CurrentRoom.Name;
            print("Name of room: " + PhotonNetwork.CurrentRoom.Name +
                "Player in current room: " + PhotonNetwork.CurrentRoom.PlayerCount +
                "\nAlle RaumStatistiken PlayerInRooms: " + PhotonNetwork.CountOfPlayersInRooms +
                "\nDEBUG: (InRoom) Name of Player: " + PhotonNetwork.NickName);
            statusText.text = "Connected to Lobby: " + lobby_Room_Name;

            photonView = gameObject.GetComponent<PhotonView>();
            photonView.RPC("RefreshPlayerNumberOnJoin", RpcTarget.All);
            photonView.RPC("RoomPlayerJoin", RpcTarget.All);
            //txtCounterPlayersInRoom.text = "("+ PhotonNetwork.CurrentRoom.PlayerCount + "/10)";

            if (PhotonNetwork.CurrentRoom.PlayerCount == maxPlayersOfRoom)
            {
                PhotonNetwork.CurrentRoom.IsOpen = false;
                //Randomize colors
                RandomColor();
                //photonView = gameObject.GetComponent<PhotonView>();
                int i = 0;
                //setupPlayer over RPC
                foreach (Player player in PhotonNetwork.PlayerList)
                {
                    print("Id and Color: " + player.NickName + "-" + randomColorList[i]);
                    photonView.RPC("RPC_setupPlayer", RpcTarget.All, player.NickName + "-" + randomColorList[i]);
                    //photonView.RPC("setupPlayer", RpcTarget.OthersBuffered, player.NickName + "-" + randomColorList[i]);
                    i++;
                }                
                photonView.RPC("RPCStartCounter", RpcTarget.All);
                setPlayerSpawnPosition();
            }
        }
        else { print("ERROR: Joining Room failed"); }
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
    [PunRPC]
    public void RPC_setupPlayer(String idAndColor)
    {
        //XOF hier werden farben eingestellt und die multiplayer referenz aufgefüllt
        String[] parts = idAndColor.Split('-');
        if (parts[0] == (PhotonNetwork.NickName))
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
        bool isMaxPlayer = m_reference.addPlayer(int.Parse(parts[0]), parts[1].Remove(0, 6), maxPlayersOfRoom);
        if (isMaxPlayer == true)
            setupMultiplayerGame();
        print("DICT ID: " + int.Parse(parts[0]) + " Color" + parts[1].Remove(0, 6));
    }
    public void setupMultiplayerGame() //wird nur einmal vom letzten playerausgeführt
    {
        // XOF hier wird der Saboteur erstellt
        IDictionary<int, string> allplayers = m_reference.getPlayers();
        int rand = UnityEngine.Random.Range(0, allplayers.Count);
        int i = 0;
        int saboteurID = -1;
        foreach (KeyValuePair<int, string> kvp in allplayers)
        {
            if (rand == i)
            {
                saboteurID = kvp.Key;
            }
        }
        photonView.RPC("RPC_setupMultiplayerGame", RpcTarget.All, saboteurID);
    }

    [PunRPC]
    public void RPC_setupMultiplayerGame(int saboteurID) // Spielbalance und saboteur einstellen
    {
        //diese methode absichern falls method durch playerleave gerufen wurde. Falls einer geht muss nach jeder Runde rebalanced werden.

        if (getActorId() == saboteurID)
        {
            isSaboteur = true;
        }
        m_reference.setSaboteurActorID(saboteurID);
        m_reference.setupGamestyle();
    }
    [PunRPC]
    public void RPCStartCounter()
    {
        CounterObject.SetActive(true);
        //Invoke("RPCStartFading", 8);
        //Invoke("RPCStartgame", 10);
    }
    public void RPCStartFading()
    {
        photonView.RPC("startFading", RpcTarget.All);
    }

    [PunRPC]
    public void startFading()
    {
        FadeObject.SetActive(true);
        Invoke("RPCstartgame", 5);
        //RPCstartgame();
    }
    #endregion
    
    #region startgame
    public void RPCstartgame(){photonView.RPC("startGame", RpcTarget.All);}

    [PunRPC]
    public void startGame(PhotonMessageInfo info)
    {
        print("TimeStamp: " + info.timestamp);
        print("TimeStamp: " + info.SentServerTime);
        RPC_timestamp = "" + info.SentServerTime;
        //GameMapPanel.SetActive(true);
        LobbyRoomPanel.SetActive(false);

        SpawnPlayer();
    }
    private void SpawnPlayer()
    {
        GameObject spawn = PhotonNetwork.Instantiate(myPlayerColorPrefab, spawnPositions, Quaternion.identity);
        CharacterControl cc = spawn.GetComponent<CharacterControl>();
        cc.interactIcon = useindicator;
        cc.setMCSScript(msc_object);
        cc.setMultiplayerReference(m_reference);
        //cc.resetPosition();
        cc.setActorID(PhotonNetwork.LocalPlayer.ActorNumber);
        cc.setMainConsoleScript(mainConsole_object);
        cc.setGameInfoScript(gInfoScript_object);
        spawn.GetComponent<CharacterControl>().interactIcon = useindicator;

        playerCamera.target = spawn.transform;
        spawnedPlayerObject = spawn;
        setPhotonViewID();
        setPlayerMovement(false);
        if (isSaboteur)
        {
            myPlayerRole.text = "Rolle >> Saboteur";
            //myPlayerRole.text = timestamp;
            Introduction_Panel_Saboteur.SetActive(true);
        }
        else
        {
            Introduction_Panel_Crewmate.SetActive(true);
        }
        //Invoke("setIntroductionOff", 8);
    }
    public void setPhotonViewID() // photon id weil nur damit andere player angesprochen werden können
    {
        photonView.RPC("RPC_setPhotonViewID", RpcTarget.All, PhotonNetwork.LocalPlayer.ActorNumber, spawnedPlayerObject.GetComponent<PhotonView>().ViewID);
    }
    [PunRPC]
    public void RPC_setPhotonViewID(int actorID, int photonViewID) //Photon View bei allen sichern
    {
        print("Got ViewID of: " + actorID + " with viewID: " + photonViewID);
        m_reference.addPhotonplayer(actorID, photonViewID);
        var player = PhotonView.Find(photonViewID);
        //tgs_object.beginTimer();
    }
    public void setPlayerSpawnPosition()
    {
        List<string> setColorPosition = new List<string> { "Black", "Blue", "Brown", "Pink", "Green", "Orange", "Purple", "Red", "White", "Yellow" };
        var count = setColorPosition.Count;
        var last = count - 1;
        for (var i = 0; i < last; ++i)
        {
            var r = UnityEngine.Random.Range(i, count);
            var tmp = setColorPosition[i];
            setColorPosition[i] = setColorPosition[r];
            setColorPosition[r] = tmp;
        }
        string chain = "";
        foreach (string item in setColorPosition)
        {
            chain = chain + item + "-";
        }
        photonView.RPC("senColorPositionForMultiplayer", RpcTarget.All, chain);
    }
    [PunRPC]
    public void senColorPositionForMultiplayer(string chain)
    {
        String[] parts = chain.Split('-');
        List<string> setColorPosition = parts.ToList();
        m_reference.setRandomColorList(setColorPosition);
        Vector3 spawnPos = m_reference.setupPositions(PhotonNetwork.LocalPlayer.ActorNumber);
        if (spawnPos == new Vector3(0, 0, 0))
            print("ERROR: Player was not found position faulty!");
        else
            spawnPositions = spawnPos;
    }
    #endregion

    #region inGame
    public void resetPlayerPosition()
    {
        photonView.RPC("RPC_resetPlayerPosition", RpcTarget.All);
    }
    [PunRPC]
    public void RPC_resetPlayerPosition()
    {
        spawnedPlayerObject.transform.position = spawnPositions;
        spawnedPlayerObject.GetComponent<CharacterControl>().resetTask();
    }
    public void setPlayerMovement(bool canWalk)
    {
        print(canWalk);
        Player_Movement pm_object = spawnedPlayerObject.GetComponent<Player_Movement>();
        if (canWalk)
            pm_object.enableMovementSpeed();
        else
            pm_object.disableMovementSpeed();
    }
    public void getNextTask()
    {
        photonView.RPC("RPC_getNextTask", RpcTarget.All, getActorId());
    }

    [PunRPC]
    public void RPC_getNextTask(int actorId)
    {
        string nextTask = m_reference.getNextTask();
        if (actorId == getActorId())
        {
            myCurrentTask = nextTask;
            sendNextTask();
        }
    }
    public void sendNextTask()
    {
        mainConsole_object.setCurrentTask(myCurrentTask);
    }

    public void incrementTaskprogress()
    {
        print("DEBUG: IncrementProgress was called");
        photonView.RPC("incrementTaskprogressNetwork", RpcTarget.All, spawnedPlayerObject.GetComponent<CharacterControl>().getIncrementPower());
    }
    [PunRPC]
    public void incrementTaskprogressNetwork(float increment)
    {
        prog_reference.setTaskprogress(increment);
    }

    #endregion

    #region Voting

    public void setScoreOfRound(bool source) // Punkte geben wird von Time_Game_Script oder Progressbar_Script gerufen
    {
        //XOF RPC EINBAUEN
        // Source -> true crewmates und vice versa
        if (source)
        {
            m_reference.setCrewPoints(m_reference.getCrewPoints() + 1);
        }
        else
        {
            m_reference.setSaboteurPoints(m_reference.getSaboteurPoints() + 1);
        }
    }

    public void callSubmitVote(bool isSubmitted, string playerColor, int photonActorID, int indexPosition)
    {
        addVotekickToList(m_reference.getGameRound(), playerColor); // in die votekickliste speichern
        photonView.RPC("submitAllPlayers", RpcTarget.All, isSubmitted, playerColor, photonActorID, indexPosition);
    }


    [PunRPC]
    public void submitAllPlayers(bool isSubmitted, string playerColor, int photonActorID, int indexPosition)
    {
        if (isSubmitted)
            result_vp.submitVote(getActorId(), getPlayerColor(), playerColor, photonActorID, indexPosition);
        else
            result_vp.submitVote(getActorId(), getPlayerColor(), "", 0, -1);
    }

    // XOF Ghost
    public void setPlayerToGhost(int photonViewId) // wird von resultpanel zum schluss von phase 3 gerufen
    {
        photonView.RPC("RPC_setPlayerToGhost", RpcTarget.All, photonViewId);
    }

    [PunRPC]
    public void RPC_setPlayerToGhost(int kickedPhotonViewId)
    {
        print("Photon Ghost call");
        if (isSaboteur)
        {
            setIsGameOver(true);
        }
        else if (kickedPhotonViewId == spawnedPlayerObject.GetComponent<PhotonView>().ViewID)
        {
            print(PhotonNetwork.LocalPlayer.ActorNumber + " Found " + myPlayerColorPrefab + " with viewID: " + spawnedPlayerObject.GetComponent<PhotonView>().ViewID);
            spawnedPlayerObject.layer = 11; //set player to invisibleLayer
            spawnedPlayerObject.GetComponent<CharacterControl>().setStatusToGhost();
            isGhost = true;
            myPlayerRole.text = "Rolle >> Geist";
            int oldMask = cam.cullingMask;
            cam.cullingMask = -1; // |= ~(1 << 11);
            var allObjects = spawnedPlayerObject.GetComponentsInChildren<SpriteRenderer>(false);
            foreach (SpriteRenderer item in allObjects)
            {
                print("Renderer False Found: " + item.name);
                item.enabled = true;
            }
            var allTrueObjects = spawnedPlayerObject.GetComponentsInChildren<SpriteRenderer>(true);
            foreach (SpriteRenderer item in allTrueObjects)
            {
                print("Renderer true Found: " + item.name);
                item.enabled = true;
            }
            spawnedPlayerObject.GetComponentInChildren<SpriteRenderer>(true).enabled = true;
        }
        else if (kickedPhotonViewId == -1)
        {
            print("Player not found");
        }

        else
        {
            print(PhotonNetwork.LocalPlayer.ActorNumber + " wurde nicht gekicked " + myPlayerColorPrefab);
            GameObject[] playerObject = GameObject.FindGameObjectsWithTag("Player"); //PhotonView.Find()//
            int i = 0;
            foreach (GameObject item in playerObject)
            {
                print("Index: " + i);
                if (kickedPhotonViewId == item.GetComponent<PhotonView>().ViewID)
                {
                    print("ID: " + PhotonNetwork.LocalPlayer.ActorNumber + " hat den gevoteten Spieler gefunden!");
                    item.layer = 11;
                }
            }
        }
    }

    #endregion

    #region leaveLobby/LobbyJoinFail
    //XOF Funktion der Methode unbekannt. Steht in Relation mit Lobbyraum verlassen
    public void OnPhotonPlayerDisconnected()
    {
        photonView.RPC("RoomPlayerLeave", RpcTarget.All, PhotonNetwork.NickName.ToString());
        photonView.RPC("RefreshPlayerNumberOnLeave", RpcTarget.All);
        // send event, add your code here
        print("DEBUG: OnPhotonPlayerDisconnected");
        PhotonNetwork.SendAllOutgoingCommands(); // send it right now
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
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        print("Joining Room failed");
        if (lobbySwitch == 0) lobbySwitch = 1;
        else if (lobbySwitch == 1) lobbySwitch = 2;
        else if (lobbySwitch == 2) lobbySwitch = 3;
        else if (lobbySwitch == 3) lobbySwitch = 4;
        pickLobbyRoom();
        base.OnJoinRoomFailed(returnCode, message);
        //PhotonNetwork.LeaveRoom();
    }

    #endregion

    #region QuitGame

    public void WebGLQuit()
    {
        this.SendQuitEvent();
    }

    void OnApplicationQuit()
    {
        print("DEBUG: Player left Application");
        this.SendQuitEvent();
    }

    void SendQuitEvent() // send event, add your code here
    {
        try
        {
            //XOF wenn saboteur leaved dann passiert noch nix
            if (m_reference.getSaboteurActorID() == getActorId())
            {
                print("QUIT: Saboteur left the game, reset?");
            }
            photonView.RPC("RoomPlayerLeave", RpcTarget.All, PhotonNetwork.NickName.ToString());
            photonView.RPC("RefreshPlayerNumberOnLeave", RpcTarget.All);

            print("DEBUG: SendQuitEvent ausführen");
            PhotonNetwork.SendAllOutgoingCommands(); // send it right now!
        }
        catch (Exception e) { print("ERROR: " + e); }
    }

    [PunRPC]
    public void RefreshPlayerNumberOnLeave()
    {
        txtCounterPlayersInRoom.text = "(" + (PhotonNetwork.CurrentRoom.PlayerCount - 1) + "/10)";
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
        }
        foreach (Player player in PhotonNetwork.PlayerList)
        {
            if (playerNickname != player.NickName)
            {
                print("ID: " + player.ActorNumber + "\n" + "Nickname: " + player.NickName);
                playerNameTexts[i].SetText(player.NickName);
                playerImageContainer[i].enabled = true;
                i++;
            }
        }
    }

    #endregion




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