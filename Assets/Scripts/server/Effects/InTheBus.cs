using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InTheBus : Effect
{
    int owner;

    public InTheBus(float _duration, int _owner, int _key)
    {
        duration = _duration;
        owner = _owner;
        priority = 1;
        name = "inTheBus";
        key = _key;
        dsilenced = true;
    }

    //Makes the player move along with the bus and when the player "jumps out" it's state will transition into parachuting
    public override Vector3 SetUpMovement(PlayerStatus status, bool[] inputs)
    {
        duration = 20;
        status.inputDirection = Vector3.zero;
        //ServerSend.BusCamera(owner, true);
        status.inputDirection = BattleBus.busMovement * Time.deltaTime * 60;
        if ((inputs[4] && BattleBus.canJump) || BattleBus.Bus.position.z >= 280)
        {
            Server.clients[owner].player.avatar.position = BattleBus.Bus.position;
            //ServerSend.BusCamera(owner, false);
            duration = 0;
            status.effects.Remove(key);
            int effect = Server.clients[owner].player.status.effectcount;
            Server.clients[owner].player.status.effects.Add(effect, new Parachuting(20, owner, effect));
            Server.clients[owner].player.status.effectcount++;
        }
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

