// ILSpyBased#2
using UnityEngine;

namespace CI.QuickSave.Core.Models
{
    public class Color32
    {
        public byte r;

        public byte g;

        public byte b;

        public byte a;

        public UnityEngine.Color32 ToUnityType()
        {
            return new UnityEngine.Color32(this.r, this.g, this.b, this.a);
        }

        public static Color32 FromUnityType(UnityEngine.Color32 color32)
        {
            return new Color32
            {
                r = color32.r,
                g = color32.g,
                b = color32.b,
                a = color32.a
            };
        }
    }
}


