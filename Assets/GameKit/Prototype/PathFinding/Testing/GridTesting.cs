using UnityEngine;

public class GridTesting : MonoBehaviour
{
    private void Start()
    {
        Grid<int> grid = new Grid<int>(4, 4, 1f);
        grid.SetValue(1, 1, 2);
    }
}