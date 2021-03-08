using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Multiplayer_Reference : MonoBehaviour
{
    private int maxGameRounds; // Best of 3 - Best of 5
    private int numberOfPlayer;
    private int gameRound;
    private int currentStage;
    private int crewPoints;
    private int saboteurPoints;
    private IDictionary<int, string> allplayers = new Dictionary<int, string>();
    
    public void setPlayers(IDictionary<int, string> allplayers)
    {
        this.allplayers = allplayers;
    }
    public IDictionary<int, string> getPlayers()
    {
        return allplayers;
    }
    public void addPlayer(int id, string charname)
    {
        if(!allplayers.Keys.Contains(id))
        allplayers.Add(id, charname);
        numberOfPlayer += 1;
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
    public void setupMultiplayerGame()
    {
        if(numberOfPlayer == 5 || numberOfPlayer == 6){
            maxGameRounds = 3;
        }
        else
        {
            maxGameRounds = 5;
        }
    }
    void Start()
    {
        gameRound = 1;
        currentStage = 1;
        saboteurPoints = 0;
        crewPoints = 0;
        numberOfPlayer = 0;
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

    public int getMaxRounds() { return maxGameRounds; }
    #endregion

}
