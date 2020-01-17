using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect
{
    public int priority;
    public float duration;
    public float dgravity = 1f, djumpspeed = 1f;
    public float dverticalRotation = 1f;
    public float dFIRETIMER = 1f, dQTIMER = 1f, dETIMER = 1f, dmovementSpeed = 1f, drunMultiplier = 1f;
    public bool dmovable, dsilenced, dinvisible;
    public float dhealth = 1f, dshield = 1f;
    public Type dType;

    public virtual void UpdateEffect()
    {
        duration -= Time.deltaTime;
    }

    public void SetValues(float _dgravity, float _djumpspeed, float _dhealth, float _dFIRETIMER, float _dQTIMER, float _dETIMER, float _dmovementSpeed)
    {
        dgravity = _dgravity;
        djumpspeed = _djumpspeed;
        dhealth = _dhealth;
        dFIRETIMER = _dFIRETIMER;
        dQTIMER = _dQTIMER;
        dETIMER = _dQTIMER;
        dmovementSpeed = _dmovementSpeed;
    }
}


 
  /* public static Effect Charmandolphin
    {
        get { return new Effect(45f, 22f, 100f, 2f, 2f, 2f, 20f, 2f, Type.water); }
    }

    public static Effect Vulcasaur
    {
        get { return new Effect(45f, 22f, 100f, 2f, 2f, 2f, 20f, 2f, Type.fire); }
    }


    public static Effect McQuirtle
    {
        get { return new Effect(45f, 22f, 100f, 2f, 2f, 2f, 20f, 2f, Type.grass); }
    }
    */
