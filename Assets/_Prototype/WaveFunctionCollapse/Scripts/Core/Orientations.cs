using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class Orientations
{
    public const int LEFT = 0;
    public const int DOWN = 1;
    public const int BACK = 2;
    public const int RIGHT = 3;
    public const int UP = 4;
    public const int FORWARD = 5;

    private static Vector3[] rotations;
    private static Vector3[] vectors;
    private static Vector3Int[] directions;

    public static Vector3[] Rotations
    {
        get
        {
            if (Orientations.rotations == null)
            {
                Orientations.initialize();
            }
            return Orientations.rotations;
        }
    }

    public static Vector3Int[] Direction
    {
        get
        {
            if (Orientations.directions == null)
            {
                Orientations.initialize();
            }
            return Orientations.directions;
        }
    }

    private static void initialize()
    {
        Orientations.vectors = new Vector3[] {
            Vector3.left,
            Vector3.down,
            Vector3.back,
            Vector3.right,
            Vector3.up,
            Vector3.forward
        };

        Orientations.rotations = Orientations.vectors;
        Orientations.directions = Orientations.vectors.Select(vector => Vector3Int.RoundToInt(vector)).ToArray();
    }

    public static readonly int[] HorizontalDirections = { 0, 2, 3, 5 };
    public static int Rotate(int direction, int rotation)
    {
        if (direction == 1 || direction == 4)
        {
            return direction;
        }
        return HorizontalDirections[(Array.IndexOf(HorizontalDirections, direction) + rotation) % 4];
    }

    public static bool IsHorizontal(int orientation)
    {
        return orientation != 1 && orientation != 4;
    }

    public static int GetIndex(Vector3 direction)
    {
        if (direction.x < 0)
        {
            return 0;
        }
        else if (direction.y < 0)
        {
            return 1;
        }
        else if (direction.z < 0)
        {
            return 2;
        }
        else if (direction.x > 0)
        {
            return 3;
        }
        else if (direction.y > 0)
        {
            return 4;
        }
        else
        {
            return 5;
        }
    }

    public static Vector3 GetRotateAngle(int rotation)
    {
        Vector3 euler = Vector3.zero;
        switch (rotation)
        {
            case 0:
                euler = Vector3.zero;
                break;
            case 1:
                euler = new Vector3(0, 90f, 0);
                break;
            case 2:
                euler = new Vector3(0, 180f, 0);
                break;
            case 3:
                euler = new Vector3(0, 270f, 0);
                break;
            default:
                euler = Vector3.zero;
                break;
        }
        return euler;
    }
}
