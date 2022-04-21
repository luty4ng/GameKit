using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PathFindingTesting : MonoBehaviour
{
    public Transform seeker;
    public Transform target;
    public PathFinding pathFinding;
    private void Awake()
    {
        pathFinding = new PathFinding(20, 20, cellSize: 1);
        // List<PathNode> path = pathFinding.FindPath(seeker.position, target.position);
        // pathFinding.VisualizePath(path);
    }

    public void UpdateGrid()
    {
        pathFinding.UpdateGrid(20, 20, cellSize: 1);
    }
}