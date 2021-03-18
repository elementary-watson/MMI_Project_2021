using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Score_Panel_Script : MonoBehaviour
{
    [SerializeField] Multiplayer_Reference m_reference;
    [SerializeField] TextMeshProUGUI tmp_currentRound;
    [SerializeField] TextMeshProUGUI tmp_crewPoints;
    [SerializeField] TextMeshProUGUI tmp_sabPoints;

    // Start is called before the first frame update    
    void Start()
    {
        m_reference.getMaxRounds();
        tmp_currentRound.text = "Runde 1";
        initalizeNextRound();
    }
    public void initalizeNextRound ()
    {
        tmp_currentRound.text = "Runde " + m_reference.getGameRound();
        tmp_crewPoints.text = "" + m_reference.getCrewPoints();
        tmp_sabPoints.text = "" + m_reference.getSaboteurPoints();
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
