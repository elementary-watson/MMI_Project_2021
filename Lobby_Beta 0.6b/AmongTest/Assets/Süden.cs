using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Süden : MonoBehaviour
{
    [SerializeField] private Button clickButton;
    public Text changeText;
        

    // Start is called before the first frame update
    public void OnClick()
    {
        changeText.text = "Hello you are in Süden";    
    }
}
