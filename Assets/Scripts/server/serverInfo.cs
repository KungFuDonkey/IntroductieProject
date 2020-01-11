using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.UI;
public class serverInfo : MonoBehaviour
{
    public Text textobject;
    private void Start()
    {

        string externalip = new WebClient().DownloadString("http://icanhazip.com");
        textobject.text = externalip;
    }

}
