using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public static Dictionary<int, PlayerManager> players = new Dictionary<int, PlayerManager>();

    public GameObject Charamandolphin;
    public GameObject McQuirtle;
    public GameObject Vulcasaur;

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
        GameObject myCharacter;
        GameObject _player;
        Debug.Log(_selectedCharacter);

        if (_selectedCharacter == 1)
        {
          myCharacter = Charamandolphin;
        }
        else if (_selectedCharacter == 2)
        {
            myCharacter = Vulcasaur;
        }
        else
        {
            myCharacter = McQuirtle;
        }
        if (_id == Client.instance.myId)
        {
            _player = Instantiate(myCharacter, _position, _rotation);
        }
        else
        {
            _player = Instantiate(myCharacter, _position, _rotation);
        }
        _player.GetComponent<PlayerManager>().id = _id;
        _player.GetComponent<PlayerManager>().username = _username;
        players.Add(_id, _player.GetComponent<PlayerManager>());
    }
}