using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public int Zahl1 = 1;
    public int Zahl2 = 2;
    public int Zahl3 = 3;
    public int test = 8;


    // Start is called before the first frame update
    void Start()
    {
        if(test > 8)
        {
            print(Zahl1 + Zahl2);
        }

        if (test < 8)
        {
            print(Zahl2 + Zahl3);
        }

        if (test == 8)
        {
            print(Zahl3 + Zahl1);
        }
    }

}
