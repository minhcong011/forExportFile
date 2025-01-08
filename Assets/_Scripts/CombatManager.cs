using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : SingletonBase<CombatManager>
{
    [SerializeField] private float speedReduceHp;
    [SerializeField] private int amountInreaseHPandXPPerLevel;
    [SerializeField] private int handDamage;
    [SerializeField] private int hammerDamage;
    [SerializeField] private int knifeDamage;
    [SerializeField] private int fireDamage;
    [SerializeField] private int boomDamage;

    [SerializeField] private GameObject currentMeet;

    [SerializeField] private GameObject meetCutMeshPref;
    [SerializeField] private GameObject meetNomalPref;
    [SerializeField] private GameObject meetDrawBrush;
    private void Start()
    {
        StartCoroutine(ReduceHp());
        Debug.Log(GameCache.GC.maxHp / 50 * 100);
    }
    public void DamageMeet()
    {
        int damage = GetDamage();
        GameCache.GC.currentXP += damage;
        GameCache.GC.currentHp -= damage;
        UIManager.Instance.UpdateHPSlider();
        UIManager.Instance.UpdateXpSlider();
        if (GameCache.GC.currentWeaponType != WeaponType.Knife) MeetAnimationController.Instance.PlayHit();
        CheckLevelUP();
        Debug.Log("Damage: " + damage);
    }
    public void CheckLevelUP()
    {
        if(GameCache.GC.currentXP >= GameCache.GC.maxXP)
        {
            GameCache.GC.currentLevel++;
            GameCache.GC.currentXP = 0;
            GameCache.GC.maxHp += amountInreaseHPandXPPerLevel;
            GameCache.GC.maxXP += amountInreaseHPandXPPerLevel;
            UIManager.Instance.UpdateLevelText();
            UIManager.Instance.UpdateXpSlider();
            ChoseWeaponButtonGp.Instance.CheckUnlock();
            AudioManager.Instance.PlayLevelUpSound();
            EffectController.Instance.PlayLevelUpUIEFF();
            CheckMeetDeath();
        }
    }
    private void CheckMeetDeath()
    {
        if(GameCache.GC.currentHp <= 0)
        {
            TapController.Instance.IsBlockTap = true;
            if (GameCache.GC.currentWeaponType != WeaponType.Knife) MeetAnimationController.Instance.PlayDeath();
            StartCoroutine(CheckFinishReduceHpWhenDeath());
        }
    }
    IEnumerator ReduceHp()
    {
        while(true)
        {
            if (GameCache.GC.currentHp < GameCache.GC.maxHp)
            {
                GameCache.GC.currentHp += Time.deltaTime * (speedReduceHp * GetDamage());
                UIManager.Instance.UpdateHPSlider();
            }
            yield return null;
        }
    }
    IEnumerator CheckFinishReduceHpWhenDeath()
    {
        while(GameCache.GC.currentHp <= (GameCache.GC.maxHp * 50 / 100))
        {
            yield return null;
        }
        MeetAnimationController.Instance.PlayIdle();
        TapController.Instance.IsBlockTap = false;
    }
    private int GetDamage()
    {
        switch (GameCache.GC.currentWeaponType)
        {
            case WeaponType.Hand:
                return handDamage;
            case WeaponType.Hammer:
                return hammerDamage;
            case WeaponType.Knife:
                return knifeDamage;
            case WeaponType.Fire:
                return fireDamage;
            case WeaponType.Boom:
                return boomDamage;
        }
        return 0;
    }
    public void CreateMeetCutMesh()
    {
        Destroy(currentMeet);
        ObjectPooling.Instance.CleanHolder();
        currentMeet = Instantiate(meetCutMeshPref);
    }
    public void CreateMeetNomal()
    {
        Destroy(currentMeet);
        ObjectPooling.Instance.CleanHolder();
        currentMeet = Instantiate(meetNomalPref);
    }
    public void CreateMeetDrawBrush()
    {
        Destroy(currentMeet);
        ObjectPooling.Instance.CleanHolder();
        currentMeet = Instantiate(meetDrawBrush);
    }
}
