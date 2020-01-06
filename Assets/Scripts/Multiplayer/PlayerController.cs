using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PlayerManager player;
    bool[] _inputs;
    bool[] animationValues = new bool[]
    {
        false,
        false,
        false,
        false,
    };

    private void FixedUpdate()
    {
        SendInputToServer();
        if (_inputs[0] || _inputs[1] || _inputs[2] || _inputs[3])
        {
            if (_inputs[5])
            {
                player.playerAnimator.SetBool("IsWalking", false);
                player.playerAnimator.SetBool("IsRunning", true);
            }
            else
            {
                player.playerAnimator.SetBool("IsWalking", true);
                player.playerAnimator.SetBool("IsRunning", false);
            }
        }
        else
        {
            player.playerAnimator.SetBool("IsWalking", false);
            player.playerAnimator.SetBool("IsRunning", false);
        }
        if (_inputs[10] || _inputs[6] || _inputs[7])
        {
            player.playerAnimator.SetTrigger("Attack");
        }
    }

    private void SendInputToServer()
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