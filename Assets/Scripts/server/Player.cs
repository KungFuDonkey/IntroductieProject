using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

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
    protected bool[] inputs;
    protected Vector3 _inputDirection;
    public static Player instance;

    void Awake()
    {
        instance = this;
    }
    
    //update the player by checking his inputs and acting on them
    public virtual void UpdatePlayer()
    {
        SetupPlayer();
        status.Update(inputs);
        _inputDirection = Vector3.zero;
        if (inputs[0])
        {
            _inputDirection += avatar.forward;
        }
        if (inputs[1])
        {
            _inputDirection -= avatar.forward;
        }
        if (inputs[2])
        {
            _inputDirection -= avatar.right;
        }
        if (inputs[3])
        {
            _inputDirection += avatar.right;
        }

        
        _inputDirection.y = status.gravity;
        if (inputs[5])
        {
            Move(_inputDirection, status.runSpeed);
        }
        else
        {
            Move(_inputDirection, status.walkSpeed);
        }

        if (status.isGrounded)  //for projectiles
        {
            _inputDirection.y = 0;
        }
        else
        {
            _inputDirection.y *= 0.2f;
        }
    }


    //find the player in the game of the masterclient, otherwise it can't move
    private void SetupPlayer()
    {
        if (controller == null)
        {
            try
            {
                GameObject _gameobject = GameObject.Find(id.ToString());
                controller = _gameobject.GetComponent<CharacterController>();
                avatar = _gameobject.transform;
                avatar.rotation = Quaternion.identity;
                int childeren = _gameobject.transform.GetChild(0).childCount;
                status.groundCheck = _gameobject.transform.GetChild(0).GetChild(childeren - 1);
                projectileSpawner = _gameobject.GetComponent<ProjectileSpawnerAllocater>().projectileSpawner;
                Debug.Log("avatar found");
            }
            catch
            {
                Debug.Log($"failed to find {id}");
                return;
            }
        }
    }
    //use the controller of the player to move the character and use his transfrom to tell the other players where this object is
    protected virtual void Move(Vector3 _inputDirection, float moveSpeed)
    {
        controller.Move(_inputDirection * Time.deltaTime * moveSpeed);
        ServerSend.PlayerPosition(this);
        ServerSend.PlayerAnimation(this);
        ServerSend.PlayerRotation(this);
    }

    public void SetInput(bool[] _inputs, Quaternion _rotation, float _verticalRotation)
    {
        inputs = _inputs;
        avatar.rotation = _rotation;
        status.verticalRotation = _verticalRotation;
    }

    //public void UseItem(int itemIndex)
    //{
    //   if (itemIndex == 0)
    //    {
    //        jumpspeed *= 3;
    //    }
    //   else if (itemIndex == 1)
    //    {
    //        jumpspeed /= 3;
    //    }
    //   else if (itemIndex == 2)
    //    {
    //        walkSpeed *= 3;
    //        runSpeed *= 3;
    //    }
    //    else if (itemIndex == 3)
    //    {
    //        walkSpeed /= 3;
    //        runSpeed /= 3;
    //    }
    //    else if (itemIndex == 4)
    //    {
    //        PlayerManager.instance.invisible.SetActive(false);
    //    }
    //    else if (itemIndex == 5)
    //    {
    //        PlayerManager.instance.invisible.SetActive(true);
    //    }
    //   else if (itemIndex == 6)
    //    {

    //    }
    //   else if (itemIndex == 7)
    //    {

    //    }
    //}

    public void SetHealth(int health)
    {

    }
}

