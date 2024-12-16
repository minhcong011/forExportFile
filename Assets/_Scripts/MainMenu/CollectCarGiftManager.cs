using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//1 booster
//4 car x2
//5 coin
public class CollectCarGiftManager : SingletonBase<CollectCarGiftManager>
{
    public GiftSheet[] giftSheets;
    [SerializeField] private Sprite[] coinIcon;
    [SerializeField] private Sprite[] sortIcon;
    [SerializeField] private Sprite[] vipIcon;
    [SerializeField] private Sprite[] shuffleIcon;
    [SerializeField] private Sprite[] carX2Icon;
    [SerializeField] private CollectCarGift[] gifts;
    protected override void Awake()
    {
        base.Awake();
        SetGiftSheetIcon();
    }
    private void Start()
    {
        SetGiftTree();
    }
    private void SetGiftTree()
    {
        for(int i = 0; i < giftSheets.Length; i++) 
        {
            Sprite icon;
            if (giftSheets[i].type == GiftSheet.GiftType.carX2)
            {
                icon = giftSheets[i].icon[GameCache.GC.carCollectID];
            }
            else
            {
                icon = giftSheets[i].icon[0];
            }
            gifts[i].Set(i + 1, giftSheets[i].title, icon);
        }
    }
    private void SetGiftSheetIcon()
    {
        foreach(GiftSheet gift in giftSheets)
        {
            switch (gift.type)
            {
                case GiftSheet.GiftType.coin:
                    {
                        gift.icon = coinIcon;
                        break;
                    }
                case GiftSheet.GiftType.sortBooster:
                    {
                        gift.icon = sortIcon;
                        break;
                    }
                case GiftSheet.GiftType.vipBooster:
                    {
                        gift.icon = vipIcon;
                        break;
                    }
                case GiftSheet.GiftType.shuffleBooster:
                    {
                        gift.icon = shuffleIcon;
                        break;
                    }
                case GiftSheet.GiftType.carX2:
                    {
                        gift.icon = carX2Icon;
                        break;
                    }
            }
        }
    }
    public GiftSheet GetGiftSheet(int giftID)
    {
        return giftSheets[giftID];
    }
}
[Serializable]
public class GiftSheet
{
    public enum GiftType
    {
        coin, sortBooster, vipBooster, shuffleBooster, carX2
    }
    public GiftType type;
    public Sprite[] icon;
    public string title;
    public int amount;
}
