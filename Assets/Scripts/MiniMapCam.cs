﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapCam : MonoBehaviour
{
    [SerializeField] protected Transform playerTransform;
    [SerializeField] private float yOffset;
    private void LateUpdate()
    {
        Vector3 targetPosition = playerTransform.position;
        targetPosition.y += yOffset;
        transform.position = targetPosition;
    }
}
