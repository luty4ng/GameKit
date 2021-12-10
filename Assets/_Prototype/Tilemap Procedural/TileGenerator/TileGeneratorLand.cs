using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using GameKit;


public class TileGeneratorLand : MonoBehaviour
{
    public Dictionary<string, Tile> tileDic = new Dictionary<string, Tile>();
    private string[,] virtualMap;
    private Tilemap tilemap;
    [Range(0, 100)] public float OverallPercent;
    private List<Vector2Int> calFactor = new List<Vector2Int>();
    [SerializeField] private List<Vector2Int> visitList = new List<Vector2Int>();
    [SerializeField] private List<Vector2Int> visitedList = new List<Vector2Int>();

    public int width;
    public int height;


    public int endThrehold = 6;


    private void Start()
    {
        InitCalFactor();
        int index = 0;
        while (true)
        {
            string tileName = "BriefTileMap_" + index.ToString();
            Tile tempTile = ResourceManager.instance.Load<Tile>("TileMaps/" + tileName);
            if (tempTile == null)
                break;
            
            if (!tileDic.ContainsKey(tileName))
            {
                tileDic.Add(tileName, tempTile);
            }
            index += 1;
        }

        virtualMap = new string[width, height];

        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                virtualMap[i, j] = "BriefTileMap_0";
            }
        }

        GameObject gridObj = new GameObject("Grid", typeof(Grid));
        GameObject tileMapObj = new GameObject("TileMap", typeof(TilemapRenderer));
        tileMapObj.transform.parent = gridObj.transform;
        tileMapObj.transform.localPosition = Vector3.zero;
        tilemap = tileMapObj.GetComponent<Tilemap>();
        gridObj.transform.localScale = new Vector3(0.99f, 0.99f, 0.99f);
        gridObj.GetComponent<Grid>().cellSize = new Vector3(0.99f, 0.99f, 0.99f);
        tileMapObj.GetComponent<TilemapRenderer>().sortingLayerName = "BackGround";

        GenerateInitalNode();
    }
    void Update()
    {
        if (visitList.Count <= 0)
            return;

        int currentX = visitList[0].x;
        int currentY = visitList[0].y;

        int surroundingTiles = GetNonDefaultTileCount(currentX, currentY);

        foreach (var dir in calFactor)
        {
            Vector2Int newPos = new Vector2Int(currentX + dir.x, currentY + dir.y);
            if (newPos.x < 0 || newPos.x >= width || newPos.y < 0 || newPos.y >= height)
                continue;
            if (visitedList.Contains(newPos) || visitList.Contains(newPos))
                continue;

            visitList.Add(newPos);
            virtualMap[newPos.x, newPos.y] = virtualMap[currentX, currentY];
        }

        if (!visitedList.Contains(visitList[0]))
            visitedList.Add(visitList[0]);
        visitList.RemoveAt(0);


        DrawTileMapByVirtual();
    }

    void InitCalFactor()
    {
        calFactor.Add(new Vector2Int(0, 1));
        calFactor.Add(new Vector2Int(0, -1));
        calFactor.Add(new Vector2Int(1, 0));
        calFactor.Add(new Vector2Int(-1, 0));
        calFactor.Add(new Vector2Int(1, 1));
        calFactor.Add(new Vector2Int(1, -1));
        calFactor.Add(new Vector2Int(-1, 1));
        calFactor.Add(new Vector2Int(-1, -1));
    }
    void GenerateInitalNode()
    {
        foreach (var tile in tileDic)
        {
            int randX = Random.Range(0, width);
            int randY = Random.Range(0, height);

            virtualMap[randX, randY] = tile.Key;
            if (tile.Key != "BriefTileMap_0")
            {
                visitList.Add(new Vector2Int(randX, randY));
                visitedList.Add(new Vector2Int(randX, randY));
            }
        }
    }

    void SmoothGenerateNodeByFlood()
    {

        int visitIndex = 0;
        while (true)
        {
            if (visitList.Count <= visitIndex)
                break;

            int currentX = visitList[visitIndex].x;
            int currentY = visitList[visitIndex].y;

            int surroundingTiles = GetNonDefaultTileCount(currentX, currentY);

            // if (surroundingTiles > endThrehold)
            //     continue;

            foreach (var dir in calFactor)
            {
                Vector2Int newPos = new Vector2Int(currentX + dir.x, currentY + dir.y);
                if (newPos.x < 0 || newPos.x >= width || newPos.y < 0 || newPos.y >= height)
                    continue;
                if (visitedList.Contains(newPos) || visitList.Contains(newPos))
                    continue;

                visitList.Add(newPos);
                virtualMap[newPos.x, newPos.y] = virtualMap[currentX, currentY];
            }

            if (!visitedList.Contains(visitList[visitIndex]))
                visitedList.Add(visitList[visitIndex]);
            visitList.RemoveAt(visitIndex);
        }
    }


    void DrawTileMapByVirtual()
    {
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                Vector3Int newLocal = new Vector3Int(i - width / 2, j - height / 2, 0);
                tilemap.SetTile(newLocal, tileDic[virtualMap[i, j]]);
            }
        }
    }

    int GetNonDefaultTileCount(int posX, int posY)
    {
        int wallCount = 0;
        for (int i = posX - 1; i <= posX + 1; i++)
        {
            for (int j = posY - 1; j <= posY + 1; j++)
            {
                if (i < 0 || i >= width || j < 0 || j >= height)
                    continue;

                if (i != posX || j != posY)
                {
                    if (virtualMap[i, j] != "BriefTileMap_0")
                        wallCount++;
                }
            }
        }
        return wallCount;
    }



}
