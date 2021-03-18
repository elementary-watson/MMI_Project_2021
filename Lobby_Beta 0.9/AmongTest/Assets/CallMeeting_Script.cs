using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CallMeeting_Script : MonoBehaviour
{
    [SerializeField] private GameObject Panel_Voting;
    [SerializeField] private GameObject preVoteTimer;
    [SerializeField] private GameObject Score_Panel;
    [SerializeField] private GameObject Player_Panel;
    [SerializeField] private GameObject Progressbar_Panel;
    [SerializeField] private GameObject Stage_Panel;
    [SerializeField] private GameObject CallMeeting_Panel;

    [SerializeField] Multiplayer_Reference m_reference;
    [SerializeField] TextMeshProUGUI tmp_crewPoints;
    [SerializeField] TextMeshProUGUI tmp_sabPoints;
    // Start is called before the first frame update
    void Start()
    {
        setup();
        Invoke("callMeeting",5);
    }
    public void setup()
    {
        tmp_crewPoints.text = "" + m_reference.getCrewPoints();
        tmp_sabPoints.text = "" + m_reference.getSaboteurPoints();
    }
    public void callMeeting()
    {

        Panel_Voting.SetActive(true);
        Stage_Panel.SetActive(true);
        preVoteTimer.SetActive(true);
        Score_Panel.SetActive(false);
        Player_Panel.SetActive(false);
        Progressbar_Panel.SetActive(false);
        CallMeeting_Panel.SetActive(false);

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
