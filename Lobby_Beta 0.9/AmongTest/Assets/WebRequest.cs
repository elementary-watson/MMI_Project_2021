using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class WebRequest : MonoBehaviour
{
    string gender, occupation, graduation, nationality, regular_gamer, amongus_played, playHoursPerWeek, email;
    void Start()
    {
        StartCoroutine(UploadPost(gender, occupation, graduation, nationality, regular_gamer, amongus_played, playHoursPerWeek, email));

    }

    public void SaveData(string gender, string occupation, string graduation, string nationality, string regular_gamer, string amongus_played, string playHoursPerWeek, string email)
    {
        this.gender = gender;
        this.occupation = occupation;
        this.graduation = graduation;
        this.nationality = nationality;
        this.regular_gamer = regular_gamer;
        this.amongus_played = amongus_played;
        this.playHoursPerWeek = playHoursPerWeek;
        this.email = email;
    }

    IEnumerator UploadPost(string gender, string occupation, string graduation, string nationality, string regular_gamer, string amongus_played, string playHoursPerWeek, string email)
    {

        WWWForm form = new WWWForm();
        //form.AddField("ID", "5");
        form.AddField("userID", "5");
        form.AddField("TimeStamp", "21031541");
        form.AddField("gender", gender);
        form.AddField("occupation", occupation);
        form.AddField("graduation", graduation);
        form.AddField("nationality", nationality);
        form.AddField("regular_gamer", regular_gamer);
        form.AddField("amongus_played", amongus_played);
        form.AddField("playHoursPerWeek", playHoursPerWeek);
        form.AddField("screen_width", "1920");
        form.AddField("screen_height", "1080");
        form.AddField("email", email);

        using (UnityWebRequest www = UnityWebRequest.Post("https://sabotage.uvrg.org/php/saveUser.php", form))
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
