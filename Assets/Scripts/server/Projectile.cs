using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Base class for all projectiles
public class Projectile
{
    public int id, owner;
    public float damage;
    public bool reUseAble = false;
    public Vector3 position;
    public Quaternion rotation;
    public Type type;
    protected float speed = 50, maxDistance = 150;
    protected bool destroyed = false;
    protected Vector3 startDirection, spawnPosition;

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
