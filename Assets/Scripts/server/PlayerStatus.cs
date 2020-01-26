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
    public bool isGrounded, movable, silenced, invisible, alive = true, inStorm, jumped, parachuting, inTheBus;
    public Vector3 inputDirection;
    public Type type;
    public int effectcount = 1; //default effect has 0
    public List<int> removeItems = new List<int>();
    public Dictionary<int, Effect> effects = new Dictionary<int, Effect>();


    public void Update(bool[] inputs, Transform _avatar)
    {
        SetStatus(defaultStatus, _avatar);
        int strongestPriority = 100;
        if (effects.Count != 0)
        {
            bool reset = false;
            foreach(Effect effect in effects.Values)
            {
                Debug.Log($"{effect.key} {effect.name}");
                effect.UpdateEffect();
                if (effect.duration == -1)
                {
                }
                else if (effect.duration <= 0)
                {
                    removeItems.Add(effect.key);
                }
                if (effect.priority < strongestPriority)
                {
                    strongestPriority = effect.priority;
                }
            }
            foreach (Effect effect in effects.Values)
            {
                UpdateStatus(effect);

                if (strongestPriority == effect.priority)
                {
                    Debug.Log($"updating {effect.key} {effect.name} with prior {effect.priority}");
                    SetUpMovement(inputs, effect, effect.key);
                    break;
                }
            }
            foreach(int i in removeItems)
            {
                Debug.Log($"Removing effect from list: {i}");
                effects.Remove(i);
                reset = true;
            }
            if (reset)
            {
                removeItems = new List<int>();
            }
        }
        else
        {
            SetUpMovement(inputs, defaultStatus, defaultStatus.key);
        }
    }

    public void UpdateStatus(Effect effect)
    {
        gravity *= effect.dgravity;
        jumpspeed *= effect.djumpspeed;
        health += effect.dhealth;
        shield += effect.dshield;
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
            invisible = !invisible;
    }

    public void SetStatus(Effect effect, Transform _avatar)
    {
        gravity = effect.dgravity;
        jumpspeed = effect.djumpspeed;
        health = effect.dhealth;
        shield = effect.dshield;
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

    public void SetUpMovement(bool[] inputs, Effect effect, int key)
    {
        inputDirection = effect.SetUpMovement(this, inputs);
        if (inputDirection == Vector3.back)
        {
            inputDirection = Vector3.up;
            removeItems.Add(key);
        }
        animationValues = effect.SetUpAnimations(this, inputs);
    }
}
