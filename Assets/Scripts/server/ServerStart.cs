using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerStart : MonoBehaviour
{
    // Start the server
    void Start()
    {
        Server.Start(50, 26950);
    }

    //let the server run on fixed ticks
    void FixedUpdate()
    {
        foreach (ServerClient _client in Server.clients.Values)
        {
            if (_client.player != null)
            {
                _client.player.UpdatePlayer();
            }
        }
        foreach(Projectile _projectile in Server.projectiles.Values)
        {
            if(_projectile != null)
            {
                _projectile.UpdateProjectile();
            }
        }
    }
}
