using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DelayStartWaitingRoomController : MonoBehaviourPunCallbacks
{
    /*This object must be attached to an object
    / in the waiting room scene of your project.*/

    // photon view for sending rpc that updates the timer
    private PhotonView myPhotonView;

    // number of players in the room out of the total room size
    private int playerCount;
    private int roomSize;

    // text variables for holding the displays for the countdown timer and player count
    [SerializeField]
    private Text playerCountDisplay;
    [SerializeField]
    private Text playerListDisplay;
    [SerializeField]
    private InputField inputField;
    //countdown timer reset variables
    [SerializeField]
    private GameObject ReadyButton;


    private void Start()
    {
        //initialize variables
        myPhotonView = GetComponent<PhotonView>();
        PlayerCountUpdate();
        if (PhotonNetwork.IsMasterClient)
            ReadyButton.SetActive(true);
    }

    void PlayerCountUpdate()
    {
        // updates player count when players join the room
        // displays player count
        // triggers countdown timer
        playerCount = PhotonNetwork.PlayerList.Length;
        roomSize = PhotonNetwork.CurrentRoom.MaxPlayers;
        playerCountDisplay.text = playerCount + ":" + roomSize;
        if(PhotonNetwork.IsMasterClient)
            myPhotonView.RPC("UpdatePlayerList", RpcTarget.AllBuffered);
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        //called whenever a new player joins the room
        PlayerCountUpdate();
        //send master clients countdown timer to all other players in order to sync time.
    }

    [PunRPC]
    private void UpdatePlayerList()
    {
        string playerlist = "";
        foreach (Player p in PhotonNetwork.PlayerList)
        {
            if (p.NickName == "")
            {
                p.NickName = "player";
            }
            playerlist += p.NickName + "\n";
        }
        playerListDisplay.text = playerlist;
        Debug.Log("updated PlayerList");
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        //called whenever a player leaves the room
        PlayerCountUpdate();
    }
    void StartCharacterSelection()
    {
        //Multiplayer scene is loaded to start the game
        if (!PhotonNetwork.IsMasterClient)
            return;
        PhotonNetwork.CurrentRoom.IsOpen = false;
        PhotonNetwork.LoadLevel(2);
    }

    public void DelayCancel()
    {
        //public function paired to cancel button in waiting room scene
        PhotonNetwork.LeaveRoom();
        SceneManager.LoadScene(0);
    }

    public void OnChangeName()
    {
        Player p = PhotonNetwork.LocalPlayer;
        p.NickName = inputField.text.ToString();
        myPhotonView.RPC("UpdatePlayerList", RpcTarget.AllBuffered);
    }

    public void OnReady()
    {
        StartCharacterSelection();
    }
}
