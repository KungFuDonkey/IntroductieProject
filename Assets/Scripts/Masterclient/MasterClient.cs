using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class MasterClient : MonoBehaviourPun
{
    // Start is called before the first frame update
    PhotonView PV;
    void Start()
    {
        PV = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
