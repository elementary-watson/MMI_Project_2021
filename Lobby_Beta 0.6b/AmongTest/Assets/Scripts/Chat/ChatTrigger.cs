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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Hello");
        networkReference.callChatWindowRPC();
    }
    // Update is called once per frame
    void Update()
    {

        /*if (Input.touchCount == 1)
        {
            Vector3 wp = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
            Vector2 touchPos = new Vector2(wp.x, wp.y);
            if (startChatElement.GetComponentInChildren<Collider2D>() == Physics2D.OverlapPoint(touchPos))
            {
                chatWindow.SetActive(true);
            };
        }*/
        /*if (Input.touchCount > 0)
        {
            Vector3 wp = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
            if (GetComponent<Collider2D>().OverlapPoint(wp))
            {
                //your code
                Debug.Log("Hello");
            }
        }*/
    }
}
