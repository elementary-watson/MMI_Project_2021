using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Umfrage2Page2 : MonoBehaviour
{
    public GameObject[] questionGroupArr;
    public QAClass[] qaArr;
    [SerializeField] WebRequestSurvey2 wr2_object;

    string value9, value10, value11, value12, value13, value14, value15, value16, value17, value18, value19;

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
        wr2_object.SaveDataPageTwo(value9, value10, value11, value12, value13, value14, value15, value16, value17, value18, value19);
        
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
                    if (result.Frage == "1. I empathized with the other(s)")
                    {
                        //value1 = a.transform.GetChild(i).Find("Label").GetComponent<Text>().text;
                        value9 = SetNumber(a.transform.GetChild(i).Find("Label").GetComponent<Text>().text);
                    }

                    if (result.Frage == "2. I felt connected to the other(s)")
                    {
                        value10 = SetNumber(a.transform.GetChild(i).Find("Label").GetComponent<Text>().text);
                    }

                    if (result.Frage == "3. I found it enjoyable to be with the other(s)")
                    {
                        value11 = SetNumber(a.transform.GetChild(i).Find("Label").GetComponent<Text>().text);
                    }

                    if (result.Frage == "4. When I was happy, the other(s) was(were) happy")
                    {
                        value12 = SetNumber(a.transform.GetChild(i).Find("Label").GetComponent<Text>().text);
                    }

                    if (result.Frage == "5. When the other(s) was(were) happy, I was happy")
                    {
                        value13 = SetNumber(a.transform.GetChild(i).Find("Label").GetComponent<Text>().text);
                    }

                    if (result.Frage == "6. I admired the other(s)")
                    {
                        value14 = SetNumber(a.transform.GetChild(i).Find("Label").GetComponent<Text>().text);
                    }

                    if (result.Frage == "7. I felt jealous about the other(s)")
                    {
                        value15 = SetNumber(a.transform.GetChild(i).Find("Label").GetComponent<Text>().text);
                    }

                    if (result.Frage == "8. I influenced the mood of the other(s)")
                    {
                        value16 = SetNumber(a.transform.GetChild(i).Find("Label").GetComponent<Text>().text);
                    }

                    if (result.Frage == "9. I was influenced by the other(s) moods")
                    {
                        value17 = SetNumber(a.transform.GetChild(i).Find("Label").GetComponent<Text>().text);
                    }

                    if (result.Frage == "10. I felt revengeful")
                    {
                        value18 = SetNumber(a.transform.GetChild(i).Find("Label").GetComponent<Text>().text);
                    }

                    if (result.Frage == "11. I felt schadenfreude (malicious delight)")
                    {
                        value19 = SetNumber(a.transform.GetChild(i).Find("Label").GetComponent<Text>().text);
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
