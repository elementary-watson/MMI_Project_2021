using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
public class Umfrage1_Script : MonoBehaviour
{
    public GameObject[] questionGroupArr;
    public GameObject frageBogenAllgemein;
    public QAClass[] qaArr;
    [SerializeField] WebRequest wr_object;
    public Text infotext;
    [SerializeField] GameObject webrequest;
    bool isFilled;
    int actorID;
    DateTime dateTime;
    int timestamp;
    string geschlecht,alter , beruf, abschluss, staatsangehörigkeit, videospiele, amongus, stdProWoche, email;


    void Start()
    {
        qaArr = new QAClass[questionGroupArr.Length];
        isFilled = false;
    }

    public void btn_finished(int actorID)
    {
        this.actorID = actorID;
        //int timestamp = (int) System.DateTime.Now;
        dateTime = System.DateTime.Now;
        var unixTime = dateTime.ToUniversalTime() - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        print("web id"+actorID);
        print("Time" + unixTime);

        for (int i = 0; i < qaArr.Length; i++)
        {
            qaArr[i] = ReadQuestionAndAnswer(questionGroupArr[i]);
        }
        //print(geschlecht +"\t"+ beruf + "\t" + abschluss + "\t" + staatsangehörigkeit + "\t" + videospiele + "\t" + amongus + "\t" + stdProWoche + "\t" + email);
        if(isFilled)
        {
            print("ISFILLED");
            webrequest.SetActive(true);
            wr_object.SaveData(actorID.ToString(), dateTime.ToString(), geschlecht, alter, beruf ,abschluss ,staatsangehörigkeit ,videospiele ,amongus ,stdProWoche ,email);
            Invoke("CloseMyPanel", 5f);
        }

    }
    public void CloseMyPanel()
    {
        frageBogenAllgemein.SetActive(false);
    }
    QAClass ReadQuestionAndAnswer (GameObject questionGroup)
    {
        QAClass result = new QAClass();


        GameObject q = questionGroup.transform.Find("Frage").gameObject;
        GameObject a = questionGroup.transform.Find("Antworten").gameObject;


        result.Frage = q.GetComponent<Text>().text;

        if (a.GetComponent<ToggleGroup>() != null)
        {
            for (int i = 0; i < a.transform.childCount; i++)
            {
                if (a.transform.GetChild(i).GetComponent<Toggle>().isOn)
                {
                    if(result.Frage == "1. Geschlecht" )
                    {
                        geschlecht = a.transform.GetChild(i).Find("Label").GetComponent<Text>().text;
                    }

                    if (result.Frage == "6. Spielst du regelmäßig Videospiele?")
                    {
                        videospiele = a.transform.GetChild(i).Find("Label").GetComponent<Text>().text;
                        if (videospiele == "Ja")
                        {
                            videospiele = "1";
                        }
                        else
                            videospiele = "0";
                    }

                    if (result.Frage == "7. Kennst du das Spiel 'Among Us' ?")
                    {
                        amongus = a.transform.GetChild(i).Find("Label").GetComponent<Text>().text;
                        if (amongus == "Ja")
                        {
                            amongus = "1";
                        }
                        else
                            amongus = "0";
                    }

                    if (result.Frage == "8. Wie viele Stunden pro Woche spielst du Video Spiele?")
                    {
                        stdProWoche = a.transform.GetChild(i).Find("Label").GetComponent<Text>().text;
                        if (stdProWoche == "0-2")
                        {
                            stdProWoche = "1";
                        }
                        if (stdProWoche == "2-4")
                        {
                            stdProWoche = "2";
                        }
                        if (stdProWoche == "4-6")
                        {
                            stdProWoche = "3";
                        }
                        if (stdProWoche == "6-8")
                        {
                            stdProWoche = "4";
                        }
                        if (stdProWoche == "8-10")
                        {
                            stdProWoche = "5";
                        }
                        if (stdProWoche == ">10")
                        {
                            stdProWoche = "6";
                        }
                    }

                    result.Antworten = a.transform.GetChild(i).Find("Label").GetComponent<Text>().text;
                    break;
                }
            }
        }
        else if (a.GetComponent<InputField>() != null)
        {
            if (result.Frage == "2. Gebe bitte dein Alter an")
            {
                alter = a.transform.Find("Text").GetComponent<Text>().text;
                if (alter == "")
                {
                    infotext.text = "Bitte fülle alle Felder aus!";
                    isFilled = false;
                }
                else
                    isFilled = true;
            }
            if (result.Frage == "9. Bitte gebe deine Email ein")
            {
                email = a.transform.Find("Text").GetComponent<Text>().text;
            }
            result.Antworten = a.transform.Find("Text").GetComponent<Text>().text;
        }
        else if (a.GetComponent<TMP_Dropdown>() != null)
        {
            if (result.Frage == "3. Beruf")
            {
                beruf = a.transform.Find("Label").GetComponent<TMP_Text>().text;
            }

            if (result.Frage == "4. Abschluss")
            {
                abschluss = a.transform.Find("Label").GetComponent<TMP_Text>().text;
            }

            if (result.Frage == "5. Nationalität")
            {
                staatsangehörigkeit = a.transform.Find("Label").GetComponent<TMP_Text>().text;
            }
            result.Antworten = a.transform.Find("Label").GetComponent<TMP_Text>().text;
        }

        return result;
    }


    public class QAClass
    {
        public string Frage = "";
        public string Antworten = "";
    }
}
