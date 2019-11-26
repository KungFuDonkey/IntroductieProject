using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PhotonPlayer : MonoBehaviour
{
    private PhotonView PV;
    public GameObject myAvatar;
    // Start is called before the first frame update
    void Start()
    {
        PV = GetComponent<PhotonView>();
        int spawnPicker = Random.Range(0, GameController.GS.spawnPoints.Length);
        if (PV.IsMine)
        {
            myAvatar = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "McQuirtle"),
                GameController.GS.spawnPoints[spawnPicker].position, GameController.GS.spawnPoints[spawnPicker].rotation,0);
            myAvatar.name = "thisPlayer";
            Debug.Log("player created");
        }
    }
}
