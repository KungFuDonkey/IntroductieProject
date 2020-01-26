using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileHit : MonoBehaviour
{
    void Start()
    {
        if (!Client.instance.host)
        {
            Destroy(this);
        }
    }

    //Determines behaviour when a projectile hits something
    private void OnTriggerEnter(Collider other)
    {
        PlayerManager playerManager = other.gameObject.GetComponent<PlayerManager>();
        if(playerManager != null)
        {
            if(playerManager.id != Server.projectiles[gameObject.GetComponent<ProjectileManager>().id].owner)
            {
                Server.projectiles[gameObject.GetComponent<ProjectileManager>().id].Hit(playerManager.id, gameObject.GetComponent<ProjectileManager>().id);
            }
            else
            {
                Server.projectiles[gameObject.GetComponent<ProjectileManager>().id].HitSelf();
            }
        }
        else
        {
            Server.projectiles[gameObject.GetComponent<ProjectileManager>().id].DestroyProjectile();
        }
    }
}
