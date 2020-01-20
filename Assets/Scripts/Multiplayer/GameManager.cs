﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static int projectileNumber = 0;
    public static Dictionary<int, PlayerManager> players = new Dictionary<int, PlayerManager>();
    public static Dictionary<int, ProjectileManager> projectiles = new Dictionary<int, ProjectileManager>();
    public GameObject[] Items = new GameObject[7];
    public static Dictionary<int, GameObject> gameItems = new Dictionary<int, GameObject>();
    public LayerMask groundMask;
    public GameObject Charmandolphin;
    public GameObject McQuirtle;
    public GameObject Vulcasaur;
    public GameObject CharmandolphinEnemy;
    public GameObject McQuirtleEnemy;
    public GameObject VulcasaurEnemy;
    public GameObject[] playerObject;
    public GameObject WaterProjectile;
    public GameObject[] walls;
    public bool freezeInput = false;
    
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

    public void SpawnProjectile(int _id, Vector3 _position, Quaternion _rotation, int moveIndex, int owner)
    {
        GameObject _projectile;
        //todo : different projectiles
        _projectile = Instantiate(playerObject[moveIndex], _position, _rotation);
        _projectile.GetComponent<ProjectileManager>().id = _id;
        projectiles.Add(_id, _projectile.GetComponent<ProjectileManager>());
        if (Client.instance.myId == owner)
        {
            players[owner].playerAnimator.SetTrigger("Attack");
        }
    }

    public void InvisiblePlayer(int _id, bool invis)
    {
        players[_id].invisible = invis;
        Debug.Log("Gamemanager Invis");
    }

    public void ResetGame()
    {
        for(int i = 1; i < players.Count+1; i++)
        {
            Debug.Log($"Destroying: {players[i].gameObject.name}");
            Destroy(players[i].gameObject);
            players.Remove(i);
        }
        for(int i = 0; i < gameItems.Count; i++)
        {
            Debug.Log($"Destroying: {gameItems[i].gameObject.name}");
            Destroy(gameItems[i].gameObject);
            gameItems.Remove(i);
        }
    }

    public void SpawnEvolution(String evolution, int id)
    {
        PlayerManager player = GameManager.players[id];
        Destroy(player.transform.GetChild(1).gameObject);
        GameObject evolutionObject = Instantiate(Resources.Load<GameObject>("PhotonPrefabs/" + evolution), player.transform);
        player.GetComponent<PlayerManager>().Allparts = player.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject;
        player.GetComponentInChildren<CharmandolphinLook>().player = player;
        player.GetComponentInChildren<CharmandolphinLook>().playerbody = player.transform;
        player.GetComponentInChildren<CharmandolphinLook>().avatar = player.transform.gameObject;
        player.GetComponent<PlayerController>().playerAnimator = player.GetComponentInChildren<Animator>();
        Debug.Log(player.GetComponent<PlayerController>().playerAnimator.ToString());
    }
}