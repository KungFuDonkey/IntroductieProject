using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleBus : MonoBehaviour
{
    public static Transform Bus;
    static float busWait = 4;
    public static Vector3 busMovement = new Vector3(0, 0, 25);
    static Vector3 startPosition = new Vector3(0, 110, -380);
    public static bool canJump = false;

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
        else
        {
            Bus.position = new Vector3(0, 0, 600);
        }
        ServerSend.SetBus();
    }

    public static void Reset()
    {
        Bus.position = startPosition;
        ServerSend.SetBus();
    }
}
