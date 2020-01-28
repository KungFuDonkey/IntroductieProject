using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stun : Effect
{
    Player player;
    public Stun(float _duration, int _owner, int _key)
    {
        duration = _duration;
        player = Server.clients[_owner].player;
        priority = 1;
        name = "stun";
        key = _key;
    }

    public override Vector3 SetUpMovement(PlayerStatus status, bool[] inputs)
    {
        status.inputDirection = Vector3.zero;
        return status.inputDirection;
    }
}
