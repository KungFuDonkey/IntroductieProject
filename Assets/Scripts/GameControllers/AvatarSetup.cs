using Photon.Pun;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

public class AvatarSetup : MonoBehaviour
{
    private PhotonView PV;
    public int CharacterValue;
    public GameObject myCharacter;
    int spawnPicker;
    string CharacterName; 

    // Start is called before the first frame update
    void Start()
    {
        PV = GetComponent<PhotonView>();
        spawnPicker = Random.Range(0, GameController.GS.spawnPoints.Length);
        if (PV.IsMine)
        {
            PV.RPC("RPC_AddCharacter", RpcTarget.AllBuffered, PlayerInfo.PI.mySelectedCharacter);
            myCharacter = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", CharacterName), GameController.GS.spawnPoints[spawnPicker].position, GameController.GS.spawnPoints[spawnPicker].rotation, 0);
        }
    }

    [PunRPC]
    void RPC_AddCharacter(int whichCharacter)
    {
        CharacterValue = whichCharacter;
        switch (whichCharacter)
        {
            case 0:
                CharacterName = "CharmandolphinAvatar";
                break; 
            case 1:
                CharacterName = "McQuirtleAvatar";
                break;
            case 2:
                CharacterName = "VulcasaurAvatar";
                break;
        }
    }
}
