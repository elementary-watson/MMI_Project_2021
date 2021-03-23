using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class WebRequestSurvey2 : MonoBehaviour
{
    string value1, value2, value3, value4, value5, value6, value7, value8, value9, value10, value11, value12, value13, value14, value15, value16, value17, value18, value19;

    public void CallCoroutine()
    { 
        StartCoroutine(UploadPost(value1, value2, value3, value4, value5, value6, value7, value8, value9, value10, value11, value12, value13, value14, value15, value16, value17, value18, value19));
    }

    public void SaveDataPageOne(string value1, string value2, string value3, string value4, string value5, string value6, string value7, string value8)
    {
        this.value1 = value1;
        this.value2 = value2;
        this.value3 = value3;
        this.value4 = value4;
        this.value5 = value5;
        this.value6 = value6;
        this.value7 = value7;
        this.value8 = value8;
    }

    public void SaveDataPageTwo(string value9, string value10, string value11, string value12, string value13, string value14, string value15, string value16, string value17 , string value18, string value19)
    {
        this.value9 = value9;
        this.value10 = value10;
        this.value11 = value11;
        this.value12 = value12;
        this.value13 = value13;
        this.value14 = value14;
        this.value15 = value15;
        this.value16 = value16;
        this.value17 = value17;
        this.value18 = value18;
        this.value19 = value19;

        CallCoroutine();
    }

    IEnumerator UploadPost(string value1, string value2, string value3, string value4, string value5, string value6, string value7, string value8, string value9, string value10, 
                   string value11, string value12, string value13, string value14, string value15, string value16, string value17, string value18, string value19)
    {

        WWWForm form = new WWWForm();
        form.AddField("userID", "5");
        form.AddField("SessionID", "21031541");
        form.AddField("TimeStamp", "01234567");
        form.AddField("avatarColor", "Blue");
        form.AddField("value1", value1);
        form.AddField("value2", value2);
        form.AddField("value3", value3);
        form.AddField("value4", value4);
        form.AddField("value5", value5);
        form.AddField("value6", value6);
        form.AddField("value7", value7); 
        form.AddField("value8", value8);
        form.AddField("value9", value9);
        form.AddField("value10", value10);
        form.AddField("value11", value11);
        form.AddField("value12", value12);
        form.AddField("value13", value13);
        form.AddField("value14", value14);
        form.AddField("value15", value15);
        form.AddField("value16", value16);
        form.AddField("value17", value17);
        form.AddField("value18", value18);
        form.AddField("value19", value19);


        using (UnityWebRequest www = UnityWebRequest.Post("https://sabotage.uvrg.org/php/saveQuestions.php", form))
        {
            www.downloadHandler = new DownloadHandlerBuffer();
            yield return www.SendWebRequest();
            //sftp://sftp_sabotage%2540uvrg.org@ssh.strato.de/surveyData.php
            if (www.isNetworkError)
            {
                Debug.Log(www.error);
            }
            else
            {
                print("else");
                string responseText = www.downloadHandler.text;
                Debug.Log("Response Text from the server = " + responseText);
            }
        }
    }
}
