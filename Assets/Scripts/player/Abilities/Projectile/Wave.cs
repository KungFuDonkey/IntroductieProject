using System.Collections;
using UnityEngine;

public class Wave : Projectile
{
    int effect;
    float distance = 0;
    bool surfing;
    Vector3 groundCheckLift = new Vector3(0, 0.2f, 0), correctedRotation;
    Transform groundCheck;
    LayerMask groundMask;
    Charmandolphin player;

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
        speed = 40f;
        surfing = false;
        reUseAble = true;
        groundMask = LayerMask.GetMask("Ground");
        player = Server.clients[_owner].player as Charmandolphin;
    }

    public override void UpdateProjectile()
    {
        if(groundCheck == null)
        {
            try
            {
                groundCheck = GameManager.projectiles[id].transform.GetChild(1);
            }
            catch
            {
                Debug.Log("not found");
                if (!player.status.alive)
                {
                    DestroyProjectile();
                }
                return;
            }
        }
        if (position.y < -20)
        {
            DestroyProjectile();
        }
        if (!destroyed)
        {
            if (!surfing)
            {
                position += (rotation * Vector3.right * speed + startDirection) * Time.deltaTime;
                if (!Physics.CheckSphere(groundCheck.position, 0.4f, groundMask))
                {
                    position.y -= 0.2f;
                }
                else if (Physics.CheckSphere(groundCheck.position + groundCheckLift, 0.4f, groundMask))
                {
                    position.y += 0.2f;
                }
            }
            else
            {
                reUseAble = false;
                position = GameManager.instance.players[owner].gameObject.transform.position;
                position.y -= 1f;
                correctedRotation = GameManager.instance.players[owner].gameObject.transform.rotation.eulerAngles;
                correctedRotation.y -= 90;
                rotation = Quaternion.Euler(0, correctedRotation.y, 0);
            }
        }
        distance += speed * Time.deltaTime; 
        if(distance > maxDistance)
        {
            DestroyProjectile();
        }
        base.UpdateProjectile();
    }
    public override void DestroyProjectile()
    {
        Debug.Log("destroying wave");
        destroyed = true;
        if (surfing)
        {
            Debug.Log($"removing effect: {effect}");
            Server.clients[owner].player.status.effects.Remove(effect);
            player.surfing = false;
        }
        base.DestroyProjectile();
    }

    public override void OnEffectRemove()
    {
        surfing = false;
        reUseAble = true;
    }

    public override void HitSelf()
    {
        if (!surfing)
        {
            Debug.Log("surfing");
            surfing = true;
            effect = Server.clients[owner].player.status.effectcount;
            Server.clients[owner].player.status.effects.Add(effect, new Surfing(-1, owner, id, effect));
            Server.clients[owner].player.status.effectcount++;
        }
    }
}
