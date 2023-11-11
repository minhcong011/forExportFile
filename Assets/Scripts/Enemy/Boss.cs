using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Boss : MonoBehaviour,IDead, IDrown
{
    public float health;
    public float duration;

    private float maxHealth;

    public GameObject healthBar;

    private EnemiesController enemiesController;

    //===========================

    public GameObject bullet;
    public Transform Target;
    public float firingAngle = 45.0f;
    public Vector2 reloadGun;


    bool canMove = true;
    bool isChecked = false ;
    bool canShoot = true;
    public float speed = 1.5f;

    public Transform shootPoint;
    public Transform shootPoint2;

    public BossKind bossKind;

    public SpriteRenderer spriteRenderer;

    private BoxCollider2D boxCollider;

    private int bossID;
    public void Init(EnemiesController enemiesController, int bossID)
    {
        this.bossID = bossID;
        this.enemiesController = enemiesController;
    }
    public void SetSprite(Sprite sprite)
    {
        this.spriteRenderer.sprite = sprite;
    }


    private void Start()
    {
        maxHealth = health;
        canMove = true;
        canShoot = true;
        boxCollider = GetComponent<BoxCollider2D>();
    }

    public void DrowningShip()
    {
        this.transform.DOMoveY(transform.position.y - 5f, 3f).OnComplete(() => {
            Destroy(this.gameObject);
        });
    }

    private void Move()
    {
        if (canMove)
        {
            transform.position = new Vector3(transform.position.x + -1 * speed * Time.deltaTime, transform.position.y, transform.position.z);
        }
        if(!isChecked && transform.position.x <= 7.38)
        {
            isChecked = true;
            canMove = false;
            switch (bossKind)
            {
                case BossKind.oneCanon:
                    StartShoot();
                    break;
                case BossKind.twoCanon:
                    StartShoot();
                    StartShootSecondCanon();
                    break;
                default:
                    StartShoot();
                    break;
            }
    
        }
    }

    void Update()
    {
        Move();
    }

    public void TakeDame(float value)
    {
        health -= value;

        if(health <= 0)
        {
            canShoot = false;
            boxCollider.enabled = false;
            healthBar.SetActive(false);
            DrowningShip();

            GameController.controller.SetArchivement(bossID, true);
            GameController.controller.UpdateScore(20);
            GameController.controller.UpdateCombo();
            GameController.controller.CreateEmote(this.transform.position);

            enemiesController.SpawnWave();

    
            return;
        }

        float percentHealth = (health / maxHealth);

        healthBar.transform.DOScale(new Vector3(percentHealth, healthBar.transform.localScale.y), duration);
    }



    private void StartShoot()
    {
        StartCoroutine(Fire());
    }

    private void StartShootSecondCanon()
    {
        StartCoroutine(FireSecondCanon());
    }

    private IEnumerator Fire()
    {

        float time = Random.Range(reloadGun.x, reloadGun.y);

        yield return new WaitForSeconds(time);

        Vector3 spawnPositon = shootPoint.position;

        float target_Distance = Vector3.Distance(transform.position, Target.position);
        Vector2 direction = (Target.position - (transform.position)).normalized;


        float projectile_Velocity = target_Distance / (Mathf.Sin(2 * firingAngle * Mathf.Deg2Rad) / (Physics2D.gravity.y * -1));

        float Vx = Mathf.Sqrt(projectile_Velocity) * Mathf.Cos(firingAngle * Mathf.Deg2Rad);
        float Vy = Mathf.Sqrt(projectile_Velocity) * Mathf.Sin(firingAngle * Mathf.Deg2Rad);

        Vector2 vel = new Vector2(Vx * direction.x, Vy);


        if (GameController.controller.isGameover || !canShoot) yield break;
        GameObject go = Instantiate(bullet, spawnPositon, new Quaternion());
        GameController.controller.PlaySound(soundEffect.gunShoot);
        go.GetComponent<Rigidbody2D>().AddForce(vel, ForceMode2D.Impulse);

        StartCoroutine(Fire());
    }

    private IEnumerator FireSecondCanon()
    {

        float time = Random.Range(reloadGun.x, reloadGun.y);

        yield return new WaitForSeconds(time);

        Vector3 spawnPositon = shootPoint2.position;

        float target_Distance = Vector3.Distance(transform.position, Target.position);
        Vector2 direction = (Target.position - (transform.position)).normalized;


        float projectile_Velocity = target_Distance / (Mathf.Sin(2 * firingAngle * Mathf.Deg2Rad) / (Physics2D.gravity.y * -1));

        float Vx = Mathf.Sqrt(projectile_Velocity) * Mathf.Cos(firingAngle * Mathf.Deg2Rad);
        float Vy = Mathf.Sqrt(projectile_Velocity) * Mathf.Sin(firingAngle * Mathf.Deg2Rad);

        Vector2 vel = new Vector2(Vx * direction.x, Vy);


        if (GameController.controller.isGameover || !canShoot) yield break;
        GameObject go = Instantiate(bullet, spawnPositon, new Quaternion());
        GameController.controller.PlaySound(soundEffect.gunShoot);
        go.GetComponent<Rigidbody2D>().AddForce(vel, ForceMode2D.Impulse);

        StartCoroutine(FireSecondCanon());
    }

    public void Reload()
    {
        StopCoroutine(Fire());
        StopCoroutine(FireSecondCanon());
    }
    
   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("playerBullet"))
        {
   
            TakeDame(20);
        }
    }

 
}
