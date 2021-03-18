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
    private int ghostIncrementPower;
    private IDictionary<int, string> allplayers = new Dictionary<int, string>();
    private Vector3[] spawnPositions = new[] {
        new Vector3(1f, 4f, 0f), new Vector3(-2.1f, 4f, 0f), //Nordposition
        new Vector3(5.45f, 1.99f, 0f), new Vector3(5.45f, 0f, 0f), new Vector3(5.45f, -1.89f, 0f), //Ostposition
        new Vector3(1f, -4f, 0f), new Vector3(-2.1f, -4f, 0f), //Südposition
        new Vector3(-5.45f, 1.99f, 0f), new Vector3(-5.45f, 0f, 0f), new Vector3(-5.45f, -1.89f, 0f)}; //Westposition
    List<string> RandomColorList;

    public void setPlayers(IDictionary<int, string> allplayers)
    {
        this.allplayers = allplayers;
    }
    public IDictionary<int, string> getPlayers()
    {
        return allplayers;
    }
    public void addPlayer(int id, string charname, int maxPlayers)
    {
        if(!allplayers.Keys.Contains(id))
            allplayers.Add(id, charname);
        numberOfPlayer += 1;
        if (numberOfPlayer == maxPlayers)
            setupMultiplayerGame();
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
        foreach (KeyValuePair<int, string> kvp in allplayers) { 
            Console.WriteLine("Key: {0}, Value: {1}", kvp.Key, kvp.Value);
            print("Key: " + kvp.Key + "Value" + kvp.Value);
        }
    }
    public Vector3 setupPositions(int actorId) // Logik arbeitet mit INDEX Werten vom Vektor3 Array und Farben Array.   
    { //Farben werden vorher gemischt-Jeder Spieler hat eigene Farbe- Die position der Farbe bestimmt die anschließende Vector3 position
        foreach (string item in RandomColorList)
        {
            print("" + item);
        }
        print("SetupPos Called");
        foreach(KeyValuePair<int, string> kvp in allplayers) // suche nach spielerfarbe
        {
            if(kvp.Key == actorId) // wenn id gefunden kann spielerposition in randomcolorlist gesucht werden um die INDEX 
            {
                for(int i = 0; i< RandomColorList.Count; i++)
                {
                    if(kvp.Value == RandomColorList[i])
                    {
                        print("Found Position for " + kvp.Value + " at pos " + i);
                        return spawnPositions[i]; // an der I-ten Stelle gefunden und gleiche stelle für player reservieren!
                    }
                }

            }            
        }
        return new Vector3(0,0,0);
    }
    public void setupMultiplayerGame() //wird nur einmal ausgeführt
    {
        if(numberOfPlayer == 5 || numberOfPlayer == 6)
        {
            maxGameRounds = 3;
            playerIncrementPower = 10;
            ghostIncrementPower = playerIncrementPower / 4;
        }
        else
        {
            maxGameRounds = 5;
            playerIncrementPower = 10;
            ghostIncrementPower = playerIncrementPower / 4;
        }
    }
    public void setRandomColorList(List<string> RandomColorList) // Farben werden randomized um zufällige positionen für spieler zu erstellen
    {
        this.RandomColorList = RandomColorList;
    }
    void Start()
    {
        gameRound = 1;
        currentStage = 1;
        saboteurPoints = 0;
        crewPoints = 0;
        numberOfPlayer = 0;
        maxGameRounds = 3; //XOF muss dynamisch werden
        playerIncrementPower = 10;
        ghostIncrementPower = playerIncrementPower / 4;
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
    public void setGhostIncrementPower(int playerIncrementPower) { this.ghostIncrementPower = ghostIncrementPower; }

    public int getMaxRounds() { return maxGameRounds; }
    #endregion

}
