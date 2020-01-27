using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldBoost : Effect
{
    public ShieldBoost(float _duration, float _dshield, int _priority, int _key)
    {
        duration = _duration;
        dshield = _dshield;
        priority = _priority;
        key = _key;
        name = "shield";
    }
}
