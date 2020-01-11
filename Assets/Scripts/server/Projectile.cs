using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile
{
    public int id;
    public Vector3 position;
    public Vector3 startDirection;
    public Quaternion rotation;
    protected float speed = 10;
    protected float maxDistance = 150;
    public float damage;
    public int owner;
    public virtual void UpdateProjectile()
    {
        ServerSend.ProjectileMove(this);
    }

    public virtual void DestroyProjectile()
    {
        ServerSend.DestroyProjectile(this);
        ServerStart.destroyId.Add(id);
    }

    public virtual void Hit(int _id, int _type)
    {
        ServerSend.Damage(_id, damage, _type);
        DestroyProjectile();
    }
}
