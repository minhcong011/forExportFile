using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoseWeaponButtonGp : SingletonBase<ChoseWeaponButtonGp>
{
    [SerializeField] private ChoseWeaponButton[] choseWeaponButtons;
    [SerializeField] private ChoseWeaponButton currentSellectBt;

    private void Start()
    {
        CheckUnlock();
        SetSellectWhenStart();
    }
    public void CheckUnlock()
    {
        foreach(ChoseWeaponButton choseWeaponButton in choseWeaponButtons)
        {
            if (!choseWeaponButton.IsUnlock) choseWeaponButton.CheckUnlock();
        }
    }
    public void SellectButton(ChoseWeaponButton buttonToSellect)
    {
        if(currentSellectBt != null) currentSellectBt.DeSellect();
        currentSellectBt = buttonToSellect;
        currentSellectBt.SetSellect();
    }
    private void SetSellectWhenStart()
    {
        switch (GameCache.GC.currentWeaponType)
        {
            case WeaponType.Hand:
                SellectButton(choseWeaponButtons[0]);
                break;
            case WeaponType.Hammer:
                SellectButton(choseWeaponButtons[1]);
                break;
            case WeaponType.Knife:
                SellectButton(choseWeaponButtons[2]);
                break;
            case WeaponType.Fire:
                SellectButton(choseWeaponButtons[3]);
                break;
            case WeaponType.Boom:
                SellectButton(choseWeaponButtons[4]);
                break;
        }
    }
}
