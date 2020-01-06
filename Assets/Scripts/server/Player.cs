using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace GameServer
{
    class Player
    {
        public int id;
        public string username;
        public int selectedCharacter;

        public Vector3 position;
        public Quaternion rotation;

        private float moveSpeed = 5f / Constants.TICKS_PER_SEC;
        private bool[] inputs;

        public Player(int _id, string _username, Vector3 _spawnPosition, int _selectedCharacter)
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
            if (inputs[2])
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
    }
}