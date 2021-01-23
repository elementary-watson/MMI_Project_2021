using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Süden : MonoBehaviour
{
    [SerializeField] private Button clickButton;
    public Text changeText;

    public void OnClick()
    {
        changeText.text = "Hallo hier ist SüdenTest4";
    }
}
