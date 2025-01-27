using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Count_Down : MonoBehaviour
{
    public int SecondsUntilStart = 3;
    private float Timer = 0;
    private Text text;
    // Use this for initialization
    void Start()
    {
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (SecondsUntilStart > 0)
        {
            Timer += Time.unscaledDeltaTime;
            if (Timer >= 1)
            {
                Timer--;
                SecondsUntilStart--;
            }
        }
        if (SecondsUntilStart > 0)
        {
            Time.timeScale = 0;
            text.text = SecondsUntilStart.ToString();
        }
        else
        {
            text.text = "";
            Time.timeScale = 1;
        }
    }
}
