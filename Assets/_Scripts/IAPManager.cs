using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
public class IAPManager : MonoBehaviour
{
    public void OnPurchaseCompleted(Product product)
    {
        switch (product.definition.id)
        {
            case "removeads":
                {
                    GameCache.GC.blockAds = true;
                    AdsManager.Instance.ActiveBannerView(false);
                    break;
                }
        }
    }
}
