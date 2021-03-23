using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class WebRequestGame : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void sendRequest(string userID, string SessionID, string TimeStamp, string Duration, string Round, string NumberPlayers, string RemaingPlayers, string avatarColor, string value, string type, string survive, string sentiment, string topic)
    {
        print(userID +" "+ SessionID + " " + TimeStamp + " " + Duration + " " + Round + " " + NumberPlayers + " " + RemaingPlayers + " " + avatarColor + " " + value + " " + type + " " + survive + " " + sentiment + " " + topic);
        
        StartCoroutine(UploadPost( userID,  SessionID,  TimeStamp,  Duration,  Round,  NumberPlayers,  RemaingPlayers,  avatarColor,  value,  type,  survive,  sentiment,  topic));
    }
    IEnumerator UploadPost(string userID, string SessionID, string TimeStamp, string Duration, string Round, string NumberPlayers, string RemaingPlayers, string avatarColor, string value, string type, string survive, string sentiment, string topic)
    {
        WWWForm form = new WWWForm();
        //form.AddField("ID", "5");
        form.AddField("userID", userID);
        form.AddField("SessionID", SessionID);
        form.AddField("TimeStamp", TimeStamp);
        form.AddField("Duration", Duration);
        form.AddField("Round", Round);
        form.AddField("NumberPlayers", NumberPlayers);
        form.AddField("RemaingPlayers", RemaingPlayers);
        form.AddField("avatarColor", avatarColor);
        form.AddField("value", value);
        form.AddField("type", type);
        form.AddField("survive", survive);
        form.AddField("sentiment", sentiment);
        form.AddField("topic", topic);

        using (UnityWebRequest www = UnityWebRequest.Post("https://sabotage.uvrg.org/php/saveGame.php", form))
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
