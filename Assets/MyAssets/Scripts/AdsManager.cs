using UnityEngine;
using GoogleMobileAds.Api;
using System.Collections;

public enum RewardAdsType
{
    AddCoin, AddBooster, X2Coin, SkipLevel
}
public class AdsManager : MonoBehaviour
{
    private string intertestialId = "ca-app-pub-3940256099942544/1033173712";
    private string bannerId = "ca-app-pub-3940256099942544/6300978111";
    private string rewardId = "ca-app-pub-3940256099942544/5224354917";
    private string openAppId = "";

    private InterstitialAd interstitialAd;
    private BannerView bannerView;
    private RewardedAd rewardedAd;
    private bool giveReward;

    public static AdsManager Instance;

    public bool enableAds;

    public BoosterType boosterToAdd;

    RewardAdsType rewardAdsType;

    AppOpenAd appOpenAd;
    public bool finishLoadOpenAppAds;
    public bool loadOpenAppAdsError;

    private bool canShowInter = true;
    private bool canShowReward;

    private void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);
        else
            Instance = this;

        DontDestroyOnLoad(gameObject);
    }
    private void OnApplicationPause(bool pause)
    {
        ShowAppOpenAd();
    }
    public void Start()
    {
        if (!enableAds)
            return;

        MobileAds.Initialize(initStatus => { });

        LoadBannerAd();

        RequestInterstitial();

        LoadRewardedAd();

        LoadAppOpenAd();
    }
    private IEnumerator CountDownInter()
    {
        float countDown = 20;
        while(countDown > 0)
        {
            countDown -= Time.deltaTime;
            yield return null;
        }
        canShowInter = true;
    }
    #region OpenApp
    public void LoadAppOpenAd()
    {
        if (GameCache.GC.blockAds) return;
        // Clean up the old ad before loading a new one.
        if (appOpenAd != null)
        {
            appOpenAd.Destroy();
            appOpenAd = null;
        }


        // Create our request used to load the ad.
        var adRequest = new AdRequest();

        AppOpenAd.Load(openAppId, adRequest, (AppOpenAd ad, LoadAdError error) =>
        {
            if (error != null)
            {
                loadOpenAppAdsError = true;
                Debug.LogError("App open ad failed to load an ad with error : "
                                + error);
                return;
            }
            if (ad == null)
            {
                loadOpenAppAdsError = true;
                Debug.LogError("Unexpected error: App open ad load event fired with " +
                               " null ad and null error.");
                return;
            }
            RegisterEventHandlers(ad);
            appOpenAd = ad;
            finishLoadOpenAppAds = true;
        });
    }

    private void RegisterEventHandlers(AppOpenAd ad)
    {
        // Raised when the ad is estimated to have earned money.
        ad.OnAdPaid += (AdValue adValue) =>
        {
        };
        // Raised when an impression is recorded for an ad.
        ad.OnAdImpressionRecorded += () =>
        {
        };
        // Raised when a click is recorded for an ad.
        ad.OnAdClicked += () =>
        {
        };
        // Raised when an ad opened full screen content.
        ad.OnAdFullScreenContentOpened += () =>
        {
        };
        // Raised when the ad closed full screen content.
        ad.OnAdFullScreenContentClosed += () =>
        {
            LoadAppOpenAd();
        };
        // Raised when the ad failed to open full screen content.
        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            LoadAppOpenAd();
            Debug.LogError("App open ad failed to open full screen content " +
                           "with error : " + error);
        };
    }
    IEnumerator ShowAppOpenAdWithDelay()
    {
        yield return new WaitForSeconds(0.2f);
        ShowAppOpenAd();
    }

    public IEnumerator ShowAppOpenAd()
    {
        if (appOpenAd != null && appOpenAd.CanShowAd())
        {
            appOpenAd.Show();
            yield return new WaitForSeconds(0.5f);
        }
        yield return null;
    }
    #endregion

    public void LoadBannerAd()
    {
        if (bannerView == null)
        {
            CreateBannerView();
        }
        var adRequest = new AdRequest();
        adRequest.Keywords.Add("unity-admob-sample");

        Debug.Log("Loading banner ad.");
        bannerView.LoadAd(adRequest);
    }

    public void CreateBannerView()
    {
        Debug.Log("Creating banner view");

        if (bannerView != null)
        {
            DestroyAd();
        }

        bannerView = new BannerView(bannerId, AdSize.Banner, AdPosition.Bottom);
    }

    public void DestroyAd()
    {
        if (bannerView != null)
        {
            Debug.Log("Destroying banner ad.");
            bannerView.Destroy();
            bannerView = null;
        }
    }
    private void RequestInterstitial()
    {
        if (interstitialAd != null)
        {
            interstitialAd.Destroy();
            interstitialAd = null;
        }

        Debug.Log("Loading the interstitial ad.");

        var adRequest = new AdRequest();
        adRequest.Keywords.Add("unity-admob-sample");

        InterstitialAd.Load(intertestialId, adRequest,
            (InterstitialAd ad, LoadAdError error) =>
            {
                if (error != null || ad == null)
                {
                    Debug.LogError("interstitial ad failed to load an ad " +
                                   "with error : " + error);
                    return;
                }

                Debug.Log("Interstitial ad loaded with response : "
                          + ad.GetResponseInfo());

                interstitialAd = ad;
            });
    }

    public void ShowInterstitialAd()
    {
        if (!enableAds)
            return;
        if (!canShowInter) return;
        if (interstitialAd != null && interstitialAd.CanShowAd())
        {
            Debug.Log("Showing interstitial ad.");
            interstitialAd.Show();
            canShowInter = false;
            StartCoroutine(CountDownInter());
        }
        else
        {
            Debug.LogError("Interstitial ad is not ready yet.");
            RequestInterstitial();
        }
    }

    public void ShowRewardedAd(RewardAdsType rewardAdsType)
    {
        this.rewardAdsType = rewardAdsType;
        if (!enableAds)
            return;

        const string rewardMsg =
            "Rewarded ad rewarded the user. Type: {0}, amount: {1}.";
        if (rewardedAd != null && rewardedAd.CanShowAd())
        {
            rewardedAd.Show((Reward reward) =>
            {
                GiveReward();
            });
        }

        LoadRewardedAd();
    }

    public void LoadRewardedAd()
    {
        if (rewardedAd != null)
        {
            rewardedAd.Destroy();
            rewardedAd = null;
        }

        Debug.Log("Loading the rewarded ad.");

        var adRequest = new AdRequest();
        adRequest.Keywords.Add("unity-admob-sample");

        RewardedAd.Load(rewardId, adRequest,
            (RewardedAd ad, LoadAdError error) =>
            {
                if (error != null || ad == null)
                {
                    Debug.LogError("Rewarded ad failed to load an ad " +
                                   "with error : " + error);
                    return;
                }

                Debug.Log("Rewarded ad loaded with response : "
                          + ad.GetResponseInfo());

                rewardedAd = ad;
            });
    }


    private void GiveReward()
    {
        AudioManager.Instance.Play("Reward");
        switch (rewardAdsType)
        {
            case RewardAdsType.AddCoin:
                {
                    FindFirstObjectByType<CoinsManager>().AddCoins(150);
                    break;
                }
            case RewardAdsType.AddBooster:
                {
                    if (boosterToAdd != BoosterType.Revice)
                    {
                        BoosterManager.Instance.AddItemBooster(boosterToAdd, 2);
                        BoosterManager.Instance.UpdateBoosterBtAmountText();
                    }
                    else BoosterManager.Instance.UseBooster(boosterToAdd);
                    break;
                }
            case RewardAdsType.X2Coin:
                {
                    CoinsManager.Instance.AddCoins(50);
                    GameManager.Instance.Next();
                    break;
                }
            case RewardAdsType.SkipLevel:
                {
                    GameManager.Instance.SkipLevel();
                    break;
                }
        }
    }
    public void ShowSkipLevelAds()
    {
        ShowRewardedAd(RewardAdsType.SkipLevel);
    }
    public void WatchX2Ads()
    {
        ShowRewardedAd(RewardAdsType.X2Coin);
    }
}