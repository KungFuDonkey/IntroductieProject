using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stun : Effect
{
    public Stun(float _duration, int _priority, int _key)
    {
        duration = _duration;
        priority = _priority;
        name = "stun";
        key = _key;
    }

    public override Vector3 SetUpMovement(PlayerStatus status, bool[] inputs)
    {
        status.inputDirection = Vector3.zero;
        return status.inputDirection;
    }
}
