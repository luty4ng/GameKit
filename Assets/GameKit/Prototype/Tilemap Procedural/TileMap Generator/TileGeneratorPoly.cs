using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileGeneratorPoly : MonoBehaviour
{
    public Dictionary<string, Tile> tileDic = new Dictionary<string, Tile>();
    private string[,] virtualMap;
    private Tilemap tilemap;
    [Range(0, 100)] public float OverallPercent;
    [SerializeField, Range(0, 100)] private float[] tilePercent;
    private List<Vector2Int> calFactor = new List<Vector2Int>();
    [SerializeField] private List<Vector2Int> visitList = new List<Vector2Int>();
    [SerializeField] private List<Vector2Int> visitedList = new List<Vector2Int>();

    public int width;
    public int height;
}
