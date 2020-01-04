using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class NetworkController : MonoBehaviour
{
    [SerializeField]
    private int waitingRoomSceneIndex;
    public GameObject PlayButton;

    public void JoinServer()
    {
        Client.instance.ConnectToServer();
        SceneManager.LoadScene(waitingRoomSceneIndex);
    }
}
