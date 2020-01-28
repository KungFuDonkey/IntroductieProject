using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleBus
{
    public static Transform Bus;
    public static Vector3 busMovement = new Vector3(0, 0, 25);
    public static bool canJump = false;
    static float busWait = 4f;
    static Vector3 startPosition = new Vector3(0, 80, -380);
    static bool finished = false;

    //Moves the bus across the map
    public static void UpdateBus()
    {
        if (busWait > 0)
        {
            busWait -= Time.deltaTime;
        }
        else
        {
            canJump = true;
        }
        if (Bus.position.z < 600)
        {
            Bus.position += busMovement * Time.deltaTime;
        }
        else if(!finished)
        {
            Bus.position = new Vector3(0, -100, 600);
            finished = true;
        }
        else
        {
            return;
        }
        ServerSend.SetBus();
    }

    public static void Reset()
    {
        Bus.position = startPosition;
        busWait = 4f;
        canJump = false;
        finished = false;
        ServerSend.SetBus();
    }
}
