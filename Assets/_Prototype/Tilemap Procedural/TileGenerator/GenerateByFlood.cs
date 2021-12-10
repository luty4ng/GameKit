using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class GenerateByFlood
{
    private string[,] virtualMap;
    private List<Vector2Int> calFactor = new List<Vector2Int>();
    private List<Vector2Int> visitList = new List<Vector2Int>();
    private List<Vector2Int> visitedList = new List<Vector2Int>();
    private List<Vector2Int> initVisitedList = new List<Vector2Int>();

    public int width;
    public int height;
    public MapPainter tilePainter;


    public GenerateByFlood()
    {
        calFactor.Add(new Vector2Int(0, 1));
        calFactor.Add(new Vector2Int(0, -1));
        calFactor.Add(new Vector2Int(1, 0));
        calFactor.Add(new Vector2Int(-1, 0));
        calFactor.Add(new Vector2Int(1, 1));
        calFactor.Add(new Vector2Int(1, -1));
        calFactor.Add(new Vector2Int(-1, 1));
        calFactor.Add(new Vector2Int(-1, -1));

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

    public string[,] GenerateTileByFlood()
    {
        visitedList = initVisitedList;
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
        return virtualMap;
    }
}
