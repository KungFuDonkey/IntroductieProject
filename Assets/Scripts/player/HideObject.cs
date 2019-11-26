using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideObject : MonoBehaviour
{
    public PhotonView PV;
    void Start()
    {
        if (PV.IsMine)
        {
            gameObject.layer = 10; 
        }
        else
        {
            gameObject.layer = 0;
        }
    }
}
