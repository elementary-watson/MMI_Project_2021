using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class info_panel : MonoBehaviour
{
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
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
