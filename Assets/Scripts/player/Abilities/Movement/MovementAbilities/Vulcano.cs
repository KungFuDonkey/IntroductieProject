using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vulcano : Projectile
{
    protected bool destroyed = false;
    Vector3 spawnPosition;

    public Vulcano(int _id, Vector3 _spawnPosition, Quaternion _rotation, Vector3 _startDirection, int _owner)
    {
        id = _id;
        position = new Vector3(_spawnPosition.x, _spawnPosition.y - 4, _spawnPosition.z);
        rotation = _rotation;
        startDirection = _startDirection;
        spawnPosition = _spawnPosition;
        owner = _owner;
        type = Type.fire;
    }

    public override void DestroyProjectile()
    {
        destroyed = true;
        base.DestroyProjectile();
    }
}
