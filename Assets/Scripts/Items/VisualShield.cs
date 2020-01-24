using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VisualShield : MonoBehaviour
{

    private float maxXValue;
    private float minXValue;
    public RectTransform shieldTransform;
    Vector3 position;
    private float cachedY;
    public float maxShield;
    float stepOffset;
    public float currentShield;
    public Image visualShield;
   

    public static VisualShield instance;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        cachedY = shieldTransform.position.y;
        position = shieldTransform.position;
        maxXValue = shieldTransform.position.x;
        minXValue = shieldTransform.position.x - shieldTransform.rect.width;
        currentShield = maxShield;
        stepOffset = shieldTransform.rect.width / maxShield;
        visualShield.color = new Color32(0, 0, 255, 255);
    }

    private void Update()
    {
        float currentXValue = currentShield * stepOffset + minXValue;
        position.x = currentXValue;
        shieldTransform.position = position;
    }
}
