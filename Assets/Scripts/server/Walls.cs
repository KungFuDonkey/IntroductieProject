using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walls
{
    public static Transform[] walls = new Transform[4];
    protected static float wallsWait, WALLSWAIT = 10, wallsMove, WALLSMOVE = 10, mapSize = 600;
    public static Vector3 circlePosition;
    public static Vector3[] distances = new Vector3[4];
    static bool started = false, allWallsStop = true, waiting = false;
    static bool[] wallMoving = new bool[4];

    public static void UpdateWalls()
    {
        if (!started)
        {
            started = true;
            mapSize *= 0.5f;
            setDistances(mapSize, mapSize);
        }
        else if (!allWallsStop)
        {
            wallsMove -= Time.deltaTime;
        }
        else
        {
            wallsWait -= Time.deltaTime;
        }

        if (allWallsStop && wallsWait < 0 && !waiting)
        {
            mapSize *= 0.5f;
            setDistances(circlePosition.x + mapSize, circlePosition.z + mapSize);
            wallsWait = WALLSWAIT;
            waiting = true;
        }
        else if (wallsWait < 0 && allWallsStop)
        {
            for (int i = 0; i < 4; i++)
            {
                wallMoving[i] = true;
            }
            wallsMove = WALLSMOVE;
            waiting = false;
        }

        if (!allWallsStop)
        {
            moveWalls();
        }
        ServerSend.SetWalls();
    }

    public static void moveWalls()
    {
        for (int i = 0; i < 4; i++)
        {
            if (Vector3.Distance(circlePosition, walls[i].position) > mapSize / 2)
            {
                walls[i].position += distances[i] * Time.deltaTime / 10;
            }
            else
            {
                wallMoving[i] = false;
            }
        }
    }
    public static void setDistances(float x, float z)
    {
        allWallsStop = true;
        circlePosition = new Vector3(Random.Range(-x/2, x/2), 20, Random.Range(-z/2, z/2));
        for (int i = 0; i < 4; i++)
        {
            distances[i] = circlePosition - walls[i].position;
        }
        Debug.Log(circlePosition.ToString());
    }
}
