using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections;
using System.Collections.Generic;

public enum MapType
{
    Rectangle,
    Hexagonal,
    Isometric
}

[System.Serializable]
public class RuntimeTilemap
{
    public string name;
    public MapType mapType;
    public Tilemap tilemap;
    public List<TileChunk> blocks;

    public RuntimeTilemap(string name, Tilemap tilemap, MapType type)
    {
        this.name=  name;
        this.tilemap = tilemap;
        this.mapType = type;
        this.blocks = new List<TileChunk>();
    }

    private void GenerateMap()
    {
        TileBase[] tilebase = tilemap.GetTilesBlock(tilemap.cellBounds);
        for (int i = 0; i < tilebase.Length; i++)
        {
            blocks.Add(new TileChunk());
        }
    }
}

public class TilemapUpdater
{
    public List<RuntimeTilemap> tileMaps;


}