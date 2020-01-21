using System.Collections;
using UnityEngine;

public class Wave : Projectile
{
    Transform groundCheck;
    LayerMask groundMask;
    bool surfing;
    Vector3 groundCheckLift = new Vector3(0, 0.2f, 0);
    Vector3 correctedRotation;



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
        speed = 10;
        surfing = false;
        groundMask = LayerMask.GetMask("Ground");
    }
    public override void UpdateProjectile()
    {
        if(groundCheck == null)
        {
            groundCheck = GameManager.projectiles[id].transform.GetChild(1);
        }
        float distance = Vector3.Distance(spawnPosition, position);
        //if (distance > maxDistance)
        //{
        //    DestroyProjectile();
        //}
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
                position = GameManager.instance.players[owner].gameObject.transform.position;
                position.y -= 1f;
                correctedRotation = GameManager.instance.players[owner].gameObject.transform.rotation.eulerAngles;
                correctedRotation.y -= 90;
                rotation = Quaternion.Euler(0, correctedRotation.y, 0);
            }
        }
        base.UpdateProjectile();
    }
    public override void DestroyProjectile()
    {
        destroyed = true;
        base.DestroyProjectile();
    }

    public override void HitSelf()
    {
        Debug.Log("surfing");
        surfing = true;
        Server.clients[owner].player.status.effects.Add(new Surfing(-1));
    }
}
