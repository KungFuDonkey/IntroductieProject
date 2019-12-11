using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapCam : MonoBehaviour
{
    public Transform playerTransform;
    [SerializeField] private float yOffset;
    void start()
    {
    }

    private void LateUpdate()
    {
        Vector3 targetPosition = playerTransform.position;
        targetPosition.y = yOffset;
        transform.position = targetPosition;
    }
}
