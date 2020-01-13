using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walls
{
    public static Transform[] walls = new Transform[4];
    protected float wallsWait;
    protected float wallsMove;
    public static Vector3 circlePosition = new Vector3(Random.Range(-200,200), 20, Random.Range(-200, 200));
    public static void UpdateWalls()
    {
        for (int i = 0; i < 4; i++)
        {
            float distance = Vector3.Distance(circlePosition, walls[i].position);
            walls[i].position += Vector3.MoveTowards(walls[i].position, circlePosition, distance) * Time.deltaTime / 60;
        }

        ServerSend.SetWalls();
    }
}
