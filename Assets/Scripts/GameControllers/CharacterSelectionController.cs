using System.Collections;
using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CharacterSelectionController : MonoBehaviour
{
    // Start is called before the first frame update
    PhotonView PV;
    private float roomTimer = 10f;
    private bool startingGame = false;
    void Start()
    {
        PV = GetComponent<PhotonView>();
        if (PhotonNetwork.IsMasterClient)
        {
            PV.RPC("RPC_SyncTimer", RpcTarget.Others, roomTimer);
        }
        CreateCursor();
    }

    // Update is called once per frame
    void Update()
    {
        roomTimer -= Time.deltaTime;
        /*
        if (roomTimer <= 0f)
        {
            if (startingGame)
                return;
            StartGame();
        }
        */
    }
    private void CreateCursor()
    {
        GameObject cursor = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PhotonNetworkCursor"), Vector3.zero, Quaternion.identity);
        cursor.transform.parent = this.transform;
    }
    [PunRPC]
    private void RPC_SyncTimer(float timeIn)
    {
        roomTimer = timeIn;
    }

    void StartGame()
    {
        startingGame = true;
        if (!PhotonNetwork.IsMasterClient)
            return;
        PhotonNetwork.LoadLevel(3);
    }
}
