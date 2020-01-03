using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantAoeBehaviour : Ability
{
    // Start is called before the first frame update
    protected float radius;
    protected float duration;
    protected float interval, INTERVAL;
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
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
        GameObject player = GameController.GS.player();
        fakemonBehaviour actionScript = player.GetComponent<fakemonBehaviour>();
        actionScript.hit(damage, type, statusEffect);
    }
}
