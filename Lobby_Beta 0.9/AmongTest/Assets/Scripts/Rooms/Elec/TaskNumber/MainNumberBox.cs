using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainNumberBox : MonoBehaviour
{
    private int fixcount = 3;
    private int count = 0;
    int srn1 = 0;
    int srn2 = 0;
    int srn3 = 0;
    int irn1 = 0;
    int irn2 = 0;
    int irn3 = 0;

    public GameObject winText;
    public Network _network;
    [SerializeField] GameObject BoxPanel;
    [SerializeField] private TextMeshProUGUI[] randomnubertext = new TextMeshProUGUI[0];
    [SerializeField] private TextMeshProUGUI[] currentvalue = new TextMeshProUGUI[0];
    [SerializeField] Button[] button = new Button[0];
    [SerializeField] private Image[] Thump = new Image[0];

    // Start is called before the first frame update
    void Start()
    {
        Thump[0].enabled = false;
        Thump[1].enabled = false;
        Thump[2].enabled = false;
        generatenumber();
    }

    private void generatenumber()
    {
        srn1 = Random.Range(1, 10) * 100;
        srn2 = Random.Range(1, 10) * 10;
        srn3 = Random.Range(1, 10);

        if (srn1 >= 500)
            irn1 = 0;
        else
            irn1 = 1000;

        if (srn2 >= 50)
            irn2 = 0;
        else
            irn2 = 100;

        if (srn3 >= 5)
            irn3 = 0;
        else
            irn3 = 10;

        Invoke("setText", 1);
    }

    public void setText()
    {
        randomnubertext[0].text = srn1 + " V";
        randomnubertext[1].text = srn2 + " V";
        randomnubertext[2].text = srn3 + " V";

        currentvalue[0].text = irn1 + " V";
        currentvalue[1].text = irn2 + " V";
        currentvalue[2].text = irn3 + " V";
    }

    public void setboxnumber(int aim, int collum)
    {
        if (collum == 0)
        {
            if (aim == -100 && irn1 == 0) { }
            else if (aim == 100 && irn1 == 1000) { }
            else
            {
                irn1 += aim;
                currentvalue[0].text = irn1 + " V";
            }

            if (irn1 == srn1)
            {
                button[0].interactable = false;
                button[1].interactable = false;
                Invoke("ShowThump", 1);

                checkcounter(1);
            }
        }

        if (collum == 1)
        {
           
            if (aim < 0 && irn2 == 0) { }
            else if (aim == 10 && irn2 == 100) { }
            else
            {
                irn2 += aim;
                currentvalue[1].text = irn2 + " V";
            }

            if (irn2 == srn2)
            {
                button[2].interactable = false;
                button[3].interactable = false;
                Invoke("ShowThump", 1);

                checkcounter(1);
            }
        }

        if (collum == 2)
        {
            if (aim < 0 && irn3 == 0) { }
            else if (aim == 1 && irn3 == 10) { }
            else
            {
                irn3 += aim;
                currentvalue[2].text = irn3 + " V";
            }

            if (irn3 == srn3)
            {
                button[4].interactable = false;
                button[5].interactable = false;
                Invoke("ShowThump", 1);
                checkcounter(1);
            }
        }
    }

    public void ShowThump()
    {
        if (irn1 == srn1)
        {
            Thump[0].enabled = true;
            currentvalue[0].text = "";
        }

        if (irn2 == srn2)
        {
            Thump[1].enabled = true;
            currentvalue[1].text = "";
        }

        if (irn3 == srn3)
        {
            Thump[2].enabled = true;
            currentvalue[2].text = "";
        }
    }

    public void checkcounter(int cp)
    {
        count = count + cp;
        if (count == fixcount)
        {
            winText.SetActive(true);
            Invoke("taskfinished", 2);
            _network.incrementTaskprogress(10);
        }
    }


    private void taskfinished()
    {
        BoxPanel.SetActive(false);
    }
}
