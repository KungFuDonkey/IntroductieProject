using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scoreboard : MonoBehaviour
{
    void OnEnable()
    {
        ServerClient[] players = Server.GetAllClients();
        foreach (ServerClient player in players)
        {
            Debug.Log(player.username);
        }
        
    }
    public void ScoreboardPlacing()
    {
        ServerClient[] players = Server.GetAllClients();
        foreach (ServerClient player in players)
        {
            Debug.Log(player.username);
            Instantiate(Resources.Load("Overlays/Score"), player.player.avatar.GetChild(0).GetChild(8).GetChild(1).GetChild(0).GetChild(1));
        }
    }
}
