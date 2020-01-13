using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePointer : MonoBehaviour
{
    private void Start()
    {
        Cursor.visible = false;
    }
    void FixedUpdate()
    {
        transform.position = new Vector2(Input.mousePosition.x + 12, Input.mousePosition.y - 26);
        ClientSend.MousePosition(Input.mousePosition);
    }
}
