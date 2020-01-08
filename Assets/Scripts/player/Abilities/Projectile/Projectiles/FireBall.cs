using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : ProjectileBehavior
{
    public FireBall()
    {
        speed = 35;
        maxDistance = 150;
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
        if (!destroyed)
        {
            speed += 10 * Time.deltaTime;
            controller.velocity = transform.forward * speed;
        }
    }
}
