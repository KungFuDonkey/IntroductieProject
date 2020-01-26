using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    bool[] _inputs;
    public Animator playerAnimator;
    KeyCode[] keys;

    private void Start()
    {
        keys = new KeyCode[12]{
            KeyCode.W,
            KeyCode.S,
            KeyCode.A,
            KeyCode.D,
            KeyCode.Space,
            KeyCode.LeftShift,
            KeyCode.Q,
            KeyCode.E,
            KeyCode.I,
            KeyCode.V,
            KeyCode.W, //Non existent, overwritten by mousebutton
            KeyCode.Z
        };
        _inputs = new bool[12];
    }

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
            for(int i = 0; i<10; i++)
            {
                _inputs[i] = Input.GetKey(keys[i]);
            }
            _inputs[10] = Input.GetMouseButton(0);
            ClientSend.PlayerMovement(_inputs);
        }
        else
        {
            for(int i = 0; i<11; i++)
            {
                _inputs[i] = false;
            }
            ClientSend.PlayerMovement(_inputs);
        }
    }
}