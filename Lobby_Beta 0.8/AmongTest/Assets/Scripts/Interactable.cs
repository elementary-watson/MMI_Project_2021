using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Hier (und in reset) wurde Boxcollider2d verwendet geändert auf Collider damit keine einschränkung gelegt wird
[RequireComponent(typeof(BoxCollider2D))]
public abstract class Interactable : MonoBehaviour
{
    private void Reset()
    {
        //GetComponent<BoxCollider2D>().isTrigger = true;
    }
    public abstract void Interact();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            collision.GetComponent<CharacterControl>().OpenInteractableIcon();
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            collision.GetComponent<CharacterControl>().CloseInteractableIcon();
    }
}
