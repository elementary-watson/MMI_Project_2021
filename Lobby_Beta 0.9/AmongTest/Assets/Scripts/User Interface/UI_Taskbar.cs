using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UI_Taskbar : MonoBehaviour
{
    //public Slider slider;
    public float maximum;
    public float current;
    [SerializeField] private Image img_progressbar;
    [SerializeField] private TMP_Text tmp_progressValue;
    [SerializeField] private GameObject Panel_Voting;
    [SerializeField] private GameObject result_VotingPanel;
    [SerializeField] private GameObject preVoteTimer;
    [SerializeField] private GameObject Score_Panel;
    [SerializeField] private GameObject Player_Panel;
    [SerializeField] private GameObject thisMain_Progressbar;

    [SerializeField] private Network _network;
    [SerializeField] private Multiplayer_Reference m_reference;

    void getCurrentFill()
    {
        float fillamount = (float)current / (float)maximum;
        img_progressbar.fillAmount = fillamount;   
        if(current == 100) // XOF timer einbauen
        {
            current = 0;
            _network.resetPlayerPosition();
            _network.setPlayerMovement(false);
            Panel_Voting.SetActive(true);
            preVoteTimer.SetActive(true);            
            Score_Panel.SetActive(false);
            Player_Panel.SetActive(false);
            thisMain_Progressbar.SetActive(false);
        }
    }
    private void Start()
    {
        current = 0;
        if(m_reference.getGameRound() > 1)
        {
            result_VotingPanel.gameObject.SetActive(false);
            Panel_Voting.SetActive(false);
        }
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
        current += increment * 10; //XOF hier wird der wert manipuliert
    }

}
