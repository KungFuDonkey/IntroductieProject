using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextCounterJump : MonoBehaviour
{
    public static TextCounterJump instance;
    public Text TimerText;
    private float starttime;

    void Awake()
    {
        instance = this;
    }

    public void Start()
    {
        starttime = Time.time;
    }

    public void Update()
    {
        float t = Time.time - starttime;
       /* if (t > 10)
        {
            t = t - 10;
        }
        else if (t < 0)
        {
            t = t + 10;
        }*/
        float ti = 10 - t;
       /* if (ti> 10)
        {
            ti = ti - 10;
        }
        else if (ti<0)
        {
            ti = ti + 10;
        }*/
        string timer = ti.ToString("f2");
        TimerText.text = timer;
    }
}

