using System.Collections;
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
        player = Server.clients[_owner].player as Player;
        priority = 1;
        name = "parachuting";
        key = _key;
        dsilenced = true;
    }

    public override Vector3 SetUpMovement(PlayerStatus status, bool[] inputs)
    {
        status.inputDirection = Vector3.zero;
        if (duration < 0.4f * startDuration && status.parachuting)
        {
            duration = 0.4f * startDuration;
        }

        status.parachuting = true;
        status.isGrounded = Physics.CheckSphere(status.groundCheck.position, 2f, status.groundmask);
        if (!status.parachuting && status.ySpeed <= 0)
        {
            Debug.Log("Parachuting");
            status.parachuting = true;
        }
        else if (!status.parachuting)
        {
            //status.ySpeed -= status.gravity * Time.deltaTime;
        }
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
        status.inputDirection *= dmovementSpeed * Time.deltaTime * 60 * 0.8f;
        status.ySpeed = -3;
        status.inputDirection.y = status.ySpeed;

        if (status.isGrounded && status.parachuting)
        {
            duration = 0;
            status.effects.Remove(key);
            status.parachuting = false;
            status.jumped = false;
            Debug.Log("stopped parachuting");
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

