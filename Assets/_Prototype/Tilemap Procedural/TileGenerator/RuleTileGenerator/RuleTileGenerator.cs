using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using GameKit;

public class RuleTileGenerator : MonoBehaviour
{
    private enum GenerateMode { Perlin, Smooth, Flood, Product }
    [SerializeField] private GameObject gridObj;
    [SerializeField] private Tilemap landformMap;
    [SerializeField] private Tilemap groundMap;
    [SerializeField] private Tilemap waterMap;
    [SerializeField] private Tilemap mountainMap;
    [SerializeField] private Tilemap crackMap;

    [Space, Header("参数")]
    private RuleTilePainter ruleTilePainter;
    private string[,] virtualMap;
    private string[,] landformVirtual;
    private List<Vector2Int> calFactor = new List<Vector2Int>();
    private List<Vector2Int> visitList = new List<Vector2Int>();
    private List<Vector2Int> visitedList = new List<Vector2Int>();
    private List<Vector2Int> initVisitedList;

    [SerializeField] private GenerateMode GMode = GenerateMode.Perlin;
    public int seed;
    public bool autoUpdate;
    public Vector2Int tileSize = new Vector2Int(256, 256);
    public float tileScale;
    public int octaves;
    [Range(0, 1)]
    public float persistence;
    public float lacunarity;
    public Vector2 offset;

    [Space, Header("填充算法参数")]
    public float filledPercent;
    [Range(1, 10)]
    public int smoothTime = 4;
    [Range(1, 10)]
    public int smoothThrehold = 4;
    public float step;

    [Space, Header("柏林噪声随机范围")]
    public float minScale = 70f;
    public float lenScale = 10f;
    public int minOctave = 4;
    public int maxOctave = 6;
    public float minPersis = 0.05f;
    public float lenPersis = 0.2f;
    public int minLac = 1;
    public int maxLac = 50;



    public void Generate()
    {
        if (GMode == GenerateMode.Perlin)
            GenerateTileMap();
        else if (GMode == GenerateMode.Smooth)
            GenerateTileMap();
        else if (GMode == GenerateMode.Flood)
            GenerateTileByFlood();
        else
        {
            GenerateGround();
            GenerateLandform();
            // GenerateCrack();
        }
        EventManager.instance.EventTrigger("UpdateOxygenMap");
    }
    private void Start()
    {
        ruleTilePainter = GetComponent<RuleTilePainter>();
        InitFlood();
        if (GameObject.Find("Grid"))
        {
            gridObj = GameObject.Find("Grid");
            landformMap = GameObject.Find("Landform").GetComponent<Tilemap>();
            groundMap = GameObject.Find("Ground").GetComponent<Tilemap>();
            waterMap = GameObject.Find("Water").GetComponent<Tilemap>();
            mountainMap = GameObject.Find("Mountain").GetComponent<Tilemap>();
            crackMap = GameObject.Find("Crack").GetComponent<Tilemap>();
        }
        Generate();
        // gridObj = new GameObject("Grid", typeof(Grid));
        // tileMapObj = new GameObject("Tilemap", typeof(TilemapRenderer));
        // tileMapObj.transform.parent = gridObj.transform;
        // tileMapObj.transform.localPosition = Vector3.zero;
        // tilemap = tileMapObj.GetComponent<Tilemap>();
        // gridObj.transform.localScale = new Vector3(0.99f, 0.99f, 0.99f);
        // gridObj.GetComponent<Grid>().cellSize = new Vector3(0.99f, 0.99f, 0.99f);
    }

    private void Update()
    {
        if (!autoUpdate)
            return;
        Generate();
    }

    public string[,] GetLandform() => landformVirtual;

    private int[,] SmoothMap(int[,] map)
    {
        for (int i = 0; i < map.GetLength(0); i++)
        {
            for (int j = 0; j < map.GetLength(1); j++)
            {
                int surroundingTiles = GetSurroundingWalls(i, j, map);
                if (surroundingTiles > smoothThrehold)
                    map[i, j] = 1;
                else if (surroundingTiles < smoothThrehold)
                    map[i, j] = 0;
            }
        }
        return map;
    }

    private int GetSurroundingWalls(int posX, int posY, int[,] map)
    {
        int wallCount = 0;
        int width = map.GetLength(0);
        int height = map.GetLength(1);
        for (int i = posX - 1; i <= posX + 1; i++)
        {
            for (int j = posY - 1; j <= posY + 1; j++)
            {
                if (i >= 0 && i < width && j >= 0 && j < height)
                {
                    if (i != posX || j != posY)
                        wallCount += map[i, j];
                }
                else
                {
                    wallCount++;
                }
            }
        }
        return wallCount;
    }

    private void GenerateCrack()
    {
        int[,] crackVirtual = new int[tileSize.x, tileSize.y];
        int bias = tileSize.x / 10;

        for (int i = 0; i < 5; i++)
        {
            int xCord = Random.Range(0 + bias, tileSize.x - bias);
            int yCord = Random.Range(0 + bias, tileSize.y - bias);
            int randWidth = Random.Range(1, 10);
            int randHeight = 10 - randWidth;
            crackVirtual = GenerateBySmooth(crackVirtual, new Vector2Int(xCord, yCord), randWidth, randHeight);
        }

        // RuleTile drawTile = RuleTilePool.Instance.GetRuleTile("Crack");
        // ruleTilePainter.DrawDualMap(crackVirtual, crackMap, drawTile);

    }

    private int[,] GenerateBySmooth(int[,] map, Vector2Int center, int randWidth, int randHeight)
    {
        System.Random pseudoRandom = new System.Random(seed.ToString().GetHashCode());
        for (int i = center.x - randWidth; i < center.x + randWidth; i++)
        {
            for (int j = center.y - randHeight; j < center.y + randHeight; j++)
            {
                if (i < 0 || i >= map.GetLength(0) || j < 0 || j > map.GetLength(1))
                    continue;
                map[i, j] = (pseudoRandom.Next(0, 100) < filledPercent) ? 1 : 0;
            }
        }
        for (int j = 0; j < smoothTime; j++)
        {
            map = SmoothMap(map);
        }
        return map;
    }

    private int[,] GenerateBySmooth(int[,] map)
    {
        System.Random pseudoRandom = new System.Random(seed.ToString().GetHashCode());
        for (int i = 0; i < map.GetLength(0); i++)
        {
            for (int j = 0; j < map.GetLength(1); j++)
            {
                map[i, j] = (pseudoRandom.Next(0, 100) < filledPercent) ? 1 : 0;
            }
        }
        for (int j = 0; j < smoothTime; j++)
        {
            map = SmoothMap(map);
        }
        return map;
    }

    private void GenerateGround()
    {
        string[,] groundVirtual = new string[tileSize.x, tileSize.y];
        for (int i = 0; i < tileSize.y; i++)
        {
            for (int j = 0; j < tileSize.x; j++)
            {
                groundVirtual[i, j] = "Ground";
            }
        }
        ruleTilePainter.DrawTileMap(groundVirtual, groundMap);
    }
    private void GenerateLandform()
    {
        landformVirtual = new string[tileSize.x, tileSize.y];
        for (int i = 0; i < tileSize.y; i++)
        {
            for (int j = 0; j < tileSize.x; j++)
            {
                landformVirtual[i, j] = "Nothing";
            }
        }
        int indexOffset = 0;
        foreach (var tile in ruleTilePainter.customPool)
        {
            if (!tile.Value.show)
                continue;
            if (tile.Value.tileType != TileType.Landform)
                continue;
            Debug.Log(tile.Value.tile.name);

            System.Random randGenerator = new System.Random(seed + indexOffset);
            float randScale = minScale + lenScale * (float)randGenerator.NextDouble();
            int randOctave = randGenerator.Next(minOctave, maxOctave);
            float randPersis = minPersis + lenPersis * (float)randGenerator.NextDouble();
            int randLac = randGenerator.Next(minLac, maxLac);
            float randOffsetX = (float)randGenerator.NextDouble();
            float randOffsetY = (float)randGenerator.NextDouble();
            Vector2 randOffset = new Vector2(randOffsetX, randOffsetY);

            // float[,] heightMap = Noise.GenerateMap(seed + indexOffset, tileSize.x, tileSize.y, 
            //                                         tile.Value.tileScale, 
            //                                         tile.Value.octaves, 
            //                                         tile.Value.persistence, 
            //                                         tile.Value.lacunarity, 
            //                                         tile.Value.offset);

            float[,] heightMap = Noise.GenerateMap(seed + indexOffset, tileSize.x, tileSize.y,
                                        randScale,
                                        randOctave,
                                        randPersis,
                                        randLac,
                                        randOffset);

            for (int x = 0; x < tileSize.x; x++)
            {
                for (int y = 0; y < tileSize.y; y++)
                {
                    if (heightMap[x, y] <= tile.Value.height)
                    {
                        landformVirtual[x, y] = tile.Value.tile.name;
                    }
                }
            }
            indexOffset++;
        }

        ruleTilePainter.DrawTileMap(landformVirtual, landformMap);
    }


    public void GenerateTileMap()
    {

        float[,] heightMap = Noise.GenerateMap(seed, tileSize.x, tileSize.y, tileScale,
                                                octaves, persistence, lacunarity, offset);

        ruleTilePainter.DrawTileMap(heightMap, landformMap);
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
        ruleTilePainter.DrawTileMap(virtualMap, landformMap);
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
        foreach (var tile in ruleTilePainter.customPool)
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
