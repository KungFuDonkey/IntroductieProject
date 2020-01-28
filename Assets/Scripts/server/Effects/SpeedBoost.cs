using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoost : Effect
{
    public SpeedBoost(float _duration, float _dmovementSpeed, int _priority, int _key)
    {
        duration = _duration;
        dmovementSpeed = _dmovementSpeed;
        priority = _priority;
        key = _key;
        name = "speedBoost";
    }
}
