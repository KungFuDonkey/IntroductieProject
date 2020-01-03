using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AquaPulse : ProjectileBehavior
{
    public AquaPulse()
    {
        speed = 20;
        maxDistance = 80;
        damage = 15;
        type = "Water";
        particleTimer = 1;
        statusEffect = "slow";
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
