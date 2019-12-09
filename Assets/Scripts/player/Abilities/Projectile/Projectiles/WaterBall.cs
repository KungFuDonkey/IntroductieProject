using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBall : ProjectileBehavior
{
    public WaterBall()
    {
        speed = 50;
        maxDistance = 100000;
        damage = 8;
        type = "Water";
        particleTimer = 3;
    }
    protected override void Start()
    {
        base.Start();
        Debug.Log("water");
    }
    protected override void Update()
    {
        base.Update();
    }
}
