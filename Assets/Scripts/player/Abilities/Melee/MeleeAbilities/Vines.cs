using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vines : Projectile
{
    protected bool destroyed = false;
    Vector3 spawnPosition;

    public Vines(int _id, Vector3 _spawnPosition, Quaternion _rotation, Vector3 _startDirection, int _owner)
    {
        id = _id;
        position = _spawnPosition;
        rotation = _rotation;
        startDirection = _startDirection;
        spawnPosition = _spawnPosition;
        owner = _owner;
        damage = 15f;
        type = Type.fire;
    }

    public override void UpdateProjectile()
    {
        position = Server.clients[owner].player.avatar.position;
        rotation = Server.clients[owner].player.avatar.rotation;
        base.UpdateProjectile();
    }

    public override void DestroyProjectile()
    {
        destroyed = true;
        base.DestroyProjectile();
    }
}
