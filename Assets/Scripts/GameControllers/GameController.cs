using Photon.Pun;
using System.IO;
using UnityEngine;

public class GameController : MonoBehaviour
{
    // This script will be added to any multiplayer scene
    public Transform[] spawnPoints = new Transform[4];
    void Start()
    {
        for (int i = 0; i < 2; i++)
        {
            for (int j = 0; j < 2; j++)
            {
                spawnPoints[2 * i + j] = Resources.Load<Transform>("PlayerSpawner");
                spawnPoints[2 * i + j] = Instantiate(spawnPoints[2 * i + j], new Vector3(25 + 50 * i, 50, 25 + 50 * j), Quaternion.identity);
                spawnPoints[2 * i + j].transform.parent = gameObject.transform;
            }
        }
        CreatePlayer(); //Create a networked player object for each player that loads into the multiplayer scenes.
    }
    //hi
    private void CreatePlayer()
    {
        Debug.Log("Creating Player");
        PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PhotonNetworkPlayer"), Vector3.zero, Quaternion.identity);
    }
    public static GameController GS;
    private void OnEnable()
    {
        if (GameController.GS == null)
        {
            GameController.GS = this;
        }
    }

}
