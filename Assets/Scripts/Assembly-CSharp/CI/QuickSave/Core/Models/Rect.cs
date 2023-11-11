// ILSpyBased#2
using UnityEngine;

namespace CI.QuickSave.Core.Models
{
    public class Rect
    {
        public float yMax;

        public float xMax;

        public float yMin;

        public float xMin;

        public float x;

        public float y;

        public float height;

        public float width;

        public Vector2 max;

        public Vector2 min;

        public Vector2 center;

        public Vector2 position;

        public Vector2 size;

        public UnityEngine.Rect ToUnityType()
        {
            UnityEngine.Rect result = default(UnityEngine.Rect);
            result.yMax = this.yMax;
            result.xMax = this.xMax;
            result.yMin = this.yMin;
            result.xMin = this.xMin;
            result.x = this.x;
            result.y = this.y;
            result.height = this.height;
            result.width = this.width;
            result.max = this.max.ToUnityType();
            result.min = this.min.ToUnityType();
            result.center = this.center.ToUnityType();
            result.position = this.position.ToUnityType();
            result.size = this.size.ToUnityType();
            return result;
        }

        public static Rect FromUnityType(UnityEngine.Rect rect)
        {
            return new Rect
            {
                yMax = rect.yMax,
                xMax = rect.xMax,
                yMin = rect.yMin,
                xMin = rect.xMin,
                x = rect.x,
                y = rect.y,
                height = rect.height,
                width = rect.width,
                max = Vector2.FromUnityType(rect.max),
                min = Vector2.FromUnityType(rect.min),
                center = Vector2.FromUnityType(rect.center),
                position = Vector2.FromUnityType(rect.position),
                size = Vector2.FromUnityType(rect.size)
            };
        }
    }
}


