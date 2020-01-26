using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBoost : Effect
{
    public HealthBoost(int _duration, float _dhealth, int _priority, int _key)
    {
        duration = _duration;
        dhealth = _dhealth;
        priority = _priority;
        key = _key;
        name = "health";
    }
}
