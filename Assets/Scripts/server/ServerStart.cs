using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerStart : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Server.Start(50, 26950);
    }

    void Update()
    {
        foreach (ServerClient _client in Server.clients.Values)
        {
            if (_client.player != null)
            {
                _client.player.UpdatePlayer();
            }
        }
    }
}
