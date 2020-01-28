using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public enum Type
{
    water,
    grass,
    fire,
    noType
}

public abstract class Player
{
    public int id, projectile, selectedCharacter, kills = 0;
    public string username;
    public PlayerStatus status;
    public Transform avatar, projectileSpawner;
    public CharacterController controller;
    public static Player instance;
    public bool[] inputs;
    public float verticalRotation;
    float stormDamage, STORMDAMAGE = 2, stormDamageTimer, STORMDAMAGETIMER = 2;
    public int evolutionStage = 1;
    bool readyToEvolve = false, inStorm = false;

    void Awake()
    {
        instance = this;
    }
    
    //update the player by checking his inputs and acting on them
    public virtual void UpdatePlayer()
    {
        if (controller == null)
        {
            try
            {
                GameObject _gameobject = GameObject.Find(id.ToString());
                controller = _gameobject.GetComponent<CharacterController>();
                avatar = _gameobject.transform;
                avatar.rotation = Quaternion.identity;
                status.groundCheck = _gameobject.GetComponentInChildren<PlayerObjectsAllocater>().groundcheck;
                projectileSpawner = _gameobject.GetComponentInChildren<PlayerObjectsAllocater>().projectileSpawner;
                Debug.Log("avatar found");
                int effect = Server.clients[id].player.status.effectcount;
                Server.clients[id].player.status.effects.Add(effect, new InTheBus(20, id, effect));
                Server.clients[id].player.status.effectcount++;
            }
            catch
            {
                Debug.Log($"failed to find {id}");
                return;
            }
        }
        status.Update(inputs, avatar);
        if(status.health <= 0 && status.alive)
        {
            status.alive = false;
            ServerSend.SendDeathScreen(this);
            ServerSend.UpdatePlayerCount();
            return;
        }
        Move(status.inputDirection);

        if (XPSystem.instance.CurrentLevel == 4 && evolutionStage == 1 || XPSystem.instance.CurrentLevel == 6 && evolutionStage == 2)
        {
            evolutionStage += 1;
            readyToEvolve = true;
        }
        if (readyToEvolve)
        {
            ServerSend.Evolve(this);
            readyToEvolve = false;
        }
        if (status.isGrounded)  //for projectiles
        {
            status.inputDirection.y = 0;
        }
        else
        {
            status.inputDirection.y *= 0.2f;
        }
        CheckStorm();
        ServerSend.UpdateHUD(this);
    }

    //use the controller of the player to move the character and use his transfrom to tell the other players where this object is
    protected virtual void Move(Vector3 _inputDirection)
    {
        controller.Move(_inputDirection * Time.deltaTime);
        ServerSend.PlayerPosition(this);
        ServerSend.PlayerAnimation(this);
        ServerSend.PlayerRotation(this);
    }

    public void SetInput(bool[] _inputs, Quaternion _rotation, float _verticalRotation)
    {
        if(avatar != null)
        {
            inputs = _inputs;
            avatar.rotation = _rotation;
            verticalRotation = _verticalRotation;
        }
    }

    //Hit() is called by the server when a player gets hit by an projectile, the projectile has a type and damage value
    public void Hit(Projectile projectile)
    {
        float damageMultiplier = 1f;
        if (status.type + 1 == Type.noType)
        {
            if (Type.water == projectile.type)
            {
                damageMultiplier = 1.5f;
            }
            else if (Type.grass == projectile.type)
            {
                damageMultiplier = 0.6667f;
            }
        }
        else if (status.type + 1 == projectile.type)
        {
            damageMultiplier = 1.5f;
        }
        else if (status.type == projectile.type)
        {
            damageMultiplier = 1;
        }
        else
        {
            damageMultiplier = 0.6667f;
        }
        status.defaultStatus.dshield -= projectile.damage * damageMultiplier * status.damageBoost;
        if (status.defaultStatus.dshield <= 0)
        {
            float remainingDamage = Mathf.Abs(status.defaultStatus.dshield);
            status.defaultStatus.dhealth -= remainingDamage;
            status.defaultStatus.dshield = 0;
        }
        if (status.defaultStatus.dhealth <= 0)
        {
            Server.clients[projectile.owner].player.kills++;
        }
    }

    //overload function of Hit(), does direct damage, not through projectile
    public void Hit(float damage)
    {
        status.defaultStatus.dshield -= damage;
        if (status.defaultStatus.dshield <= 0)
        {
            float remainingDamage = Mathf.Abs(status.defaultStatus.dshield);
            status.defaultStatus.dhealth -= remainingDamage;
            status.defaultStatus.dshield = 0;
        }
    }

    //checks if the player is outside of the play area and "in the storm", does damage if they are
    public void CheckStorm()
    {
        if (BattleBus.canJump)
        {
            Vector3 pos = avatar.position;
            if (pos.z > Walls.walls[0].position.z ||
                pos.z < Walls.walls[1].position.z ||
                pos.x > Walls.walls[2].position.x ||
                pos.x < Walls.walls[3].position.x)
            {
                if (!inStorm)
                {
                    ServerSend.StormOverlay(id, true);
                    inStorm = true;
                    stormDamage = STORMDAMAGE;
                    stormDamageTimer = 0.1f;
                }
                if (stormDamageTimer <= 0)
                {
                    stormDamageTimer = STORMDAMAGETIMER;
                    Hit(stormDamage);
                    stormDamage += 1f;
                }
                else
                {
                    stormDamageTimer -= Time.deltaTime;
                }
            }
            else if (inStorm)
            {
                ServerSend.StormOverlay(id, false);
                inStorm = false;
            }
            if (avatar.position.y < -15)
            {
                Hit(400);
            }
        }
       
        
        
    }

}

