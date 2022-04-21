using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using GameKit;


public class MapPainter : MonoBehaviour
{
    [System.Serializable]
    public struct CustomTile
    {
        public Tile tile;
        [Range(0, 1)] public float level;

        public CustomTile(Tile newTile, float newLevel)
        {
            tile = newTile;
            level = newLevel;
        }
    }
    public Dictionary<string, CustomTile> tilePalletes = new Dictionary<string, CustomTile>();
    [SerializeField] private GameObject planeCanvas;

    void Start()
    {
        Tile[] tilePalleteArray = ResourceManager.instance.LoadPath<Tile>("Temp");
        if (tilePalleteArray == null)
            return;

        for (int i = 0; i < 2; i++)
        {
            if (!tilePalletes.ContainsKey(tilePalleteArray[i].name))
                tilePalletes.Add(tilePalleteArray[i].name, 
                                new CustomTile(tilePalleteArray[i], (float)(i + 1) / (float)tilePalleteArray.Length));
        }

        if (planeCanvas == null)
        {
            GameObject tmpObj = GameObject.Find("PlaneCanvas");
            if (tmpObj != null)
                planeCanvas = tmpObj;
            else
                planeCanvas = ResourceManager.instance.Load<GameObject>("Prefabs/PlaneCanvas");
        }
        planeCanvas.SetActive(false);
    }

    public void DrawTileMap(string[,] virtualMap, Tilemap tileMap)
    {
        int width = virtualMap.GetLength(0);
        int height = virtualMap.GetLength(1);
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                Vector3Int newLocal = new Vector3Int(i - width / 2, j - height / 2, 0);
                tileMap.SetTile(newLocal, tilePalletes[virtualMap[i, j]].tile);
            }
        }
    }



    public void DrawTileMap(float[,] heightMap, Tilemap tileMap)
    {
        int width = heightMap.GetLength(0);
        int height = heightMap.GetLength(1);
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                float currentHeight = heightMap[x, y];
                Vector3Int newLocal = new Vector3Int(x - width / 2, y - height / 2, 0);
                // tileMap.SetTile(newLocal, tilePalletes[virtualMap[i, j]].tile);
                foreach (var pallete in tilePalletes)
                {
                    if (currentHeight <= pallete.Value.level)
                    {
                        tileMap.SetTile(newLocal, pallete.Value.tile);
                        break;
                    }
                }
            }
        }
    }

    public void DrawCanvasMap(float[,] heightMap, Tilemap tileMap)
    {


        int width = heightMap.GetLength(0);
        int height = heightMap.GetLength(1);
        Color[] colorMap = new Color[width * height];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                colorMap[y * width + x] = Color.Lerp(Color.black, Color.white, heightMap[x, y]);
            }
        }

        Texture2D texture = new Texture2D(width, height);
        texture.filterMode = FilterMode.Point;
        texture.wrapMode = TextureWrapMode.Clamp;
        texture.SetPixels(colorMap);
        texture.Apply();


        Renderer textureRender = planeCanvas.GetComponent<MeshRenderer>();
        textureRender.sharedMaterial.mainTexture = texture;
        textureRender.transform.localPosition = Vector3.zero;
    }
}
