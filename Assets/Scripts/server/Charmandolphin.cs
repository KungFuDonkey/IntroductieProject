using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charmandolphin : Player
{
    public Charmandolphin(int _id, string _username, int _selectedCharacter)
    {
        id = _id;
        username = _username;
        selectedCharacter = _selectedCharacter;
        status = new PlayerStatus();
        status.defaultStatus = Effect.Charmandolphin;
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
        }

        if (inputs[6] && status.qTimer < 0)
        {
            qAttack();
        }
        else
        {
            status.qTimer -= Time.deltaTime;
        }

        if (inputs[7] && status.eTimer < 0)
        {
            eAttack();
        }
        else
        {
            status.eTimer -= Time.deltaTime;
        }


    }

    public void basicAttack()
    {
        status.fireTimer = status.FIRETIMER;
        Quaternion rotation = Quaternion.Euler(verticalRotation, avatar.rotation.eulerAngles.y, avatar.rotation.eulerAngles.z);
        ServerSend.Projectile(this, 4, new WaterBall((int)GameManager.projectileNumber, projectileSpawner.position, rotation, status.inputDirection, id));
        Debug.Log("shooting");
        status.animationValues[2] = true;

    }

    public void qAttack()
    {
        status.qTimer = status.QTIMER;
        Quaternion rotation = Quaternion.Euler(verticalRotation, avatar.rotation.eulerAngles.y, avatar.rotation.eulerAngles.z);
        ServerSend.Projectile(this, 5, new WaterBall((int)GameManager.projectileNumber, projectileSpawner.position, rotation, status.inputDirection, id));
        Debug.Log("shooting");
        status.animationValues[2] = true;

    }

    public void eAttack()
    {
        status.eTimer = status.ETIMER;
        Quaternion rotation = Quaternion.Euler(verticalRotation, avatar.rotation.eulerAngles.y, avatar.rotation.eulerAngles.z);
        ServerSend.Projectile(this, 6, new WaterBall((int)GameManager.projectileNumber, projectileSpawner.position, rotation, status.inputDirection, id));
        Debug.Log("shooting");
        status.animationValues[2] = true;

    }
}
