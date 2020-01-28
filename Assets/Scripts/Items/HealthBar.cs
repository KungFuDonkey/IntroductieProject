using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private float maxXValue;
    private float minXValue;
    public RectTransform healthTransform;
     public Vector3 position;
    Color32 color;
    float stepOffset;
    private float cachedY;
    public float maxHealth;
    public float currentHealth;
    public Image visualHealth;

    public static HealthBar instance;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        cachedY = healthTransform.position.y;
        position = healthTransform.position;
        maxXValue = healthTransform.position.x;
        minXValue = healthTransform.position.x - healthTransform.rect.width;
        stepOffset = healthTransform.rect.width / maxHealth;
        currentHealth = maxHealth;
        color = new Color32(0, 100, 0, 255);
        visualHealth.color = color;
    }

    public void Update()
    {
        float currentXValue = currentHealth * stepOffset + minXValue;
        color.g = (byte)currentHealth;
        color.r = (byte)(100 - currentHealth);
        position.x = currentXValue;
        healthTransform.position = position;
        visualHealth.color = color;
    }
}
