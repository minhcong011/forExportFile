using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
public class IAPPurchase : MonoBehaviour
{
    private void OnEnable()
    {
        GameController.Instance.currentGameStage = GameStage.Pause;
    }
    private void OnDisable()
    {
        GameController.Instance.currentGameStage = GameStage.Playing;
    }
    public void OnPurchaseCompleted(Product product)
    {
        switch (product.definition.id)
        {
            case "coin1":
                {
                    GameCache.GC.coin += 600;
                    break;
                }
            case "coin2":
                {
                    GameCache.GC.coin += 2600;
                    break;
                }
            case "coin3":
                {
                    GameCache.GC.coin += 7200;
                    break;
                }
            case "coin4":
                {
                    GameCache.GC.coin += 16000;
                    break;
                }
            case "coin5":
                {
                    GameCache.GC.coin += 32000;
                    break;
                }
            case "coin6":
                {
                    GameCache.GC.coin += 64000;
                    break;
                }
            case "starterpack":
                {
                    GameCache.GC.coin += 2600;
                    GameCache.GC.shuffleBoosterAmount += 1;
                    GameCache.GC.sortBoosterAmount += 1;
                    GameCache.GC.vipSlotBoosterAmount += 1;
                    break;
                }
            case "enjoypack":
                {
                    GameCache.GC.coin += 10000;
                    GameCache.GC.shuffleBoosterAmount += 2;
                    GameCache.GC.sortBoosterAmount += 2;
                    GameCache.GC.vipSlotBoosterAmount += 2;
                    break;
                }
        }
        UICoinText.Instance.UpdateCoinText();
    }
    public void GetCoinWithAds()
    {
        AdsManager.Instance.ShowRewardedAd(AdsManager.RewardAdsContent.Coin);
    }
}
