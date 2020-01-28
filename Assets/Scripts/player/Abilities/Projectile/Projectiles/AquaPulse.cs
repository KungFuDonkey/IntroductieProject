using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AquaPulse : Projectile
{
    public AquaPulse(int _id, Vector3 _spawnPosition, Quaternion _rotation, Vector3 _startDirection, int _owner)
    {
        id = _id;
        position = _spawnPosition;
        rotation = _rotation;
        startDirection = _startDirection;
        spawnPosition = _spawnPosition;
        owner = _owner;
        damage = 15;
        type = Type.water;
    }

    public override void UpdateProjectile()
    {
        float distance = Vector3.Distance(spawnPosition, position);
        if (distance > maxDistance)
        {
            DestroyProjectile();
        }
        if (!destroyed)
        {
            position += (rotation * Vector3.right * speed + startDirection) * Time.deltaTime;
        }
        base.UpdateProjectile();
    }

    public override void Hit(int _id, int _type)
    {
        int effect = Server.clients[_id].player.status.effectcount;
        Server.clients[_id].player.status.effects.Add(effect, new Confusion(5, 1, effect));
        Server.clients[_id].player.status.effectcount++;
        base.Hit(_id, _type);
    }

    public override void DestroyProjectile()
    {
        destroyed = true;
        base.DestroyProjectile();
    }
}
