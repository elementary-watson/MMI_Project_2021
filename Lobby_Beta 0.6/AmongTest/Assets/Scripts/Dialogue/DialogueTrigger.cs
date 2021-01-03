using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    public void setTutorial()
    {
        List<string> tutorialList = new List<string>();
        tutorialList.Add("Hi");
        tutorialList.Add("This is a turorial.");
        tutorialList.Add("Thank you, have fun!");
        string[] str = tutorialList.ToArray();

        dialogue = new Dialogue { name = "Teacher", sentences = str };

        TriggerDialogue(dialogue);
    }

    public void TriggerDialogue(Dialogue dialogue)
    {

        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }
}
