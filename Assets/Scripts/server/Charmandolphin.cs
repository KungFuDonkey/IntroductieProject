using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charmandolphin : Player
{
    public Charmandolphin(int _id, string _username, int _selectedCharacter)
    {
        id = _id;
        username = _username;
        selectedCharacter = _selectedCharacter;
        groundmask = GameManager.instance.groundMask;
        inputs = new bool[11];
        animationValues = new bool[4]
        {
            false,
            false,
            false,
            false
        };
    }

    public override void UpdatePlayer()
    {
        base.UpdatePlayer();
    }
    protected override void Move(Vector3 _inputDirection, float moveSpeed)
    {
        base.Move(_inputDirection, moveSpeed);
    }
    
    public override void SetInput(bool[] _inputs, Quaternion _rotation)
    {
        base.SetInput(_inputs, _rotation);
    }
 
}
