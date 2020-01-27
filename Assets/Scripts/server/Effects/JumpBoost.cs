using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpBoost : Effect
{
    public JumpBoost(float _duration, float _djumpspeed, int _priority, int _key)
    {
        duration = _duration;
        djumpspeed = _djumpspeed;
        priority = _priority;
        key = _key;
        name = "jump";
    }
}
