using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scoreboard : MonoBehaviour
{
    static int playerCount = 0;
    static List<GameObject> scores = new List<GameObject>();
    static GameObject Column;

    public static void ScoreboardPlacing()
    {
        Column = HUD.instance.transform.GetChild(11).GetChild(1).GetChild(1).gameObject;
        foreach (ServerClient client in Server.clients.Values)
        {
            if (client.connected)
            {
                playerCount++;
                GameObject score = Instantiate(Resources.Load("Overlays/Score"), GameObject.Find("Scores").transform) as GameObject;
                string username = Server.clients[playerCount + 1].username;
                if (username == null)
                {
                    username = (playerCount).ToString();
                }
                score.transform.GetChild(0).gameObject.GetComponent<Text>().text = username;
                score.transform.GetChild(1).gameObject.GetComponent<Text>().text = "0";
                score.transform.GetChild(2).gameObject.GetComponent<Text>().text = "0";
                scores.Add(score);
            }
        }
        if (playerCount > 12)
        {
            Column.SetActive(true);
        }
    }

    /*
    public void Update()
    {
        int i = 0;
        foreach (ServerClient client in Server.clients.Values)
        {
            if (client.connected)
            {
                i++;
                GameObject updateScore;
                if (i == 1)
                {
                    updateScore = GameObject.Find("Column 1/Score");
                }
                else
                {
                    updateScore = GameObject.Find("Column 2/Score" + (" (" + (i - 1) + ")"));
                }
                scores[i].GetComponent<Text>().text = client.player.kills.ToString();
                scores[i].GetComponent<Text>().text = "0";
                Debug.Log(client.username + " Kills: " + client.player.kills);
            }
        }

    }
    */

    public static void updateScoreboard()
    {
        int i = 0;
        foreach (ServerClient client in Server.clients.Values)
        {
            if (client.connected)
            {
                i++;
                scores[i - 1].GetComponent<Text>().text = client.player.kills.ToString();
                scores[i - 1].GetComponent<Text>().text = "0";
                Debug.Log(client.username + " Kills: " + client.player.kills);
            }
        }
    }

    public static void Reset()
    {
        foreach (GameObject score in scores)
        {
            Destroy(score);
        }
        scores.Clear();
        Column.SetActive(false);
    }
}
