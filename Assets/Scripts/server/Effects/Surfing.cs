using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Surfing : Effect
{
    float surfSpeed = 10f;
    float adjPos = 2f;
    Ray ray = new Ray();


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


        ray.direction = Vector3.down;
        ray.origin = status.avatar.position;
        if (Physics.Raycast(ray, out RaycastHit hit, 1000, GameManager.instance.groundMask))
        {
            status.inputDirection.y = -1 * ((hit.distance) - adjPos);
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
