using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Vulcasaur : fakemonBehaviour
{
    public Vulcasaur()
    {
        type = "fire";
        movementSpeed = 7;
        jumpspeed = 10;
        lives = 200;
    }

    [PunRPC]
    protected override void RPC_Die()
    {
        base.RPC_Die();
    }
}
