using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MultiplayerProgressBar : MonoBehaviour
{
    // Start is called before the first frame update
    public int maximum;
    public int current;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetCurrentFill()
    {
        float fillAmount = (float)current / (float)maximum;
        //Mask.fillAmount = fillAmount;
    }
}
