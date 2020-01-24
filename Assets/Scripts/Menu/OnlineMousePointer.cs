using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnlineMousePointer : MonoBehaviour
{
    public void ChangePosition(Vector2 _position)
    {
        transform.localPosition = _position;
    }
}