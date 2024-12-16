using System;
using UnityEngine;
using UnityEngine.U2D;

public class GameTest : MonoBehaviour
{
    [SerializeField] private SpriteAtlas test;
    private void Start()
    {
        Sprite[] sprites = new Sprite[test.spriteCount];
        test.GetSprites(sprites);
        Array.Sort(sprites, (a, b) => string.Compare(a.name, b.name));
        string s = "";
        for(int i = 0; i < sprites.Length; i++)
        {
            s += sprites[i].name.Replace("(Clone)", "") + ",";
        }
    }
}
