using System;
using UnityEngine;

namespace Game.Utils
{
    public static class MathUtils
    {
        public static float Remap(this float value, 
                                  float from1, float to1, 
                                  float from2, float to2,
                                  bool isClamped = false)
        {
            if (isClamped)
            {
                float min = Mathf.Min(from2, to2);
                float max = Mathf.Max(from2, to2);
                return Mathf.Clamp((value - from1) / (to1 - from1) * (to2 - from2) + from2, min, max);
            }
            else
            {
                return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
            }
        }
        
        public static Tuple<int, int> ToGridIndex(this Vector2 worldPos, float cellSize)
        {
            var x = Mathf.Abs((int)Math.Round(worldPos.x / cellSize, 0));
            var y = Mathf.Abs((int)Math.Round(worldPos.y / cellSize, 0));
            return new Tuple<int, int>(x, y);
        }
    }

    
}