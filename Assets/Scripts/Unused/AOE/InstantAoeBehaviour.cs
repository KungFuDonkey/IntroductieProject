using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantAoeBehaviour : Ability
{
    protected float radius;
    protected float duration;
    protected float interval, INTERVAL;
    protected override void Start()
    {
    }

    protected override void Update()
    {
        duration -= Time.deltaTime;
        if(duration < 0)
        {
            Destroy(this.gameObject);
        }
        if(name[name.Length-1] != '*')
        {
            interval -= Time.deltaTime;
            if (interval < 0)
            {
                if (Physics.CheckSphere(this.gameObject.transform.position, radius, LayerMask.NameToLayer("ThisPlayer")) && name[name.Length - 1] != '*')
                {
                    hitPlayer();
                }
                interval = INTERVAL;
            }
        }
    }
    public void hitPlayer()
    {
        
    }
}
