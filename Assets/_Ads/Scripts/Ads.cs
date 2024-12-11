using UnityEngine;

public abstract class Ads : Singleton<Ads>
{
    public abstract bool IsRewardedAdLoaded();

    public abstract void ShowBanner();
    public abstract void ShowInterstitial();
    public abstract void ShowRewarded();

    public virtual void LoadAds() { }
}
