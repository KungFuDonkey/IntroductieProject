﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class McQuirtle : Player
{
    public McQuirtle(int _id, string _username, int _selectedCharacter)
    {
        id = _id;
        username = _username;
        selectedCharacter = _selectedCharacter;
        status = new PlayerStatus();
        Effect defaultEffect = new Effect();
        defaultEffect.SetValues(45f, 22f, 100f, 2f, 2f, 2f, 20f);
        status.defaultStatus = defaultEffect;
        status.groundmask = GameManager.instance.groundMask;
        inputs = new bool[11];
        status.animationValues = new bool[4]
        {
            false,
            false,
            false,
            false
        };
    }

    public override void UpdatePlayer()
    {
        base.UpdatePlayer();
        if (inputs[10] && status.fireTimer < 0)
        {
            basicAttack();
        }
        else
        {
            status.fireTimer -= Time.deltaTime;
            status.animationValues[2] = false;
        }

        if (inputs[6] && status.qTimer < 0)
        {
            qAttack();

        }
        else
        {
            status.qTimer -= Time.deltaTime;
            status.animationValues[2] = false;

        }

        if (inputs[7] && status.eTimer < 0)
        {
            eAttack();
            status.animationValues[2] = false;

        }
        else
        {
            status.eTimer -= Time.deltaTime;
            status.animationValues[2] = false;

        }


    }

    public void basicAttack()
    {
        status.fireTimer = status.FIRETIMER;
        Quaternion rotation = Quaternion.Euler(verticalRotation, avatar.rotation.eulerAngles.y, avatar.rotation.eulerAngles.z);
        ServerSend.Projectile(this, 8, new WaterBall((int)GameManager.projectileNumber, projectileSpawner.position, rotation, status.inputDirection, id));
        Debug.Log("shooting");
        status.animationValues[2] = true;

    }

    public void qAttack()
    {
        status.qTimer = status.QTIMER;
        Quaternion rotation = Quaternion.Euler(verticalRotation, avatar.rotation.eulerAngles.y, avatar.rotation.eulerAngles.z);
        ServerSend.Projectile(this, 9, new WaterBall((int)GameManager.projectileNumber, projectileSpawner.position, rotation, status.inputDirection, id));
        Debug.Log("shooting");
        status.animationValues[2] = true;

    }

    public void eAttack()
    {
        status.eTimer = status.ETIMER;
        Quaternion rotation = Quaternion.Euler(verticalRotation, avatar.rotation.eulerAngles.y, avatar.rotation.eulerAngles.z);
        ServerSend.Projectile(this, 10, new WaterBall((int)GameManager.projectileNumber, projectileSpawner.position, rotation, status.inputDirection, id));
        Debug.Log("shooting");
        status.animationValues[2] = true;

    }
}
