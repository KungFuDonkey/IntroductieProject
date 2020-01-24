using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walls
{
    public static Transform[] walls = new Transform[4];
    static float wallsWait, WALLSWAIT = 25f, wallsMove = 26f, mapSize = 600f;
    public static Vector3 circlePosition;
    static Vector3[] distances = new Vector3[4];
    public static Vector3[] startingPos = new Vector3[4];
    static bool waiting = false;
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
            WALLSWAIT -= 1;
            wallsMove -= 1;
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
        for (int i = 0; i < 4; i++)
        {
            if (i < 2)
            {
                walls[i].localScale = new Vector3(walls[2].position.x - walls[3].position.x + 0.01f, 65, 0.01f);
                walls[i].position = new Vector3((walls[2].position.x + walls[3].position.x) / 2, 20, walls[i].position.z);
            }
            else
            {
                walls[i].localScale = new Vector3(walls[0].position.z - walls[1].position.z + 0.01f, 65, 0.01f);
                walls[i].position = new Vector3(walls[i].position.x, 20, (walls[0].position.z + walls[1].position.z) / 2);
            }
        }
    }
    public static void setDistances(float x, float z)
    {
        float quarter = (mapSize * 0.25f);
        float half = (mapSize * 0.5f);
        circlePosition = new Vector3(Random.Range(x - quarter, x + quarter), 20, Random.Range(z - quarter, z + quarter));
        distances[0].z = circlePosition.z - walls[0].position.z + half;
        distances[1].z = circlePosition.z - walls[1].position.z - half;
        distances[2].x = circlePosition.x - walls[2].position.x + half;
        distances[3].x = circlePosition.x - walls[3].position.x - half;
        Debug.Log("New circle: " + circlePosition.ToString());
    }
    public static void Reset()
    {
        for(int i = 0; i < 4; i++)
        {
            walls[i].position = startingPos[i];
        }
        wallsWait = 0f;
        WALLSWAIT = 25f;
        wallsMove = 26f;
        mapSize = 600f;
        waiting = false;
        smaller = 0;
        ServerSend.SetWalls();
    }
}
