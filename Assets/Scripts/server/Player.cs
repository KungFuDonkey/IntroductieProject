using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace GameServer
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
    public bool[] animationValues;
    private bool[] inputs;

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
            false,
        };
    }

    public void UpdatePlayer()
    {
        if (controller == null)
        {
            id = _id;
            username = _username;
            position = _spawnPosition;
            rotation = Quaternion.identity;
            selectedCharacter = _selectedCharacter;

            inputs = new bool[10];
        }

        public void Update()
        {
            Vector2 _inputDirection = Vector2.zero;
            if (inputs[0])
            {
                _inputDirection.y += 1;
            }
            if (inputs[1])
            {
                _inputDirection.y -= 1;
            }
        }
        bool isGrounded = Physics.CheckSphere(groundCheck.position, 2f, groundmask);

        if (isGrounded && gravity < 0)
        {
            if (inputs[4])
            {
                _inputDirection.x += 1;
            }
            if (inputs[3])
            {
                _inputDirection.x -= 1;
            }
            Move(_inputDirection);
        }

        private void Move(Vector2 _inputDirection)
        {
            //todo: move

            ServerSend.PlayerPosition(this);
            ServerSend.PlayerRotation(this);
        }

        public void SetInput(bool[] _inputs, Quaternion _rotation)
        {
            inputs = _inputs;
            rotation = _rotation;
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
            animationValues[2] = true;
        }
        else
        {
            animationValues[2] = false;
        }

        gravity -= 7 * Time.deltaTime;
        _inputDirection.y = gravity;
        Move(_inputDirection);
    }

    private void Move(Vector3 _inputDirection)
    {
        controller.Move(_inputDirection * Time.deltaTime * 20);
        position = avatar.position;
        rotation = avatar.rotation;
        ServerSend.PlayerPosition(this);
        ServerSend.PlayerRotation(this);
        ServerSend.PlayerAnimation(this);
    }

    public void SetInput(bool[] _inputs, Quaternion _rotation)
    {
        inputs = _inputs;
        rotation = _rotation;
    }
}