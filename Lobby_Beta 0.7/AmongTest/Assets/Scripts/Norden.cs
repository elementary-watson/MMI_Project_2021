using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Norden : MonoBehaviour
{
    [SerializeField] private Text changeMe;
    [SerializeField] private Button thisButton;
    // Start is called before the first frame update

    public void btnClick()
    {
        changeMe.text = "Norden ist besser!";
        thisButton.GetComponentInChildren<Text>().text = "BRAAAAAHH";
    }
    void Start()
    {
        print("norden test");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
