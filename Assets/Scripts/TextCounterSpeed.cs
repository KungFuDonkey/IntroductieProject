using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextCounterSpeed : MonoBehaviour
{
    public static TextCounterSpeed instance;

    void Awake()
    {
        instance = this;
    }

    public Text TimerText;
    private float starttime;




    // Start is called before the first frame update
    public void Start()
    {

        starttime = Time.time;

    }

    // Update is called once per frame
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
