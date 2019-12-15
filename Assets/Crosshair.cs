using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Crosshair : MonoBehaviour
{
    public RectTransform top, bottom, right, left;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            top.position = new Vector3((float)646.5, (float)326.5, 0);
            bottom.position = new Vector3((float)646.5, (float)302.8, 0);
            right.position = new Vector3((float)657.5, (float)313.5, 0);
            left.position = new Vector3((float)633.8, (float)313.5, 0);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            top.position = new Vector3((float)646.5, (float)331.5, 0);
            bottom.position = new Vector3((float)646.5, (float)297.8, 0);
            right.position = new Vector3((float)662.5, (float)313.5, 0);
            left.position = new Vector3((float)628.8, (float)313.5, 0);
        }
    }
}
