using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BusCamera : MonoBehaviour
{
    //Makes sure the position of the camera is correct
    private void LateUpdate()
    {
        Vector3 targetPosition = BattleBus.Bus.position;
        targetPosition.y += 178;
        targetPosition.z -= 52;
        transform.position = targetPosition;
    }
}
