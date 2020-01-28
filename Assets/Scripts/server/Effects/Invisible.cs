using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invisible : Effect
{
    public Invisible(float _duration, bool _dinvisible, int _priority,int _key)
    {
        duration = _duration;
        priority = _priority;
        name = "invisible";
        key = _key;
        dinvisible = _dinvisible;
    }
}
