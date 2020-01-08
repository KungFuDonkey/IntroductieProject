﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static uint projectileNumber = 0;
    public static Dictionary<int, PlayerManager> players = new Dictionary<int, PlayerManager>();
    public static Dictionary<int, ProjectileManager> projectiles = new Dictionary<int, ProjectileManager>();

    public LayerMask groundMask;
    public GameObject Charmandolphin;
    public GameObject McQuirtle;
    public GameObject Vulcasaur;
    public GameObject CharmandolphinEnemy;
    public GameObject McQuirtleEnemy;
    public GameObject VulcasaurEnemy;
    public GameObject WaterProjectile;

    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Debug.Log("Instance already exists, destroying object!");
            Destroy(this);
        }
        //DontDestroyOnLoad(gameObject);
    }

    public void SpawnPlayer(int _id, string _username, int _selectedCharacter, Vector3 _position, Quaternion _rotation)
    {
        GameObject _player;
        Debug.Log(_selectedCharacter);

        if (_selectedCharacter == 1)
        {
            if (_id == Client.instance.myId)
            {
                _player = Instantiate(Charmandolphin, _position, _rotation);
            }
            else
            {
                _player = Instantiate(CharmandolphinEnemy, _position, _rotation);
            }
        }
        else if (_selectedCharacter == 2)
        {
            if (_id == Client.instance.myId)
            {
                _player = Instantiate(Vulcasaur, _position, _rotation);
            }
            else
            {
                _player = Instantiate(VulcasaurEnemy, _position, _rotation);
            }
        }
        else
        {
            if (_id == Client.instance.myId)
            {
                _player = Instantiate(McQuirtle, _position, _rotation);
            }
            else
            {
                _player = Instantiate(McQuirtleEnemy, _position, _rotation);
            }
        }

        _player.GetComponent<PlayerManager>().id = _id;
        _player.GetComponent<PlayerManager>().username = _username;
        _player.name = _id.ToString();
        players.Add(_id, _player.GetComponent<PlayerManager>());
    }

    public void SpawnProjectile(int _id, Vector3 _position, Quaternion _rotation, int _projectileType)
    {
        GameObject _projectile;
        //todo : different projectiles
        _projectile = Instantiate(WaterProjectile,_position,_rotation);
        _projectile.GetComponent<ProjectileManager>().id = _id;
        _projectile.GetComponent<ProjectileManager>().projectileType = _projectileType;
        projectiles.Add(_id, _projectile.GetComponent<ProjectileManager>());
    }
}