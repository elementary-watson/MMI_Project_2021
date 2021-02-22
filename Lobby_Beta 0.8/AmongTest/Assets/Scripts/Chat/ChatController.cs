using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Chat;
using Photon.Pun;
using ExitGames.Client.Photon;
using System;
using TMPro;
using System.IO;

public class ChatController : MonoBehaviour, IChatClientListener
{
    [Header("Chat Interface")]
    [SerializeField] public TMP_InputField tmp_userInput;
    [SerializeField] private Transform content;
    [SerializeField] private ScrollRect scrollRect;
    [SerializeField] private GameObject chatListing;
    [SerializeField] private Network network;

    [Header("Photon Chat Logic")]
    private ChatClient chatClient;

    //ingame logic
    private string nickName;

    // Start is called before the first frame update
    void Start()
    {
        chatClient = new ChatClient(this);
        print("CHAT: chatClient created");
        ConnectToPhotonChat();
    }
    // Update is called once per frame
    void Update()
    {
        chatClient.Service();
        if (Input.GetKeyUp(KeyCode.Return)) btnSendMessage();
    }

    //Eigene Methoden 
    #region Eigene Methode
    private void ConnectToPhotonChat()
    {
        print("CHAT: Connect to Photonchat startet:");
        nickName = network.getPlayerColor();
        chatClient.AuthValues = new Photon.Chat.AuthenticationValues(nickName);
        ChatAppSettings chatSettings = PhotonNetwork.PhotonServerSettings.AppSettings.GetChatSettings();
        chatClient.ConnectUsingSettings(chatSettings);
        print("CHAT: Connect Methode wurde ausgeführt");
    }

    public void SendeDirectMessage(string recipients, string message)
    {
        chatClient.SendPrivateMessage(recipients, message);
    }

    public void btnSendMessage()
    {
        print("Current Actor id: " + network.getActorId());
        var tempUInput = tmp_userInput.GetComponent<TMP_InputField>();
        string userInputText = tempUInput.text;
        chatClient.PublishMessage("channelA", userInputText);
        tmp_userInput.GetComponent<TMP_InputField>().text = "";

    }

    public void subscribeToChannel(String channel)
    {
        // call only after Chat : OnConnected was called
        chatClient.Subscribe(channel);
    }

    private void createChatListElement(string channelName, string[] senders, object[] messages)
    {
        //Erstelle ein chat element in der Scroll View
        GameObject chatElement = Instantiate(chatListing);
        chatElement.transform.SetParent(content);
        //Update die scroll view damit Scroll view ganz nach unten aktualisiert
        Canvas.ForceUpdateCanvases();
        content.transform.parent.GetParentComponent<ScrollRect>().verticalNormalizedPosition = 0;

        string msg = messages[0].ToString();
        for (int i = 0; i < chatElement.transform.childCount; i++)
        {
            Transform currentItem = chatElement.transform.GetChild(i);
            if (currentItem.name.Equals("txt_usertext"))
            {
                chatElement.transform.GetChild(i).GetComponent<Text>().text = msg;

            }
            if (currentItem.name.Equals("r_img_player"))
            {
                string color = network.getPlayerColor();
                if (color == null) color = "Red_Char";

                print("CHAT Player-Color: " + color);
                Texture2D texture2D = new Texture2D(92, 92);                
                string filename = "Player Color/" + senders[0];

                //byte[] bytes = File.ReadAllBytes(Path.Combine(Application.persistentDataPath, filename));
                texture2D = Resources.Load<Texture2D>(filename);
                chatElement.transform.GetChild(i).GetComponent<RawImage>().texture = texture2D;//= msg;
            }
        }
    }

    #endregion

    //Interface Implementation, alles von IChatClientListener
    #region Photon Chat Callbacks
    public void DebugReturn(DebugLevel level, string message)
    {
    }

    public void OnDisconnected()
    {
        Debug.Log("CHAT: User Disconnected");
    }

    public void OnConnected()
    {
        chatClient.Subscribe(new string[] { "channelA", "channelB" });
        Debug.Log("CHAT: Connected");
        SendeDirectMessage("", "Hello");
    }

    public void OnChatStateChange(ChatState state)
    {
    }

    public void OnGetMessages(string channelName, string[] senders, object[] messages)
    {
        print("CHAT: We revieved a message");
        print(messages[0]);
        string msgs = "";
        for (int i = 0; i < senders.Length; i++)
        {
            msgs = string.Format("{0} {1}={2}", channelName, senders.Length, msgs);
        }
        Console.WriteLine("OnGetMessages: {0} ({1}) > {2}", channelName, senders.Length, msgs);
        print("CHAT OnGetMessages: \nChannelname:" + channelName +" Length: "+ senders.Length+ " Message: "+ msgs);

        createChatListElement(channelName, senders, messages);
    }



    public void OnPrivateMessage(string sender, object message, string channelName)
    {
        if (!string.IsNullOrEmpty(message.ToString()))
        {
            // "Channel name" format[Sender : Recipient]
            string[] splitName = channelName.Split(new char[] {':'});
            string senderName = splitName[0];
            if(!sender.Equals(senderName, StringComparison.OrdinalIgnoreCase))
            {
                Debug.Log($"{sender}:{message}");
            }
        }
    }
    
    public void OnSubscribed(string[] channels, bool[] results)
    {
        print("CHAT: OnSubscribed wird ausgeführt");
        int i = 0;
        print("CHAT: We subscribed to Channel/s: ");
        foreach(string item in channels)
        {
            print(item);
            print("CHAT: Results " + i + ": " + results[i]);
            i++;
        }
    }

    public void OnUnsubscribed(string[] channels)
    {
    }

    public void OnStatusUpdate(string user, int status, bool gotMessage, object message)
    {
    }

    public void OnUserSubscribed(string channel, string user)
    {
    }

    public void OnUserUnsubscribed(string channel, string user)
    {
    }
    #endregion
}
