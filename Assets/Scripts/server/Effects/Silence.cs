using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Silence : Effect
{
    public Silence(float _duration, bool _dsilenced , int _priority, int _key)
    {
        duration = _duration;
        priority = _priority;
        name = "stun";
        key = _key;
        dsilenced = _dsilenced;
    }
}
