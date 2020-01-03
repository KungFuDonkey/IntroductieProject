using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBall : ProjectileBehavior
{
    public WaterBall()
    {
        speed = 30;
        maxDistance = 150;
        damage = 8;
        type = "Water";
        particleTimer = 3;
    }
    protected override void Start()
    {
        base.Start();
    }
    protected override void Update()
    {
        base.Update();
    }
}
