using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vulcasaur : Player
{
    public bool jumping;
    public Vulcasaur(int _id, string _username, int _selectedCharacter)
    {

        id = _id;
        username = _username;
        selectedCharacter = _selectedCharacter;
        status = new PlayerStatus();
        Effect defaultEffect = Effect.Vulcasaur;
        status.defaultStatus = defaultEffect;
        status.effects.Add(defaultEffect);
        Debug.Log("values are Set");
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
            ServerSend.Projectile(this, 2, new Vulcano((int)GameManager.projectileNumber, status.groundCheck.position, rotation, status.inputDirection * 0.2f, id));
            Debug.Log("shooting");
        }
        else
        {
            return;
        }

    }
}
