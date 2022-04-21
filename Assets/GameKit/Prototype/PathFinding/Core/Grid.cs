using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Grid<TGrid>
{
    // public event System.EventHandler<> OnChanged;
    private int width;
    private int height;
    private float cellSize;
    private float offset;
    private TGrid[,] gridMap;
    private TextMesh[,] debugMap;

    public int Height
    {
        get
        {
            return height;
        }
    }

    public int Width
    {
        get
        {
            return width;
        }
    }

    public Grid(int width, int height, float cellSize = 10f, System.Func<int, int, TGrid> initialize = null)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        this.offset = cellSize / 2;
        this.gridMap = new TGrid[width, height];
        this.debugMap = new TextMesh[width, height];

        for (int x = 0; x < gridMap.GetLength(0); x++)
        {
            for (int y = 0; y < gridMap.GetLength(1); y++)
            {
                if (initialize != null)
                    gridMap[x, y] = initialize.Invoke(x, y);
                // debugMap[x, y] = DebugUtilities.GridVisual(gridMap[x, y].ToString(), GetWorldPosition(x, y) + new Vector3(cellSize, cellSize) * 0.5f, Color.white, fontSize: 5);
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), Color.white, 100f);
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), Color.white, 100f);
            }
        }
        Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height), Color.white, 100f);
        Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height), Color.white, 100f);
        Debug.Log("Creating Grid. Width: " + width + " Height: " + height);
    }

    private Vector3 GetWorldPosition(int x, int y) => new Vector3(x, y, 0) * cellSize;
    public void GetXY(Vector3 worldPosition, out int x, out int y)
    {
        x = Mathf.FloorToInt(worldPosition.x / cellSize);
        y = Mathf.FloorToInt(worldPosition.y / cellSize);
    }

    public void GetPos(int x, int y, out Vector3 pos)
    {
        pos = new Vector3(x * cellSize + offset, y * cellSize + offset);
    }

    public void SetValue(int x, int y, TGrid value)
    {
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            gridMap[x, y] = value;
            debugMap[x, y].text = value.ToString();
        }
    }

    public void SetValue(Vector3 worldPosition, TGrid value)
    {
        GetXY(worldPosition, out int x, out int y);
        SetValue(x, y, value);
    }

    public TGrid GetValue(int x, int y)
    {
        if (x >= 0 && y >= 0 && x < width && y < height)
            return gridMap[x, y];
        else
            return default(TGrid);
    }

    public TGrid GetValue(Vector3 worldPosition, TGrid value)
    {
        GetXY(worldPosition, out int x, out int y);
        return GetValue(x, y);
    }

    public Vector3 GetPos(int x, int y)
    {
        GetPos(x, y, out Vector3 worldPos);
        return worldPos;
    }

	

}


