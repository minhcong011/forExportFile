using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoseWeaponButton : MonoBehaviour
{
    [SerializeField] private WeaponType weaponType;
    [SerializeField] private int levelToUnlock;
    [SerializeField] private GameObject levelToUnlockText;
    [SerializeField] private GameObject lockIcon;
    [SerializeField] private GameObject sellectBG;
    private bool isUnlock;

    public bool IsUnlock { get { return isUnlock; } }
    public void Sellect()
    {
        ChoseWeaponButtonGp.Instance.SellectButton(this);
    }
    public void CheckUnlock()
    {
        if (isUnlock) return;
        if (levelToUnlock <= GameCache.GC.currentLevel)
        {
            levelToUnlockText.SetActive(false);
            lockIcon.SetActive(false);
            isUnlock = true;
        }
    }
    public void SetSellect()
    {
        if (isUnlock)
        {
            sellectBG.SetActive(true);
            GameCache.GC.currentWeaponType = weaponType;
            switch (weaponType)
            {
                case WeaponType.Hammer: 
                case WeaponType.Hand:
                    {
                        CombatManager.Instance.CreateMeetNomal();
                        break;
                    }
                case WeaponType.Knife:
                    {
                        CombatManager.Instance.CreateMeetCutMesh();
                        break;
                    }
                case WeaponType.Fire:
                case WeaponType.Boom:
                    {
                        CombatManager.Instance.CreateMeetDrawBrush();
                        break;
                    }

            }
        }
    }
    public void DeSellect()
    {
        sellectBG.SetActive(false);
    }
}
