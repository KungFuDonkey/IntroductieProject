using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Spawning : MonoBehaviour
{
    int test;
    // Start is called before the first frame update
    private PhotonView PV;
    public GameObject myAvatar;
    Transform[] spawner = new Transform[4];
    public int playerCount;
    void Start()
    {
        PV = GetComponent<PhotonView>();
        for (int i = 0; i<2; i++)
        {
            for(int j = 0; j<2; j++)
            {
                spawner[2*i + j] = Resources.Load<Transform>("PlayerSpawner");
                spawner[2*i + j] = Instantiate(spawner[2*i + j], new Vector3(25 + 50 * i, 20, 25 + 50 * j), Quaternion.identity);
                spawner[2 * i + j].transform.parent = gameObject.transform;
            }
        }
    }
    public Transform[] getSpawnPoints()
    {
        return spawner;
    }

    // Update is called once per frame
}
