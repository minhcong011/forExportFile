// dnSpy decompiler from Assembly-CSharp.dll class: UKAdsManager
using System;
using System.Collections;
using ChartboostSDK;
using GoogleMobileAds.Api;
using UnityEngine;
using UnityEngine.Advertisements;

public class UKAdsManager : MonoBehaviour
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

    public enum RewardAdsContent
    {
        Coin
    }
    private int rewardAdsContent;
    [SerializeField] string bannerId = "ca-app-pub-2787841785988179/9426421152";
    [SerializeField] string interId = "ca-app-pub-2787841785988179/2184437955";
    [SerializeField] string rewardedId = "ca-app-pub-2787841785988179/3716527969";
    [SerializeField] string openAppId = "ca-app-pub-3940256099942544/9257395921";
    BannerView bannerView;
    InterstitialAd interstitialAd;
    RewardedAd rewardedAd;

    private bool showOpenGame;
    private void Awake()
    {
        if (!UKAdsManager.isCreated)
        {
            Singleton<GameController>.Instance.adsManager = this;
            UKAdsManager.isCreated = true;
            UnityEngine.Object.DontDestroyOnLoad(this);
        }
        else
        {
            UnityEngine.Object.DestroyImmediate(base.gameObject);
        }
    }
    private void OnApplicationPause(bool pause)
    {
        ShowAppOpenAd();
    }

    private void Start()
    {
        InitAds();
    }
    public void InitAds()
    {
        MobileAds.RaiseAdEventsOnUnityMainThread = true;
        MobileAds.Initialize(initStatus =>
        {
            finishInit = true;
            //print("Ads Initialised !!");
            LoadRewardedAd();
            LoadInterstitialAd();
            LoadAppOpenAd();
        });
    }
    #region OpenApp
    public void LoadAppOpenAd()
    {
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
#if UNITY_EDITOR
            return;
#endif
            if (!showOpenGame)
            {
                ShowAppOpenAd();  
                showOpenGame = true;
            }
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

    public void ShowAppOpenAd()
    {
        if (appOpenAd != null && appOpenAd.CanShowAd())
        {
            appOpenAd.Show();
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
        StartCoroutine(ShowInterAdsCoroutinue());
        IEnumerator ShowInterAdsCoroutinue()
        {
            yield return new WaitForSeconds(delayTime);
            ShowInterstitialAd();
        }
    }
    public bool ShowInterstitialAd()
    {
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
    public void ShowRewardedAd(int rewardAdsContent)
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
        UnityEngine.Debug.Log("Completed video with reward: " + rewardAdsContent);
        if (this.rewardedVideoCB != null)
        {
            this.rewardedVideoCB(rewardAdsContent);
        }
        this.rewardedVideoCB = null;
    }
    #endregion


    public bool ShowRewardedVideo(int type)
    {
        ShowRewardedAd(type);
        return true;
    }

    //public bool ShowRewardedUnityAd()
    //{
    //	if (Advertisement.IsReady("rewardedVideo"))
    //	{
    //		ShowOptions showOptions = new ShowOptions
    //		{
    //			resultCallback = new Action<ShowResult>(this.HandleShowResult)
    //		};
    //		Advertisement.Show("rewardedVideo", showOptions);
    //		return true;
    //	}
    //	return false;
    //}


    public void showInterstitial(string screenName = "MainMenu")
    {
        ShowInterstitialAd();
    }

    public void HideBanner(string bannerName = "AdMob")
    {
    }

    public void ShowBanner(string bannerName = "AdMob")
    {
    }

    public void StartSession()
    {
    }

    public void LogScreen(string s)
    {
    }

    public void LogEvent()
    {
    }

    private static bool isCreated;

    public UKAdsManager.videoCallBack rewardedVideoCB;

    private float LastWatchTime;

    public delegate void videoCallBack(int type);
}
