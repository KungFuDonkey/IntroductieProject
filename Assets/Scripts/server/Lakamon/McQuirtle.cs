using System.Collections;
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
        Effect defaultEffect = Effect.McQuirtle;
        status.defaultStatus = defaultEffect;
        status.effects.Add(0,defaultEffect);
        status.groundmask = GameManager.instance.groundMask;
        inputs = new bool[12];
        status.animationValues = new bool[4]
        {
            false,
            false,
            false,
            false
        };
    }

    public override void HandleEvolve()
    {
        base.HandleEvolve();
        if (evolutionStage == 2)
        {
            projectileSpawner = projectileSpawner2;
            controller.center.Set(0f, 2.5f, 0.3f);
            controller.radius = 2;
            controller.height = 5.2f;
        }
        else
        {
            projectileSpawner = projectileSpawner3;
            controller.center.Set(0f, 3.6f, 0f);
            controller.radius = 3f;
            controller.height = 7.5f;
        }
    }

    public override void UpdatePlayer()
    {
        base.UpdatePlayer();
        if (!status.silenced && !GameManager.instance.inInventory)
        {
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
    }

    public void basicAttack()
    {
        status.fireTimer = status.FIRETIMER;
        Quaternion rotation = Quaternion.Euler(verticalRotation, avatar.rotation.eulerAngles.y, avatar.rotation.eulerAngles.z);
        ServerSend.Projectile(this, 8, new GrassBall((int)GameManager.projectileNumber, projectileSpawner.position, rotation, status.inputDirection * 0.2f, id));
        Debug.Log("shooting");
        status.animationValues[2] = true;

    }

    public void qAttack()
    {
        if (XPSystem.instance.CurrentLevel >= 5)
        {
            status.qTimer = status.QTIMER;
            Quaternion rotation = Quaternion.Euler(verticalRotation, avatar.rotation.eulerAngles.y, avatar.rotation.eulerAngles.z);
            ServerSend.Projectile(this, 9, new ReturningBall((int)GameManager.projectileNumber, projectileSpawner.position, rotation, status.inputDirection * 0.2f, id));
            Debug.Log("shooting");
            status.animationValues[2] = true;
        }
        else
        {
            return;
        }
    }

    public void eAttack()
    {
        if (XPSystem.instance.CurrentLevel >= 3)
        {
            status.eTimer = status.ETIMER;
            Quaternion rotation = Quaternion.Euler(verticalRotation, avatar.rotation.eulerAngles.y, avatar.rotation.eulerAngles.z);
            ServerSend.Projectile(this, 10, new SnakeBall((int)GameManager.projectileNumber, projectileSpawner.position, rotation, status.inputDirection * 0.2f, id));
            Debug.Log("shooting");
            status.animationValues[2] = true;
        }
        else
        {
            return;
        }
    }
}
