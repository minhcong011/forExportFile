using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;
using UnityEngine.Advertisements;
public class AdsController : MonoBehaviour, IUnityAdsShowListener, IUnityAdsLoadListener, IUnityAdsInitializationListener
{
    [Header("IOS")]
    [SerializeField] private string IosAppID;
    [SerializeField] private string IosBanner;
    [SerializeField] private string IosInterstitial;
    [SerializeField] private string IosReward;

    [Header("Android")]
    [SerializeField] private string AndroidAppID;
    [SerializeField] private string AndroidBanner;
    [SerializeField] private string AndroidInterstitial;
    [SerializeField] private string AndroidReward;
    public AdPosition position;
    public bool isTest;

    private RewardedAd rewardedAd;
    private InterstitialAd interstitial;
    private BannerView bannerView;



    [Header("UnityAds")]

    [SerializeField] string _androidGameId;
    [SerializeField] string _iOSGameId;
    private string _gameId;

    string _iosBannerAdUnitId = "Banner_iOS";
    string _iosInterstitialAdUnitId = "Interstitial_iOS";
    string _iOSRewardedAdUnitId = "Rewarded_iOS";

    string _androidBannerAdUnitId = "Banner_Android";
    string _androidInterstitialAdUnitId = "Interstitial_Android";
    string _androidRewardedAdUnitId = "Rewarded_Android";

    private string bannerPlacementId;
    private string interstitialPlacementId;
    private string rewardPlacementId;

    [SerializeField] private BannerPosition bannerPosition = BannerPosition.BOTTOM;
    bool isInitUnityAds = false;
    bool isloadadStartgame = true;
    private void InitAdmod()
    {
        MobileAds.Initialize(initStatus =>
        {
            Debug.Log("Init Status : " + initStatus);
  
        });
    }

    private void Awake()
    {
        InitAdmod();
        CreateAndLoadAdmodRewardedAd();

        isInitUnityAds = false;
        InitUntiyAds();

    }
    private void Start()
    {
        isloadadStartgame = true;
        RequestAdmodBanner();
        RequestAdmodInterstitial();
    }
    private void RequestAdmodBanner()
    {
        string adUnitId;
        // These ad units are configured to always serve test ads.
        #if UNITY_ANDROID
                if (isTest)
                    adUnitId = "ca-app-pub-3940256099942544/6300978111";
                else
                    adUnitId = AndroidBanner;
        #elif UNITY_IPHONE
                                if (isTest)
                                    adUnitId = "ca-app-pub-3940256099942544/2934735716";
                                else
                                    adUnitId = IosBanner;
        #else
                                adUnitId = "unexpected_platform";
        #endif

        // Create a 320x50 banner at the top of the screen.
        this.bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Top);

        this.bannerView.OnAdFailedToLoad += this.HandleAdmodBannerFailedToLoad;

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the banner with the request.
        this.bannerView.LoadAd(request);
    }

    private void RequestAdmodInterstitial()
    {
        string adUnitId;
        // These ad units are configured to always serve test ads.
#if UNITY_ANDROID
        if (isTest)
            adUnitId = "ca-app-pub-3940256099942544/1033173712";
        else
            adUnitId = AndroidInterstitial;
#elif UNITY_IPHONE
                if (isTest)
                    adUnitId = "ca-app-pub-3940256099942544/4411468910";
                else
                    adUnitId = IosInterstitial;
#else
                adUnitId = "unexpected_platform";
#endif

        // Create an interstitial.
        this.interstitial = new InterstitialAd(adUnitId);

        this.interstitial.OnAdFailedToLoad += HandleOnAdmodInterstitialAdFailedToLoad;
        this.interstitial.OnAdClosed += HandleOnAdClosed;
        this.interstitial.OnAdLoaded += HandleOnAdLoaded;

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the interstitial with the request.
        this.interstitial.LoadAd(request);

    }

    public void ShowInterAds()
    {
        Debug.Log("Show Inter Ads : " + this.interstitial.IsLoaded());
        if (this.interstitial.IsLoaded())
        {
            this.interstitial.Show();
        }
    }


    private void HandleAdmodBannerFailedToLoad(object sender, AdFailedToLoadEventArgs e)
    {
       LoadUnityBanner();
    }
    private void HandleOnAdLoaded(object sender, EventArgs e)
    {
        GameController.controller.EnableLoadingPanel(false);
        if (isloadadStartgame)
        {
            isloadadStartgame = false;
            ShowInterAds();
        }
    }

    private void OnDestroy()
    {
        interstitial.Destroy();
    }

    private void HandleOnAdClosed(object sender, EventArgs e)
    {
        RequestAdmodInterstitial();
    }



    private void HandleOnAdmodInterstitialAdFailedToLoad(object sender, AdFailedToLoadEventArgs e)
    {
        LoadUntiyInterstitialAd();
    }


    public void CreateAndLoadAdmodRewardedAd()
    {
        string adUnitId;
        // These ad units are configured to always serve test ads.
#if UNITY_ANDROID
        if (isTest)
            adUnitId = "ca-app-pub-3940256099942544/5224354917";
        else
            adUnitId = AndroidReward;
#elif UNITY_IPHONE
                                        if (isTest)
                                            adUnitId = "ca-app-pub-3940256099942544/1712485313";
                                        else
                                            adUnitId = IosReward;
#else
                                        adUnitId = "unexpected_platform";
#endif


        this.rewardedAd = new RewardedAd(adUnitId);

        this.rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
        this.rewardedAd.OnAdClosed += HandleRewardedAdClosed;
        this.rewardedAd.OnAdFailedToLoad += HandleOnAdmodRewardAdFailToLoad;
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the rewarded ad with the request.
        this.rewardedAd.LoadAd(request);
    }

    private void HandleOnAdmodRewardAdFailToLoad(object sender, AdFailedToLoadEventArgs e)
    {
        LoadUnityRewardAd();
    }

    private void HandleRewardedAdClosed(object sender, EventArgs e)
    {
        rewardedAd.Destroy();
        CreateAndLoadAdmodRewardedAd();
    }
    private void HandleUserEarnedReward(object sender, Reward e)
    {
        GameController.controller.OpenPopupReward();
    }

    public void UserChoseToWatchAd()
    {
        if (this.rewardedAd.IsLoaded())
        {
            this.rewardedAd.Show();
        }
        else
        {
            ShowUnityRewardAd();
        }
    }

    #region Unity Ads
    private void InitUntiyAds()
    {
        _gameId = (Application.platform == RuntimePlatform.IPhonePlayer)
                     ? _iOSGameId
                     : _androidGameId;
        Advertisement.Initialize(_gameId, isTest, this);

        bannerPlacementId = (Application.platform == RuntimePlatform.IPhonePlayer)
            ? _iosBannerAdUnitId
            : _androidBannerAdUnitId;

        interstitialPlacementId = (Application.platform == RuntimePlatform.IPhonePlayer)
          ? _iosInterstitialAdUnitId
          : _androidInterstitialAdUnitId;


#if UNITY_IOS
                rewardPlacementId = _iOSRewardedAdUnitId;
#elif UNITY_ANDROID
        rewardPlacementId = _androidRewardedAdUnitId;
        #endif

        Advertisement.Banner.SetPosition(UnityEngine.Advertisements.BannerPosition.BOTTOM_CENTER);
    }

    public void OnInitializationComplete()
    {
        isInitUnityAds = true;

    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log("OnInitializationFailed Unity");
    }

    #region Banner
    public void LoadUnityBanner()
    {
        

        // Set up options to notify the SDK of load events:
        BannerLoadOptions options = new BannerLoadOptions
        {
            loadCallback = OnBannerLoaded,
            errorCallback = OnBannerError
        };


        Advertisement.Banner.Load(bannerPlacementId, options);
    }

    public void ShowUnityBanner()
    {
        // Set up options to notify the SDK of show events:
        BannerOptions options = new BannerOptions
        {
            clickCallback = OnBannerClicked,
            hideCallback = OnBannerHidden,
            showCallback = OnBannerShown
        };


        Advertisement.Banner.Show(bannerPlacementId);
    }
    #endregion
    #region Handles banner
    private void OnBannerError(string message)
    {

    }
    private void OnBannerLoaded()
    {
        Debug.Log("Unity Banner Loaded");
        ShowUnityBanner();
    }
    private void OnBannerShown()
    {

    }
    private void OnBannerHidden()
    {

    }
    private void OnBannerClicked()
    {

    }
    #endregion

    #region Interstitial
    public void LoadUntiyInterstitialAd()
    {
        if (!isInitUnityAds)
        {
            GameController.controller.EnableLoadingPanel(false);
            return;
        }

        Debug.Log("Load unity ads ");
        Advertisement.Load(interstitialPlacementId,this);
    }

    public void ShowUntiyInterstitialAd()
    {
        Advertisement.Show(interstitialPlacementId,this);
    }

    #endregion

    #region Rewarded
    public void LoadUnityRewardAd()
    {
        Advertisement.Load(rewardPlacementId,this);
    }
    public void ShowUnityRewardAd()
    {

        // Then show the ad:
        Advertisement.Show(rewardPlacementId, this);
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
       
    }

    public void OnUnityAdsShowStart(string placementId)
    {
       
    }

    public void OnUnityAdsShowClick(string placementId)
    {
        
    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        if(rewardPlacementId == placementId)
        {
            GameController.controller.OpenPopupReward();

            CreateAndLoadAdmodRewardedAd();

        }
        if(interstitialPlacementId == placementId)
        {
            RequestAdmodInterstitial();
        }
    }

    public void OnUnityAdsAdLoaded(string placementId)
    {
        if(placementId == interstitialPlacementId)
        {
            Debug.Log("Loaded unity ads interstitial PlacementId");
            GameController.controller.EnableLoadingPanel(false);
            if (isloadadStartgame)
            {
                isloadadStartgame = false;
                ShowUntiyInterstitialAd();
            }
        }
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        if(placementId == interstitialPlacementId)
        {
            Debug.Log("Load fail unity ads interstitial PlacementId");
            GameController.controller.EnableLoadingPanel(false);
        }
    }


    #endregion
    #endregion

}