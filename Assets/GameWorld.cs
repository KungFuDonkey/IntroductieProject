using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameWorld : MonoBehaviour
{
    GameObject WorldTerrain;
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(WorldTerrain);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
