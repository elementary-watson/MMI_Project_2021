using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Summary_Panel_Script : MonoBehaviour
{
    [SerializeField] GameObject Summary_Panel;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("turnmeoff", 5);
    }
    public void turnmeoff()
    {
        Summary_Panel.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
