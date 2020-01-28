using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VulcanoJumping : Effect
{
    int owner;
    Vulcasaur player;
    float startDuration, headRotation, LaunchSpeed = 80f;
    Vector3 jumpDirection;
    bool jumping;
    Projectile projectileOwner;

    public VulcanoJumping(float _duration, int _owner, Projectile _projectileOwner, int _key)
    {
        startDuration = _duration;
        duration = _duration;
        owner = _owner;
        player = Server.clients[_owner].player as Vulcasaur;
        jumping = false;
        priority = 1;
        name = "vulcano";
        key = _key;
    }

    //Determines the strength of the jump based on vertical rotation of the head
    public override Vector3 SetUpMovement(PlayerStatus status, bool[] inputs)
    {
        status.inputDirection = Vector3.zero;
        if (duration < startDuration * 0.4f && jumping)
        {
            duration = startDuration * 0.4f;
        }

        status.isGrounded = Physics.CheckSphere(status.groundCheck.position, 1f, status.groundmask);
        if (duration < startDuration * 0.6f && status.isGrounded)
        {
            jumping = false;
            duration = 0;
            Collider[] HitPlayers = Physics.OverlapSphere(status.groundCheck.position, 20, 11);
            foreach (Collider player in HitPlayers)
            {
                PlayerManager playerManager = player.gameObject.GetComponent<PlayerManager>();
                if (playerManager != null)
                {
                    if (playerManager.id != owner)
                    {
                        Server.clients[playerManager.id].player.Hit(projectileOwner);
                    }
                }
            }
        }
        else
        {
            if (duration <= startDuration * 0.75f)
            {
                if (!jumping)
                {
                    jumping = true;
                    jumpDirection = status.avatar.forward;
                    headRotation = -Mathf.Clamp(player.verticalRotation, -20, 20);
                    headRotation = ((headRotation + 20) / 40) * 0.5f + 0.5f;
                    status.ySpeed = LaunchSpeed * headRotation;
                }
                status.inputDirection += jumpDirection * (headRotation * 12 + 12);
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
