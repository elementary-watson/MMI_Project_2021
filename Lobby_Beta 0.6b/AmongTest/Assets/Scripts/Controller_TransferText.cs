using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Controller_TransferText : MonoBehaviour
{
    private string userInput;
    public GameObject UserInput_Field;
    public GameObject DisplayInput_Field;


    public void TransferText()
    {
        userInput = UserInput_Field.GetComponent<Text>().text;
        DisplayInput_Field.GetComponent<Text>().text = userInput;
    }

}

