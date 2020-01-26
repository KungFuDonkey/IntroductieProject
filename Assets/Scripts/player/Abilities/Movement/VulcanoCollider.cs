using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VulcanoCollider : MonoBehaviour
{
    Animator animator;
    public float LaunchSpeed;
    public GameObject FreezeBone;
    public float timer = 2f;

    void Start()
    {
        if(Client.instance.host)
        animator = GetComponent<Animator>();
        animator.SetTrigger("Launch");
    }

    //Calls HitSelf() when the ability is used
    private void OnTriggerEnter(Collider other)
    {
        PlayerManager playerManager = other.gameObject.GetComponent<PlayerManager>();
        if (playerManager != null)
        {
            if (playerManager.id == Server.projectiles[gameObject.GetComponent<ProjectileManager>().id].owner)
            {
                Server.projectiles[gameObject.GetComponent<ProjectileManager>().id].HitSelf();
            }
        }
    }
}

