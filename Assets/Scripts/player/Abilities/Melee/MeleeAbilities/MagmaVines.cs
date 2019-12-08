using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagmaVines : MeleeBehaviour
{
    MagmaVines()
    {
        range = 7f;
    }
    Animator Vines;
    // Start is called before the first frame update
    protected override void Start()
    {
        Vines = GetComponent<Animator>();
        Vines.SetTrigger("VineAttack");
    }
    public void VineAttackEnd()
    {
        Destroy(gameObject);
    }
}
