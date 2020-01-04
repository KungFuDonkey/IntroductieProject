using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private void FixedUpdate()
    {
        SendInputToServer();
    }

    private void SendInputToServer()
    {
        bool[] _inputs = new bool[]
        {
            Input.GetKey(KeyCode.W),
            Input.GetKey(KeyCode.S),
            Input.GetKey(KeyCode.A),
            Input.GetKey(KeyCode.D),
            Input.GetKey(KeyCode.LeftShift),
            Input.GetKey(KeyCode.Q),
            Input.GetKey(KeyCode.E),
            Input.GetKey(KeyCode.P),
            Input.GetKey(KeyCode.V),
            Input.GetMouseButton(0),
        };

        ClientSend.PlayerMovement(_inputs);
    }
}