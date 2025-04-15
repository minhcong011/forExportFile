using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "gunData", menuName = "SO", order = 1)]
public class GunSO : ScriptableObject
{
    public GunSheet[] machineGuns;
    public GunSheet[] stunGuns;
    public GunSheet[] specialGuns;
}
[Serializable]
public class GunSheet
{
    public string gunName;
    public GameObject gunPref;
    public Sprite gunIcon;
    public bool unlockWithAds;
    public bool IsUnlock
    {
        get
        {
            if (!unlockWithAds) return true;

            return GameCache.GC.GetGunUnlock(gunPref.name);
        }
        set
        {
            GameCache.GC.SetGunUnlock(gunPref.name, value);
        }
    }

}
