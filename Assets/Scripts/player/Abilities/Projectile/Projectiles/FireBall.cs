using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : ProjectileBehavior
{
    public FireBall()
    {
        speed = 35;
        maxDistance = 150;
        damage = 10;
        type = "Fire";
        particleTimer = 1;
    }
    protected override void Start()
    {
        base.Start();
        FindObjectOfType<AudioManager>().Play("Fire");
    }
    protected override void Update()
    {
        base.Update();
        if (!destroyed)
        {
            speed += 10 * Time.deltaTime;
            controller.velocity = transform.forward * speed;
        }
    }
    protected override void destroy()
    {
        base.destroy();
        FindObjectOfType<AudioManager>().Stop("Fire");
    }
}
