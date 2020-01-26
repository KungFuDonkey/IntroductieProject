using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BusCamera : MonoBehaviour
{
    private void LateUpdate()
    {
        Vector3 targetPosition = BattleBus.Bus.position;
        targetPosition.y += 290;
        transform.position = targetPosition;
    }
}
