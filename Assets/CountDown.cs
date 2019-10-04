using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CountDown : MonoBehaviour
{
    public float SecondsUntilStart = 6;
    private Animator animator;
    private Text text;
    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (SecondsUntilStart > 0)
        {
            Time.timeScale = 0;
            text.text = ((int)SecondsUntilStart).ToString();
            SecondsUntilStart -= Time.unscaledDeltaTime;
        }
        else
        {
            text.text = "";
            Time.timeScale = 1;
        }
    }
}
