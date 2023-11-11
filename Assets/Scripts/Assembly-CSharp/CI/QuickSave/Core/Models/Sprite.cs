// ILSpyBased#2
using UnityEngine;

namespace CI.QuickSave.Core.Models
{
    public class Sprite
    {
        public Texture2D texture;

        public Rect rect;

        public Vector2 pivot;

        public float pixelsPerUnit;

        public Vector4 border;

        public UnityEngine.Sprite ToUnityType()
        {
            return UnityEngine.Sprite.Create(this.texture.ToUnityType(), this.rect.ToUnityType(), this.pivot.ToUnityType(), this.pixelsPerUnit, 0u, SpriteMeshType.Tight, this.border.ToUnityType());
        }

        public static Sprite FromUnityType(UnityEngine.Sprite sprite)
        {
            return new Sprite
            {
                texture = Texture2D.FromUnityType(sprite.texture),
                rect = Rect.FromUnityType(sprite.rect),
                pivot = Vector2.FromUnityType(sprite.pivot),
                pixelsPerUnit = sprite.pixelsPerUnit,
                border = Vector4.FromUnityType(sprite.border)
            };
        }
    }
}


