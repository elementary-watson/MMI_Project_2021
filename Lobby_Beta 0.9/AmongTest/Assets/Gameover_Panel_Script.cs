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
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
