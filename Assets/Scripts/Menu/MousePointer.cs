using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePointer : MonoBehaviour
{
    // Start is called before the first frame update
    public float widthDiff = 0;
    public float heightDiff = 0;
    protected virtual void Start()
    {
        Cursor.visible = false;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        transform.position = new Vector3(Input.mousePosition.x + 12, Input.mousePosition.y - 26, Input.mousePosition.z);
    }
}