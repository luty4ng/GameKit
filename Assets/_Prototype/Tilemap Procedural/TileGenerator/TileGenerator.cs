using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class TileGenerator : MonoBehaviour
{
    private enum GenerateMode { Perlin, Smooth, Flood }
    private GameObject gridObj;
    private GameObject tileMapObj;
    private Tilemap tilemap;
    private MapPainter mapPainter;

    private string[,] virtualMap;
    private List<Vector2Int> calFactor = new List<Vector2Int>();
    private List<Vector2Int> visitList = new List<Vector2Int>();
    private List<Vector2Int> visitedList = new List<Vector2Int>();
    private List<Vector2Int> initVisitedList;
    private GenerateMode GMode = GenerateMode.Perlin;
    public int seed;
    public bool autoUpdate;
    public Vector2Int tileSize = new Vector2Int(256, 256);
    public float tileScale;
    public int octaves;
    public float persistence;
    public float lacunarity;
    public Vector2 offset;
    public float filledPercent;
    public float smoothTime;
    public float Step;
    public void Generate()
    {
        if(GMode==GenerateMode.Perlin)
            GenerateTileMap();
        else if(GMode==GenerateMode.Smooth)
            GenerateTileMap();
        else if(GMode ==GenerateMode.Flood)
            GenerateTileByFlood(); 
    }
    private void Start()
    {
        mapPainter = GetComponent<MapPainter>();
        InitFlood();
        if (GameObject.Find("Grid") != null && GameObject.Find("Tilemap") != null)
        {
            gridObj = GameObject.Find("Grid");
            tileMapObj = GameObject.Find("Tilemap");
            return;
        } 
        gridObj = new GameObject("Grid", typeof(Grid));
        tileMapObj = new GameObject("Tilemap", typeof(TilemapRenderer));
        tileMapObj.transform.parent = gridObj.transform;
        tileMapObj.transform.localPosition = Vector3.zero;
        tilemap = tileMapObj.GetComponent<Tilemap>();
        gridObj.transform.localScale = new Vector3(0.99f, 0.99f, 0.99f);
        gridObj.GetComponent<Grid>().cellSize = new Vector3(0.99f, 0.99f, 0.99f);
    }

    private void Update()
    {
        if(!autoUpdate)
            return;
        Generate();
    }

    
    public void GenerateTileMap()
    {
        float[,] heightMap = Noise.GenerateMap(seed, tileSize.x, tileSize.y, tileScale,
                                                octaves, persistence, lacunarity, offset);
        
        mapPainter.DrawTileMap(heightMap, tilemap);
    }

    public void GenerateTileByFlood()
    {
        FlushSetting();
        while (true)
        {
            if (visitList.Count <= 0)
                break;

            int currentX = visitList[0].x;
            int currentY = visitList[0].y;

            foreach (var dir in calFactor)
            {
                Vector2Int newPos = new Vector2Int(currentX + dir.x, currentY + dir.y);
                if (newPos.x < 0 || newPos.x >= tileSize.x || newPos.y < 0 || newPos.y >= tileSize.y)
                    continue;
                if (visitedList.Contains(newPos) || visitList.Contains(newPos))
                    continue;

                visitList.Add(newPos);
                virtualMap[newPos.x, newPos.y] = virtualMap[currentX, currentY];
            }

            if (!visitedList.Contains(visitList[0]))
                visitedList.Add(visitList[0]);
            visitList.RemoveAt(0);
        }
        mapPainter.DrawTileMap(virtualMap,tilemap);
    }

    void FlushSetting()
    {
        virtualMap = new string[tileSize.x, tileSize.y];
        for (int i = 0; i < tileSize.y; i++)
        {
            for (int j = 0; j < tileSize.x; j++)
            {
                virtualMap[i, j] = "Nothing";
            }
        }
        visitList.Clear();
        visitedList.Clear();
        foreach (var tile in mapPainter.tilePalletes)
        {
            int randX = Random.Range(0, tileSize.x);
            int randY = Random.Range(0, tileSize.y);

            virtualMap[randX, randY] = tile.Key;
            if (tile.Key != "Nothing")
            {
                visitList.Add(new Vector2Int(randX, randY));
                visitedList.Add(new Vector2Int(randX, randY));
            }
        }
    }
    void InitFlood()
    {
        calFactor.Add(new Vector2Int(0, 1));
        calFactor.Add(new Vector2Int(0, -1));
        calFactor.Add(new Vector2Int(1, 0));
        calFactor.Add(new Vector2Int(-1, 0));
        calFactor.Add(new Vector2Int(1, 1));
        calFactor.Add(new Vector2Int(1, -1));
        calFactor.Add(new Vector2Int(-1, 1));
        calFactor.Add(new Vector2Int(-1, -1));
        FlushSetting();
    }


    void OnValidate()
    {
        if (tileSize.x < 1)
            tileSize.x = 1;
        if (tileSize.y < 1)
            tileSize.y = 1;
        if (octaves < 1)
            octaves = 1;
    }
}
