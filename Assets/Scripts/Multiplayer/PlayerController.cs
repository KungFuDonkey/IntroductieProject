using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    bool[] _inputs;
    public Animator playerAnimator;
    private void FixedUpdate()
    {
        SendInputToServer();
        if (_inputs[0] || _inputs[1] || _inputs[2] || _inputs[3])
        {
            if (_inputs[5])
            {
                playerAnimator.SetBool("IsWalking", false);
                playerAnimator.SetBool("IsRunning", true);
            }
            else
            {
                playerAnimator.SetBool("IsWalking", true);
                playerAnimator.SetBool("IsRunning", false);
            }
        }
        else
        {
            playerAnimator.SetBool("IsWalking", false);
            playerAnimator.SetBool("IsRunning", false);
        }
    }

    private void SendInputToServer()
    {
        if (!GameManager.instance.freezeInput)
        {
            _inputs = new bool[]
            {
                Input.GetKey(KeyCode.W),
                Input.GetKey(KeyCode.S),
                Input.GetKey(KeyCode.A),
                Input.GetKey(KeyCode.D),
                Input.GetKey(KeyCode.Space),
                Input.GetKey(KeyCode.LeftShift),
                Input.GetKey(KeyCode.Q),
                Input.GetKey(KeyCode.E),
                Input.GetKey(KeyCode.P),
                Input.GetKey(KeyCode.V),
                Input.GetMouseButton(0)
            };

            ClientSend.PlayerMovement(_inputs);
        }
    }
}