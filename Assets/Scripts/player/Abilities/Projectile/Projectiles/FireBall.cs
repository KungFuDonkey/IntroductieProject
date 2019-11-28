﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : ProjectileBehavior
{
    public FireBall()
    {
        speed = 10;
        maxDistance = 10000;
        damage = 10;
        type = "Fire";
    }
    protected override void Start()
    {
        base.Start();
    }
    protected override void Update()
    {
        base.Update();
        speed += 10 * Time.deltaTime;
        controller.velocity = transform.forward * speed;
    }
}
