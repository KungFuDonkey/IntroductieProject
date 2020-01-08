using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public static Dictionary<int, PlayerManager> players = new Dictionary<int, PlayerManager>();

    public LayerMask groundMask;
    public GameObject Charamandolphin;
    public GameObject McQuirtle;
    public GameObject Vulcasaur;
    public GameObject CharamandolphinEnemy;
    public GameObject McQuirtleEnemy;
    public GameObject VulcasaurEnemy;

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
        Debug.Log(_selectedCharacter);

        if (_selectedCharacter == 1)
        {
            if (_id == Client.instance.myId)
            {
                _player = Instantiate(Charamandolphin, _position, _rotation);
            }
            else
            {
                _player = Instantiate(CharamandolphinEnemy, _position, _rotation);
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
}