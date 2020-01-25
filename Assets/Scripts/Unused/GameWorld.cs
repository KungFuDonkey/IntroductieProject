using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameWorld : MonoBehaviour
{
    GameObject WorldTerrain;
    void Start()
    {
        Instantiate(WorldTerrain);
    }

    void Update()
    { 
    }
}
