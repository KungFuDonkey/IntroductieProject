using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile
{
    public int id;
    public int projectileType;
    public Vector3 position;
    public Quaternion rotation;
    public LayerMask groundmask;
    public CharacterController controller;
    
    public Projectile(int _id, Vector3 _spawnPosition, Quaternion _spawnRotation, int _projectileType)
    {
        id = _id;
        position = _spawnPosition;
        rotation = _spawnRotation;
    }

    public void UpdateProjectile()
    {
        position.x += 10 * Time.deltaTime;
        ServerSend.ProjectileMove(this);
    }
}
