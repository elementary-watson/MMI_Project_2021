using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Single_StagePanel_Script : MonoBehaviour
{
    [SerializeField] Image img_fillNextStage;
    float current;
    float maximum;
    bool canStart;
    // Start is called before the first frame update
    void Start()
    {
        setup();
    }

    public void setup()
    {
        canStart = false;
        current = 0f;
        maximum = 1f;
    }
    public void setCanStart(bool canStart)
    {
        this.canStart = canStart;
    }
    // Update is called once per frame
    void Update()
    {
        if (canStart)
        {
            current += Time.deltaTime / 2;
            if (current >= maximum)
            {
                canStart = false;
                maximum = 1f;
            }
            float normalizedValue = Mathf.Clamp(current/ maximum, 0.0f, 1.0f);
            img_fillNextStage.fillAmount = normalizedValue;
        }
    }
}
