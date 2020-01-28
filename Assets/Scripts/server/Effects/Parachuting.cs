﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parachuting : Effect
{
    int owner;
    int id;
    Player player;
    float startDuration;

    public Parachuting(float _duration, int _owner, int _key)
    {
        startDuration = _duration;
        duration = _duration;
        owner = _owner;
        player = Server.clients[_owner].player;
        priority = 1;
        name = "parachuting";
        key = _key;
        dsilenced = true;
    }

    //Determines the movement of the player while parachuting down from the bus, the direction in which the player looks will determine forwards and downwards speed
    public override Vector3 SetUpMovement(PlayerStatus status, bool[] inputs)
    {
        status.inputDirection = Vector3.zero;
        if (duration < 0.4f * startDuration)
        {
            duration = 0.4f * startDuration;
        }

        status.isGrounded = Physics.CheckSphere(status.groundCheck.position, 1f, status.groundmask);
        float headRotation = player.verticalRotation;
        status.inputDirection += status.avatar.forward * Mathf.Clamp((headRotation / -85 + 1) * 2, 0.5f, 1.6f);
        status.ySpeed = -8 * Mathf.Clamp(headRotation / 30, 1, 2.5f);
        status.inputDirection *= dmovementSpeed * Time.deltaTime * 60 * 0.8f;
        status.inputDirection.y = status.ySpeed;

        if (status.isGrounded)
        {
            duration = 0;
            status.effects.Remove(key);
            status.silenced = false;
        }
        return status.inputDirection;
    }

    public override bool[] SetUpAnimations(PlayerStatus status, bool[] inputs)
    {
        status.animationValues[0] = false;
        status.animationValues[1] = false;
        return status.animationValues;
    }
}

