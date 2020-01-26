using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InTheBus : Effect
{
    int owner;
    Player player;
    float startDuration;
    bool onBus;

    public InTheBus(float _duration, int _owner, int _key)
    {
        startDuration = _duration;
        duration = _duration;
        owner = _owner;
        player = Server.clients[_owner].player as Player;
        priority = 1;
        name = "inTheBus";
        key = _key;
        dsilenced = true;
    }

    public override Vector3 SetUpMovement(PlayerStatus status, bool[] inputs)
    {
        status.inputDirection = Vector3.zero;
        if (duration < 0.4f * startDuration)
        {
            duration = 0.4f * startDuration;
        }
        ServerSend.BusCamera(player.id, true);
        onBus = Physics.CheckSphere(status.groundCheck.position, 0.5f, GameManager.instance.busMask);
        if (!onBus)
        {
            status.ySpeed -= status.gravity * Time.deltaTime;
        }
        status.inputDirection = BattleBus.busMovement * Time.deltaTime * 60;
        if ((inputs[4] && BattleBus.canJump) || BattleBus.Bus.position.z >= 280)
        {
            ServerSend.BusCamera(owner, false);
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

