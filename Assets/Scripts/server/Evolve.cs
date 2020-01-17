using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Evolve 
{
    int _selectedCharacter;
    GameObject _Evolution;
    bool started = false, ended = false;
    float evolveTime = 4f, interPolation;
    PlayerStatus status;
    String evo, character;

    /*
    public void UpdateEvolve()
    {
        
        if (!started)
        {
            started = true;
            evolve();
            PlayerStatus.movable = false;
            PlayerStatus.silenced = true;
            status = _player.status;
        }

        if (evolveTime > 0)
        {
            evolveTime -= Time.deltaTime;
            interPolation = UnityEngine.Random.Range(0, 100);

            if (interPolation > evolveTime * (100/evolveTime))
            {
                GetChild(evo).enabled = true;
                GetChild(character).enabled = false;
            }
            else
            {
                GetChild(evo).enabled = false;
                GetChild(character).enabled = true;
            }
        }
        else
        {

        }
    }

    void evolve()
    {
        if (_selectedCharacter == 1)
        {
            evo = "Vulcasaur";
            character = "Charmandolphin";
        }
        else if (_selectedCharacter == 2)
        {
            evo = "McQuirtle";
            character = "Vulcasaur";
        }
        else
        {
            evo = "Charmandolphin";
            character = "McQuirtle";
        }
        _Evolution = _player.Instantiate(evo);
    }
    */
}
