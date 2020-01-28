using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walls
{
    public static Transform[] walls = new Transform[4];
    public static Vector3[] startingPos = new Vector3[4];
    static int smaller, wallsMove = 26;
    static float wallsWait, WALLSWAIT = 25f, mapSize = 600, preGameTimer = 30;
    static bool[] wallMoving = new bool[4];
    static bool waiting = false;
    static Vector3[] distances = new Vector3[4];
    static Vector3 circlePosition;

    //Updates the position of the walls
    public static void UpdateWalls()
    {
        if (preGameTimer > 0 || GameManager.instance.freezeInput)
        {
            preGameTimer -= Time.deltaTime;
        }
        else
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
                setDistances(circlePosition.x, circlePosition.z);
                wallsWait = WALLSWAIT;
                waiting = true;
                smaller += 1;
                WALLSWAIT -= 1;
                wallsMove -= 1;
                mapSize *= 0.75f;
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
    }

    //Set the new position based on a Vector3 and the scale based on the position of the other walls
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
                walls[i].localScale = new Vector3(walls[2].position.x - walls[3].position.x + 0.01f, 200, 0.01f);
                walls[i].position = new Vector3((walls[2].position.x + walls[3].position.x) / 2, 20, walls[i].position.z);
            }
            else
            {
                walls[i].localScale = new Vector3(walls[0].position.z - walls[1].position.z + 0.01f, 200, 0.01f);
                walls[i].position = new Vector3(walls[i].position.x, 20, (walls[0].position.z + walls[1].position.z) / 2);
            }
        }
    }

    //Gives the position where the walls should move to
    public static void setDistances(float x, float z)
    {
        float eighth = (mapSize * 0.125f);
        float quarter = (mapSize * 0.25f);
        circlePosition = new Vector3(Random.Range(x - eighth, x + eighth), 20, Random.Range(z - eighth, z + eighth));
        distances[0].z = circlePosition.z - walls[0].position.z + quarter;
        distances[1].z = circlePosition.z - walls[1].position.z - quarter;
        distances[2].x = circlePosition.x - walls[2].position.x + quarter;
        distances[3].x = circlePosition.x - walls[3].position.x - quarter;
        Debug.Log("New circle: " + circlePosition.ToString());
    }

    //Reset function for when the game is reset
    public static void Reset()
    {
        for(int i = 0; i < 4; i++)
        {
            walls[i].position = startingPos[i];
        }
        wallsWait = 0f;
        WALLSWAIT = 25f;
        wallsMove = 26;
        mapSize = 600;
        waiting = false;
        smaller = 0;
        ServerSend.SetWalls();
    }
}
