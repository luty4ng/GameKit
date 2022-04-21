using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[ExecuteInEditMode]
public class TileGeneratorCave : MonoBehaviour
{
    
    private int[,] virtualMap;
    [Range(0, 100)] public float filledPercent;
    
    public int width;
    public int height;

    [Range(0,5)]public int smoothTime;
    public float seed;
    public bool useRandomSeed;

    private void Start()
    {
        // virtualMap = new int[width,height];
    }

    void Update()
    {
        virtualMap = new int[width,height];
        SmoothGeneration();
    }

    void SmoothGeneration()
    {
        // if (useRandomSeed)
        //     seed = System.DateTime.Now.ToString();
        System.Random pseudoRandom = new System.Random(seed.ToString().GetHashCode());
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if (i == 0 || i == width - 1 || j == 0 || j == height - 1)    
                    virtualMap[i, j] = 1;
                else
                    virtualMap[i, j] = (pseudoRandom.Next(0, 100) < filledPercent) ? 1 : 0;  //1是墙，0是空地  
            }
        }

        for (int i = 0; i < smoothTime; i++)
        {
            SmoothMap();
        }
    }



    void OnDrawGizmos()
    {
        if (virtualMap != null)
        {
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    Gizmos.color = virtualMap[i, j] == 1 ? Color.black : Color.white; //黑色代表道路，白色代表障碍  
                    Vector3 pos = new Vector3(-width / 2 + i + .5f, -height / 2 + j + .5f, 0);
                    Gizmos.DrawCube(pos, Vector3.one);
                }
            }
        }
    }

    int GetSurroundingWalls(int posX, int posY)  
    {  
        int wallCount = 0;  
        for (int i = posX-1; i <=posX+1; i++)  
        {  
            for (int j =posY-1; j <=posY+1; j++)  
            {  
                if (i>= 0 && i < width && j >= 0 && j < height)  
                {  
                    if (i != posX || j != posY)  
                        wallCount += virtualMap[i, j];  
                }  
                else  
                {  
                    wallCount++;  
                }  
            }  
        }  
        return wallCount;  
    }

    void SmoothMap()  
    {  
        for (int i = 0; i < width; i++)  
        {  
            for (int j = 0; j < height; j++)  
            {  
                int surroundingTiles = GetSurroundingWalls(i, j);  
                if (surroundingTiles > 4)  
                    virtualMap[i, j] = 1;  
                else if (surroundingTiles < 4)  
                    virtualMap[i, j] = 0;  
            }  
        }  
    }

}
