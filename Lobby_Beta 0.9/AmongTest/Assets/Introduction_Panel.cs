using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Introduction_Panel : MonoBehaviour
{
    [SerializeField] Time_Game_Script timeGame_object;

    [SerializeField] GameObject Introduction_Panel_Saboteur;
    [SerializeField] GameObject Introduction_Panel_Crewmate;
    [SerializeField] GameObject Main_Introduction_Panel;
    [SerializeField] Network _network;
    [SerializeField] Image img_cremate;
    [SerializeField] Image img_saboteur;
    [SerializeField] Progressbar_Script prog_object;
    bool isWhite;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void setup()
    {
        Invoke("fadeScreen", 3);
    }
    public void fadeScreen()
    {
        if (_network.getIsSaboteur())
            StartCoroutine(ChangeColor(img_saboteur, Color.white, Color.black, 1f));
        else
            StartCoroutine(ChangeColor(img_cremate, Color.white, Color.black, 1f));
    }
    private IEnumerator ChangeColor(Image image, Color from, Color to, float duration)
    {
        float timeElapsed = 0.0f;
        int singleCall=0;
        float t = 0.0f;
        while (t < 1.0f)
        {
            timeElapsed += Time.deltaTime/2;

            t = timeElapsed / duration;

            image.color = Color.Lerp(from, to, t);
            if (t > 0.99f)
            {
                singleCall += 1;
                if(singleCall == 1)
                    _network.finalyIntroductionOff();
            }
            yield return null;
        }
        
    }
    public void setIntroductionOff()
    {
        Introduction_Panel_Saboteur.SetActive(false);
        Introduction_Panel_Crewmate.SetActive(false);
        Main_Introduction_Panel.SetActive(false);
        _network.setPlayerMovement(true);
        timeGame_object.setup(); // Timer starten
        prog_object.setup();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
