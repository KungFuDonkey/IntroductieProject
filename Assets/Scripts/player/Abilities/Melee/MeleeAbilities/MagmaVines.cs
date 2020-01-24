using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagmaVines : MeleeBehaviour
{
    public LayerMask playerMask;
    Animator Vines;

    MagmaVines()
    {
        range = 2.5f;
        damage = 10;
        type = "normal";
    }

    protected override void Start()
    {
        Vines = GetComponent<Animator>();
        Vines.SetTrigger("VineAttack");
        playerMask = LayerMask.NameToLayer("ObjectWithLives");
    }

    public void VineAttackEnd()
    {
        Server.projectiles[gameObject.GetComponent<ProjectileManager>().id].DestroyProjectile();
    }

    public void onAttack()
    {
        if (Client.instance.host)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position + transform.forward * range + transform.up, 2, playerMask);
            foreach (Collider c in colliders)
            {
                PlayerManager playerManager = c.gameObject.GetComponent<PlayerManager>();
                if (playerManager != null)
                {
                    if (playerManager.id != Server.projectiles[gameObject.GetComponent<ProjectileManager>().id].owner)
                    {
                        Server.projectiles[gameObject.GetComponent<ProjectileManager>().id].Hit(playerManager.id, gameObject.GetComponent<ProjectileManager>().id);
                    }
                }
                else
                {
                    Server.projectiles[gameObject.GetComponent<ProjectileManager>().id].DestroyProjectile();
                }
            }
        }
    }
}
