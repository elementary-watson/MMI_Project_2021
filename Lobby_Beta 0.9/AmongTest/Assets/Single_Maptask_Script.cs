using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Single_Maptask_Script : Interactable
{
    [SerializeField] Main_Console_Script mcs_object;
    [SerializeField] string mytag;
    [SerializeField] SpriteRenderer the_active_state;
    [SerializeField] bool isInteractable;
    [SerializeField] GameObject myPanel;
    public override void Interact()
    {
        if (isInteractable)
        {
            myPanel.SetActive(true);
            the_active_state.enabled = false;
            isInteractable = false;
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isInteractable)
        {
            print("PLayer found in task");
            if (collision.CompareTag("Player"))
                collision.GetComponent<CharacterControl>().OpenInteractableIcon();
        }
       
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (isInteractable)
        {
            if (collision.CompareTag("Player"))
                collision.GetComponent<CharacterControl>().CloseInteractableIcon();
        }
    }
    public void setIsInteractable(bool isInteractable)
    {
        the_active_state.enabled = true;
        this.isInteractable = isInteractable;
    }
    // Start is called before the first frame update
    void Start()
    {
        mytag = GetComponent<BoxCollider2D>().tag;
        the_active_state.enabled = false;
    }
    public string getMyTag() {return mytag;}
    // Update is called once per frame
    void Update()
    {
        
    }
}
