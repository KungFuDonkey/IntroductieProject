using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walls
{
    public static Transform[] walls = new Transform[4];
    protected float wallsWait;
    protected float wallsMove;
    public static Vector3 circlePosition = new Vector3(Random.Range(-200,200), 20, Random.Range(-200, 200));
    void Start()
    {
        Debug.Log(circlePosition.ToString());
    }
    public static void UpdateWalls()
    {
        for (int i = 0; i < 4; i++)
        {
            float distance = Vector3.Distance(circlePosition, walls[i].position);
            walls[i].position += new Vector3(0,0,1) * Time.deltaTime;
        }

        ServerSend.SetWalls();
    }
}
