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
    object[] inputobject;
    fakemonBehaviour script;
    // Start is called before the first frame update
    void Start()
    {
        inputobject = new object[4];
        PV = GetComponent<PhotonView>();
        spawnPicker = Random.Range(0, GameController.GS.spawnPoints.Length);
        if (PV.IsMine)
        {
            PV.RPC("RPC_AddCharacter", RpcTarget.AllBuffered, PlayerInfo.PI.mySelectedCharacter);
            myCharacter = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", CharacterName), GameController.GS.spawnPoints[spawnPicker].position, GameController.GS.spawnPoints[spawnPicker].rotation, 0);
            script = myCharacter.GetComponentInChildren<fakemonBehaviour>();
        }
    }
    private void Update()
    {
        if (PV.IsMine)
        {
            inputobject[0] = Input.GetAxis("Horizontal");
            inputobject[1] = Input.GetAxis("Vertical");
            inputobject[2] = Input.GetKey(KeyCode.LeftShift);
            inputobject[3] = Input.GetAxis("Jump");
            /*
            inputobject[5] = Input.GetMouseButton(0);
            inputobject[6] = Input.GetKey(KeyCode.E);
            inputobject[7] = Input.GetKey(KeyCode.Q);
            inputobject[8] = Input.GetKey(KeyCode.V);
            */
            script.PV.RPC("recieveInput", RpcTarget.MasterClient, inputobject);
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
