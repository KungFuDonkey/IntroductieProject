using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCircle : Projectile
{
    float timer, TIMER = 1;

    public FireCircle(int _id, Vector3 _spawnPosition, int _owner)
    {
        id = _id;
        position = new Vector3(_spawnPosition.x, _spawnPosition.y - 4, _spawnPosition.z);
        spawnPosition = _spawnPosition;
        owner = _owner;
        type = Type.fire;
        speed = 0;
    }

    public override void UpdateProjectile()
    {
        if (timer < TIMER)
        {
            timer += Time.deltaTime;
        }
        else
        {
            DestroyProjectile();
        }
        base.UpdateProjectile();
    }

    //Gives VulcanoJumping effect to the player when casting
    public override void Hit(int _id, int _type)
    {
        Server.clients[_id].player.Hit(this);
    }
}
