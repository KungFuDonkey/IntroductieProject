﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturningBall : Projectile
{
    protected bool returning = false;

    public ReturningBall(int _id, Vector3 _spawnPosition, Quaternion _rotation, Vector3 _startDirection, int _owner)
    {
        id = _id;
        position = _spawnPosition;
        rotation = _rotation;
        startDirection = _startDirection;
        spawnPosition = _spawnPosition;
        owner = _owner;
        damage = 8;
        type = Type.water;
        speed = 60;
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
            if (distance > maxDistance * 0.66 && !returning)
            {
                returning = true;
            }

            if (returning)
            {
                position -= (rotation * Vector3.forward * speed + startDirection) * Time.deltaTime;
            }
            else
            {
                position += (rotation * Vector3.forward * speed + startDirection) * Time.deltaTime;
            }

            if (returning && distance < 0.5f)
            {
                DestroyProjectile();
            }
        }
        base.UpdateProjectile();
    }

    public override void DestroyProjectile()
    {
        destroyed = true;
        base.DestroyProjectile();
    }
}
