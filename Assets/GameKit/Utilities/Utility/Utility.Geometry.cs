using System;
using System.Text;
using System.Security.Cryptography;
using System.Collections;
using UnityEngine;
namespace GameKit
{
    public static partial class Utility
    {
        public static class Geometry
        {
            public static float EuclidDistance(Vector3 orgin, Vector3 end)
            {
                return (orgin - end).sqrMagnitude; 
            }

            public static float EuclidDistance(Vector2 orgin, Vector2 end)
            {
                return (orgin - end).sqrMagnitude; 
            }
        }
    }
}

