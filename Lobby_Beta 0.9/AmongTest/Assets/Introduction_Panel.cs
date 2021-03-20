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
    [SerializeField] Image img_info;
    bool isWhite;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ChangeColor(img_info, Color.white, Color.black, 1f));
    }

    private IEnumerator ChangeColor(Image image, Color from, Color to, float duration)
    {
        float timeElapsed = 0.0f;

        float t = 0.0f;
        while (t < 1.0f)
        {
            timeElapsed += Time.deltaTime;

            t = timeElapsed / duration;

            image.color = Color.Lerp(from, to, t);

            yield return null;
        }
        setIntroductionOff();
    }
    public void setIntroductionOff()
    {
        Introduction_Panel_Saboteur.SetActive(false);
        Introduction_Panel_Crewmate.SetActive(false);
        Main_Introduction_Panel.SetActive(false);
        _network.setPlayerMovement(true);
        timeGame_object.beginTimer(); // Timer starten
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
