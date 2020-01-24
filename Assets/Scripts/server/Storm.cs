using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storm : MonoBehaviour
{
    public void WallShrink()
    {
        Walls.walls[0].localScale = new Vector3(Walls.walls[1].position.x + Walls.walls[3].position.x, 65, 0.2f);
        Walls.walls[1].localScale = new Vector3(Walls.walls[2].position.x + Walls.walls[0].position.x, 65, 0.2f);
        Walls.walls[2].localScale = new Vector3(Walls.walls[3].position.x + Walls.walls[1].position.x, 65, 0.2f);
        Walls.walls[3].localScale = new Vector3(Walls.walls[0].position.x + Walls.walls[2].position.x, 65, 0.2f);
    }
}
