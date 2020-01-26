using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BusCamera : MonoBehaviour
{
    Transform bus;
    private void Start()
    {
        bus = GameManager.instance.BattleBus.transform;
    }
    private void LateUpdate()
    {
        Vector3 targetPosition = bus.position;
        targetPosition.y += 290;
        transform.position = targetPosition;
    }
}
