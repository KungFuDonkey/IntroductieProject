using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile
{
    public int id, owner;
    public Vector3 position, startDirection, spawnPosition;
    public Quaternion rotation;
    public Type type;
    protected float speed = 50, maxDistance = 150;
    public float damage;
    protected bool destroyed = false;
    public bool reUseAble = false; 

    public virtual void UpdateProjectile()
    {
        ServerSend.ProjectileMove(this);
    }

    public virtual void DestroyProjectile()
    {
        Debug.Log($"queing {id}");
        ServerStart.destroyId.Add(id);
        ServerSend.DestroyProjectile(this);
    }

    public virtual void Hit(int _id, int _type)
    {
        Server.clients[_id].player.Hit(this);
        DestroyProjectile();
    }

    public virtual void OnEffectRemove()
    {

    }
    public virtual void HitSelf()
    {

    }
}
