using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Umfrage2Page1 : MonoBehaviour
{
    public GameObject[] questionGroupArr;
    public QAClass[] qaArr;
    [SerializeField] WebRequestSurvey2 wr2_object;

    string value1, value2, value3, value4, value5, value6, value7, value8;

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
        wr2_object.SaveDataPageOne(value1, value2, value3, value4, value5, value6, value7, value8);
    }

    QAClass ReadQuestionAndAnswer(GameObject questionGroup)
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
                    if (result.Frage == "1. I forgot everything around me")
                    {
                        //value1 = a.transform.GetChild(i).Find("Label").GetComponent<Text>().text;
                        value1 = SetNumber(a.transform.GetChild(i).Find("Label").GetComponent<Text>().text);
                    }

                    if (result.Frage == "2. I felt completely absorbed")
                    {
                        value2 = SetNumber(a.transform.GetChild(i).Find("Label").GetComponent<Text>().text);
                    }

                    if (result.Frage == "3. I felt content")
                    {
                        value3 = SetNumber(a.transform.GetChild(i).Find("Label").GetComponent<Text>().text);
                    }

                    if (result.Frage == "4. I felt good")
                    {
                        value4 = SetNumber(a.transform.GetChild(i).Find("Label").GetComponent<Text>().text);
                    }

                    if (result.Frage == "5. I felt bored")
                    {
                        value5 = SetNumber(a.transform.GetChild(i).Find("Label").GetComponent<Text>().text);
                    }

                    if (result.Frage == "6. I found it tiresome")
                    {
                        value6 = SetNumber(a.transform.GetChild(i).Find("Label").GetComponent<Text>().text);
                    }

                    if (result.Frage == "7. I felt challenged")
                    {
                        value7 = SetNumber(a.transform.GetChild(i).Find("Label").GetComponent<Text>().text);
                    }
                    if (result.Frage == "8. I had to put a lot of effort into it")
                    {
                        value8 = SetNumber(a.transform.GetChild(i).Find("Label").GetComponent<Text>().text);
                    }

                    result.Antworten = a.transform.GetChild(i).Find("Label").GetComponent<Text>().text;
                    break;
                }
            }
        }

        return result;
    }

    public string SetNumber(string answer)
    {
        string tmp = "";

        if (answer == "Überhaupt nicht")
        {
            tmp = "1";
        }
        if (answer == "geringfügig")
        {
            tmp = "2";
        }
        if (answer == "mittelmäßig")
        {
            tmp = "3";
        }
        if (answer == "ziemlich")
        {
            tmp = "4";
        }
        if (answer == "sehr")
        {
            tmp = "5";
        }
        return tmp;
    }

    [System.Serializable]
    public class QAClass
    {
        public string Frage = "";
        public string Antworten = "";
    }
}
