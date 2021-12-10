using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public enum TileType
{
    Ground,
    Landform,
    Water,
    Mountain,
    Crack
}

public struct CustomTile
{
    public bool show;
    public RuleTile tile;
    public TileType tileType;
    [Range(0, 1)] public float height;

    public float tileScale;
    public int octaves;
    public float persistence;
    public float lacunarity;
    public Vector2 offset;

    public CustomTile(RuleTile newTile, float newLevel, TileType type)
    {
        show = true;
        tile = newTile;
        height = newLevel;
        tileType = type;
        tileScale = 24;
        octaves = 4;
        persistence = 0.06f;
        lacunarity = 1f;
        offset = new Vector2(0,0);
    }
}

public class RuleTilePainter : MonoBehaviour
{
    public bool autoLoad = true;
    public Dictionary<string, CustomTile> customPool = new Dictionary<string, CustomTile>();
    private void Awake()
    {
        // if (RuleTilePool.Instance.pool == null)
        //     return;
        // float landfomFill = 0.4f;
        // for (int i = 0; i < RuleTilePool.Instance.pool.Count; i++)
        // {
        //     float fillPercent = 0.4f;
        //     TileType type = TileType.Landform;
        //     if(RuleTilePool.Instance.pool[i].name == "Water")
        //         type = TileType.Water;
        //     else if(RuleTilePool.Instance.pool[i].name == "Crack")
        //         type = TileType.Crack;
        //     else if(RuleTilePool.Instance.pool[i].name == "Mountain")
        //         type = TileType.Mountain;
        //     else if(RuleTilePool.Instance.pool[i].name == "Ground")
        //         type = TileType.Ground;
        //     else
        //     {
        //         fillPercent = landfomFill;
        //         landfomFill -= 0.03f;
        //     }
            
        //     if (!customPool.ContainsKey(RuleTilePool.Instance.pool[i].name))
        //     {
        //         customPool.Add(RuleTilePool.Instance.pool[i].name, 
        //                         new CustomTile(RuleTilePool.Instance.pool[i], fillPercent, type));
        //     }  
        // }
    }
    public void DrawTileMap(string[,] virtualMap, Tilemap tileMap)
    {
        int width = virtualMap.GetLength(0);
        int height = virtualMap.GetLength(1);
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                Vector3Int newLocal = new Vector3Int(i - width / 2, j - height / 2, 0);
                if(virtualMap[i, j] == "Nothing")
                {
                    tileMap.SetTile(newLocal, null);
                    continue;
                }
                    
                
                tileMap.SetTile(newLocal, customPool[virtualMap[i, j]].tile);
            }
        }
    }

    public void DrawTileMap(float[,] heightMap, Tilemap tileMap)
    {
        int width = heightMap.GetLength(0);
        int height = heightMap.GetLength(1);
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                float currentHeight = heightMap[x, y];
                Vector3Int newLocal = new Vector3Int(x - width / 2, y - height / 2, 0);

                foreach (var pallete in customPool)
                {
                    if (currentHeight <= pallete.Value.height)
                    {
                        tileMap.SetTile(newLocal, pallete.Value.tile);
                        break;
                    }
                }
            }
        }
    }

    public void DrawDualMap(int[,] virtualMap, Tilemap tileMap, RuleTile drawtile)
    {
        int width = virtualMap.GetLength(0);
        int height = virtualMap.GetLength(1);
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                Vector3Int newLocal = new Vector3Int(i - width / 2, j - height / 2, 0);
                if(virtualMap[i, j] == 0)
                {
                    tileMap.SetTile(newLocal, null);
                    continue;
                }

                tileMap.SetTile(newLocal, drawtile);
            }
        }
    }
}
