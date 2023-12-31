﻿// ILSpyBased#2
using UnityEngine;

namespace CI.QuickSave.Core.Models
{
    public class Quaternion
    {
        public float x;

        public float y;

        public float z;

        public float w;

        public UnityEngine.Quaternion ToUnityType()
        {
            return new UnityEngine.Quaternion(this.x, this.y, this.z, this.w);
        }

        public static Quaternion FromUnityType(UnityEngine.Quaternion quaternion)
        {
            return new Quaternion
            {
                x = quaternion.x,
                y = quaternion.y,
                z = quaternion.z,
                w = quaternion.w
            };
        }
    }
}


