using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarSetup : MonoBehaviour
{
    private PhotonView PV;
    public int CharacterValue;
    public GameObject myCharacter;
    int spawnPicker;
    // Start is called before the first frame update
    void Start()
    {
        PV = GetComponent<PhotonView>();
        spawnPicker = Random.Range(0, GameController.GS.spawnPoints.Length);
        if (PV.IsMine)
        {
            PV.RPC("RPC_AddCharacter", RpcTarget.AllBuffered, PlayerInfo.PI.mySelectedCharacter);
        }
    }

    [PunRPC]
    void RPC_AddCharacter(int whichCharacter)
    {
        CharacterValue = whichCharacter;
        myCharacter = Instantiate(PlayerInfo.PI.allCharacters[whichCharacter], GameController.GS.spawnPoints[spawnPicker].position, GameController.GS.spawnPoints[spawnPicker].rotation, transform);
    }
}
