using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect 
{
    public int priority;
    public float duration;
    public float dgravity, djumpspeed = 3f;
    public float dhealth = 100f;
    public float dverticalRotation;
    public float dFIRETIMER = 2f, dQTIMER = 2f, dETIMER = 2f, dwalkSpeed = 20f, drunSpeed = 40f;
    public bool dmovable, dsilenced;

    public virtual void UpdateEffect()
    {
        duration -= Time.deltaTime;

    }

}
