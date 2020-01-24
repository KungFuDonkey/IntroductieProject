using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vulcano : Projectile
{
    public bool jumping, used;
    LayerMask groundMask;

    public Vulcano(int _id, Vector3 _spawnPosition, Quaternion _rotation, Vector3 _startDirection, int _owner)
    {
        id = _id;
        position = new Vector3(_spawnPosition.x, _spawnPosition.y - 4, _spawnPosition.z);
        startDirection = _startDirection;
        spawnPosition = _spawnPosition;
        rotation = _rotation;
        owner = _owner;
        type = Type.fire;
        groundMask = LayerMask.GetMask("Ground");
        jumping = false;
        used = false;
    }

    public override void DestroyProjectile()
    {
        Debug.Log("Destroying vulcano");
        destroyed = true;
        base.DestroyProjectile();
    }

    public override void HitSelf()
    {
        if (!used)
        {
            used = true;
            Debug.Log("jumping");
            Server.clients[owner].player.status.effects.Add(new VulcanoJumping(3, owner, id));
        }
    }
}
