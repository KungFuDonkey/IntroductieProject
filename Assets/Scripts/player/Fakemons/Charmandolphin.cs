using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Charmandolphin : fakemonBehaviour
{
    public Charmandolphin()
    {
        type = "water";
        movementSpeed = 10;
        jumpspeed = 10;
        lives = 100;
    }

    protected override void RPC_Die()
    {
        base.RPC_Die();
    }
}
