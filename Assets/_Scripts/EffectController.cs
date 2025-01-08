using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectController : SingletonBase<EffectController>
{
    [SerializeField] private ParticleSystem sparkEff;
    [SerializeField] private ParticleSystem splashEff;
    [SerializeField] private ParticleSystem fireEff;
    [SerializeField] private GameObject levelUpUIEff;
    [SerializeField] private GameObject hammerPref;
    [SerializeField] private GameObject handPref;
    ParticleSystem effHoldToPlay;
    public void PlayHitEffect(Vector3 tapPos)
    {
        if (GameCache.GC.currentWeaponType == WeaponType.Knife) return;
        ParticleSystem effToPlay = null;
        switch (GameCache.GC.currentWeaponType)
        {
            case WeaponType.Hand:
                CreateHandTouch(tapPos);
                effToPlay = sparkEff;
                break;
            case WeaponType.Hammer:
                CreateHammer(tapPos + new Vector3(0,0,1));
                effToPlay = sparkEff;
                break;
            case WeaponType.Knife:
                effToPlay = splashEff;
                break;
        }
        switch (GameCache.GC.currentWeaponType)
        {
            case WeaponType.Hand:
                break;
            case WeaponType.Hammer:
                break;
            case WeaponType.Knife:
                effToPlay.transform.eulerAngles = new Vector3(0, Random.Range(0, 360), 0);
                break;
        }
        effToPlay.transform.position = tapPos;
        effToPlay.Play();
    }
    public void PlayHoldEff(Vector3 touchPos)
    {
        effHoldToPlay = fireEff;

        if (!effHoldToPlay.isPlaying)
        {
            switch (GameCache.GC.currentWeaponType)
            {
                case WeaponType.Fire:
                    {
                        effHoldToPlay.gameObject.transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
                        break;
                    }
                case WeaponType.Boom:
                    {
                        effHoldToPlay.gameObject.transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
                        break;
                    }
            }
            effHoldToPlay.Play();
        }
        effHoldToPlay.transform.position = touchPos;
    }
    public void StopPlayHoldEff()
    {
        if (effHoldToPlay == null) return;
        effHoldToPlay.Stop();
        effHoldToPlay = null;
    }
    public void PlayLevelUpUIEFF()
    {
        levelUpUIEff.SetActive(true);
    }
    public void CreateHammer(Vector3 spawnPos)
    {
        GameObject newHammer = ObjectPooling.Instance.CreateObject(hammerPref);
        newHammer.transform.position = spawnPos + new Vector3(0, 1, 0);
    }
    public void CreateHandTouch(Vector3 spawnPos)
    {
        //GameObject newHand = ObjectPooling.Instance.CreateObject(handPref);
        //newHand.transform.position = spawnPos;
    }
}
