using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class AttackEnemy : BaseEnemy, IShoot, IDead, IDrown
{
    public GameObject bullet;

    public Vector2 shootTime;


    public Transform Target;
    public float firingAngle = 45.0f;

    [SerializeField] private RotatingImage rotateShip;

    private bool canShoot = true;
    private void Start()
    {
        //StartCoroutine(SimulateProjectile());
        boxCollider = GetComponent<BoxCollider2D>();
        Shoot();
    }

    public void Shoot()
    {
        float time = Random.Range(shootTime.x, shootTime.y);
        StartCoroutine(Fire(time));
    }

    private IEnumerator Fire(float time)
    {
        yield return new WaitForSeconds(time);

        Vector3 spawnPositon = transform.position + new Vector3(0, 0.5f, 0);

        float target_Distance = Vector3.Distance(transform.position , Target.position);
        Vector2 direction = (Target.position - (transform.position )).normalized;


        float projectile_Velocity = target_Distance / (Mathf.Sin(2 * firingAngle * Mathf.Deg2Rad) / (Physics2D.gravity.y * -1));

        float Vx = Mathf.Sqrt(projectile_Velocity) * Mathf.Cos(firingAngle * Mathf.Deg2Rad);
        float Vy = Mathf.Sqrt(projectile_Velocity) * Mathf.Sin(firingAngle * Mathf.Deg2Rad);

        Vector2 vel = new Vector2(Vx * direction.x, Vy);

        if (!canShoot) yield break;
        GameObject go = Instantiate(bullet, spawnPositon, new Quaternion());
        GameController.controller.PlaySound(soundEffect.gunShoot);
        go.GetComponent<Rigidbody2D>().AddForce(vel, ForceMode2D.Impulse);
    }

    public override void TakeDame()
    {
        boxCollider.enabled = false;
        Reload();
        GameController.controller.UpdateScore(1);
        GameController.controller.UpdateCombo();
        GameController.controller.CreateEmote(this.transform.position);
    }

    public override void EventEndDrowing()
    {
        rotateShip.canRotate = false;
        Destroy(rotateShip);
        Destroy(this.gameObject);
    }

    public void Reload()
    {
        StopAllCoroutines();
        DrowningShip();
    }

    public void DrowningShip()
    {
        canMove = false;
        canShoot = false;
        this.transform.DOMoveY(transform.position.y - 5f, 3f).OnComplete(EventEndDrowing);
    }
}
