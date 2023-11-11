using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessBox : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;

    public void SetSprite(Sprite sprite)
    {
        spriteRenderer.sprite = sprite;
    }

    private void Start()
    {
        Destroy(this.gameObject, 1f);
    }
}
