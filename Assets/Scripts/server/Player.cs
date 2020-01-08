using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    public int id;
    public string username;
    public int selectedCharacter;
    public Vector3 position;
    public float gravity;
    public Quaternion rotation;
    public Transform avatar;
    public Transform groundCheck;
    public LayerMask groundmask;
    public CharacterController controller;
    private bool[] inputs;
    public bool[] animationValues;

    public Player(int _id, string _username, Vector3 _spawnPosition, int _selectedCharacter)
    {
        id = _id;
        username = _username;
        position = _spawnPosition;
        rotation = Quaternion.identity;
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

    //update the player by checking his inputs and acting on them
    public void UpdatePlayer()
    {
        //find the player in the game of the masterclient, otherwise it can't move
        if(controller == null)
        {
            try
            {
                GameObject _gameobject = GameObject.Find(id.ToString());
                controller = _gameobject.GetComponent<CharacterController>();
                avatar = _gameobject.transform;
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
        if (inputs[10] || inputs[6] || inputs[7])
        {
            animationValues[3] = true;
        }
        gravity -= 7 * Time.deltaTime;
        _inputDirection.y = gravity;
        Move(_inputDirection);
    }
    //use the controller of the player to move the character and use his transfrom to tell the other players where this object is
    private void Move(Vector3 _inputDirection)
    {
        controller.Move(_inputDirection * Time.deltaTime * 20);
        position = avatar.position;
        rotation = avatar.rotation;
        ServerSend.PlayerPosition(this);
        ServerSend.PlayerAnimation(this);
        ServerSend.PlayerRotation(this);
    }

    public void SetInput(bool[] _inputs, Quaternion _rotation)
    {
        inputs = _inputs;
        rotation = _rotation;
    }
}

