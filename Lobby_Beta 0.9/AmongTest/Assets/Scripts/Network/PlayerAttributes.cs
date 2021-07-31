using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttributes
{
    public PlayerAttributes(int actorID, string playerColor)
    {
        setActorID(actorID);
        setPlayerColor(playerColor);
    }
    private int photonViewID;
    private int actorID;
    private int playerID;
    private float playerScore;
    private int playerNumTasks;
    private string playerColor;

    private bool isGhost;
    private bool isSaboteur;

    public void setPhotonViewID(int photonViewID) { this.photonViewID = photonViewID; }
    public int getPhotonViewID() { return photonViewID; }    
    public void setActorID(int actorID) { this.actorID = actorID; }
    public int getActorID() { return actorID; }
    public void setPlayerID(int playerID) { this.playerID = playerID; }
    public int getPlayerID() { return playerID; }
    public void setPlayerScore(int playerScore) { this.playerScore = playerScore; }
    public float getPlayerScore() { return playerScore; }
    public void setPlayerNumTasks(int addTask) { playerNumTasks = playerNumTasks + addTask; }
    public int getPlayerNumTasks() { return playerNumTasks; }
    public void setPlayerColor(string playerColor) { this.playerColor = playerColor; }
    public string getPlayerColor() { return playerColor; }
    public void setPlayerIsGhost(bool isGhost) { this.isGhost = isGhost; }
    public bool getPlayerIsGhost() { return isGhost; }
    public void setPlayerIsSaboteur(bool isSaboteur) { this.isSaboteur = isSaboteur; }
    public bool getPlayerIsSaboteur() { return isSaboteur; }

    public int getPlayerColorCode()
    {
        int playerColorCode = -1;
        switch (this.playerColor)
        {
            case "Purple":
                playerColorCode = 1;
                break;
            case "Brown":
                playerColorCode = 2;
                break;
            case "Green":
                playerColorCode = 3;
                break;
            case "Yellow":
                playerColorCode = 4;
                break;
            case "Blue":
                playerColorCode = 5;
                break;
            case "White":
                playerColorCode = 6;
                break;
            case "Black":
                playerColorCode = 7;
                break;
            case "Pink":
                playerColorCode = 8;
                break;
            case "Orange":
                playerColorCode = 9;
                break;
            case "Red":
                playerColorCode = 10;
                break;
        }
        return playerColorCode;
    }
}
