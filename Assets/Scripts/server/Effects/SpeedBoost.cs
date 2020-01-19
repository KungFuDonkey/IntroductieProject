using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoost : Effect
{
    public SpeedBoost(int _duration, float _dmovementSpeed, int _priority)
    {
        duration = _duration;
        dmovementSpeed = _dmovementSpeed;
        priority = _priority;
    }
}
