using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vulcano : Projectile
{
    public bool jumping, used;
    LayerMask groundMask;
    float timer;

    public Vulcano(int _id, Vector3 _spawnPosition, Quaternion _rotation, Vector3 _startDirection, int _owner, float _timer)
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
        timer = _timer;
    }

    public override void UpdateProjectile()
    {
        timer -= Time.deltaTime;
        if(timer < 0)
        {
            DestroyProjectile();
        }
        base.UpdateProjectile();
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
            int key = Server.clients[owner].player.status.effectcount;
            Server.clients[owner].player.status.effects.Add(key,new VulcanoJumping(3, owner, id, key));
            Server.clients[owner].player.status.effectcount++;
        }
    }
}
