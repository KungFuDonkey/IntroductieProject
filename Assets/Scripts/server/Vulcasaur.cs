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
        status.defaultStatus = new Effect(45f, 22f, 100f, 2f, 2f, 2f);
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
        Quaternion rotation = Quaternion.Euler(status.verticalRotation, avatar.rotation.eulerAngles.y, avatar.rotation.eulerAngles.z);
        ServerSend.Projectile(this, 0, new WaterBall((int)GameManager.projectileNumber, projectileSpawner.position, rotation, status.inputDirection, id));
        Debug.Log("shooting");
    }

    public void qAttack()
    {
        status.qTimer = status.QTIMER;
        Quaternion rotation = Quaternion.Euler(status.verticalRotation, avatar.rotation.eulerAngles.y, avatar.rotation.eulerAngles.z);
        ServerSend.Projectile(this, 1, new Vines((int)GameManager.projectileNumber, avatar.position, avatar.rotation, status.inputDirection, id));
        Debug.Log("shooting");
    }

    public void eAttack()
    {
        status.eTimer = status.ETIMER;
        Quaternion rotation = Quaternion.Euler(17.34f, avatar.rotation.eulerAngles.y, avatar.rotation.eulerAngles.z);
        ServerSend.Projectile(this, 2, new Vulcano((int)GameManager.projectileNumber, status.groundCheck.position, rotation, status.inputDirection, id));
        Debug.Log("shooting");
    }
}
