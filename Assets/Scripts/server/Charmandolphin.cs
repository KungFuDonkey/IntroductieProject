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
        groundmask = GameManager.instance.groundMask;
        inputs = new bool[11];
        animationValues = new bool[4]
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
        if (inputs[10] && fireTimer < 0)
        {
            basicAttack();
        }
        else
        {
            fireTimer -= Time.deltaTime;
        }

        if (inputs[6] && qTimer < 0)
        {
            qAttack();
        }
        else
        {
            qTimer -= Time.deltaTime;
        }

        if (inputs[7] && eTimer < 0)
        {
            eAttack();
        }
        else
        {
            eTimer -= Time.deltaTime;
        }


    }

    public void basicAttack()
    {
        fireTimer = FIRETIMER;
        Quaternion rotation = Quaternion.Euler(verticalRotation, avatar.rotation.eulerAngles.y, avatar.rotation.eulerAngles.z);
        ServerSend.Projectile(this, 4, new WaterBall((int)GameManager.projectileNumber, projectileSpawner.position, rotation, _inputDirection * runSpeed, id));
        Debug.Log("shooting");
    }

    public void qAttack()
    {
        qTimer = QTIMER;
        Quaternion rotation = Quaternion.Euler(verticalRotation, avatar.rotation.eulerAngles.y, avatar.rotation.eulerAngles.z);
        ServerSend.Projectile(this, 5, new WaterBall((int)GameManager.projectileNumber, projectileSpawner.position, rotation, _inputDirection * runSpeed, id));
        Debug.Log("shooting");
    }

    public void eAttack()
    {
        Quaternion rotation = Quaternion.Euler(verticalRotation, avatar.rotation.eulerAngles.y, avatar.rotation.eulerAngles.z);
        ServerSend.Projectile(this, 6, new WaterBall((int)GameManager.projectileNumber, projectileSpawner.position, rotation, _inputDirection * runSpeed, id));
        Debug.Log("shooting");
    }
}
