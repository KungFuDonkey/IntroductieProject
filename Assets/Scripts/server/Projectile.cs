using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile
{
    public int id;
    public Vector3 position;
    public Vector3 startDirection;
    public Quaternion rotation;
    protected LayerMask groundMask = 9;

    public virtual void UpdateProjectile()
    {
        ServerSend.ProjectileMove(this);
    }
    public virtual void DestroyProjectile()
    {
        ServerSend.DestroyProjectile(this);
        ServerStart.destroyId.Add(id);
    }
}
