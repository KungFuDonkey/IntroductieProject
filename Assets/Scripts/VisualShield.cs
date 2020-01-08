﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VisualShield : MonoBehaviour
{

    private float maxXValue;
    private float minXValue;
    public RectTransform healthTransform;
    private float cachedY;
    public int maxHealth;

    public int currentHealth;
    public Image visualHealth;
   

    public static VisualShield instance;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        cachedY = healthTransform.position.y;
        maxXValue = healthTransform.position.x;
        minXValue = healthTransform.position.x - healthTransform.rect.width;
        visualHealth.color = new Color32(255, 255, 255, 255);
        currentHealth = 0;
      
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("r") && currentHealth > 0)
        {
            CurrentHealth -= 1;
        }
        if (Input.GetKey("t") && currentHealth < maxHealth)
        {
            CurrentHealth += 1;
        }
    }

    public void HandleHealth()
    {
        float currentXValue = MapValues(currentHealth, 0, maxHealth, minXValue, maxXValue);

        healthTransform.position = new Vector3(currentXValue, cachedY);

        if (currentHealth == 0)
        {
            visualHealth.color = new Color32(255, 255, 255, 255);
        }
        else
        {
            visualHealth.color = new Color32(0, 0, 255, 255);        }
    }

    public float MapValues(float x, float inMin, float inMax, float outMin, float outMax)
    {
        return (x - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
    }

    public int CurrentHealth
    {
        get { return currentHealth; }
        set
        {
              Debug.Log("currentHealth:" + currentHealth);
            currentHealth = value;
            HandleHealth();
        }
    }
}
