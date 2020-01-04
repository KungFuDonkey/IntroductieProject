﻿using System.Collections;
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

    public Player(int _id, string _username, Vector3 _spawnPosition, int _selectedCharacter)
    {
        id = _id;
        username = _username;
        position = _spawnPosition;
        rotation = Quaternion.identity;
        selectedCharacter = _selectedCharacter;
        groundmask = GameManager.instance.groundMask;
        inputs = new bool[11];
    }

    public void UpdatePlayer()
    {
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
    }

    public void SetInput(bool[] _inputs, Quaternion _rotation)
    {
        inputs = _inputs;
        rotation = _rotation;
    }
}
