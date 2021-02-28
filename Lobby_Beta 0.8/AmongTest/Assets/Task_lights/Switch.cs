using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    public GameObject up;
    public GameObject on;
    public bool isOn;
    public bool isUp;
    public lights_logic l_logic;

    // Start is called before the first frame update
    void Start()
    {
        on.SetActive(isOn);
        up.SetActive(isUp);
        if (isOn)
        {
            l_logic.SwitchChange(1);
        }
    }

    private void OnMouseUp()
    {
            isUp = !isUp;
            isOn = !isOn;
            on.SetActive(isOn);
            up.SetActive(isUp);
            if (isOn)
            {
                l_logic.SwitchChange(1);
            }
            else
            {
                l_logic.SwitchChange(-1);
            }
        
    }
}
