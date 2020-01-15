using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckForNotHost : MonoBehaviour
{
    private void Start()
    {
        if (Client.instance.host)
        {
            Destroy(gameObject);
        }
    }
}
