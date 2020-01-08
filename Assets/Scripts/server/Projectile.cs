using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile
{
    public int id;
    public int projectileType;
    public Vector3 position;
    public Quaternion rotation;
    protected LayerMask groundMask = 9;
    protected string type = "Normal";
    protected float damage;
    protected float speed;
    protected float maxDistance;

    public virtual void UpdateProjectile()
    {
        ServerSend.ProjectileMove(this);
    }
}
