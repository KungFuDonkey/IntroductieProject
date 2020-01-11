using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBall : Projectile
{
    protected bool destroyed = false;
    Vector3 spawnPosition;
    static float speed = 10;
    static float maxDistance = 150;
    static float damage = 8;
    static string type = "Water";
    static int projectileType = 1;
    public WaterBall(int _id, Vector3 _spawnPosition, Quaternion _rotation, Vector3 _startDirection)
    {
        id = _id;
        position = _spawnPosition;
        rotation = _rotation;
        startDirection = _startDirection;
        spawnPosition = _spawnPosition;
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
            position += (rotation * Vector3.forward * speed + startDirection) * Time.deltaTime;
        }
        base.UpdateProjectile();
    }
    public override void DestroyProjectile()
    {
        destroyed = true;
        base.DestroyProjectile();
    }
}
