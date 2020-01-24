using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class heightgenereation : MonoBehaviour
{
    public int width;
    public int height;
    public float heightModifier;
    public float scale = 1.0f;
    public Vector2 offset;
    private float[,] pixelHeight;
    private Renderer renderer;
    Terrain terrain;
    TerrainData data;

    void Start()
    {
        renderer = GetComponent<Renderer>();
        terrain = Terrain.activeTerrain;
        data = terrain.terrainData;
        pixelHeight = new float[data.heightmapResolution, data.heightmapResolution];
        CalcNoise();
    }

    void CalcNoise()
    {
        float y = 0;
        while (y < height)
        {
            float x = 0;
            while (x < width)
            {
                float X = offset.x + x / width * scale;
                float Y = offset.y + y / height * scale;
                float pixelheight = Mathf.PerlinNoise(X, Y) * heightModifier;
                pixelHeight[(int)x,(int)y] = pixelheight;
                x++;
            }
            y++;
        }
        data.SetHeights(0, 0, pixelHeight);
    }
}
