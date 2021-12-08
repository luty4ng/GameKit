using UnityEngine;
public enum CellType
{
    Crack,
    Grass,
    Ice,
    Mine,
    Sand,
    Water,
    Wild
}

public class TileChunk
{
    public Vector3 position;
    public CellType cellType;
    
}