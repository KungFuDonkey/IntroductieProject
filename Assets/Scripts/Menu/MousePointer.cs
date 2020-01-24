﻿using System.Collections;
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
        Vector2 pos = new Vector2(Input.mousePosition.x + 12, Input.mousePosition.y - 26);
        transform.position = pos;
        ClientSend.MousePosition(transform.localPosition);
    }
}
