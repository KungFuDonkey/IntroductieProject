using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleBus : MonoBehaviour
{
    public static Transform Bus;
    static float busWait = 5;
    public static Vector3 busMovement = new Vector3(0, 0, 10);
    static Vector3 startPosition = new Vector3(0, 110, -350);
    public static bool canJump = false;

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

        if (busWait < 5)
        {
            if (Bus.transform.position.z < 400)
            {
                Bus.transform.position += busMovement * Time.deltaTime;
            }
        }
        ServerSend.SetBus();
    }

    public static void Reset()
    {
        Bus.position = startPosition;
        ServerSend.SetBus();
    }
}
