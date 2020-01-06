using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vulcasaur : fakemonBehaviour
{
    public Vulcasaur()
    {
        type = "fire";
        movementSpeed = 7;
        jumpspeed = 10;
        lives = 200;
    }

    protected override void RPC_Die()
    {
    }
}
