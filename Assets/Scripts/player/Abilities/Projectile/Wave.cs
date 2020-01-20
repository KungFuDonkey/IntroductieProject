using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : Projectile
{
    Transform groundCheck;
    LayerMask groundMask;
    Vector3 groundCheckLift = new Vector3(0, 0.3f, 0);
    public Wave(int _id, Vector3 _spawnPosition, Quaternion _rotation, Vector3 _startDirection, int _owner)
    {
        id = _id;
        position = _spawnPosition;
        rotation = _rotation;
        startDirection = _startDirection;
        spawnPosition = _spawnPosition;
        owner = _owner;
        damage = 20;
        type = Type.water;
        speed = 20;
        groundMask = LayerMask.GetMask("Ground");
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
            groundCheck = GameManager.projectiles[id].transform.GetChild(1);
            position += (rotation * Vector3.right * speed + startDirection) * Time.deltaTime;
            if(!Physics.CheckSphere(groundCheck.position, 0.4f, groundMask))
            {
                position.y -= 0.2f;
            }
            else if (Physics.CheckSphere(groundCheck.position + groundCheckLift, 0.4f, groundMask))
            {
                position.y += 0.2f;
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
