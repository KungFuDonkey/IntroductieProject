﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    public int id;
    public void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}
