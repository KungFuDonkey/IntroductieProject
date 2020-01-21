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
    public LayerMask groundmask = GameManager.instance.groundMask;
    public Transform avatar;
    public float fireTimer = 0, FIRETIMER = 2, qTimer = 0, QTIMER = 2, eTimer = 0, ETIMER = 2, evolveTimer = 5, EVOLVETIMER = 10, movementSpeed = 20, runMultiplier = 2;
    public bool isGrounded, movable, silenced, invisible, alive = true;
    public Vector3 inputDirection;
    public Type type;
    public List<Effect> effects = new List<Effect>();

    public void Update(bool[] inputs, Transform _avatar)
    {
        SetStatus(defaultStatus, _avatar);
        int strongestPriority = 100;
        if (effects.Count != 0)
        {
            for (int i = effects.Count -1; i >= 0; i--)
            {
                effects[i].UpdateEffect();
                if (effects[i].duration == -1)
                {
                    
                }
                else if (effects[i].duration <= 0)
                {
                    effects.Remove(effects[i]);
                }
                if (effects[i].priority < strongestPriority)
                {
                    strongestPriority = effects[i].priority;
                }
            }

            effects = effects.OrderBy(x => x.priority).ToList();
            foreach (Effect effect in effects)
            {
                UpdateStatus(effect);

                if (strongestPriority == effect.priority)
                {
                    SetUpMovement(inputs, effect);
                    break;
                }
            }
        }
        else
        {
            SetUpMovement(inputs, defaultStatus);
        }
    }
    public void UpdateStatus(Effect effect)
    {
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
    public void SetStatus(Effect effect, Transform _avatar)
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
        avatar = _avatar;
    }

    public void SetUpMovement(bool[] inputs, Effect effect)
    {
        inputDirection = effect.SetUpMovement(this, inputs);
        Debug.Log(inputDirection);
        if (inputDirection == Vector3.back)
        {
            inputDirection = Vector3.up;
            Debug.Log("removing");
            effects.Remove(effect);
        }
        animationValues = effect.SetUpAnimations(this, inputs);
    }
}
