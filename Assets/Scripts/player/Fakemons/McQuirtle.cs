using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class McQuirtle : fakemonBehaviour
{
    public McQuirtle()
    {
        type = "grass";
        movementSpeed = 12;
        jumpspeed = 14;
        lives = 80;
    }

    [PunRPC]
    protected override void RPC_Die()
    {
        base.RPC_Die();
    }
}
