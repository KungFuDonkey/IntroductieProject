using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vulcasaur : Player
{
    public Vulcasaur(int _id, string _username, int _selectedCharacter)
    {
        id = _id;
        username = _username;
        selectedCharacter = _selectedCharacter;
        status = new PlayerStatus();
        Effect defaultEffect = Effect.Vulcasaur;
        status.defaultStatus = defaultEffect;
        status.effects.Add(0,defaultEffect);
        Debug.Log("values are Set");
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
            controller.center.Set(0f, 1.5f, 0.2f);
            controller.radius = 1.5f;
            controller.height = 1f;
        }
        else
        {
            projectileSpawner = projectileSpawner3;
            controller.center.Set(0f, 3.6f, 0f);
            controller.radius = 3.6f;
            controller.height = 1f;
        }
    }

    public override void UpdatePlayer()
    {
        base.UpdatePlayer();
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
                qAttack();
            }
            else
            {
                status.qTimer -= Time.deltaTime;
                status.animationValues[2] = false;
            }

            if (inputs[7] && status.eTimer < 0 && status.isGrounded)
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
        ServerSend.Projectile(this, 0, new FireBall((int)GameManager.projectileNumber, projectileSpawner.position, rotation, status.inputDirection * 0.2f, id));
        status.animationValues[2] = true;
        Debug.Log("shooting");
    }

    public void qAttack()
    {
        if (XPSystem.instance.CurrentLevel >= 5)
        {
            status.qTimer = status.QTIMER;
            Quaternion rotation = Quaternion.Euler(verticalRotation, avatar.rotation.eulerAngles.y, avatar.rotation.eulerAngles.z);
            ServerSend.Projectile(this, 1, new Vines((int)GameManager.projectileNumber, avatar.position, avatar.rotation, status.inputDirection * 0.2f, id));
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
            Quaternion rotation = Quaternion.Euler(17.34f, avatar.rotation.eulerAngles.y, avatar.rotation.eulerAngles.z);
            ServerSend.Projectile(this, 2, new Vulcano((int)GameManager.projectileNumber, status.groundCheck.position, rotation, status.inputDirection * 0.2f, id, 5f));
            Debug.Log("shooting");
        }
        else
        {
            return;
        }
    }
}
