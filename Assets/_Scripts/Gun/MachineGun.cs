using AndroidNativeCore;
using CandyCoded.HapticFeedback;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGun : MonoBehaviour, IGunShoot
{
    [SerializeField] private GameObject smokeEFPref;
    [SerializeField] private GameObject smokeHolder;

    [SerializeField] private Animator animator;

    [SerializeField] private ParticleSystem bulletEf;

    [SerializeField] private AudioClip shootSound;

    private bool isShoot;

    private float delay;
    [SerializeField] private GameObject bulletController;
    private List<GameObject> bullets = new();

    Flash flash;
    private void Awake()
    {
        //flash = new Flash();
        bullets.Clear();

        if (bulletController)
        {
            for (int i = 0; i < bulletController.transform.GetChild(0).childCount; i++)
            {
                GameObject bullet = bulletController.transform.GetChild(0).transform.GetChild(i).gameObject;
                bullets.Add(bullet);
            }
        }
        Reload();
    }
    public void DecreaseBullet(int index)
    {
        if (bullets.Count == 0) return;
        bullets[index].SetActive(false);
    }
    public void Reload()
    {
        foreach(GameObject bullet in bullets)
        {
            bullet.SetActive(true);
        }
    }
    private void Update()
    {
        if (isShoot) delay = 0.16f;
    }
    public void ActiveShootStatus(bool active)
    {
        if (active)
        {
            Debug.Log("a");
            if (!isShoot)
            {
                isShoot = true;
                delay = 0.16f;
                animator.SetBool("Shoot", true);
                bulletEf.Play();
                StartCoroutine(CheckShootStop());
            }
        }
        else isShoot = false;
    }
    IEnumerator CheckShootStop()
    {
        while (delay > 0 && MachineGunInGame.Instance.CanShoot)
        {
            delay -= Time.deltaTime;
            yield return null;
        }
        animator.SetBool("Shoot", false);
        bulletEf.Stop();
    }
    public void CreateShootEf()
    {
        GameObject smoke = Instantiate(smokeEFPref);
        smoke.transform.SetParent(smokeHolder.transform, false);

        MachineGunInGame.Instance.CurrentBullet--;
        AudioManager.Instance.PlaySound(shootSound);
        if(!GameCache.GC.soundOff) HapticFeedback.LightFeedback();

        //StartCoroutine(Flash());
        //Instantiate(bulletPref).transform.position = bulletHolder.transform.position;
    }
    IEnumerator Flash()
    {
        if (flash.isFlashAvailable()) flash.setFlashEnable(true);
        yield return new WaitForSeconds(0.2f);
        flash.setFlashEnable(false);
    }
}
