using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walls
{
    public static Transform[] walls = new Transform[4];
    protected static float wallsWait = 10;
    protected static float wallsMove = 10;
    public static Vector3 circlePosition;
    public static Vector3[] distances = new Vector3[4];
    static bool started = false;
    public static void Start()
    {
        
    }
    public static void UpdateWalls()
    {
        if (!started)
        {
            started = true;
            circlePosition = new Vector3(Random.Range(-250, 250), 20, Random.Range(-250, 250));
            Debug.Log(circlePosition.ToString());
            for (int i = 0; i < 4; i++)
            {
                distances[i] = circlePosition - walls[i].position;
            }
        }
        for (int i = 0; i < 4; i++)
        {
            walls[i].position += distances[i] * Time.deltaTime / 60;
        }

        ServerSend.SetWalls();
    }
}
