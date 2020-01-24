using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VulcanoJumping : Effect
{
    int owner;
    int id;
    Vulcasaur player;
    float startDuration, headRotation;
    public float LaunchSpeed = 60f;
    Vector3 jumpDirection;
    public VulcanoJumping(float _duration, int _owner, int _id, int _key)
    {
        startDuration = _duration;
        duration = _duration;
        owner = _owner;
        id = _id;
        player = Server.clients[_owner].player as Vulcasaur;
        player.jumping = false;
        priority = 1;
        name = "vulcano";
        key = _key;
    }

    public override Vector3 SetUpMovement(PlayerStatus status, bool[] inputs)
    {
        if (duration < startDuration * 0.4f && player.jumping)
        {
            duration = startDuration * 0.4f;
        }

        status.inputDirection = Vector3.zero;
        status.isGrounded = Physics.CheckSphere(status.groundCheck.position, 2f, status.groundmask);

        if (duration < startDuration * 0.5f && status.isGrounded)
        {
            player.jumping = false;
            duration = 0;
        }
        else
        {
            if (duration <= startDuration * 0.75f)
            {
                if (!player.jumping)
                {
                    player.jumping = true;
                    jumpDirection = status.avatar.forward;
                    headRotation = -Mathf.Clamp(status.avatar.GetComponentInChildren<playerLook>().verticalRotation, -20, 20);
                    headRotation = ((headRotation + 20) / 40) * 0.5f + 0.5f;
                    status.ySpeed = LaunchSpeed * headRotation;
                }
                status.inputDirection += jumpDirection * (headRotation * 10 + 10);
            }
        }

        status.ySpeed -= status.gravity * Time.deltaTime;
        status.inputDirection.y = status.ySpeed;

        return status.inputDirection;
    }

    public override bool[] SetUpAnimations(PlayerStatus status, bool[] inputs)
    {
        status.animationValues[0] = false;
        status.animationValues[1] = false;
        return status.animationValues;
    }
}
