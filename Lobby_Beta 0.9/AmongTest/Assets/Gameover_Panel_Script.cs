using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gameover_Panel_Script : MonoBehaviour
{
    [SerializeField] GameObject GameOver_Panel;
    [SerializeField] Multiplayer_Reference m_reference;
    [SerializeField] Network _network;
    [SerializeField] Image img_crewmateTotalwin;
    [SerializeField] Image img_saboteurWin;
    [SerializeField] Image img_draw;
    // Start is called before the first frame update


    public void setup(bool caught, bool final)
    {
        if (caught)
        {            
            img_crewmateTotalwin.enabled = true;
        }
        else 
        {
            m_reference.getSaboteurPoints();
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
