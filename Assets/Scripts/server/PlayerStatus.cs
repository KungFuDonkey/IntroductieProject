using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerStatus 
{
    public float gravity, jumpspeed = 3f;
    public float health = 100f;
    public float verticalRotation;
    public bool[] animationValues;
    public Transform groundCheck;
    public LayerMask groundmask;
    public float fireTimer = 0f, FIRETIMER = 2f, qTimer = 0f, QTIMER = 2f, eTimer = 0f, ETIMER = 2f, walkSpeed = 20f, runSpeed = 40f;
    public bool isGrounded, movable, silenced;
    public List<Effect> effects = new List<Effect>();

    public void Update(bool[] inputs)
    {
        int highestPriority = 0;
        if(effects.Count != 0)
        {
            foreach (Effect effect in effects)
            {
                effect.UpdateEffect();
                if (effect.duration <= 0)
                {
                    effects.Remove(effect);
                }
                if (effect.priority > highestPriority)
                {
                    highestPriority = effect.priority;
                }
            }
            effects = effects.OrderBy(x => x.priority).ToList();
            foreach (Effect effect in effects)
            {
                if (highestPriority == effect.priority)
                {
                    UpdateStatus(effect);
                }
                else
                    break;
            }

        }
        else
        {
            SetDefaultStatus();
        }
        
        isGrounded = Physics.CheckSphere(groundCheck.position, 2f, groundmask);
        if (isGrounded && gravity < 0)
        {
            if (inputs[4])
            {
                gravity = jumpspeed;
            }
            else
            {
                //gravity = -2;
            }
        }
        gravity -= 7 * Time.deltaTime;

        if (inputs[0] || inputs[1] || inputs[2] || inputs[3])
        {
            if (inputs[5])
            {
                animationValues[0] = false;
                animationValues[1] = true;
            }
            else
            {
                animationValues[0] = true;
                animationValues[1] = false;
            }
        }
        else
        {
            animationValues[0] = false;
            animationValues[1] = false;
        }
        if (inputs[10] || inputs[6] || inputs[7])
        {
            animationValues[3] = true;
        }
    }
    public void UpdateStatus(Effect effect)
    {
        gravity *= effect.dgravity;
        jumpspeed *= effect.djumpspeed;
        health *= effect.dhealth;
        verticalRotation *= effect.dverticalRotation;
        FIRETIMER *= effect.dFIRETIMER;
        QTIMER *= effect.dQTIMER;
        ETIMER *= effect.dETIMER;
        walkSpeed *= effect.dwalkSpeed;
        runSpeed *= effect.drunSpeed;
        if (effect.dmovable) 
            movable = false;
        if (effect.dsilenced)
            silenced = true;   
    }
    public void SetDefaultStatus()
    {

    }

}
