using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBall : Projectile
{

    protected bool destroyed = false;
    public WaterBall(int _id, Vector3 _spawnPosition, Quaternion _rotation, Vector3 _startDirection)
    {
        id = _id;
        projectileType = 1;
        position = _spawnPosition;
        rotation = _rotation;
        speed = 30;
        maxDistance = 150;
        damage = 8;
        type = "Water";
        startDirection = _startDirection;
    }
    public override void UpdateProjectile()
    {
        if (!destroyed)
        {
            position += (rotation * Vector3.forward * speed + startDirection * 20) * Time.deltaTime;
        }
        base.UpdateProjectile();
    }
}
