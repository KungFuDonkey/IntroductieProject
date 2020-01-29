using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charmandolphin : Player
{
    public bool surfing;

    public Charmandolphin(int _id, string _username, int _selectedCharacter)
    {
        id = _id;
        username = _username;
        selectedCharacter = _selectedCharacter;
        status = new PlayerStatus();
        Effect defaultEffect = Effect.Charmandolphin;
        status.defaultStatus = defaultEffect;
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
            controller.center.Set(0f, 2.45f, 0f);
            controller.radius = 1;
            controller.height = 5;
        }
        else
        {
            projectileSpawner = projectileSpawner3;
            controller.center.Set(0f, 3.5f, 0f);
            controller.radius = 2f;
            controller.height = 7f;
        }
    }

    public override void UpdatePlayer()
    {
        if (!status.silenced)
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
                eAttack();
            }
            else
            {
                status.qTimer -= Time.deltaTime;
                status.animationValues[2] = false;
            }
            if (inputs[7] && status.eTimer < 0 && !surfing)
            {
                qAttack();
                status.animationValues[2] = false;
            }
            else
            {
                status.eTimer -= Time.deltaTime;
                status.animationValues[2] = false;
            }
        }
        base.UpdatePlayer();
    }

    public void basicAttack()
    {
        status.fireTimer = status.FIRETIMER;
        Quaternion rotation = Quaternion.Euler(verticalRotation, avatar.rotation.eulerAngles.y, avatar.rotation.eulerAngles.z);
        ServerSend.Projectile(this, 4, new WaterBall(GameManager.projectileNumber, projectileSpawner.position, rotation, status.inputDirection * 0.2f, id));
        status.animationValues[2] = true;
    }

    public void eAttack()
    {
        if (XPSystem.instance.CurrentLevel >= 3)
        {
            status.qTimer = status.QTIMER;
            Quaternion rotation = Quaternion.Euler(0, avatar.rotation.eulerAngles.y - 90, -verticalRotation);
            ServerSend.Projectile(this, 5, new AquaPulse(GameManager.projectileNumber, projectileSpawner.position, rotation, status.inputDirection * 0.2f, id));
            status.animationValues[2] = true;
        }
        else
        {
            return;
        }
    }

    public void qAttack()
    {
        if (XPSystem.instance.CurrentLevel >= 5)
        {
            status.eTimer = status.ETIMER;
            Quaternion rotation = Quaternion.Euler(0, avatar.rotation.eulerAngles.y - 90, avatar.rotation.eulerAngles.z);
            ServerSend.Projectile(this, 6, new Wave(GameManager.projectileNumber, avatar.position, rotation, status.inputDirection * 0.2f, id));
            status.animationValues[2] = true;
        }
        else
        {
            return;
        }
    }
}
