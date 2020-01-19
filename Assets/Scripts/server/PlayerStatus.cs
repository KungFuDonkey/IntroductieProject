using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerStatus 
{
    public Effect defaultStatus;
    public float ySpeed;
    public float gravity = 15;
    public float health = 100, shield = 0, jumpspeed = 3, damageBoost = 1;
    public bool[] animationValues;
    public Transform groundCheck;
    public LayerMask groundmask;
    public Transform avatar;
    public float fireTimer = 0, FIRETIMER = 2, qTimer = 0, QTIMER = 2, eTimer = 0, ETIMER = 2, evolveTimer = 5, EVOLVETIMER = 10, movementSpeed = 20, runMultiplier = 2;
    public bool isGrounded, movable, silenced, invisible, alive = true;
    public Vector3 inputDirection;
    public Type type;
    public List<Effect> effects = new List<Effect>();

    public void Update(bool[] inputs)
    {
        SetStatus(defaultStatus);
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
                Debug.Log(effect.priority);
                if (highestPriority == effect.priority)
                {
                    Debug.Log("Playerstatus prior");
                    UpdateStatus(effect);
                }
                else
                    break;
            }
        }
        SetUpMovement(inputs);

    }
    public void UpdateStatus(Effect effect)
    {
        Debug.Log("Playerstatus");
        gravity *= effect.dgravity;
        jumpspeed *= effect.djumpspeed;
        health *= effect.dhealth;
        FIRETIMER *= effect.dFIRETIMER;
        QTIMER *= effect.dQTIMER;
        ETIMER *= effect.dETIMER;
        movementSpeed *= effect.dmovementSpeed;
        runMultiplier *= effect.drunMultiplier;
        if (effect.dmovable) 
            movable = false;
        if (effect.dsilenced)
            silenced = true;
        if (effect.dinvisible)
        {
            invisible = !invisible;
        }
        
    }
    public void SetStatus(Effect effect)
    {
        gravity = effect.dgravity;
        jumpspeed = effect.djumpspeed;
        health = effect.dhealth;
        FIRETIMER = effect.dFIRETIMER;
        QTIMER = effect.dQTIMER;
        ETIMER = effect.dETIMER;
        movementSpeed = effect.dmovementSpeed;
        runMultiplier = effect.drunMultiplier;
        type = effect.dType;
        if (effect.dmovable)
            movable = false;
        if (effect.dsilenced)
            silenced = true;
        if (effect.dinvisible)
            invisible = true;
    }

    public void SetUpMovement(bool[] inputs)
    {
        inputDirection = Vector3.zero;
        if (inputs[0])
        {
            inputDirection += avatar.forward;
        }
        if (inputs[1])
        {
            inputDirection -= avatar.forward;
        }
        if (inputs[2])
        {
            inputDirection -= avatar.right;
        }
        if (inputs[3])
        {
            inputDirection += avatar.right;
        }

        inputDirection *= movementSpeed * Time.deltaTime * 60;
        if (inputs[5])
        {
            inputDirection *= runMultiplier;
        }

        isGrounded = Physics.CheckSphere(groundCheck.position, 2f, groundmask);
        if (isGrounded && ySpeed < 0)
        {
            if (inputs[4])
            {
                ySpeed = jumpspeed;
            }
            else
            {
                //ySpeed = -2f;
            }
        }
        ySpeed -= gravity * Time.deltaTime;

        inputDirection.y = ySpeed;

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
    }
}
