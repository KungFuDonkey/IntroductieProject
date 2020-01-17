using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VisualShield : MonoBehaviour
{

    private float maxXValue;
    private float minXValue;
    public RectTransform shieldTransform;
    private float cachedY;
    public float maxShield;

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
        maxXValue = shieldTransform.position.x;
        minXValue = shieldTransform.position.x - shieldTransform.rect.width;
        currentShield = maxShield;
    }

    public void HandleHealth()
    {
        float currentXValue = MapValues(currentShield, 0, maxShield, minXValue, maxXValue);

        shieldTransform.position = new Vector3(currentXValue, cachedY);

        if (currentShield == 0)
        {
            visualShield.color = new Color32(255, 255, 255, 255);
        }
        else
        {
            visualShield.color = new Color32(0, 0, 255, 255);        }
    }

    public float MapValues(float x, float inMin, float inMax, float outMin, float outMax)
    {
        return (x - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
    }

    public float CurrentShield
    {
        get { return currentShield; }
        set
        {
            currentShield = value;
            HandleHealth();
        }
    }
}
