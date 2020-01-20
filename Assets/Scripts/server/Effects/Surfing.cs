using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Surfing : Effect
{
    float surfSpeed = 10f;
    
    public Surfing(float _duration)
    {
        duration = _duration;
    }

    public override void UpdateEffect()
    {

    }

    public override Vector3 SetUpMovement(PlayerStatus status, bool[] inputs)
    {
        status.inputDirection = Vector3.zero;

        status.inputDirection += status.avatar.forward;
 
        status.inputDirection *= surfSpeed * Time.deltaTime * 60;
        if (inputs[4])
        {
            return Vector3.negativeInfinity;
        }

        status.isGrounded = Physics.CheckSphere(status.groundCheck.position, 4f, status.groundmask);
        if (status.isGrounded)
        {
            
        }
        else
        {
            status.ySpeed -= status.gravity * Time.deltaTime;
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
