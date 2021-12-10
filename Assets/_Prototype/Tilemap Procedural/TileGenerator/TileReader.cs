using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.EventSystems;
using GameKit;

public class TileReader : MonoBehaviour
{
    public Dictionary<string, string[,]> moduleMap;
    public Tilemap landfromMap;
    public Tilemap groundMap;
    public Tilemap waterMap;
    public Tilemap crackMap;
    public Vector2Int pivotOffset = new Vector2Int(4, 6);
    public Vector2Int offset;
    public Vector2Int[,] pivotMap = new Vector2Int[5, 5];
    private int[] slice = new int[25] { 3, 2, 2, 3, 3,
                                        3, 2, 1, 2, 3,
                                        2, 1, 0, 1, 2,
                                        3, 2, 1, 2, 3,
                                        3, 3, 2, 2, 3 };
    public List<string> blockType = new List<string>();
    private Dictionary<string, int> dictionary = new Dictionary<string, int>();
    private Dictionary<int, List<GameObject>> newDict = new Dictionary<int, List<GameObject>>();
    private void Start()
    {
        Clear();
        LoadModule();
        EventCenter.instance.EventTrigger("RescanAstarMap");
    }

    public void Clear()
    {
        landfromMap.ClearAllTiles();
        // groundMap.ClearAllTiles();
        waterMap.ClearAllTiles();
        // crackMap.ClearAllTiles();
    }
    public void LoadModule()
    {
        List<GameObject> modules = new List<GameObject>();

        dictionary = new Dictionary<string, int>();
        dictionary.Add("GG", 1);
        dictionary.Add("D", 3);
        dictionary.Add("IL", 3);
        dictionary.Add("WG", 2);
        dictionary.Add("RL", 2);
        dictionary.Add("H", 0);

        foreach (var module in modules)
        {
            string themeName = module.gameObject.name.Split('_')[0];
            int level = dictionary[themeName];
            if (!newDict.ContainsKey(level))
                newDict.Add(level, new List<GameObject>());
            newDict[level].Add(module);
        }

        for (int x = 0; x < 5; x++)
        {
            for (int y = 0; y < 5; y++)
            {
                pivotMap[x, y] = new Vector2Int(-32 + x * 16 + 4, -32 + y * 16 + 6);
            }
        }

        int index = 0;
        for (int x = 0; x < 5; x++)
        {
            for (int y = 0; y < 5; y++)
            {
                int currentThreat = slice[index];
                int maxrand = newDict[currentThreat].Count - 1;
                int rand = Random.Range(0, maxrand);

                Tilemap[] tilemaps = newDict[currentThreat][rand].GetComponentsInChildren<Tilemap>();

                index++;
                foreach (var tilemap in tilemaps)
                {
                    Vector3Int newPos = new Vector3Int(tilemap.cellBounds.position.x + pivotMap[x, y].x, tilemap.cellBounds.position.y + pivotMap[x, y].y, 0);
                    TileBase[] tilebase = tilemap.GetTilesBlock(tilemap.cellBounds);
                    BoundsInt newBounds = new BoundsInt(newPos, tilemap.cellBounds.size);

                    if (tilemap.gameObject.name == "Crack")
                    {
                        crackMap.SetTilesBlock(newBounds, tilebase);
                    }
                    else if (tilemap.gameObject.name == "Water")
                    {
                        waterMap.SetTilesBlock(newBounds, tilebase);
                    }
                    else if (tilemap.gameObject.name == "Landform")
                    {
                        landfromMap.SetTilesBlock(newBounds, tilebase);
                    }
                    else if (tilemap.gameObject.name == "BackGround")
                    {
                        groundMap.SetTilesBlock(newBounds, tilebase);
                    }
                }
                // Vector3 center = new Vector3(-16 + x * 8, -16 + y * 8, 0) * 2 * 0.99f;
                // GameObject zone = Instantiate(PrefabPool.Instance.GetPrefab("Zone_DetectArea"), center, Quaternion.identity, GlobalRefer.instance.zoneDetect.transform);
                // blockType.Add(newDict[currentThreat][rand].gameObject.name);
                // zone.GetComponent<Zone_DetectArea>().Init(index - 1, newDict[currentThreat][rand].gameObject.name, dictionary[newDict[currentThreat][rand].gameObject.name.Split('_')[0]]);
                // newDict[currentThreat].RemoveAt(rand);
            }
        }
    }
}