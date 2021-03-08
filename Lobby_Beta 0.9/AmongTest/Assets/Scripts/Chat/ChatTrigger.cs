using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatTrigger : MonoBehaviour
{
    [SerializeField] private GameObject circleTriggerElement;
    [SerializeField] private Network networkReference;
    // Start is called before the first frame update
    void Start()
    {
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Contains("Task_01"))
        {
            Debug.Log("This was a collision on 01");
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //print("Name of Object" + circleTriggerElement.gameObject.name);
        if (collision.gameObject.tag.Contains("player_all"))
        {
            Debug.Log("Task 00");
            //networkReference.callChatWindowRPC();
        }
        if(collision.gameObject.name == "normalTask_01_A")
        {

        }
        if(collision.gameObject.name == "normalTask_01_A")
        {
            Debug.Log("Task 01 by name");
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
}