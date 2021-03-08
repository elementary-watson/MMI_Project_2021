using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Single_ShootPlayer_Script : MonoBehaviour
{
    [SerializeField] Image img_player;
    [SerializeField] Button btn_player;
    [SerializeField] MainSkriptGame ms_object;
    int position;
    string color;
    // Start is called before the first frame update

    public void setup(int position, string color)
    {
        this.position = position;
        this.color = color;
    }
    public void btn_switch()
    {
        ms_object.sendTarget(position);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
