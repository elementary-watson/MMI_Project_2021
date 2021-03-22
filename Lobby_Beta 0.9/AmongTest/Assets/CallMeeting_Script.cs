using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
    [SerializeField] private GameObject PrevotingIntro_Panel;

    [SerializeField] Multiplayer_Reference m_reference;
    [SerializeField] TextMeshProUGUI tmp_crewPoints;
    [SerializeField] TextMeshProUGUI tmp_sabPoints;

    [SerializeField] Image img_saboteur;
    [SerializeField] Image img_crewmate;
    [SerializeField] GameObject[] messages = new GameObject[0];


    // Start is called before the first frame update
    void Start()
    {        
    }
    public void setup(bool isCrewmate)
    {
        if (isCrewmate)
            StartCoroutine(ChangeColor(img_crewmate, Color.white, Color.black, 0.5f));
        else
            StartCoroutine(ChangeColor(img_saboteur, Color.white, Color.black, 0.5f));

        tmp_crewPoints.text = "" + m_reference.getCrewPoints();
        tmp_sabPoints.text = "" + m_reference.getSaboteurPoints();
        messages[0].SetActive(true); 
        messages[1].SetActive(false);
        messages[2].SetActive(false);
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
        PrevotingIntro_Panel.SetActive(false);
    }
    public void startMessage() { PrevotingIntro_Panel.SetActive(true); Invoke("messageInstuctions", 3); }
    public void messageInstuctions() { messages[0].SetActive(false); messages[1].SetActive(true); Invoke("messageFinal", 5); }
    public void messageFinal() { messages[1].SetActive(false); messages[2].SetActive(true); Invoke("callMeeting", 3);}
    private IEnumerator ChangeColor(Image image, Color from, Color to, float duration)
    {
        float timeElapsed = 0.0f;
        int singleCall = 0;
        float t = 0.0f;
        while (t < 1.0f)
        {
            timeElapsed += Time.deltaTime / 2;

            t = timeElapsed / duration;

            image.color = Color.Lerp(from, to, t);
            if (t > 0.99f)
            {
                singleCall += 1;
                if (singleCall == 1)
                    Invoke("startMessage", 2); 
            }
            yield return null;
        }

    }
}
