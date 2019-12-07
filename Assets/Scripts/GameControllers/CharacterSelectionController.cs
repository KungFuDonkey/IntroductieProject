using System.Collections;
using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterSelectionController : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    PhotonView PV;
    [SerializeField]
    private Text playerCountDisplay;
    [SerializeField]
    private Text timerToStartDisplay;
    private float roomTimer = 10f;
    private bool startingGame = false;
    private int playerCount;
    private int roomSize;
    void Start()
    {
        PV = GetComponent<PhotonView>();
        if (PhotonNetwork.IsMasterClient)
        {
            PV.RPC("RPC_SyncTimer", RpcTarget.Others, roomTimer);
        }
        CreateCursor();
        playerCountUpdate();
    }

    void playerCountUpdate()
    {
        playerCount = PhotonNetwork.PlayerList.Length;
        roomSize = PhotonNetwork.CurrentRoom.MaxPlayers;
        playerCountDisplay.text = playerCount + ":" + roomSize;
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        playerCountUpdate();
    }
    void Update()
    {
        //roomTimer -= Time.deltaTime;
        if (roomTimer <= 0f)
        {
            if (startingGame)
                return;
            StartGame();
        }
        string tempTimer = string.Format("{0:00}", roomTimer);
        timerToStartDisplay.text = tempTimer;
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
    public void DelayCancel()
    {
        //public function paired to cancel button in waiting room scene
        PhotonNetwork.LeaveRoom();
        SceneManager.LoadScene(0);
    }
}
