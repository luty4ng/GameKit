
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

public class PathFinding
{
    private const int UNIT_DIAGONAL_COST = 14;
    private const int UNIT_STRAIGHT_COST = 10;
    private Grid<PathNode> grid;
    private Heap<PathNode> openSet;
    private List<PathNode> closeSet;

    public PathFinding(int width, int height, int cellSize = 10)
    {
        grid = new Grid<PathNode>(width, height, cellSize, (int x, int y) => new PathNode(x, y));
        UpdateGrid(width, height, cellSize);
    }

    public void UpdateGrid(int width, int height, int cellSize = 10)
    {
        for (int x = 0; x < grid.Width; x++)
        {
            for (int y = 0; y < grid.Height; y++)
            {
                Vector2 nodePos = new Vector2(x, y) * cellSize;
                PathNode node = grid.GetValue(x, y);
                node.grid = grid;
                node.gCost = int.MaxValue;
                node.isWalkable = CalculateWalkable(x, y, cellSize);
                node.predecessor = null;
            }
        }
    }
    // 
    private bool CalculateWalkable(int x, int y, int cellSize)
    {
        bool hasObstacle = Physics2D.OverlapCircle(grid.GetPos(x, y), cellSize / 2, LayerMask.NameToLayer("Obstacle"));
        if (hasObstacle)
            UnityEngine.Debug.DrawLine(grid.GetPos(x, y), grid.GetPos(x, y) + Vector3.down / 4, Color.red, 100f);
        return !hasObstacle;
    }
    public List<PathNode> FindPath(Vector3 startPos, Vector3 endPos)
    {
        grid.GetXY(startPos, out int startX, out int startY);
        grid.GetXY(endPos, out int endX, out int endY);
        return FindPath(startX, startY, endX, endY);
    }

    public Vector3 FindNextPos(Vector3 startPos, Vector3 endPos)
    {
        grid.GetXY(startPos, out int startX, out int startY);
        grid.GetXY(endPos, out int endX, out int endY);
        List<PathNode> path = FindPath(startX, startY, endX, endY);
        PathNode nextNode = path[0];

        if (path.Count > 1)
            nextNode = path[1];

        if (nextNode == null)
            return startPos;
        else
            return grid.GetPos(nextNode.x, nextNode.y);
    }
    
    // Input: start and end position in grid map.
    // Output: The list of path node as the shortest path.
    public List<PathNode> FindPath(int startX, int startY, int endX, int endY)
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        PathNode startNode = grid.GetValue(startX, startY);
        PathNode endNode = grid.GetValue(endX, endY);

        openSet = new Heap<PathNode>(grid.Height * grid.Width);
        openSet.Add(startNode);
        closeSet = new List<PathNode>();

        startNode.gCost = 0;
        startNode.hCost = CalculateDisCost(startNode, endNode);

        while (openSet.Count > 0)
        {
            PathNode currentNode = openSet.RemoveFirst();

            if (currentNode == endNode)
            {
                stopwatch.Start();
                UnityEngine.Debug.Log("Path Found in " + stopwatch.ElapsedMilliseconds + " ms.");
                return VisualizePath(CalculatePath(endNode));
            }
            // openSet.Remove(currentNode);
            closeSet.Add(currentNode);

            foreach (var neighbourNode in currentNode.GetNeighbours())
            {
                if (closeSet.Contains(neighbourNode) || !neighbourNode.isWalkable)
                    continue;
                int gCost = currentNode.gCost + CalculateDisCost(currentNode, neighbourNode);
                if (gCost < neighbourNode.gCost)
                {
                    neighbourNode.predecessor = currentNode;
                    neighbourNode.gCost = gCost;
                    neighbourNode.hCost = CalculateDisCost(neighbourNode, endNode);

                    // UnityEngine.Debug.DrawLine(grid.GetPos(currentNode.x, currentNode.y), grid.GetPos(currentNode.x, currentNode.y) + Vector3.down / 4, Color.blue, 100f);
                    if (!openSet.Contains(neighbourNode))
                        openSet.Add(neighbourNode);
                }
            }
        }
        return null;
    }

    // Input: end Node.
    // Output: retraced path.
    // Retrace the path once the path finding is over.
    private List<PathNode> CalculatePath(PathNode endNode)
    {
        List<PathNode> path = new List<PathNode>();
        path.Add(endNode);
        PathNode currentNode = endNode;

        while (currentNode.predecessor != null)
        {
            path.Add(currentNode.predecessor);
            currentNode = currentNode.predecessor;
        }
        path.Reverse();
        return path;
    }

    // Input: current node n and end node
    // Output: h(n) cost
    // Calculate the heuristic cost by manhattan distance.
    private int CalculateDisCost(PathNode nodeA, PathNode nodeB)
    {
        int xDis = Mathf.Abs(nodeA.x - nodeB.x);
        int yDis = Mathf.Abs(nodeA.y - nodeB.y);
        int substract = Mathf.Abs(xDis - yDis);
        return UNIT_DIAGONAL_COST * Mathf.Min(xDis, yDis) + UNIT_STRAIGHT_COST * substract;
    }

    private PathNode GetLoswestFCostNode(List<PathNode> nodeList)
    {
        PathNode lowestNode = nodeList[0];
        for (int i = 0; i < nodeList.Count; i++)
        {
            if (nodeList[i].fCost < lowestNode.fCost)
                lowestNode = nodeList[i];
        }
        return lowestNode;
    }

    public List<PathNode> VisualizePath(List<PathNode> nodes)
    {
        foreach (var node in nodes)
        {
            UnityEngine.Debug.DrawLine(grid.GetPos(node.x, node.y), grid.GetPos(node.x, node.y) + Vector3.down / 4, Color.green, 100f);
        }
        return nodes;
    }

}