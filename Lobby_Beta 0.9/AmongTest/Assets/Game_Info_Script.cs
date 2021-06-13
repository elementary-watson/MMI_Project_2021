using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Game_Info_Script : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI tmp_infoText;
    [SerializeField] AudioSource notify_sound;


    // Start is called before the first frame update
    void Start()
    {
        Invoke("startCenterCall", 8);       
    }
    public void startCenterCall()
    {
        shortNotification("startCenter");
    }
    public void shortNotification(string source)
    {
        switch (source)
        {
            case "startCenter":
                tmp_infoText.text = "Use the main console (Location: center of map)"; // startCenter
                notify_sound.Play();
                break;
            case "goTask":
                tmp_infoText.text = "Complete the task quickly! (Use your map!)"; // goTask
                notify_sound.Play();
                break;
            case "backCenter":
                tmp_infoText.text = "Great! Back to the main console! (Check map)"; // backCenter
                break;
            case "isGhost":
                tmp_infoText.text = "You are now a ghost and invisible. Continue with tasks to win!"; // isGhost
                notify_sound.Play();
                break;
            case "shortTime":
                tmp_infoText.text = "ATTENTION! Not much time left!"; // shortTime
                notify_sound.Play();
                break;
            case "roundsLeft":
                tmp_infoText.text = "Only " + "X" + " rounds left"; // roundsLeft
                notify_sound.Play();
                break;
        }        
        //notify_sound.Play();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
