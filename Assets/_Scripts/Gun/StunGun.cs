using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AndroidNativeCore;
using CandyCoded.HapticFeedback;

public class StunGun : MonoBehaviour, IGunShoot
{
    [SerializeField] private GameObject powerOffIcon;
    [SerializeField] private GameObject eletricEf;
    [SerializeField] private GameObject flashEf;
    [SerializeField] private AudioClip soundClip;
    [SerializeField] private Color flashColor;

    private bool canVibrate;
    Flash flash;
    private void Start()
    {
        //flash = new Flash();
        StartCoroutine(VibrateLoop());
        //StartCoroutine(FlashLoop());

        flashEf.GetComponent<SpriteRenderer>().color = flashColor;
        AudioManager.Instance.SetEletricSound(soundClip);
    }
    public void ActiveShootStatus(bool active)
    {
        powerOffIcon.SetActive(!active);
        eletricEf.SetActive(active);
        flashEf.SetActive(active);
        StunGunInGame.Instance.IsShoot = active;
        canVibrate = active;
        AudioManager.Instance.PlayEletricSound(active);
    }
    IEnumerator VibrateLoop()
    {
        while (true)
        {
            if (canVibrate)
            {
                if (!GameCache.GC.soundOff) HapticFeedback.LightFeedback();
                yield return new WaitForSeconds(0.1f);
            }
            yield return null;
        }
    }
    IEnumerator FlashLoop()
    {
        while (true)
        {
            if (canVibrate)
            {
                if (flash.isFlashAvailable())
                    flash.setFlashEnable(true);
                yield return new WaitForSeconds(0.5f);
                flash.setFlashEnable(false);
            }
            yield return null;
        }
    }
}
