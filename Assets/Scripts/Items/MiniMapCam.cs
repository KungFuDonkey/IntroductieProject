using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapCam : MonoBehaviour
{
    [SerializeField] public Transform playerTransform;
    [SerializeField] private float yOffset = 500;

    private void LateUpdate()
    {
        Vector3 targetPosition = playerTransform.position;
        targetPosition.y += yOffset;
        transform.position = targetPosition;
    }
}
