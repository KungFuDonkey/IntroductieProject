using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Silence : Effect
{
    public Silence(float _duration, int _owner, int _key)
    {
        duration = _duration;
        priority = 1;
        name = "stun";
        key = _key;
        dsilenced = true;
    }
}
