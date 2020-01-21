using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Surfing : Effect
{
    float surfSpeed = 40f;
    float adjPos = 2f;
    int owner;
    Ray ray = new Ray();


    public Surfing(float _duration, int _owner)
    {
        duration = _duration;
        owner = _owner;
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
            Server.projectiles[owner].OnEffectRemove();
            return Vector3.back;
        }


        ray.direction = Vector3.down;
        ray.origin = status.avatar.position;
        if (Physics.Raycast(ray, out RaycastHit hit, 1000, GameManager.instance.groundMask))
        {
            status.inputDirection.y = -3 * ((hit.distance) - adjPos);
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
