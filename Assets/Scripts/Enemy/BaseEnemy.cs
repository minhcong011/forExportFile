using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class BaseEnemy : MonoBehaviour
{
    public float speed;
    public int health;

    public bool canMove = false;

    protected BoxCollider2D boxCollider;
    protected EnemySkin skinShip;
    private void Move()
    {
        if (canMove)
        {
            transform.position = new Vector3(transform.position.x + -1 * speed * Time.deltaTime, transform.position.y, transform.position.z);
        }
    }

    public void Init()
    {
        skinShip = GetComponent<EnemySkin>();
        skinShip.Set();
    }

    void Update()
    {
        Move();
    }


    public virtual void TakeDame()
    {
       
    }


    //su kien vien dan cua player va cham vao con thuyen
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("playerBullet"))
        {
            TakeDame();
        }
    }

    public virtual void EventEndDrowing()
    {
        Debug.Log("EventEndDrowing");
    }

}
