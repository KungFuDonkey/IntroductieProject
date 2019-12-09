using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : ProjectileBehavior
{
    public FireBall()
    {
        speed = 20;
        maxDistance = 10000;
        damage = 10;
        type = "Fire";
        particleTimer = 1;
    }
    protected override void Start()
    {
        base.Start();
    }
    protected override void Update()
    {
        base.Update();
        if (!destroyed)
        {
            speed += 10 * Time.deltaTime;
            controller.velocity = transform.forward * speed;
        }
    }
}
