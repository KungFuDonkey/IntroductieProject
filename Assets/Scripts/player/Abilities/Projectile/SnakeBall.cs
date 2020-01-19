using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeBall : Projectile
{
    protected bool returning = false;
    float snakeTimer, sin;

    public SnakeBall(int _id, Vector3 _spawnPosition, Quaternion _rotation, Vector3 _startDirection, int _owner)
    {
        id = _id;
        position = _spawnPosition;
        rotation = _rotation;
        startDirection = _startDirection;
        spawnPosition = _spawnPosition;
        owner = _owner;
        damage = 8;
        type = Type.water;
        speed = 30;
    }
    public override void UpdateProjectile()
    {
        float distance = Vector3.Distance(spawnPosition, position);
        if (distance > maxDistance )
        {
            DestroyProjectile();
        }
        if (!destroyed)
        {
            position += (rotation * Vector3.forward * speed + startDirection) * Time.deltaTime;
            snakeTimer += Time.deltaTime;
            if (snakeTimer < 0.25)
            {
                sin = Mathf.Sin(snakeTimer * 360) * 10;
            }
            else
            {
                sin = Mathf.Sin(snakeTimer * 360) * 20;
            }
            position += (rotation * Vector3.right * sin + startDirection) * Time.deltaTime;
        }
        base.UpdateProjectile();
    }
    public override void DestroyProjectile()
    {
        destroyed = true;
        base.DestroyProjectile();
    }
}
