using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNode : IHeap<PathNode>
{
    public int x;
    public int y;
    public Grid<PathNode> grid;

    public int gCost;
    public int hCost;
    public int fCost
    {
        get
        {
            return gCost + hCost;
        }
    }

    public bool isWalkable = true;

    private int heapIndex;
    public PathNode predecessor;
    public PathNode(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public override string ToString()
    {
        return x + "," + y;
    }

    public int index
    {
        get
        {
            return heapIndex;
        }
        set
        {
            heapIndex = value;
        }
    }

    public int CompareTo(PathNode nodeToCompare)
    {
        int compare = fCost.CompareTo(nodeToCompare.fCost);
        if (compare == 0)
            compare = hCost.CompareTo(nodeToCompare.hCost);
        return -compare;
    }

    public List<PathNode> GetNeighbours()
    {
        List<PathNode> neighbours = new List<PathNode>();
        if (this.x - 1 >= 0)
            neighbours.Add(grid.GetValue(this.x - 1, this.y));
        if (this.x + 1 < grid.Width)
            neighbours.Add(grid.GetValue(this.x + 1, this.y));
        if (this.y - 1 >= 0)
            neighbours.Add(grid.GetValue(this.x, this.y - 1));
        if (this.y + 1 < grid.Height)
            neighbours.Add(grid.GetValue(this.x, this.y + 1));
        return neighbours;
    }
}
