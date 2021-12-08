using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public int noiseWidth;
    public int noiseHeight;
    public float noiseScale;

    public int octaves;
    [Range(0, 1)] public float persistance;
    public float lacunarity;

    public int seed;
    public Vector2 offset;

    public bool autoUpdate;
    public TerrianType[] regions;


    public void GenerateMap()
    {
        float[,] noiseMap = Noise.GenerateMap(seed, noiseWidth, noiseHeight, noiseScale, octaves, persistance, lacunarity, offset);
        Color[] colorMap = new Color[noiseWidth * noiseHeight];
        for (int y = 0; y < noiseHeight; y++)
        {
            for (int x = 0; x < noiseWidth; x++)
            {
                float currentHeight = noiseMap[x, y];
                for (int i = 0; i < regions.Length; i++)
                {
                    if (currentHeight <= regions[i].height)
                    {
                        colorMap[y * noiseWidth + x] = regions[i].color;
                        break;
                    }

                }
            }
        }

        MapDisplay display = FindObjectOfType<MapDisplay>();
        // display.DrawNoiseMap(noiseMap);
        display.DrawColorMap(colorMap, noiseWidth, noiseHeight);
    }

    void OnValidate()
    {
        if (noiseWidth < 1)
            noiseWidth = 1;
        if (noiseHeight < 1)
            noiseHeight = 1;
        if (octaves < 1)
            octaves = 1;

    }
}

[System.Serializable]
public struct TerrianType
{
    [Range(0,1)]public float height;
    public Color color;
    public string name;
}
