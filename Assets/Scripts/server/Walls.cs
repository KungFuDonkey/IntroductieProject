using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walls
{
    public static Transform[] walls = new Transform[4];
    static float wallsWait, WALLSWAIT = 30, wallsMove = 30, mapSize = 600, startTimer = 5;
    static Vector3 circlePosition;
    static Vector3[] distances = new Vector3[4];
    static bool started = false, waiting = false;
    static bool[] wallMoving = new bool[4];
    static int smaller;

    public static void UpdateWalls()
    {
        bool allWallsStop = true;
        for (int i = 0; i < 4; i++)
        {
            if (wallMoving[i])
            {
                allWallsStop = false;
                break;
            }
        }

        if (allWallsStop)
        {
            wallsWait -= Time.deltaTime;

        }

        if (allWallsStop && !waiting)
        {
            mapSize *= 0.75f;
            setDistances(circlePosition.x, circlePosition.z);
            wallsWait = WALLSWAIT;
            waiting = true;
            smaller += 1;

        }
        else if (wallsWait < 0 && allWallsStop && smaller < 13)
        {
            allWallsStop = false;
            for (int i = 0; i < 4; i++)
            {
                wallMoving[i] = true;
            }
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
            if (Vector3.Distance(circlePosition, walls[i].position) > mapSize * 0.5f)
            {
                walls[i].position += distances[i] * Time.deltaTime / wallsMove;
            }
            else
            {
                wallMoving[i] = false;
            }
        }
    }
    public static void setDistances(float x, float z)
    {
        float quarter = (mapSize * 0.25f);
        float half = (mapSize * 0.5f);
        circlePosition = new Vector3(Random.Range(x - quarter, x + quarter), 20, Random.Range(z - quarter, z + quarter));
        distances[0] = circlePosition - walls[0].position + new Vector3(half, 0, 0);
        distances[1] = circlePosition - walls[1].position + new Vector3(-half, 0, 0);
        distances[2] = circlePosition - walls[2].position + new Vector3(0, 0, -half);
        distances[3] = circlePosition - walls[3].position + new Vector3(0, 0, half);
        Debug.Log(circlePosition.ToString());
    }
}
