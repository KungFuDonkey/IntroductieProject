using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    public int id;
    public int projectileType;
    public int owner;
    public void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}
