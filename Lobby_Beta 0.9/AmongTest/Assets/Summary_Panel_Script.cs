using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Summary_Panel_Script : MonoBehaviour
{
    [SerializeField] GameObject Summary_Panel;
    [SerializeField] Network _network;
    [SerializeField] Time_Game_Script timeGame_object;
    [SerializeField] Game_Info_Script gInfo_script;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("turnmeoff", 5);
    }
    public void setNextMode(bool caught, bool final)
    {
        if (caught)
        {

        }
    }
    public void turnmeoff()
    {
        Summary_Panel.SetActive(false);
        _network.setPlayerMovement(true);
        timeGame_object.beginTimer();
        // gInfo_script.shortNotification("isGhost");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
