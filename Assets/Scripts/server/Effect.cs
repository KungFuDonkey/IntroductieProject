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
    public float dFIRETIMER = 2f, dQTIMER = 2f, dETIMER = 2f, dmovementSpeed = 20f, drunMultiplier = 2f;
    public bool dmovable, dsilenced;

    public Effect(float _dgravity, float _djumpspeed, float _dhealth, float _dFIRETIMER, float _dQTIMER, float _dETIMER)
    {
        djumpspeed = _djumpspeed;
        dgravity = _dgravity;
        dhealth = _dhealth;
        dFIRETIMER = _dFIRETIMER;
        dQTIMER = _dQTIMER;
        dETIMER = _dETIMER;
    }

    public virtual void UpdateEffect()
    {
        duration -= Time.deltaTime;
    }

}
