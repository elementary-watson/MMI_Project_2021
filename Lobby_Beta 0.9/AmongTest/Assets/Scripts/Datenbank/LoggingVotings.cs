using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoggingVotings : MonoBehaviour
{
    [SerializeField] private Multiplayer_Reference m_reference;
    [SerializeField] private Network _network;
    [SerializeField] WebRequestGame databaseLogger;
    public void loggingVote(string playerColor, int photonActorID)
    {
        //get The Stage
        string stage = "" + m_reference.getCurrentStage();
        //Loggin des Votings - Eigene Farbe und gewaehlte Farbe finden.
        string myPlayerColorNumber = "";
        string votePlayerColorNumber = "";
        //my colorNumber
        int tempColorNum = m_reference.getColorNumberByActorID(_network.getActorId());
        if (tempColorNum > -1) myPlayerColorNumber = tempColorNum + "";
        //voted volorNumber
        tempColorNum = m_reference.getColorNumberByActorID(photonActorID);
        if (tempColorNum > -1) votePlayerColorNumber = tempColorNum + "";
        //Survival state
        int survived = 1;
        if (m_reference.isGhostByActorID(_network.getActorId()))
            survived = 0;
        //Remainig players
        string remainingPlayer = "";
        List<PlayerAttributes> fullPlayerList = m_reference.getFullPlayerList();
        for (int i = 0; i < fullPlayerList.Count; i++)
        {
            if (!fullPlayerList[i].getPlayerIsGhost()) 
            {
                if (remainingPlayer.Equals(""))
                    remainingPlayer = fullPlayerList[i].getPlayerColor();
                else { remainingPlayer = remainingPlayer + fullPlayerList[i].getPlayerColor() + "-"; }
            }
        }

        int durationTimestamp = (int)_network.getRPC_currentTimestampDouble() - _network.getRPC_GameStartTimestamp();
        string isSab;
        if (_network.getIsSaboteur())
            isSab = "1";
        else
            isSab = "0";
        databaseLogger.sendRequest(
            _network.getActorId().ToString(), _network.getSessionID(), _network.getRPC_currentTimestampString(), //userID SessionID Timesstamp
            durationTimestamp.ToString(), m_reference.getGameRound().ToString(), _network.getMaxPlayer().ToString(), // Duration Round Numberofplayers
            remainingPlayer, myPlayerColorNumber, votePlayerColorNumber, stage, survived.ToString(), "", isSab); // Remainingplayers, avatarColor(MyColor), value(voted Color), type(1=pre 2=chat 3=vote), survived(0=kicked 1=surived), sentiment, topic(isntSab = 0 isSab=1)

    }
}
