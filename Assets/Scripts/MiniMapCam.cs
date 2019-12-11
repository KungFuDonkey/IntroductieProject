using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapCam : MonoBehaviour
{
    protected Transform playerTransform;
    [SerializeField] private float yOffset;
    void start()
    {
        playerTransform = gameObject.GetComponent<Transform>();
        Debug.Log(gameObject.GetComponent<Transform>().name);
    }
    private void LateUpdate()
    {
        Vector3 targetPosition = playerTransform.position;
        targetPosition.y = yOffset;
        transform.position = targetPosition;
    }
}
