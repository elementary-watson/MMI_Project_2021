using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using TMPro;
using System;

public class Gameover_Panel_Script : MonoBehaviour
{
    [SerializeField] GameObject GameOver_Panel;
    [SerializeField] Multiplayer_Reference m_reference;
    [SerializeField] Network _network;
    [SerializeField] Image img_crewmateTotalwin;
    [SerializeField] Image img_saboteurWin;
    [SerializeField] Image img_draw;

    [SerializeField] GameObject rank_element;
    [SerializeField] Transform trfm_content_rankElements;
    // Start is called before the first frame update

    public IDictionary<int, int> allplayerTasks;
    public IDictionary<int, float> allPlayerScores;
    public IDictionary<int, string> allPlayers;

    void Start()
    {
        /*
        GameObject rankElement = Instantiate(rank_element, trfm_content_rankElements, false);
        Canvas.ForceUpdateCanvases();
        trfm_content_rankElements.transform.parent.GetParentComponent<ScrollRect>().verticalNormalizedPosition = 0;

        TextMeshProUGUI[] tmp_columns = rankElement.GetComponentsInChildren<TextMeshProUGUI>();
        tmp_columns[0].text = "a";
        tmp_columns[1].text = "b";
        tmp_columns[2].text = "c";
        tmp_columns[3].text = "d";
        */
    }

    public void setup(bool caught, bool final)
    {
        if (caught)
        {            
            img_crewmateTotalwin.enabled = true;
        }
        else 
        {
            int highPoints;
            int maxrounds = m_reference.getMaxRounds();
            if (maxrounds == 5)
                highPoints = 3;
            else
                highPoints = 2;
            if (m_reference.getSaboteurPoints() == highPoints)
            {
                img_saboteurWin.enabled = true;
            }
            else if (m_reference.getCrewPoints() == highPoints)
            {
                img_draw.enabled = true;
            }
        }
        _network.RPC_buildStatistics();
    }
    public void createTable()
    {
        allPlayerScores = m_reference.getAllPlayerScores();
        allplayerTasks =  m_reference.getAllPlayerTasks();
        allPlayers = m_reference.getPlayers();
        string[] playerRankingChains = new string[allplayerTasks.Count];
        
        float findBiggest = 0;
        int[] orderOfPlayer = new int[allPlayers.Count];
        float[] scoreOfPlayer = new float[allPlayers.Count];
        float temp;
        int temp2;
        int m = 0;
        foreach (KeyValuePair<int, float> kvp in allPlayerScores)
        {
            print("Check " + kvp.Key + " value " + kvp.Value);
        }
            //Einlesen der Actor ID in richtiger Reihenfolge
        foreach (KeyValuePair<int, float> kvp in allPlayerScores)
        {
            orderOfPlayer[m] = kvp.Key;
            print("Oder player: " + orderOfPlayer[m] );
            m++;
        }
        //Einlesen der Scores in richtiger Reihenfolge
        for (int l = 0; l<allPlayers.Count; l++)
        {
            scoreOfPlayer[l] = allPlayerScores[orderOfPlayer[l]];
        }

        for (int i = 0; i < scoreOfPlayer.Length - 1; i++)
        { 
            for (int j = i + 1; j < scoreOfPlayer.Length; j++) { 
                if (scoreOfPlayer[i] < scoreOfPlayer[j])
                {
                    temp = scoreOfPlayer[i];
                    temp2 = orderOfPlayer[i];

                    scoreOfPlayer[i] = scoreOfPlayer[j];
                    orderOfPlayer[i] = orderOfPlayer[j];

                    scoreOfPlayer[j] = temp;
                    orderOfPlayer[i] = temp2;
                }
            }
        }
        print("sorted scores");
        for (int l = 0; l < allPlayers.Count; l++)
        {
                print("Score " + scoreOfPlayer[l]  + " ID" + orderOfPlayer[l]);
        }

        foreach (int value in scoreOfPlayer)
        {
            print("Print scores: " + value);
        }
        int k = 0;

        /*foreach (KeyValuePair<int, string> kvp in allPlayers) // für jeden Player
        {
            foreach (KeyValuePair<int, float> innerkvp in allPlayerScores) // jeder player score
            {
                if (findBiggest < innerkvp.Value) 
                { 
                    findBiggest = innerkvp.Value;
                    orderOfPlayer[k] = innerkvp.Key;
                }
            }
            k++;                
        }*/
        for (int j = 0; j < allPlayers.Count; j++)
        {
            playerRankingChains[j] = orderOfPlayer[j] + "-" + allPlayers[orderOfPlayer[j]] + "-" + allplayerTasks[orderOfPlayer[j]] + "-" + allPlayerScores[orderOfPlayer[j]];
            createRankElement(playerRankingChains[j]);
        }
        

        //var sortedDict = from entry in allPlayerScores orderby entry.Value descending select entry;
        //var top5 = allPlayerScores.(pair => pair.Value).Take(5);
    }
    public void createRankElement(string playerRankingChains)
    {
        String[] parts = playerRankingChains.Split('-');

        GameObject rankElement = Instantiate(rank_element, trfm_content_rankElements, false);
        Canvas.ForceUpdateCanvases();
        trfm_content_rankElements.transform.parent.GetParentComponent<ScrollRect>().verticalNormalizedPosition = 0;

        float scorePoints = _network.getScorePoints();
        int numOfTask = _network.getNumberOfTasks();

        TextMeshProUGUI[] tmp_columns = rankElement.GetComponentsInChildren<TextMeshProUGUI>();
        //rankElement.GetComponentInChildren
        tmp_columns[0].text = parts[0];
        tmp_columns[1].text = parts[1];
        tmp_columns[2].text = parts[2];
        tmp_columns[3].text = parts[3];
    }
}
