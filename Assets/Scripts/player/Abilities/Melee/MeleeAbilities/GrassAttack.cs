using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassAttack : MeleeBehaviour
{
    GrassAttack()
    {
        range = 4f;
        damage = 15;
        type = "Grass";
    }
    Animator melee;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }
}
