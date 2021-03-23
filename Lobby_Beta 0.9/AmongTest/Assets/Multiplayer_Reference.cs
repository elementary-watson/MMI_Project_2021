using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Multiplayer_Reference : MonoBehaviour
{
    private int maxGameRounds; // Best of 3 - Best of 5
    private int numberOfPlayer;
    private int gameRound;
    private int currentStage;
    private int crewPoints;
    private int saboteurPoints;
    private int playerIncrementPower;
    private int saboteurDecrementPower;
    private int ghostIncrementPower;
    private int saboteurActorID;
    private float progressbarMaximum;
    private int nextTaskIndex;
    private IDictionary<int, string> allplayers = new Dictionary<int, string>();
    private IDictionary<int, float> allPlayerScores = new Dictionary<int, float>();
    private IDictionary<int, int> allPlayerTasks = new Dictionary<int, int>();
    private IDictionary<int, string> kickedplayers = new Dictionary<int, string>();
    private IDictionary<int, int> allPhotonplayers = new Dictionary<int, int>();

    private Vector3[] spawnPositions = new[] {
        new Vector3(1f, 4f, 0f), new Vector3(-2.1f, 4f, 0f), //Nordposition
        new Vector3(5.45f, 1.99f, 0f), new Vector3(5.45f, 0f, 0f), new Vector3(5.45f, -1.89f, 0f), //Ostposition
        new Vector3(1f, -4f, 0f), new Vector3(-2.1f, -4f, 0f), //Südposition
        new Vector3(-5.45f, 1.99f, 0f), new Vector3(-5.45f, 0f, 0f), new Vector3(-5.45f, -1.89f, 0f)}; //Westposition

    List<string> RandomColorList;

    List<string> AllTasksList = new List<string> { // XOF AMIR
        "Tag_WaterDispenser", "Tag_Game", "Tag_NumberRadio",
        "Tag_NumberBox", "Tag_ElectricBox", "Tag_ClickCabinet",
        "Tag_Fillgauge", "Tag_EnergyNumber", "Tag_LeverEnergy",
        "Tag_ClickMediKit", "Tag_Sink", "Tag_Tablet",
        "Tag_ComputerLabor", "Tag_LaborSingleTube", "Tag_ClickLabor",
        "Tag_NR_no", "Tag_NR_so", "Tag_NR_nw", "Tag_NR_sw"
    };

    public void setupGamestyle()
    {
        nextTaskIndex = 0;
        saboteurActorID = -1;
        gameRound = 1;
        currentStage = 1;
        saboteurPoints = 0;
        crewPoints = 0;

        progressbarMaximum = 1000;

        if (numberOfPlayer == 5 || numberOfPlayer == 6)
        {
            maxGameRounds = 3;
            playerIncrementPower = 10;
            ghostIncrementPower = playerIncrementPower / 4;
            saboteurDecrementPower = -10;
            progressbarMaximum = 100;
        }
        else
        {
            maxGameRounds = 5;
            playerIncrementPower = 10;
            ghostIncrementPower = playerIncrementPower / 4;
            saboteurDecrementPower = -10;
            progressbarMaximum = 100;
        }

        List<string> AllTasksList = new List<string> {
            "Tag_WaterDispenser", "Tag_Game", "Tag_NumberRadio",
            "Tag_NumberBox", "Tag_ElectricBox", "Tag_ClickCabinet",
            "Tag_Fillgauge", "Tag_EnergyNumber", "Tag_LeverEnergy",
            "Tag_ClickMediKit", "Tag_Sink", "Tag_Tablet",
            "Tag_ComputerLabor", "Tag_LaborSingleTube", "Tag_ClickLabor",
            "Tag_NR_no", "Tag_NR_so", "Tag_NR_nw", "Tag_NR_sw" };

        var count = AllTasksList.Count;
        var last = count - 1;
        for (var i = 0; i < last; ++i)
        {
            var r = UnityEngine.Random.Range(i, count);
            var tmp = AllTasksList[i];
            AllTasksList[i] = AllTasksList[r];
            AllTasksList[r] = tmp;
        }
        //this.AllTasksList = AllTasksList;
    }
    public string getNextTask()
    {
        if (AllTasksList.Count == (nextTaskIndex + 1))
        {
            nextTaskIndex = 0;
        }
        nextTaskIndex += 1;
        return AllTasksList[nextTaskIndex - 1];
    }

    public IDictionary<int, string> getKickedplayers()
    {
        return kickedplayers;
    }
    public void addKickedplayers(int id, string charname)
    {
        if (!kickedplayers.Keys.Contains(id))
            kickedplayers.Add(id, charname);
    }
    #region player
    public void setPlayers(IDictionary<int, string> allplayers)
    {
        this.allplayers = allplayers;
    }
    public IDictionary<int, string> getPlayers()
    {
        return allplayers;
    }

    public bool addPlayer(int id, string charname, int maxPlayers)
    {
        if (!allplayers.Keys.Contains(id))
        {
            allplayers.Add(id, charname);
            numberOfPlayer += 1;
        }
        if (numberOfPlayer == maxPlayers)
        { // Wird nur vom letzten "maxplayer" ausgeführt
            print("addPLayer was max with id:" + id + "and maxplayer " + maxPlayers);
            return true;
        }
        return false;
    }

    public void deleteplayer(int photonId)
    {
        if (allplayers.ContainsKey(photonId))
        { // check key before removing it
            allplayers.Remove(photonId);
        }
        numberOfPlayer -= 1;
    }
    public void readPlayer()
    {
        foreach (KeyValuePair<int, string> kvp in allplayers)
        {
            Console.WriteLine("Key: {0}, Value: {1}", kvp.Key, kvp.Value);
            print("Key: " + kvp.Key + "Value" + kvp.Value);
        }
    }
    #endregion

    public void addPhotonplayer(int actorID, int photonViewID)
    {//allPhotonplayers
        if (!allPhotonplayers.Keys.Contains(actorID))
            allPhotonplayers.Add(actorID, photonViewID);
    }
    public int getPhotonIDbyActorID(int actorID) // gibt photon id zurück weil nur damit andere player angesprochen werden können
    {
        foreach (KeyValuePair<int, int> kvp in allPhotonplayers)
        {
            if(kvp.Key == actorID)
                return kvp.Value;
        }
        return -1;
    }

    public IDictionary<int, float> getAllPlayerScores()
    {
        return allPlayerScores;
    }

    public bool addAllPlayerScores(int id, float playerScorePoints, int maxPlayers)
    {
        if (!allPlayerScores.Keys.Contains(id))
        {
            allPlayerScores.Add(id, playerScorePoints);            
        }
        if (numberOfPlayer == maxPlayers) // Wird nur vom letzten "maxplayer" ausgeführt
            return true;
        return false;
    }

    public IDictionary<int, int> getAllPlayerTasks()
    {
        return allPlayerTasks;
    }

    public bool addAllPlayerTasks(int id, int playerTasks, int maxPlayers)
    {
        if (!allPlayerTasks.Keys.Contains(id))
        {
            allPlayerTasks.Add(id, playerTasks);
        }
        if (numberOfPlayer == maxPlayers) // Wird nur vom letzten "maxplayer" ausgeführt
            return true;
        return false;
    }

    //Diese Methode ist zur Platzierung der Spieler um die Hauptkonsole da
    public Vector3 setupPositions(int actorId) // Logik arbeitet mit INDEX Werten vom Vektor3 Array und Farben Array.   
    { //Farben werden vorher gemischt-Jeder Spieler hat eigene Farbe- Die position der Farbe bestimmt die anschließende Vector3 position

        foreach(KeyValuePair<int, string> kvp in allplayers) // suche nach spielerfarbe
        {
            if(kvp.Key == actorId) // wenn id gefunden kann spielerposition in randomcolorlist gesucht werden um die INDEX 
            {
                for(int i = 0; i< RandomColorList.Count; i++)
                {
                    if(kvp.Value == RandomColorList[i])
                    {
                        return spawnPositions[i]; // an der I-ten Stelle gefunden und gleiche stelle für player reservieren!
                    }
                }

            }            
        }
        return new Vector3(0,0,0);
    }

    public void setRandomColorList(List<string> RandomColorList) // Farben werden randomized um zufällige positionen für spieler zu erstellen
    {
        this.RandomColorList = RandomColorList;
    }
    void Start()
    {

    }

    #region getundset
    public int getGameRound() { return gameRound; }
    public void setGameRound(int gameRound) { this.gameRound = gameRound; }

    public int getCurrentStage() { return currentStage; }
    public void setCurrentStage(int currentStage) { this.currentStage = currentStage; }    

    public int getCrewPoints() { return crewPoints; }
    public void setCrewPoints(int crewPoints) { this.crewPoints = crewPoints; }    

    public int getSaboteurPoints() { return saboteurPoints; }
    public void setSaboteurPoints(int saboteurPoints) { this.saboteurPoints = saboteurPoints; }
    
    public int getPlayerIncrementPower() { return playerIncrementPower; }
    public void setPlayerIncrementPower(int playerIncrementPower) { this.playerIncrementPower = playerIncrementPower; }

    public int getGhostIncrementPower() { return ghostIncrementPower; }
    public void setGhostIncrementPower(int ghostIncrementPower) { this.ghostIncrementPower = ghostIncrementPower; }
    
    public int getSaboteurActorID() { return saboteurActorID; }
    public void setSaboteurActorID(int saboteurActorID) { if(this.saboteurActorID == -1) this.saboteurActorID = saboteurActorID; }
    
    public int getNumberOfPlayer() { return numberOfPlayer; }
    public void setNumberOfPlayer(int numberOfPlayer) { this.numberOfPlayer = numberOfPlayer; }

    public int getSaboteurDecrementPower() { return saboteurDecrementPower; }
    public void setSaboteurDecrementPower(int saboteurDecrementPower) { this.saboteurDecrementPower = saboteurDecrementPower; }

    public int getMaxRounds() { return maxGameRounds; }

    public float getMaximumProgressbar() { return progressbarMaximum; }

    #endregion

    }
