using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class WebRequestTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {//Weil man auf antworten des servers warten muss, wird diese Funktion genutzt
        //StartCoroutine(GetUsers());        
        //StartCoroutine(GetDate());        
        StartCoroutine(Login("red","sabotage"));        
        StartCoroutine(AddUser("pink","Admintable blabla"));        
    }

    IEnumerator GetUsers()
    {
        using(UnityWebRequest www = UnityWebRequest.Get("http://localhost/UnityBackendMMI/GetUsers.php"))
        {
            yield return www.Send();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);
                //Hier speichern wir den Request in ein Byte array. Sehr gut zum parsen in andere Formate
                byte[] results = www.downloadHandler.data;
            }
        }
    }
    IEnumerator GetDate()
    {
        using(UnityWebRequest www = UnityWebRequest.Get("http://localhost/UnityBackendMMI/GetDate.php"))
        {
            yield return www.Send();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);
                //Hier speichern wir den Request in ein Byte array. Sehr gut zum parsen in andere Formate
                byte[] results = www.downloadHandler.data;
            }
        }
    }
    IEnumerator Login(string username, string password)
    {
        WWWForm form = new WWWForm();
        // erster parameter muss genauso geschrieben werden wie in php-datei
        form.AddField("loginUser", username);
        form.AddField("loginPass", password);

        using(UnityWebRequest www = UnityWebRequest.Post("http://localhost/UnityBackendMMI/Login.php", form))
        {
            yield return www.SendWebRequest();
            
            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);
            }
        }
    }    
    IEnumerator AddUser(string username, string password)
    {
        WWWForm form = new WWWForm();
        // erster parameter muss genauso geschrieben werden wie in php-datei
        form.AddField("loginUser", username);
        form.AddField("loginPass", password);

        using(UnityWebRequest www = UnityWebRequest.Post("http://localhost/UnityBackendMMI/AddUser.php", form))
        {
            yield return www.SendWebRequest();
            
            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
