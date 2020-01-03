using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


public class CharmanDolphin : fakemonBehaviour
{
    public CharmanDolphin()
    {
        type = "water";
        movementSpeed = 10;
        jumpspeed = 10;
        lives = 100;
    }

    [PunRPC]
    protected override void RPC_Die()
    {
        base.RPC_Die();
    }
}
