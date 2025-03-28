using UnityEngine;
using UnityEngine.UI;

public class BuyBoosterPanel : MonoBehaviour
{
    [SerializeField] private int amountCoinToBuy = 300;
    [SerializeField] private int amountToAdd = 2;
    [SerializeField] private BoosterType boosterType;
    [SerializeField] private BoosterButton boosterButton;
    [SerializeField] private Text countBuyText;

    private int countBuy;
    public void WatchAds()
    {
        AdsManager.Instance.boosterToAdd = boosterType;
        AdsManager.Instance.ShowRewardedAd(RewardAdsType.AddBooster);
        if (boosterType != BoosterType.Revice) IncreaseCountBuy();
    }
    private void IncreaseCountBuy()
    {
        countBuy++;
        countBuyText.text = $"+ {countBuy + 1}";

        if (boosterType == BoosterType.AddTime)
        {
            gameObject.SetActive(false);
        }
        else if (countBuy == 2)
        {
            gameObject.SetActive(false);
        }
    }
    public void BuyWithCoin()
    {
        if(amountCoinToBuy <= CoinsManager.Instance.GetCoin())
        {
            if (boosterType != BoosterType.Revice)
            {
                CoinsManager.Instance.LessCoins(amountCoinToBuy);
                BoosterManager.Instance.AddItemBooster(boosterType, amountToAdd);
                boosterButton.UpdateBoosterAmountText();

                IncreaseCountBuy();
            }
            else
            {
                BoosterManager.Instance.UseBooster(boosterType);
            }
        }
    }
}
