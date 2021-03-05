using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [Header("Dialogue Window")]
    public Image imgDialogueBox;
    public Text txtTutorialName;
    public Text txtTutorialMessages;
    public Button btnContinue;
    //good practice too set a type <string>
    private Queue<string> sentences;

    private void Awake()
    {
        imgDialogueBox.gameObject.SetActive(false);
    }

    void Start()
    {
        sentences = new Queue<string>();
    }
    //Queue leeren und mit neuem dialog initialisieren
    public void StartDialogue(Dialogue dialogue)
    {
        Debug.Log("Start Conversation with " + dialogue.name);
        sentences.Clear();

        foreach (string item in dialogue.sentences)
        {
            sentences.Enqueue(item);
        }
        txtTutorialName.text = dialogue.name;
        imgDialogueBox.gameObject.SetActive(true);
        DisplayNextSentence();
    }
    public void DisplayNextSentence()
    {
        if (sentences.Count == 1)
        {
            btnContinue.GetComponentInChildren<Text>().text = "Exit";
        }
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        string nextSentence = sentences.Dequeue();
        txtTutorialMessages.text = nextSentence;
        print(nextSentence);
    }
    void EndDialogue()
    {
        print("End of conversation");
        imgDialogueBox.gameObject.SetActive(false);
        btnContinue.GetComponentInChildren<Text>().text = "Continue";
    }
}
