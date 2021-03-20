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
                tmp_infoText.text = "Nutze die Hauptkonsole (Im Zentrum)"; // startCenter
                notify_sound.Play();
                break;
            case "goTask":
                tmp_infoText.text = "Erledige schnell die Task! (Siehe Karte)"; // goTask
                notify_sound.Play();
                break;
            case "backCenter":
                tmp_infoText.text = "Super! Zurueck zur Hauptkonsole (Siehe Karte)"; // backCenter
                break;
            case "isGhost":
                tmp_infoText.text = "Du bist nun ein Geist. Aufgabe abholen!"; // isGhost
                notify_sound.Play();
                break;
            case "shortTime":
                tmp_infoText.text = "ACHTUNG! Nicht mehr viel Zeit!"; // shortTime
                notify_sound.Play();
                break;
            case "roundsLeft":
                tmp_infoText.text = "Nur noch " + "X" + " Runden"; // roundsLeft
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
