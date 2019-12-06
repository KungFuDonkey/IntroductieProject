using System.Collections;
using System.Collections.Generic;
using System.IO;
using Photon.Pun;
using UnityEngine;

public class CursorSetup : MonoBehaviour
{
    // Start is called before the first frame update
    private PhotonView PV;
    void Start()
    {
        PV = GetComponent<PhotonView>();
        if (PV.IsMine)
        {
            GameObject cursor = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Cursor"), Vector3.zero, Quaternion.identity);
        }
    }
}
