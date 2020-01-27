using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invisible : Effect
{
    public Invisible(float _duration, bool _dinvisible, int _priority,int _key)
    {
        duration = _duration;
        dinvisible = _dinvisible;
        priority = _priority;
        key = _key;
        name = "invisible";
    }
}
