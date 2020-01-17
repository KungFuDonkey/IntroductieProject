using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect
{
    public int priority;
    public float duration;
    public float dgravity, djumpspeed = 1f;
    public float dhealth = 1f;
    public float dverticalRotation = 1f;
    public float dFIRETIMER = 1f, dQTIMER = 1f, dETIMER = 1f, dmovementSpeed = 1f, drunMultiplier = 1f;
    public bool dmovable, dsilenced;

    public Effect(float _dgravity, float _djumpspeed, float _dhealth, float _dFIRETIMER, float _dQTIMER, float _dETIMER, float _dmovementSpeed, float _drunMultiplier)
    {
        djumpspeed = _djumpspeed;
        dgravity = _dgravity;
        dhealth = _dhealth;
        dFIRETIMER = _dFIRETIMER;
        dQTIMER = _dQTIMER;
        dETIMER = _dETIMER;
        dmovementSpeed = _dmovementSpeed;
        drunMultiplier = _drunMultiplier;
    }

    public virtual void UpdateEffect()
    {
        duration -= Time.deltaTime;

    }
 
    public static Effect Charmandolphin
    {
        get { return new Effect(45f, 22f, 100f, 2f, 2f, 2f, 20f, 2f); }
    }

    public static Effect Vulcasaur
    {
        get { return new Effect(45f, 22f, 100f, 2f, 2f, 2f, 20f, 2f); }
    }


    public static Effect McQuirtle
    {
        get { return new Effect(45f, 22f, 100f, 2f, 2f, 2f, 20f, 2f); }
    }
}
