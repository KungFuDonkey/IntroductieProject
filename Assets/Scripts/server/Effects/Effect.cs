using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect
{
    public int key;
    public string name;
    public int priority = 50;
    public float duration;
    public float dgravity = 1, djumpspeed = 1;
    public float dverticalRotation = 1;
    public float dFIRETIMER = 1, dQTIMER = 1, dETIMER = 1, dEVOLVETIMER, dmovementSpeed = 1, drunMultiplier = 1;
    public bool dmovable, dsilenced, dinvisible;
    public float dhealth = 1, dshield = 1;
    public Type dType;

    public virtual void UpdateEffect()
    {
        duration -= Time.deltaTime;
    }

    public Effect(float _dgravity = 1f, float _djumpspeed = 1f, float _dhealth = 1f, float _dshield = 1f, float _dFIRETIMER = 2f, float _dQTIMER = 2f, float _dETIMER = 2f, float _dmovementSpeed = 20f, float _drunMultiplier = 2f, Type _dType = Type.noType, int _key = -1)
    {
        djumpspeed = _djumpspeed;
        dgravity = _dgravity;
        dhealth = _dhealth;
        dshield = _dshield;
        dFIRETIMER = _dFIRETIMER;
        dQTIMER = _dQTIMER;
        dETIMER = _dETIMER;
        dmovementSpeed = _dmovementSpeed;
        drunMultiplier = _drunMultiplier;
        dType = _dType;
        key = _key;
        name = "default";
        Debug.Log(dmovementSpeed + "effect");
    }

    public virtual Vector3 SetUpMovement(PlayerStatus status, bool[] inputs)
    {
        status.inputDirection = Vector3.zero;
        if (inputs[0])
        {
            status.inputDirection += status.avatar.forward;
        }
        if (inputs[1])
        {
            status.inputDirection -= status.avatar.forward;
        }
        if (inputs[2])
        {
            status.inputDirection -= status.avatar.right;
        }
        if (inputs[3])
        {
            status.inputDirection += status.avatar.right;
        }

        status.inputDirection *= dmovementSpeed * Time.deltaTime * 60;
        if (inputs[5])
        {
            status.inputDirection *= drunMultiplier;
        }

        status.isGrounded = Physics.CheckSphere(status.groundCheck.position, 2f, status.groundmask);
        if (status.isGrounded && status.ySpeed < 0)
        {
            if (inputs[4])
            {
                status.ySpeed = status.jumpspeed;
            }
            else
            {
                //ySpeed = -2f;
            }
        }
        status.ySpeed -= status.gravity * Time.deltaTime;
        status.inputDirection.y = status.ySpeed;

        return status.inputDirection;
    }

    public virtual bool[] SetUpAnimations(PlayerStatus status, bool[] inputs)
    {
        if (inputs[0] || inputs[1] || inputs[2] || inputs[3])
        {
            if (inputs[5])
            {
                status.animationValues[0] = false;
                status.animationValues[1] = true;
            }
            else
            {
                status.animationValues[0] = true;
                status.animationValues[1] = false;
            }
        }
        else
        {
            status.animationValues[0] = false;
            status.animationValues[1] = false;
        }
        return status.animationValues;
    }

    public static Effect Charmandolphin
    {
        get { return new Effect(45f, 22f, 100f, 0f, 2f, 2f, 2f, 20f, 2f, Type.water, 0); }
    }

    public static Effect Vulcasaur
    {
        get { return new Effect(45f, 22f, 100f, 0f, 2f, 2f, 2f, 20f, 2f, Type.fire, 0); }
    }

    public static Effect McQuirtle
    {
        get { return new Effect(45f, 22f, 100f, 0f, 2f, 2f, 2f, 20f, 2f, Type.grass, 0); }
    }
}

