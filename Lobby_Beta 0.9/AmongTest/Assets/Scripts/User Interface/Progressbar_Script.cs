using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Progressbar_Script : MonoBehaviour
{
    /*[SerializeField] private GameObject Panel_Voting;
    [SerializeField] private GameObject result_VotingPanel;
    [SerializeField] private GameObject preVoteTimer;
    [SerializeField] private GameObject Score_Panel;
    [SerializeField] private GameObject Player_Panel;
    [SerializeField] private GameObject Progressbar_Panel;
    [SerializeField] private GameObject Stage_Panel;*/
    [SerializeField] private GameObject Callmeeting_Panel;
    [SerializeField] Time_Game_Script tgs_object;

    [SerializeField] private Image img_progressbar;
    [SerializeField] private TMP_Text tmp_progressValue;
    [SerializeField] private Network _network;
    [SerializeField] private Multiplayer_Reference m_reference;
    [SerializeField] float maximum;
    [SerializeField] float current;

    bool isUseable;
    void getCurrentFill()
    {
        float fillamount = (float)current / (float)maximum;
        img_progressbar.fillAmount = fillamount;   
        if(current >= maximum) // XOF timer einbauen
        {
            tgs_object.stopTimer();
            current = 0;
            startMeeting(true);
        }
    }
    public void startMeeting(bool isCrewmate)
    {
        _network.resetPlayerPosition();
        _network.setPlayerMovement(false);
        _network.setScoreOfRound(isCrewmate); // false - saboteur kriegt punkt
        _network.callMeeting(isCrewmate);
    }
    public void stopProgressbar()
    {
        isUseable = false;
        current = 0;
    }
    private void Start()
    {
        setup();
        
    }
    public void setup()
    {
        isUseable = true;
        current = 0;
        maximum = m_reference.getMaximumProgressbar();
    }
    private void Update()
    {
        getCurrentFill();
        tmp_progressValue.text = ((float)current / (float)maximum) * 100 + " %";
    }
    public void setMaxTaskbar(int max)
    {
        //slider.maxValue = max;
    }
    public void setTaskprogress(float increment)
    {
        if(isUseable)
        current += increment; //XOF hier wird der wert manipuliert
    }

}
