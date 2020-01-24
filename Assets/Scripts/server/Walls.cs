using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walls
{
    public static Transform[] walls = new Transform[4];
    static float wallsWait, WALLSWAIT = 30f, wallsMove = 30f, mapSize = 598f;
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
        //WallShrink();

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
        distances[0] = circlePosition - walls[2].position + new Vector3(0, 0, -half);
        distances[1] = circlePosition - walls[3].position + new Vector3(0, 0, half);
        distances[2] = circlePosition - walls[0].position + new Vector3(half, 0, 0);
        distances[3] = circlePosition - walls[1].position + new Vector3(-half, 0, 0);
        Debug.Log("New circle: " + circlePosition.ToString());
    }
    public static void Reset()
    {
        for(int i = 0; i < 4; i++)
        {
            walls[i].position = startingPos[i];
        }
        wallsWait = 0f;
        WALLSWAIT = 30f;
        wallsMove = 30f;
        mapSize = 600f;
        waiting = false;
        smaller = 0;
        ServerSend.SetWalls();
    }

    public static void WallShrink()
    {
        walls[0].localScale = new Vector3(Mathf.Abs(walls[2].position.x - walls[0].position.x) + Mathf.Abs(walls[3].position.x - walls[0].position.x) + 0.2f, 65, 0.2f);
        walls[1].localScale = new Vector3(Mathf.Abs(walls[3].position.x - walls[1].position.x) + Mathf.Abs(walls[2].position.x - walls[1].position.x) + 0.2f, 65, 0.2f);
        walls[2].localScale = new Vector3(Mathf.Abs(walls[0].position.z - walls[2].position.z) + Mathf.Abs(walls[1].position.z - walls[2].position.z) + 0.2f, 65, 0.2f);
        walls[3].localScale = new Vector3(Mathf.Abs(walls[1].position.z - walls[3].position.z) + Mathf.Abs(walls[0].position.z - walls[3].position.z) + 0.2f, 65, 0.2f);
        /*
        for (int i = 0; i < 4; i++)
        {
            if (i < 2)
            {
                walls[i].localScale = new Vector3(Mathf.Abs(walls[(i + 1) % 4].position.x - walls[i].position.x) + Mathf.Abs(walls[(i + 3) % 4].position.x - walls[i].position.x) + 0.2f, 65, 0.2f);
            }
            else
            {
                walls[i].localScale = new Vector3(Mathf.Abs(walls[(i + 1) % 4].position.z - walls[i].position.z) + Mathf.Abs(walls[(i + 3) % 4].position.z - walls[i].position.z) + 0.2f, 65, 0.2f);
            }
        }
        */
    }
}
