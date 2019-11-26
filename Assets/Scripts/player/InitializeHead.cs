using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializeHead : MonoBehaviour
{
    public PhotonView PV;
    public GameObject Head;
    void Start()
    {
        if (PV.IsMine)
        {
            Head.layer = 10; 
        }
        else
        {
            Head.layer = 0;
        }
    }
}
