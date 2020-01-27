using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scoreboard : MonoBehaviour
{
    public static void ScoreboardPlacing(int playerCount)
    {
        for (int i = 0; i < playerCount; i++)
        {
            GameObject score = Instantiate(Resources.Load("Overlays/Score"), GameObject.Find("Scores").transform) as GameObject;
            score.transform.GetChild(0).gameObject.GetComponent<Text>().text = Server.clients[i + 1].username;
        }
    }
}
