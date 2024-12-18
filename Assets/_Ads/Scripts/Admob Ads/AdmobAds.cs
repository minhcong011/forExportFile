using System.Collections;
using System.Collections.Generic;
using GoogleMobileAds.Api;
using GoogleMobileAds.Common;
using System;
using UnityEngine;
using TMPro;

public class AdmobAds : Ads
{
    [SerializeField] AdsDataSO _data;
	[SerializeField] private float thoiGianCho;
    [SerializeField] private GameObject notificationShowInterAds;
    [SerializeField] private TextMeshProUGUI timeRemainShowAdsText;
    [Space]
    [SerializeField] AdPosition _bannerPosition;
    [SerializeField]
    TagForChildDirectedTreatment _tagForChildDirectedTreatment;

    [HideInInspector] BannerView AdBanner;
    [HideInInspector] InterstitialAd AdInterstitial;
    [HideInInspector] RewardedAd AdReward;

    bool _isInitComplete = false;
    private bool notificationAdsIsOpen;
    private void Start()
    {
	DontDestroyOnLoad(gameObject);
        RequestConfiguration requestConfiguration =
            new RequestConfiguration.Builder()
                .SetTagForChildDirectedTreatment(_tagForChildDirectedTreatment)
                .build();

        MobileAds.Initialize(initstatus =>
        {
            MobileAdsEventExecutor.ExecuteInUpdate(() =>
            {
                _isInitComplete = true;

                RequestAndShowBanner();
                RequestRewardAd();
                RequestInterstitialAd();
            StartCoroutine(ShowInterWithNoTouch());
            });
        });
    }
	    IEnumerator ShowInterWithNoTouch()
    {
        float totalTime = thoiGianCho;
        float countDown = totalTime;
        while (true)
        {
            countDown -= Time.deltaTime;
            if(countDown <= 5 && !notificationAdsIsOpen)
            {
                StartCoroutine(ShowNotificationInterAds());
                notificationAdsIsOpen = true;
            }
            if(countDown <= 0)
            {
                countDown = totalTime;
            }
            yield return null;
        }
    }
    private IEnumerator ShowNotificationInterAds()
    {
        notificationShowInterAds.SetActive(true);
        float countDown = 5;
        while(countDown > 0)
        {
            timeRemainShowAdsText.text = $"{(int)countDown}";
            countDown -= Time.deltaTime;
            yield return null;
        }
        notificationShowInterAds.SetActive(false);
        yield return new WaitForSeconds(0.2f); notificationAdsIsOpen = false;
	ShowInterstitial();
    }
    private void OnDestroy()
    {
        DestroyBannerAd();
        DestroyInterstitialAd();
    }

    public override bool IsRewardedAdLoaded()
    {
        if (AdReward != null && AdReward.IsLoaded())
            return true;
        else
            return false;
    }

    AdRequest CreateAdRequest()
    {
        return new AdRequest.Builder()
           //.TagForChildDirectedTreatment(false)
           .AddExtra("npa", PlayerPrefs.GetInt("npa", 1).ToString())
           .Build();
    }

    #region Banner Ad -----------------------------------------------------------------------------
    public override void ShowBanner()
    {
        if (!_isInitComplete) return;

        if(AdBanner != null)
        {
            AdBanner.Show();
        }
    }

    public void RequestAndShowBanner()
    {
        if (!_data.BannerEnabled) return;

        AdSize adaptiveSize = AdSize.Banner;
        AdBanner = new BannerView(_data.BannerID, adaptiveSize, _bannerPosition);

        AdBanner.LoadAd(CreateAdRequest());
    }

    public void DestroyBannerAd()
    {
        if (AdBanner != null)
        {
            AdBanner.Destroy();
        }
    }
    #endregion

    #region Interstitial Ad ------------------------------------------------------------------------
    public void RequestInterstitialAd()
    {
        if (!_data.InterstitialEnabled) return;

        AdInterstitial = new InterstitialAd(_data.InterstitialID);

        AdInterstitial.OnAdClosed += HandleInterstitialAdClosed;

        AdInterstitial.LoadAd(CreateAdRequest());
    }

    public override void ShowInterstitial()
    {
        if (AdInterstitial != null && AdInterstitial.IsLoaded())
            AdInterstitial.Show();
    }

    public void DestroyInterstitialAd()
    {
        if (AdInterstitial != null)
        {
            AdInterstitial.Destroy();
        }
    }
    #endregion

    #region Rewarded Ad ----------------------------------------------------------------------------
    public void RequestRewardAd()
    {
        if (!_data.RewardedEnabled) return;

        AdReward = new RewardedAd(_data.RewardedID);

        AdReward.OnAdClosed += HandleOnRewardedAdClosed;
        AdReward.OnUserEarnedReward += HandleOnRewardedAdWatched;

        AdReward.LoadAd(CreateAdRequest());
    }


    public override void ShowRewarded()
    {
        if (AdReward != null && AdReward.IsLoaded())
            AdReward.Show();
    }
    #endregion

    #region Event Handler
    private void HandleInterstitialAdClosed(object sender, EventArgs e)
    {
        DestroyInterstitialAd();
        RequestInterstitialAd();
        AdsManager.HandleClosedInterstitial();
    }

    private void HandleOnRewardedAdClosed(object sender, EventArgs e)
    {
        RequestRewardAd();
    }

    private void HandleOnRewardedAdWatched(object sender, Reward e)
    {
        AdsManager.HandleRewardedAdWatchedComplete();
    }
    #endregion
}
