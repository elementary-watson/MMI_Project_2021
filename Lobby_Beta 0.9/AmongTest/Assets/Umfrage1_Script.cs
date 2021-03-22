using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Umfrage1_Script : MonoBehaviour
{
    public GameObject[] questionGroupArr;
    public QAClass[] qaArr;
    [SerializeField] WebRequest wr_object;

    string geschlecht, beruf, abschluss, staatsangehörigkeit, videospiele, amongus, stdProWoche, email;


    void Start()
    {
        qaArr = new QAClass[questionGroupArr.Length];
    }

    public void btn_finished()
    {
        for (int i = 0; i < qaArr.Length; i++)
        {
            qaArr[i] = ReadQuestionAndAnswer(questionGroupArr[i]);
        }
        //print(geschlecht +"\t"+ beruf + "\t" + abschluss + "\t" + staatsangehörigkeit + "\t" + videospiele + "\t" + amongus + "\t" + stdProWoche + "\t" + email);
        wr_object.SaveData(geschlecht,beruf ,abschluss ,staatsangehörigkeit ,videospiele ,amongus ,stdProWoche ,email);

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

                    if (result.Frage == "5. Spielst du Videospiele?")
                    {
                        videospiele = a.transform.GetChild(i).Find("Label").GetComponent<Text>().text;
                    }

                    if (result.Frage == "6. Kennst du das Spiel 'Among Us' ?")
                    {
                        amongus = a.transform.GetChild(i).Find("Label").GetComponent<Text>().text;
                    }

                    if (result.Frage == "7. Wie viele Stunden pro Woche spielst du Video Spiele?")
                    {
                        stdProWoche = a.transform.GetChild(i).Find("Label").GetComponent<Text>().text;
                    }

                    result.Antworten = a.transform.GetChild(i).Find("Label").GetComponent<Text>().text;
                    break;
                }
            }
        }
        else if (a.GetComponent<InputField>() != null)
        {
            email = a.transform.Find("Text").GetComponent<Text>().text;
            result.Antworten = a.transform.Find("Text").GetComponent<Text>().text;
        }
        else if (a.GetComponent<TMP_Dropdown>() != null)
        {
            if (result.Frage == "2. Beruf")
            {
                beruf = a.transform.Find("Label").GetComponent<TMP_Text>().text;
            }

            if (result.Frage == "3. Abschluss")
            {
                abschluss = a.transform.Find("Label").GetComponent<TMP_Text>().text;
            }

            if (result.Frage == "4. Staatsangehörigkeit")
            {
                staatsangehörigkeit = a.transform.Find("Label").GetComponent<TMP_Text>().text;
            }
            result.Antworten = a.transform.Find("Label").GetComponent<TMP_Text>().text;
        }

        return result;
    }

    [System.Serializable]
    public class QAClass
    {
        public string Frage = "";
        public string Antworten = "";
    }
}
