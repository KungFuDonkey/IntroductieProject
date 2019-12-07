using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagmaVines : MeleeBehaviour
{
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
