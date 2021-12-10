using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class TileGeneratorLandB : MonoBehaviour
{
    [SerializeField] private string[,] virtualMap;
    private Tilemap tilemap;
    private List<Vector2Int> calFactor = new List<Vector2Int>();
    [SerializeField] private List<Vector2Int> visitList = new List<Vector2Int>();
    [SerializeField] private List<Vector2Int> visitedList = new List<Vector2Int>();
    private List<Vector2Int> initVisitedList = new List<Vector2Int>();


    public int width;
    public int height;

    public MapPainter tilePainter;


    private void Start()
    {
        InitCalFactor();
        virtualMap = new string[width, height];

        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                virtualMap[i, j] = "Nothing";
            }
        }

        GenerateInitalNode();
        
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
        foreach (var tile in tilePainter.tilePalletes)
        {
            int randX = Random.Range(0, width);
            int randY = Random.Range(0, height);

            virtualMap[randX, randY] = tile.Key;
            if (tile.Key != "Nothing")
            {
                visitList.Add(new Vector2Int(randX, randY));
                visitedList.Add(new Vector2Int(randX, randY));
            }
        }
        initVisitedList = visitedList;
    }

    void GenerateNodeByFlood()
    {
        visitedList =  initVisitedList;
        while (true)
        {
            if (visitList.Count <= 0)
                break;

            int currentX = visitList[0].x;
            int currentY = visitList[0].y;

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
        }
    }


    void DrawTileMapByVirtual()
    {
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                Vector3Int newLocal = new Vector3Int(i - width / 2, j - height / 2, 0);
                // tilemap.SetTile(newLocal, tilePainter.tilePalletes[virtualMap[i, j]]);
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
