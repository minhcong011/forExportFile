using GoogleMobileAds.Api;
using SimpleSolitaire.Model.Enum;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Advertisements;

namespace SimpleSolitaire.Controller
{

    [Serializable]
    public class AdsIds
    {
        public string InterId;
        public string RewardId;
        public string BannerId;
    }

    public class AdsData
    {
        public AdsIds Ids;

        public bool IsTestADS;
        public bool IsBanner;
        public bool IsIntersitial;
        public bool IsReward;
        public bool IsHandeldAction;
    }

    public class InterVideoAds : MonoBehaviour
    {
        public static Action<RewardAdsState, RewardAdsType> RewardAction { get; set; }

        [SerializeField]
        private GameManager _gameManagerComponent;
        [SerializeField]
        private UndoPerformer _undoPerformerComponent;

        [SerializeField]
        private AdsIds _androidIds;
        [SerializeField]
        private AdsIds _iosIds;

        private string _interId;
        private string _rewardId;
        private string _bannerId;
		public string UnityAppID = "";
		public string UnityInterstitialAd = "";

        private readonly string _testBannerId = "ca-app-pub-3940256099942544/6300978111";
        private readonly string _testIntersitialId = "ca-app-pub-3940256099942544/1033173712";
        private readonly string _testRewardId = "ca-app-pub-3940256099942544/5224354917";

        [Space(5f)]
        [SerializeField]
        private int _firstCallIntersitialTime;
        [SerializeField]
        private int _repeatIntersitialTime;

        private AdRequest _requestAdmob;
        private BannerView _bannerView;
        private InterstitialAd _interstitialAndroid;

        private RewardBasedVideoAd _rewardVideoAndroid;

        [SerializeField]
        private bool _isTestADS;
        [SerializeField]
        private bool _isBanner;
        [SerializeField]
        private bool _isIntersitial;
        [SerializeField]
        private bool _isReward;

        private AdSize _currentBannerSize = AdSize.Banner;

        public readonly string NoAdsKey = "NoAds";

        private RewardAdsType _lastShowingType = RewardAdsType.None;
        private RewardVideoStatus _lastRewardVideoStatus = RewardVideoStatus.None;

        public static InterVideoAds Instance;

        private void Start()
        {
            Instance = this;
            InitializeADS();
			InitializeAdvertisement();
        }
		
		private void InitializeAdvertisement()
	
		{
			Advertisement.Initialize(UnityAppID);
		}
		
		public void ShowUnityInterstitialAd()
	    {
		   // if (!Advertisement.IsReady(UnityInterstitialAd))
		   //{
			  // return;
		   //}
		    Advertisement.Show(UnityInterstitialAd);
	    }
	

        /// <summary>
        /// Initialize admob requests variable.
        /// </summary>
        private void AdMobRequest()
        {
            _requestAdmob = (_isTestADS) ? new AdRequest.Builder().AddTestDevice(SystemInfo.deviceUniqueIdentifier).Build() : new AdRequest.Builder().Build();
        }

        #region Requests ADS
        /// <summary>
        /// Banned ad request.
        /// </summary>
        private void RequestBanner()
        {
            if (IsHasKeyNoAds())
                return;
            _bannerView = new BannerView((_isTestADS) ? _testBannerId : _bannerId, _currentBannerSize, AdPosition.Bottom);

            if (_bannerView != null)
            {
                AdMobRequest();
                _bannerView.LoadAd(_requestAdmob);
            }
        }

        /// <summary>
        /// Intersitial video request.
        /// </summary>
        public void RequestInterstitial()
        {
            if (IsHasKeyNoAds())
                return;

            _interstitialAndroid = new InterstitialAd((_isTestADS) ? _testIntersitialId : _interId);
            AdMobRequest();
            _interstitialAndroid.LoadAd(_requestAdmob);
        }

        /// <summary>
        /// Reward video request.
        /// </summary>
        private void RequestRewardBasedVideo(bool isRequiredRequest = false)
        {
            if (IsHasKeyNoAds() && !isRequiredRequest)
                return;

            _rewardVideoAndroid = RewardBasedVideoAd.Instance;
            AdMobRequest();
            _rewardVideoAndroid.LoadAd(_requestAdmob, (_isTestADS) ? _testRewardId : _rewardId);
        }

        #endregion

        #region Show/Hide ADS
        /// <summary>
        /// Show banner ads <see cref="_bannerView"/> if ads available for watch.
        /// </summary>
        public void ShowBanner()
        {
            if (IsHasKeyNoAds())
                return;
            if (_bannerView != null)
            {
                _bannerView.Show();
            }
        }

        /// <summary>
        /// Show intersitial ads <see cref="_interstitial"/> if ads available for watch.
        /// </summary>
        /// 
        private bool canShowInter = true;
        public void ShowInterstitial()
        {
            if (!canShowInter) return;
            if (IsHasKeyNoAds())
                return;

            if (_interstitialAndroid.IsLoaded())
            {
                _interstitialAndroid.Show();

                _interstitialAndroid = new InterstitialAd((_isTestADS) ? _testIntersitialId : _interId);
                AdMobRequest();
                _interstitialAndroid.LoadAd(_requestAdmob);

                StartCoroutine(CalculateTimeShowInter());
            }
        }
        IEnumerator CalculateTimeShowInter()
        {
            canShowInter = false;
            yield return new WaitForSeconds(_repeatIntersitialTime);
            canShowInter = true;
        }
        /// <summary>
        /// Show reward video <see cref="_rewardVideoAndroid"/> if ads available for watch.
        /// </summary>
        public void ShowRewardBasedVideo()
        {
            if (PlayerPrefs.HasKey(NoAdsKey))
                return;

            if (_rewardVideoAndroid.IsLoaded())
            {
                _rewardVideoAndroid.Show();

                _rewardVideoAndroid = RewardBasedVideoAd.Instance;
                AdMobRequest();
                _rewardVideoAndroid.LoadAd(_requestAdmob, (_isTestADS) ? _testRewardId : _rewardId);
            }
        }

        /// <summary>
        /// This method hide Smart banner from bottom of screen.
        /// </summary>
        public void HideBanner()
        {
            if (_bannerView != null)
                _bannerView.Hide();
            _gameManagerComponent.InitializeBottomPanel(0);
        }
        /// <summary>
        /// Show reward video. If user watch it the ads will disappear for current game session.
        /// </summary>
        public void NoAdsAction()
        {
            _lastShowingType = RewardAdsType.NoAds;
            StartCoroutine(LoadRewardedVideo(_rewardVideoAndroid, _lastShowingType));
        }

        /// <summary>
        /// Show reward video. If user watch it the free undo tries will be add for user.
        /// </summary>
        public void ShowGetUndoAction()
        {
            _lastShowingType = RewardAdsType.GetUndo;
            StartCoroutine(LoadRewardedVideo(_rewardVideoAndroid, _lastShowingType));
        }

        private IEnumerator LoadRewardedVideo(RewardBasedVideoAd ads, RewardAdsType type)
        {
            _lastRewardVideoStatus = RewardVideoStatus.None;

            ads.OnAdFailedToLoad += RewardedVideoFailedToLoad;
            ads.OnAdLoaded += RewardedVideoLoaded;

#if UNITY_IPHONE
                Handheld.SetActivityIndicatorStyle(UnityEngine.iOS.ActivityIndicatorStyle.Gray);
#elif UNITY_ANDROID
                Handheld.SetActivityIndicatorStyle(AndroidActivityIndicatorStyle.Small);
#endif 
                Handheld.StartActivityIndicator();

            yield return new WaitUntil(() => _lastRewardVideoStatus == RewardVideoStatus.Loaded
                                          || _lastRewardVideoStatus == RewardVideoStatus.FailedToLoad
                                          || _isTestADS
                                          || ads.IsLoaded()
                                          || Application.isEditor);
            Handheld.StopActivityIndicator();

            _lastRewardVideoStatus = ads.IsLoaded() || _isTestADS ? _lastRewardVideoStatus = RewardVideoStatus.Loaded : _lastRewardVideoStatus;

            ads.OnAdFailedToLoad -= RewardedVideoFailedToLoad;
            ads.OnAdLoaded -= RewardedVideoLoaded;

            if (Application.isEditor)
            {
                OnRewardedUser();
                yield break;
            }

            switch (_lastRewardVideoStatus)
            {
                case RewardVideoStatus.None:
                case RewardVideoStatus.FailedToLoad:
                    if (RewardAction != null)
                        RewardAction.Invoke(RewardAdsState.DID_NOT_LOADED, type);
                    break;
                case RewardVideoStatus.Loaded:
                    ads.OnAdRewarded += HandleRewardBasedVideoRewarded;
                    ads.OnAdClosed += HandleClosedBasedVideoRewarded;
                    ads.Show();
                    break;
            }


        }
        #endregion

        #region EventsHandlers

        /// <summary>
        /// On reward loaded video event.
        /// </summary>
        private void RewardedVideoLoaded(object sender, EventArgs e)
        {
            Debug.Log("RewardedVideoLoaded");
            _lastRewardVideoStatus = RewardVideoStatus.Loaded;
        }

        /// <summary>
        /// On reward failed load video event.
        /// </summary>
        private void RewardedVideoFailedToLoad(object sender, AdFailedToLoadEventArgs e)
        {
            Debug.Log("RewardedVideoFailedToLoad " + e.Message);
            _lastRewardVideoStatus = RewardVideoStatus.FailedToLoad;
        }

        /// <summary>
        /// On close reward video event.
        /// </summary>
        private void HandleClosedBasedVideoRewarded(object sender, EventArgs eventArgs)
        {
            switch (_lastShowingType)
            {
                case RewardAdsType.NoAds:
                    _rewardVideoAndroid.OnAdClosed -= HandleClosedBasedVideoRewarded;
                    RequestRewardBasedVideo();
                    break;
                case RewardAdsType.GetUndo:
                    _rewardVideoAndroid.OnAdClosed -= HandleClosedBasedVideoRewarded;
                    RequestRewardBasedVideo(true);
                    break;
            }

            if (RewardAction != null)
                RewardAction.Invoke(RewardAdsState.TOO_EARLY_CLOSE, _lastShowingType);
        }

        /// <summary>
        /// On full watch reward video event.
        /// </summary>
        public void HandleRewardBasedVideoRewarded(object sender, Reward args)
        {
            OnRewardedUser();
        }

        /// <summary>
        /// Reward actions by type of reward ads.
        /// </summary>
        public void OnRewardedUser()
        {
            switch (_lastShowingType)
            {
                case RewardAdsType.NoAds:
                    PlayerPrefs.SetInt(NoAdsKey, 1);
                    HideBanner();
                    _gameManagerComponent.OnClickAdsCloseBtn();
                    _gameManagerComponent.AdsBtn.SetActive(false);
                    RequestRewardBasedVideo(true);
                    break;
                case RewardAdsType.GetUndo:
                    _gameManagerComponent.OnClickAdsCloseBtn();
                    _undoPerformerComponent.UpdateUndoCounts();
                    RequestRewardBasedVideo(true);
                    break;
            }

            if (_rewardVideoAndroid != null)
            {
                _rewardVideoAndroid.OnAdRewarded -= HandleRewardBasedVideoRewarded;
                _rewardVideoAndroid.OnAdClosed -= HandleClosedBasedVideoRewarded;
            }
        }
        #endregion

        /// <summary>
        /// Initialize all active advertisment.
        /// </summary>
        public void InitializeADS()
        {
            if (IsHasKeyNoAds())
                PlayerPrefs.DeleteKey(NoAdsKey);

            //Uncomment for old versions of admob sdk
            //MobileAds.Initialize(initStatus => { Debug.Log("Sdk init status:" + initStatus); });
            var ids = Application.platform == RuntimePlatform.Android ? _androidIds : Application.platform == RuntimePlatform.IPhonePlayer ? _iosIds : null;

            if (ids != null)
            {
                _interId = ids.InterId;
                _rewardId = ids.RewardId;
                _bannerId = ids.BannerId;
            }

            if (_isBanner)
            {
                Debug.Log("ShowBanner");
                RequestBanner();
                ShowBanner();
                _gameManagerComponent.InitializeBottomPanel(_currentBannerSize.Height * GetAdmobBannerScaleBasedOnDPI());
            }

            if (_isReward)
            {
                RequestRewardBasedVideo(true);
            }

            if (_isIntersitial)
            {
                RequestInterstitial();
                // First call after _firstCallIntersitialTime seconds. Repeating intersitial video every _repeatIntersitialTime seconds.
                //InvokeRepeating("ShowInterstitial", _firstCallIntersitialTime, _repeatIntersitialTime);
            }
        }

        /// <summary>
        /// Check for exist in player prefs key <see cref="NoAdsKey"/>
        /// </summary>
        /// <returns></returns>
        private bool IsHasKeyNoAds()
        {
            return PlayerPrefs.HasKey(NoAdsKey);
        }

        private float GetAdmobBannerScaleBasedOnDPI()
        {
            //By default banner has no scaling.
            float scale = 1f;

            //All information about scaling has provided on Google Admob API
            //Low Density Screens, around 120 DPI, scaling factor 0.75, e.g. 320×50 becomes 240×37.
            //Medium Density Screens, around 160 DPI, no scaling, e.g. 320×50 stays at 320×50.
            //High Density Screens, around 240 DPI, scaling factor 1.5, e.g. 320×50 becomes 480×75.
            //Extra High Density Screens, around 320 DPI, scaling factor 2, e.g. 320×50 becomes 640×100.
            //Extra Extra High Density Screens, around 480 DPI, scaling factor 3, e.g. 320×50 becomes 960×150.

            if (Screen.dpi > 480)
            {
                scale = 3f;
            }
            else if (Screen.dpi > 320)
            {
                scale = 2f;
            }
            else if (Screen.dpi > 240)
            {
                scale = 1.5f;
            }
            else if (Screen.dpi > 160)
            {
                scale = 1f;
            }
            else if (Screen.dpi > 120)
            {
                scale = 0.75f;
            }

            return scale;
        }
    }
}