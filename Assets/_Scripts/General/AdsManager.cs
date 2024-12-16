using System.Collections;
using UnityEngine;
using GoogleMobileAds.Api;
//using HmsPlugin;

public class AdsManager : SingletonBaseDontDestroyOnLoad<AdsManager>
{
    [SerializeField] private AdPosition bannerPosition;
    [SerializeField] private bool test;
    AppOpenAd appOpenAd;
    public bool finishLoadOpenAppAds;
    public bool loadOpenAppAdsError;
    private bool finishLoadInterAds;
    private bool interAdsOpen;
    private bool rewardAdsFinish;
    private bool finishInit;

    private CarHolder carHolderToUnlock;
    public enum RewardAdsContent
    {
        UnlockCarHolder, UseBooster, Coin
    }
    private int boosterId;
    private RewardAdsContent rewardAdsContent;
#if UNITY_STANDALONE_WIN
    string bannerId = "ca-app-pub-2787841785988179/9426421152";
    string interId = "ca-app-pub-2787841785988179/2184437955";
    string rewardedId = "ca-app-pub-2787841785988179/3716527969";
    string openAppId = "ca-app-pub-2787841785988179/6127704629";
#endif
#if UNITY_ANDROID
    string bannerId = "ca-app-pub-2787841785988179/3036746667";
    string interId = "ca-app-pub-3940256099942544/1033173712";
    string rewardedId = "ca-app-pub-3940256099942544/5224354917";
    string openAppId = "ca-app-pub-2787841785988179/6528774273";

#elif UNITY_IPHONE
    string bannerId = "ca-app-pub-3940256099942544/2934735716";
    string interId = "ca-app-pub-3940256099942544/4411468910";
    string rewardedId = "ca-app-pub-3940256099942544/1712485313";

#endif

    BannerView bannerView;
    InterstitialAd interstitialAd;
    RewardedAd rewardedAd;


    private void Start()
    {
        if (test)
        {
            bannerId = "ca-app-pub-3940256099942544/6300978111";
            interId = "ca-app-pub-3940256099942544/1033173712";
            rewardedId = "ca-app-pub-3940256099942544/5224354917";
            openAppId = "ca-app-pub-3940256099942544/9257395921";
        }
        InitAds();
    }
    public void InitAds()
    {
        MobileAds.RaiseAdEventsOnUnityMainThread = true;
        MobileAds.Initialize(initStatus =>
        {
            finishInit = true;
            //print("Ads Initialised !!");
            LoadBannerAd();
            LoadRewardedAd();
            LoadInterstitialAd();
        });
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
        };
        // Raised when the ad failed to open full screen content.
        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            Debug.LogError("App open ad failed to open full screen content " +
                           "with error : " + error);
        };
    }
    private void SetHuaweiAppOpenEvent()
    {
        //HMSAdsKitManager.Instance.OnSplashAdLoaded += () =>
        //{
        //    finishLoadOpenAppAds = true;
        //};
        //HMSAdsKitManager.Instance.OnSplashAdFailedToLoad += (int error) =>
        //{
        //    loadOpenAppAdsError = true;
        //};
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

    #region Banner
    public void LoadBannerAd()
    {
        if (bannerView != null) return;
        //create a banner
        CreateBannerView();

        //listen to banner events
        ListenToBannerEvents();

        //load the banner
        if (bannerView == null)
        {
            CreateBannerView();
        }

        var adRequest = new AdRequest();
        adRequest.Keywords.Add("unity-admob-sample");

        bannerView.LoadAd(adRequest);

        bannerView.Show();
    }
    public void ActiveBannerView(bool active)
    {
        if (active && bannerView != null) bannerView.Show();
        if (!active && bannerView != null) bannerView.Hide();
    }
    void CreateBannerView()
    {
        if (bannerView != null)
        {
            DestroyBannerAd();
        }
        bannerView = new BannerView(bannerId, AdSize.Banner, bannerPosition);
    }
    void ListenToBannerEvents()
    {
        bannerView.OnBannerAdLoaded += () =>
        {

        };
        // Raised when an ad fails to load into the banner view.
        bannerView.OnBannerAdLoadFailed += (LoadAdError error) =>
        {

        };
        // Raised when the ad is estimated to have earned money.
        bannerView.OnAdPaid += (AdValue adValue) =>
        {

        };
        // Raised when an impression is recorded for an ad.
        bannerView.OnAdImpressionRecorded += () =>
        {

        };
        // Raised when a click is recorded for an ad.
        bannerView.OnAdClicked += () =>
        {

        };
        // Raised when an ad opened full screen content.
        bannerView.OnAdFullScreenContentOpened += () =>
        {
        };
        // Raised when the ad closed full screen content.
        bannerView.OnAdFullScreenContentClosed += () =>
        {
        };
    }
    public void DestroyBannerAd()
    {
        if (bannerView != null)
        {
            bannerView.Destroy();
            bannerView = null;
        }
    }
    #endregion

    #region Interstitial
    public void LoadInterstitialAd()
    {

        finishLoadInterAds = false;
        if (interstitialAd != null)
        {
            interstitialAd.Destroy();
            interstitialAd = null;
        }
        var adRequest = new AdRequest();
        adRequest.Keywords.Add("unity-admob-sample");

        InterstitialAd.Load(interId, adRequest, (InterstitialAd ad, LoadAdError error) =>
        {
            if (error != null || ad == null)
            {
                finishLoadInterAds = true;
                return;
            }

            //print("Interstitial ad loaded !!" + ad.GetResponseInfo());
            finishLoadInterAds = true;
            interstitialAd = ad;
            InterstitialEvent(interstitialAd);
        });

    }
    public void ShowInterstitialAd(float delayTime)
    {
        if (GameCache.GC.blockAds) return;
        StartCoroutine(ShowInterAdsCoroutinue());
        IEnumerator ShowInterAdsCoroutinue()
        {
            yield return new WaitForSeconds(delayTime);
            ShowInterstitialAd();
        }
    }
    public bool ShowInterstitialAd()
    {
        if (GameCache.GC.blockAds) return false;
        if (interstitialAd != null && interstitialAd.CanShowAd())
        {
            interstitialAd.Show();
            return true;
        }
        else
        {
            //print("Intersititial ad not ready!!");
            return false;
        }
    }
    public IEnumerator ShowInterAync()
    {
        if (GameCache.GC.blockAds) yield break;
        interAdsOpen = true;
        if (ShowInterstitialAd())
        {
            while (interAdsOpen)
            {
                yield return null;
            }
        }
    }
    public void InterstitialEvent(InterstitialAd ad)
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
            Time.timeScale = 1;
            interAdsOpen = false;
        };
        // Raised when an ad opened full screen content.
        ad.OnAdFullScreenContentOpened += () =>
        {
        };
        // Raised when the ad closed full screen content.
        ad.OnAdFullScreenContentClosed += () =>
        {
            LoadInterstitialAd();
            interAdsOpen = false;
        };
        // Raised when the ad failed to open full screen content.
        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            LoadInterstitialAd();
            interAdsOpen = false;
        };
    }

    #endregion

    #region Rewarded

    public void LoadRewardedAd()
    {
        rewardAdsFinish = false;
        if (rewardedAd != null)
        {
            rewardedAd.Destroy();
            rewardedAd = null;
        }

        var adRequest = new AdRequest();
        adRequest.Keywords.Add("unity-admob-sample");

        RewardedAd.Load(rewardedId, adRequest, (RewardedAd ad, LoadAdError error) =>
        {
            if (error != null || ad == null)
            {
                return;
            }

            rewardedAd = ad;
            RewardedAdEvents(rewardedAd);
        });
    }
    public void ShowRewardAdsUseBooster(int boosterId)
    {
        this.boosterId = boosterId;
        ShowRewardedAd(RewardAdsContent.UseBooster);
    }
    public void ShowRewardAdsUnlockCarHolder(CarHolder carHolderToUnlock)
    {
        this.carHolderToUnlock = carHolderToUnlock;
        ShowRewardedAd(RewardAdsContent.UnlockCarHolder);
    }
    public void ShowRewardedAd(RewardAdsContent rewardAdsContent)
    {
        this.rewardAdsContent = rewardAdsContent;
        ShowRewardedAdIfReady();
    }

    private void ShowRewardedAdIfReady()
    {
        if (rewardedAd != null && rewardedAd.CanShowAd())
        {
            rewardedAd.Show((Reward reward) =>
            {
                rewardAdsFinish = true;
                CompletedRewardAds();
            });
        }
    }
    public void RewardedAdEvents(RewardedAd ad)
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
            LoadRewardedAd();
        };
        // Raised when the ad failed to open full screen content.
        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            LoadRewardedAd();
        };
    }
    public void CompletedRewardAds()
    {
        switch (rewardAdsContent)
        {
            case RewardAdsContent.UnlockCarHolder:
                {
                    carHolderToUnlock.Unlock();
                    break;
                }
            case RewardAdsContent.UseBooster:
                {
                    BoosterManager.Instance.UseBooster(boosterId);
                    break;
                }
            case RewardAdsContent.Coin:
                {
                    GameCache.GC.coin += 180;
                    UICoinText.Instance.UpdateCoinText();
                    break;
                }
        }
    }
    #endregion

}
