using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Purchasing;

public class SupperPack : MonoBehaviour
{
    [SerializeField] private SupperPackType packType;
    [SerializeField] private string IAPID;
    [SerializeField] private ItemSheet coin;
    [SerializeField] private ItemSheet suffer;
    [SerializeField] private ItemSheet sort;
    [SerializeField] private ItemSheet vip;
    [SerializeField] private TextMeshProUGUI coinText;
    [SerializeField] private TextMeshProUGUI sufferAmountText;
    [SerializeField] private TextMeshProUGUI sortAmountText;
    [SerializeField] private TextMeshProUGUI vipAmountText;
    [SerializeField] private TextMeshProUGUI packRemainText;

    [SerializeField] private TextMeshProUGUI timeRemainText;
    private void Start()
    {
        coinText.text = coin.amount.ToString();
        sufferAmountText.text = suffer.amount.ToString();
        sortAmountText.text = sort.amount.ToString();
        vipAmountText.text = vip.amount.ToString();
    }
    private void OnEnable()
    {
        if (packType == SupperPackType._private || packType == SupperPackType.mexican) SetPackRemainText();
        if (packType == SupperPackType.tropical || packType == SupperPackType.space) SetTimeRemainText();
    }
    private void SetTimeRemainText()
    {
        timeRemainText.text = GetTimeUntilNextReset();
    }
    string GetTimeUntilNextReset()
    {
        DateTime currentTime = DateTime.Now;
        DateTime nextResetTime = currentTime.Date.AddDays(1);
        TimeSpan timeUntilReset = nextResetTime - currentTime;

        return $"{timeUntilReset.Hours}h {timeUntilReset.Minutes}m";
    }
    private void SetPackRemainText()
    {
        int amountPackFinishBuy = 0;
        switch(packType)
        {
            case SupperPackType._private:
                {
                    amountPackFinishBuy = GameCache.GC.piratePackFinishBuy; break;
                }
            case SupperPackType.mexican:
                {
                    amountPackFinishBuy = GameCache.GC.mexicanPackFinishBuy; break;
                }
        }
        packRemainText.text = $"{amountPackFinishBuy}/2";
    }
    public void OnPurchaseCompleted(Product product)
    {
        if (product.definition.id == IAPID)
        {
            ClampGameItem.Clamp(coin);
            ClampGameItem.Clamp(suffer);
            ClampGameItem.Clamp(sort);
            ClampGameItem.Clamp(vip);

            switch(packType)
            {
                case SupperPackType._private:
                    {
                        GameCache.GC.piratePackFinishBuy++;
                        break;
                    }
                case SupperPackType.mexican:
                    {
                        GameCache.GC.mexicanPackFinishBuy++;
                        break;
                    }
                case SupperPackType.tropical:
                    {
                        GameCache.GC.finishBuyTropicalPackDaily = true;
                        break;
                    }
                case SupperPackType.space:
                    {
                        GameCache.GC.finishBuySpacePackDaily = true;
                        break;
                    }
            }
            gameObject.SetActive(false);
        }
    }
}
[Serializable] 
public class ItemSheet
{
    public GameItemType itemType;
    public int amount;
}
[Serializable]
public enum SupperPackType
{
    _private, mexican, tropical, space
}
