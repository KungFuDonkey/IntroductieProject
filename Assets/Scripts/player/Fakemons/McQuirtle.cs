using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class McQuirtle : fakemonBehaviour
{
    public McQuirtle()
    {
        type = "grass";
        movementSpeed = 12;
        jumpspeed = 14;
        lives = 80;
    }

    protected override void RPC_Die()
    {

    }
}
