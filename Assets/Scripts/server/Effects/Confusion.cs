using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Confusion : Effect
{
    int confused;
    public Confusion(float _duration, int _priority, int _key)
    {
        duration = _duration;
        priority = _priority;
        name = "stun";
        key = _key;
        confused = Random.Range(1, 4);
    }

    public override Vector3 SetUpMovement(PlayerStatus status, bool[] inputs)
    {
        status.inputDirection = Vector3.zero;
        if (inputs[(0 + confused)])
        {
            status.inputDirection += status.avatar.forward;
        }
        if (inputs[(1 + confused) % 4])
        {
            status.inputDirection -= status.avatar.forward;
        }
        if (inputs[(2 + confused) % 4])
        {
            status.inputDirection -= status.avatar.right;
        }
        if (inputs[(3 + confused) % 4])
        {
            status.inputDirection += status.avatar.right;
        }

        status.inputDirection *= dmovementSpeed * Time.deltaTime * 60;
        if (inputs[5])
        {
            status.inputDirection /= drunMultiplier;
        }

        status.isGrounded = Physics.CheckSphere(status.groundCheck.position, 1f, status.groundmask);
        if (status.isGrounded && status.ySpeed < 0)
        {
            if (inputs[4])
            {
                status.ySpeed = status.jumpspeed;
            }
            else
            {
                status.ySpeed = -4;
            }
        }
        status.ySpeed -= status.gravity * Time.deltaTime;
        status.inputDirection.y = status.ySpeed;

        return status.inputDirection;
    }
}
