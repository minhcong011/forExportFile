using UnityEngine;

public class BuyBoosterPanel : MonoBehaviour
{
    [SerializeField] private int amountCoinToBuy = 300;
    [SerializeField] private int amountToAdd = 2;
    [SerializeField] private BoosterType boosterType;
    [SerializeField] private BoosterButton boosterButton;
    public void WatchAds()
    {
        AdsManager.Instance.boosterToAdd = boosterType;
        AdsManager.Instance.ShowRewardedAd(RewardAdsType.AddBooster);
        if (boosterType != BoosterType.Revice)
            gameObject.SetActive(false);
    }

    public void BuyWithCoin()
    {
        if(amountCoinToBuy <= CoinsManager.Instance.GetCoin())
        {
            if (boosterType != BoosterType.Revice)
            {
                CoinsManager.Instance.LessCoins(amountCoinToBuy);
                BoosterManager.Instance.AddItemBooster(boosterType, amountToAdd);
                gameObject.SetActive(false);
                boosterButton.UpdateBoosterAmountText();
            }
            else
            {
                BoosterManager.Instance.UseBooster(boosterType);
            }
        }
    }
}
