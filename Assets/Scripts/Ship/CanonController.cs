using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class CanonController : MonoBehaviour
{
    public Transform StartPoint;
 
    public float firingAngle = 45f;
    public GameObject bullet;

    public int stepLr;
    public LineRenderer lr;

    public Transform Target;
    public MovingTarget movingTarget;

    private float gravityScale = 1f;
    private float gravityDrag = 0;

    public Button shootBtn;

    public shoot pharse;
    GameObject cloneBullet;

    public Transform timeGatlingImage;
    private void Start()
    {
        cam = Camera.main;
        shootBtn.onClick.AddListener(() => {
            if (pharse == shoot.Reloaded && kindCanon == kindCanon.normal)
            {
                pharse = shoot.Aim;
                movingTarget.StartMoving();

                if (!cloneBullet) {
                    GameObject go = Instantiate(bullet, StartPoint.position, new Quaternion());
                    go.GetComponent<PlayerBullet>().Init(this);
                    cloneBullet = go;
                }
                return;
            }

            if (pharse == shoot.Aim && kindCanon == kindCanon.normal)
            {
                pharse = shoot.Shoot;
                lr.positionCount = 0;
                movingTarget.StopMoving();
                Fires();
            }

        });
    }

    public void DisablePath()
    {
        lr.positionCount = 0;
    }

    public void DestroyBullet() {
        if (!cloneBullet) return;
        Destroy(cloneBullet);
    }


    private Vector2 veltical;
    public void UpdateTrajectory()
    {
        if (pharse != shoot.Aim)
        {
            Debug.Log("Path not ready");

            return;
        }

        float target_Distance = Vector3.Distance(StartPoint.position, Target.position);
        Vector2 direction = (Target.position - (StartPoint.position)).normalized;

        float projectile_Velocity = target_Distance / (Mathf.Sin(2 * firingAngle * Mathf.Deg2Rad) / (Physics2D.gravity.y * -1));

        float Vx = Mathf.Sqrt(projectile_Velocity) * Mathf.Cos(firingAngle * Mathf.Deg2Rad);
        float Vy = Mathf.Sqrt(projectile_Velocity) * Mathf.Sin(firingAngle * Mathf.Deg2Rad);

        Vector2 vel = new Vector2(Vx * direction.x, Vy);
        veltical = vel;

        Vector2[] trajectory = Plot((Vector2)StartPoint.position, vel, stepLr);
        lr.positionCount = trajectory.Length;

        Vector3[] positions = new Vector3[trajectory.Length];
        for (int i = 0; i < positions.Length; i++)
        {
            positions[i] = trajectory[i];
        }

        lr.SetPositions(positions);
    }

    private IEnumerator Fire()
    {

        yield return new WaitForSeconds(1f);

        Vector3 spawnPositon = StartPoint.position;

        float target_Distance = Vector3.Distance(StartPoint.position, Target.position);
        Vector2 direction = (Target.position - (StartPoint.position)).normalized;


        float projectile_Velocity = target_Distance / (Mathf.Sin(2 * firingAngle * Mathf.Deg2Rad) / (Physics2D.gravity.y * -1));

        float Vx = Mathf.Sqrt(projectile_Velocity) * Mathf.Cos(firingAngle * Mathf.Deg2Rad);
        float Vy = Mathf.Sqrt(projectile_Velocity) * Mathf.Sin(firingAngle * Mathf.Deg2Rad);

        Vector2 vel = new Vector2(Vx * direction.x, Vy);
        GameObject go = Instantiate(bullet, spawnPositon, new Quaternion());



        go.GetComponent<Rigidbody2D>().AddForce(vel, ForceMode2D.Impulse);


    }

    private void Fires()
    {
        if (!cloneBullet)
        {
            return;
        }
        GameController.controller.PlaySound(soundEffect.throwEffect);
        cloneBullet.GetComponent<PlayerBullet>().KindBullet(kindCanon);
        cloneBullet.AddComponent<Rigidbody2D>().AddForce(veltical, ForceMode2D.Impulse);
        cloneBullet.GetComponent<BulletRotation>().StartRotate();
        cloneBullet = null;
    }

    public Vector2[] Plot(Vector2 pos, Vector2 velocity, int steps)
    {
        Vector2[] results = new Vector2[steps];

        float timeStep = 0.01f; //Time.fixedDeltaTime / Physics2D.velocityIterations;
        Vector2 gravityAccel = Physics2D.gravity * gravityScale * timeStep * timeStep;

        float drag = 1f - timeStep * gravityDrag;
        Vector2 moveStep = velocity * timeStep;

        for (int i = 0; i < steps; i++)
        {
            moveStep += gravityAccel;
            moveStep *= drag;
            pos += moveStep;
            results[i] = pos;
        }

        return results;
    }


    private Camera cam;

    public float timeGatling = 10f; 
    public float powerGatling = 5000f;

    public bool isReload = false;
    public float reloadTime = 0.2f;
    public kindCanon kindCanon;
    private void Update()
    {
        if (kindCanon == kindCanon.gatling && Input.GetMouseButtonDown(0))
        {
            if (isReload)
            {
              
                return;
            }
            Vector3 worldPosition;
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = Camera.main.nearClipPlane;
            worldPosition = Camera.main.ScreenToWorldPoint(mousePos);
            worldPosition = new Vector3(worldPosition.x, worldPosition.y);

            ShootGatling(worldPosition);
        }
    }


    private void ShootGatling(Vector3 endpoint)
    {
        Vector3 dir = endpoint - this.transform.position;
        dir = dir.normalized;

        if (!cloneBullet)
        {
            return;
        }
        GameController.controller.PlaySound(soundEffect.gunShoot);
        cloneBullet.GetComponent<PlayerBullet>().KindBullet(kindCanon);
        cloneBullet.AddComponent<Rigidbody2D>().AddForce(dir * powerGatling);
        cloneBullet.GetComponent<BulletRotation>().StartRotate();
        cloneBullet = null;
        StartCoroutine(Reload());
    }

    public void StartGatling()
    {
        movingTarget.StopMoving();
        DisablePath();
        StartCoroutine(Reload());
        StartCoroutine(GatlingTime());
    }

    private IEnumerator GatlingTime()
    {
        kindCanon = kindCanon.gatling;
        timeGatlingImage.gameObject.SetActive(true);
        timeGatlingImage.transform.localScale = Vector3.one;
        timeGatlingImage.DOScaleX(0, timeGatling);
        yield return new WaitForSeconds(timeGatling);
        timeGatlingImage.gameObject.SetActive(false);
        movingTarget.StopMoving();
        DisablePath();
        pharse = shoot.Reloaded;
        kindCanon = kindCanon.normal;
    }

    private IEnumerator Reload()
    {
        isReload = true;
        yield return new WaitForSeconds(reloadTime);
        if (!cloneBullet) {
            GameObject go = Instantiate(bullet, StartPoint.position, new Quaternion());
            go.GetComponent<PlayerBullet>().Init(this);
            cloneBullet = go;
        }
 
        isReload = false;
    }

}
