using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vulcasaur : Player
{
    public Vulcasaur(int _id, string _username, int _selectedCharacter)
    {
        id = _id;
        username = _username;
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
}
