using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    public void setTutorial()
    {
        List<string> tutorialList = new List<string>();
        tutorialList.Add("This is a turorial.");
        tutorialList.Add("WHO IS IT ist ein Spiel der Teamarbeit und des Verrats. Spieler sind entweder Mannschaftskameraden oder Saboteur");
        tutorialList.Add("Spielen Sie mit 4 oder bis zu 10 Spielern online");
        tutorialList.Add("Spieler haben eine persönliche Karte, um sich auf der Karte zurechtzufinden");
        tutorialList.Add("Mitspieler: \n1. Erledige Aufgaben, um die Anzeige zu füllen \n2. Auf die Sabotage des Saboteurs reagieren \n3. Rufe ein Notfalltreffen ein, wenn der Saboteur ermittelt wurde");
        tutorialList.Add("Saboteur: \n1. Mischen Sie sich unter die Mitspieler \n2. Gib vor Aufgaben zu erledigen \n3. Sabotiere die Aufgaben, um Chaos zu verursachen");
        tutorialList.Add("Während des Spielens ist es nicht erlaubt zu Reden, um das Spiel zwischen Mitspielern und Saboteur fair zu halten");
        tutorialList.Add("Wenn jemand ein Notfalltreffen einberuft, versammeln sich die Mitspieler/Saboteur, um zu besprechen, was sie über den Saboteur wissen. Die Spieler können jetzt offen darüber sprechern, wer der mögliche Saboteur ist und welche informationen er hat. Der Saboteur versucht seine Unschuld zu beweisen oder ein anderen Mitspieler zu beschuldigen.");
        tutorialList.Add("Stimmen Sie nach der Diskussion ab, wer Ihrer Meinung nach der Saboteur ist. Wer die Mehrheit der Stimmen erhält, wird aus der Gruppe ausgeschlossen. Stimmen sie also mit Bedacht ab. Wenn Sie nicht über genügend Informationen verfügen und sich mit den Mitspielern besprochen haben, können Sie auch die Abstimmung überspringen, um zu vermeiden, dass jemand Unschuldiges herausgeworfen wird und weitere Informationen sammeln.");

        string[] str = tutorialList.ToArray();

        dialogue = new Dialogue { name = "Tutorial", sentences = str };

        TriggerDialogue(dialogue);
    }

    public void TriggerDialogue(Dialogue dialogue)
    {

        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }
}
