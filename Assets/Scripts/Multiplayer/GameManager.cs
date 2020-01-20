using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static int projectileNumber = 0;
    public PlayerManager[] players = new PlayerManager[Server.MaxPlayers];
    public static Dictionary<int, ProjectileManager> projectiles = new Dictionary<int, ProjectileManager>();
    public GameObject[] Items = new GameObject[7];
    public GameObject[] gameItems = new GameObject[20];
    public LayerMask groundMask;
    public GameObject[] characters = new GameObject[3];
    public GameObject[] enemies = new GameObject[3];
    public GameObject[] playerObject;
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
        _player.name = _id.ToString();
        players[_id] = _player.GetComponent<PlayerManager>();
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
        for(int i = 1; i < players.Length; i++)
        {
            if(players[i] != null)
            {
                Debug.Log($"Destroying: {players[i].gameObject.name}");
                Destroy(players[i].gameObject);
                players[i] = null;
            }
        }
        int[] remove = new int[projectiles.Count];
        int a = 0;
        foreach(ProjectileManager p in projectiles.Values)
        {
            remove[a] = p.id;
            a++;
        }
        for(int i = 0; i < a; i++)
        {
            Destroy(projectiles[remove[i]].gameObject);
            projectiles.Remove(remove[i]);
        }
        for(int i = 0; i < gameItems.Length; i++)
        {
            if(gameItems[i] != null)
            {
                Debug.Log($"Destroying: {gameItems[i].gameObject.name}");
                Destroy(gameItems[i].gameObject);
                gameItems[i] = null;
            }
        }
        projectileNumber = 0;
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