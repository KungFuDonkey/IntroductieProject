using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invisible : Effect
{

    public Invisible(int _duration, bool _dinvisible, int _priority)
    {
        duration = _duration;
        dinvisible = _dinvisible;
        priority = _priority;
    }

}
