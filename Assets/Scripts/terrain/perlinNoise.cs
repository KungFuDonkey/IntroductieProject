using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class perlinNoise : MonoBehaviour
{
    // Start is called before the first frame update
    public int width;
    public int height;
    public float scale = 1.0f;
    public Vector2 offset;
    private Texture2D texture;
    private Color[] color;
    private Renderer renderer;

    void Start()
    {
        renderer = GetComponent<Renderer>();
        texture = new Texture2D(width, height);
        color = new Color[texture.width * texture.height];
        renderer.material.mainTexture = texture;
        CalcNoise();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void CalcNoise()
    {
        float y = 0;
        while (y < texture.height)
        {
            float x = 0;
            while (x < texture.width)
            {
                float X = offset.x + x / texture.width * scale;
                float Y = offset.y + y / texture.height * scale;
                float pixelcollor = Mathf.PerlinNoise(X, Y);
                color[(int)y * texture.width + (int)x] = new Color(pixelcollor, pixelcollor, pixelcollor);
                x++;
            }
            y++;
        }
        texture.SetPixels(color);
        texture.Apply();
    }
}
