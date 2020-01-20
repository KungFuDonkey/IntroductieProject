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
    public int id;
    public int projectile;
    public int selectedCharacter;
    public string username;
    public PlayerStatus status;
    public Transform avatar;
    public Transform projectileSpawner;
    public CharacterController controller;
    public static Player instance;
    public bool[] inputs;
    public float verticalRotation;

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
                status.avatar = _gameobject.transform;
                avatar.rotation = Quaternion.identity;
                int childeren = _gameobject.transform.GetChild(0).childCount;
                status.groundCheck = _gameobject.transform.GetChild(0).GetChild(childeren - 1);
                projectileSpawner = _gameobject.GetComponentInChildren<PlayerObjectsAllocater>().projectileSpawner;
                Debug.Log("avatar found");
            }
            catch
            {
                Debug.Log($"failed to find {id}");
                return;
            }
        }
        status.Update(inputs);
        if(status.health <= 0 && status.alive)
        {
            status.alive = false;
            ServerSend.SendDeathScreen(this);
            ServerSend.UpdatePlayerCount();
            return;
        }
        Move(status.inputDirection);
        ServerSend.SetInvis(id, status.invisible);


        if (status.isGrounded)  //for projectiles
        {
            status.inputDirection.y = 0;
        }
        else
        {
            status.inputDirection.y *= 0.2f;
        }
        ServerSend.UpdateHUD(this);

        if (inputs[9] && status.evolveTimer < 0)
        {
            Evolve();
        }
        else
        {
            status.evolveTimer -= Time.deltaTime;
        }
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
        inputs = _inputs;
        avatar.rotation = _rotation;
        verticalRotation = _verticalRotation;
    }

    public void Hit(Projectile projectile)
    {
        float damageMultiplier = 1f;
        if (status.type + 1 == Type.noType)
        {
            if (Type.water == projectile.type)
            {
                damageMultiplier = 1.5f;
            }
        }
        else if (status.type + 1 == projectile.type)
        {
            damageMultiplier = 1.5f;
        }
        else if (status.type == projectile.type)
        {
            damageMultiplier = 1.5f;
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
    }
    public void Evolve()
    {
        status.evolveTimer = status.EVOLVETIMER;
        ServerSend.Evolve(this);
        Debug.Log("evolving");
    }
}

