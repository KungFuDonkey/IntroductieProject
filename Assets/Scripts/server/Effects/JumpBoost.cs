using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpBoost : Effect
{
    
    public JumpBoost(int _duration, float _djumpspeed, int _priority)
    {
        duration = _duration;
        djumpspeed = _djumpspeed;
        priority = _priority;
    }

}
