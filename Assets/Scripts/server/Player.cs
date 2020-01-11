using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public abstract class Player
{
    public int id;
    public int projectile;
    public string username;
    public float gravity;
    public Transform avatar;
    public Transform groundCheck;
    public LayerMask groundmask;
    public CharacterController controller;
    protected bool[] inputs;
    public bool[] animationValues;
    public float fireTimer = 0f, FIRETIMER = 2f, walkSpeed = 20f, runSpeed = 40f;
    

    //update the player by checking his inputs and acting on them
    public virtual void UpdatePlayer()
    {
        //find the player in the game of the masterclient, otherwise it can't move
        if(controller == null)
        {
            try
            {
                GameObject _gameobject = GameObject.Find(id.ToString());
                controller = _gameobject.GetComponent<CharacterController>();
                avatar = _gameobject.transform;
                avatar.rotation = Quaternion.identity;
                int childeren = _gameobject.transform.GetChild(0).childCount;
                groundCheck = _gameobject.transform.GetChild(0).GetChild(childeren - 1);
                Debug.Log("avatar found");
            }
            catch
            {
                Debug.Log($"failed to find {id}");
                return;
            }
        }
        bool isGrounded = Physics.CheckSphere(groundCheck.position, 2f, groundmask);
       
        if (isGrounded && gravity < 0)
        {
            if (inputs[4])
            {
                gravity = 3;
            }
            else
            {
                gravity = -2;
            }
        }
        Vector3 _inputDirection = Vector3.zero;
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

        if (inputs[0] || inputs[1] || inputs[2] || inputs[3])
        {
            if (inputs[5])
            {
                animationValues[0] = false;
                animationValues[1] = true;
            }
            else
            {
                animationValues[0] = true;
                animationValues[1] = false;
            }
        }
        else
        {
            animationValues[0] = false;
            animationValues[1] = false;
        }
        if (inputs[10] || inputs[6] || inputs[7])
        {
            animationValues[3] = true;
        }
        gravity -= 7 * Time.deltaTime;
        if (inputs[10] && fireTimer < 0)
        {
            fireTimer = FIRETIMER;
            ServerSend.Projectile(this, projectile, _inputDirection);
            Debug.Log("shooting");
        }
        else
        {
            fireTimer -= Time.deltaTime;
        }
        _inputDirection.y = gravity;
        if (inputs[5])
        {
            Move(_inputDirection, runSpeed);
        }
        else
        {
            Move(_inputDirection, walkSpeed);
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

    public virtual void SetInput(bool[] _inputs, Quaternion _rotation)
    {
        inputs = _inputs;
        avatar.rotation = _rotation;
    }
}

