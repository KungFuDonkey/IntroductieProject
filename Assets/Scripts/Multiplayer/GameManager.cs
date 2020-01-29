using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static int projectileNumber = 0;
    public static Dictionary<int, ProjectileManager> projectiles = new Dictionary<int, ProjectileManager>();

    public PlayerManager[] players = new PlayerManager[Server.MaxPlayers];
    public GameObject[] Items = new GameObject[7];
    public GameObject[] gameItems = new GameObject[20];
    public GameObject[] walls;
    public GameObject BattleBus;
    public LayerMask groundMask;
    public bool freezeInput = false;
    [SerializeField] GameObject[] characters = new GameObject[3];
    [SerializeField] GameObject[] enemies = new GameObject[3];
    [SerializeField] GameObject[] playerObject;

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
    }

    public void SpawnPlayer(int _id, string _username, int _selectedCharacter, Vector3 _position, Quaternion _rotation)
    {
        GameObject _player;
        if (_id == Client.instance.myId)
        {
            _player = Instantiate(characters[_selectedCharacter], _position, _rotation);
        }
        else
        {
            _player = Instantiate(enemies[_selectedCharacter], _position, _rotation);
        }
        _player.GetComponent<PlayerManager>().id = _id;
        _player.GetComponent<PlayerManager>().username = _username;
        _player.GetComponent<PlayerManager>().selectedCharacter = _selectedCharacter;
        _player.name = _id.ToString();
        players[_id] = _player.GetComponent<PlayerManager>();
    }

    public void SpawnProjectile(int _id, Vector3 _position, Quaternion _rotation, int moveIndex, int owner)
    {
        GameObject _projectile;
        _projectile = Instantiate(playerObject[moveIndex], _position, _rotation);
        _projectile.GetComponent<ProjectileManager>().id = _id;
        projectiles.Add(_id, _projectile.GetComponent<ProjectileManager>());
        if (Client.instance.myId == owner)
        {
            players[owner].playerAnimator.SetTrigger("Attack");
        }
    }

    public void ResetGame()
    {
        for(int i = 1; i < players.Length; i++)
        {
            if(players[i] != null)
            {
                Destroy(players[i].gameObject);
                players[i] = null;
            }
        }
        foreach(ProjectileManager p in projectiles.Values)
        {
            Destroy(p.gameObject);
        }
        projectiles = new Dictionary<int, ProjectileManager>();
        projectileNumber = 0;
        for(int i = 0; i < gameItems.Length; i++)
        {
            if(gameItems[i] != null)
            {
                Destroy(gameItems[i].gameObject);
                gameItems[i] = null;
            }
        }
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.F12) && Client.instance.host)
        {
            ServerStart.instance.resetScreen.SetActive(true);
            freezeInput = true;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}