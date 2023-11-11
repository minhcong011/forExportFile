using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class BaseSkill : MonoBehaviour
{
    public float speed;
    public bool canMove = false;

    [SerializeField] private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider;
    private void Move()
    {
        if (canMove)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - ( speed * Time.deltaTime), transform.position.z);
        }
    }

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        Move();
    }

    protected void Fade()
    {
        boxCollider.enabled = false;
        spriteRenderer.DOFade(0,0.5f).OnComplete(()=> {
            Destroy(this.gameObject);
        });
    }
}
