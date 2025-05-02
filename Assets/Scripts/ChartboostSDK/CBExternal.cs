// dnSpy decompiler from Assembly-CSharp.dll class: ChartboostSDK.CBExternal
using System;
using UnityEngine;

namespace ChartboostSDK
{
	public class CBExternal
	{
		public static void Log(string message)
		{
			if (CBSettings.isLogging() && UnityEngine.Debug.isDebugBuild)
			{
				UnityEngine.Debug.Log(CBExternal._logTag + "/" + message);
			}
		}

		public static bool isInitialized()
		{
			return CBExternal.initialized;
		}

		private static bool checkInitialized()
		{
			if (CBExternal.initialized)
			{
				return true;
			}
			UnityEngine.Debug.LogError("The Chartboost SDK needs to be initialized before we can show any ads");
			return false;
		}

		public static void init()
		{
			string selectAndroidAppId = CBSettings.getSelectAndroidAppId();
			string selectAndroidAppSecret = CBSettings.getSelectAndroidAppSecret();
			CBExternal.initWithAppId(selectAndroidAppId, selectAndroidAppSecret);
		}

		public static void initWithAppId(string appId, string appSignature)
		{
			string unityVersion = Application.unityVersion;
			CBExternal.Log("Unity : initWithAppId " + appId + " and version " + unityVersion);
			using (AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.chartboost.sdk.unity.CBPlugin"))
			{
				CBExternal._plugin = androidJavaClass.CallStatic<AndroidJavaObject>("instance", new object[0]);
			}
			CBExternal._plugin.Call("init", new object[]
			{
				appId,
				appSignature,
				unityVersion
			});
			CBExternal.initialized = true;
		}

		public static bool isAnyViewVisible()
		{
			bool flag = false;
			if (!CBExternal.checkInitialized())
			{
				return flag;
			}
			flag = CBExternal._plugin.Call<bool>("isAnyViewVisible", new object[0]);
			CBExternal.Log("Android : isAnyViewVisible = " + flag);
			return flag;
		}

		public static void cacheInterstitial(CBLocation location)
		{
			if (!CBExternal.checkInitialized())
			{
				return;
			}
			if (location == null)
			{
				UnityEngine.Debug.LogError("Chartboost SDK: location passed is null cannot perform the operation requested");
				return;
			}
			CBExternal._plugin.Call("cacheInterstitial", new object[]
			{
				location.ToString()
			});
			CBExternal.Log("Android : cacheInterstitial at location = " + location.ToString());
		}

		public static bool hasInterstitial(CBLocation location)
		{
			if (!CBExternal.checkInitialized())
			{
				return false;
			}
			if (location == null)
			{
				UnityEngine.Debug.LogError("Chartboost SDK: location passed is null cannot perform the operation requested");
				return false;
			}
			CBExternal.Log("Android : hasInterstitial at location = " + location.ToString());
			return CBExternal._plugin.Call<bool>("hasInterstitial", new object[]
			{
				location.ToString()
			});
		}

		public static void showInterstitial(CBLocation location)
		{
			if (!CBExternal.checkInitialized())
			{
				return;
			}
			if (location == null)
			{
				UnityEngine.Debug.LogError("Chartboost SDK: location passed is null cannot perform the operation requested");
				return;
			}
			CBExternal._plugin.Call("showInterstitial", new object[]
			{
				location.ToString()
			});
			CBExternal.Log("Android : showInterstitial at location = " + location.ToString());
		}

		public static void cacheInPlay(CBLocation location)
		{
			if (!CBExternal.checkInitialized())
			{
				return;
			}
			if (location == null)
			{
				UnityEngine.Debug.LogError("Chartboost SDK: location passed is null cannot perform the operation requested");
				return;
			}
			CBExternal._plugin.Call("cacheInPlay", new object[]
			{
				location.ToString()
			});
			CBExternal.Log("Android : cacheInPlay at location = " + location.ToString());
		}

		public static bool hasInPlay(CBLocation location)
		{
			if (!CBExternal.checkInitialized())
			{
				return false;
			}
			if (location == null)
			{
				UnityEngine.Debug.LogError("Chartboost SDK: location passed is null cannot perform the operation requested");
				return false;
			}
			CBExternal.Log("Android : hasInPlay at location = " + location.ToString());
			return CBExternal._plugin.Call<bool>("hasCachedInPlay", new object[]
			{
				location.ToString()
			});
		}

		public static CBInPlay getInPlay(CBLocation location)
		{
			CBExternal.Log("Android : getInPlay at location = " + location.ToString());
			if (!CBExternal.checkInitialized())
			{
				return null;
			}
			if (location == null)
			{
				UnityEngine.Debug.LogError("Chartboost SDK: location passed is null cannot perform the operation requested");
				return null;
			}
			CBInPlay result;
			try
			{
				AndroidJavaObject inPlayAd = CBExternal._plugin.Call<AndroidJavaObject>("getInPlay", new object[]
				{
					location.ToString()
				});
				CBInPlay cbinPlay = new CBInPlay(inPlayAd, CBExternal._plugin);
				result = cbinPlay;
			}
			catch
			{
				result = null;
			}
			return result;
		}

		public static void cacheRewardedVideo(CBLocation location)
		{
			if (!CBExternal.checkInitialized())
			{
				return;
			}
			if (location == null)
			{
				UnityEngine.Debug.LogError("Chartboost SDK: location passed is null cannot perform the operation requested");
				return;
			}
			CBExternal._plugin.Call("cacheRewardedVideo", new object[]
			{
				location.ToString()
			});
			CBExternal.Log("Android : cacheRewardedVideo at location = " + location.ToString());
		}

		public static bool hasRewardedVideo(CBLocation location)
		{
			if (!CBExternal.checkInitialized())
			{
				return false;
			}
			if (location == null)
			{
				UnityEngine.Debug.LogError("Chartboost SDK: location passed is null cannot perform the operation requested");
				return false;
			}
			CBExternal.Log("Android : hasRewardedVideo at location = " + location.ToString());
			return CBExternal._plugin.Call<bool>("hasRewardedVideo", new object[]
			{
				location.ToString()
			});
		}

		public static void showRewardedVideo(CBLocation location)
		{
			if (!CBExternal.checkInitialized())
			{
				return;
			}
			if (location == null)
			{
				UnityEngine.Debug.LogError("Chartboost SDK: location passed is null cannot perform the operation requested");
				return;
			}
			CBExternal._plugin.Call("showRewardedVideo", new object[]
			{
				location.ToString()
			});
			CBExternal.Log("Android : showRewardedVideo at location = " + location.ToString());
		}

		public static void chartBoostShouldDisplayInterstitialCallbackResult(bool result)
		{
			if (!CBExternal.checkInitialized())
			{
				return;
			}
			CBExternal._plugin.Call("chartBoostShouldDisplayInterstitialCallbackResult", new object[]
			{
				result
			});
			CBExternal.Log("Android : chartBoostShouldDisplayInterstitialCallbackResult");
		}

		public static void chartBoostShouldDisplayRewardedVideoCallbackResult(bool result)
		{
			if (!CBExternal.checkInitialized())
			{
				return;
			}
			CBExternal._plugin.Call("chartBoostShouldDisplayRewardedVideoCallbackResult", new object[]
			{
				result
			});
			CBExternal.Log("Android : chartBoostShouldDisplayRewardedVideoCallbackResult");
		}

		public static void didPassAgeGate(bool pass)
		{
			CBExternal._plugin.Call("didPassAgeGate", new object[]
			{
				pass
			});
		}

		public static void setShouldPauseClickForConfirmation(bool shouldPause)
		{
			CBExternal._plugin.Call("setShouldPauseClickForConfirmation", new object[]
			{
				shouldPause
			});
		}

		public static string getCustomId()
		{
			return CBExternal._plugin.Call<string>("getCustomId", new object[0]);
		}

		public static void setCustomId(string customId)
		{
			CBExternal._plugin.Call("setCustomId", new object[]
			{
				customId
			});
		}

		public static bool getAutoCacheAds()
		{
			return CBExternal._plugin.Call<bool>("getAutoCacheAds", new object[0]);
		}

		public static void setAutoCacheAds(bool autoCacheAds)
		{
			CBExternal._plugin.Call("setAutoCacheAds", new object[]
			{
				autoCacheAds
			});
		}

		public static void setShouldRequestInterstitialsInFirstSession(bool shouldRequest)
		{
			CBExternal._plugin.Call("setShouldRequestInterstitialsInFirstSession", new object[]
			{
				shouldRequest
			});
		}

		public static void setShouldPrefetchVideoContent(bool shouldPrefetch)
		{
			CBExternal._plugin.Call("setShouldPrefetchVideoContent", new object[]
			{
				shouldPrefetch
			});
		}

		public static void trackLevelInfo(string eventLabel, CBLevelType type, int mainLevel, int subLevel, string description)
		{
			CBExternal._plugin.Call("trackLevelInfo", new object[]
			{
				eventLabel,
				(int)type,
				mainLevel,
				subLevel,
				description
			});
			CBExternal.Log(string.Format("Android : PIA Level Tracking:\n\teventLabel = {0}\n\ttype = {1}\n\tmainLevel = {2}\n\tsubLevel = {3}\n\tdescription = {4}", new object[]
			{
				eventLabel,
				(int)type,
				mainLevel,
				subLevel,
				description
			}));
		}

		public static void trackLevelInfo(string eventLabel, CBLevelType type, int mainLevel, string description)
		{
			CBExternal._plugin.Call("trackLevelInfo", new object[]
			{
				eventLabel,
				(int)type,
				mainLevel,
				description
			});
			CBExternal.Log(string.Format("Android : PIA Level Tracking:\n\teventLabel = {0}\n\ttype = {1}\n\tmainLevel = {2}\n\tdescription = {3}", new object[]
			{
				eventLabel,
				(int)type,
				mainLevel,
				description
			}));
		}

		public static void setGameObjectName(string name)
		{
			CBExternal._plugin.Call("setGameObjectName", new object[]
			{
				name
			});
		}

		public static void pause(bool paused)
		{
			if (!CBExternal.checkInitialized())
			{
				return;
			}
			CBExternal._plugin.Call("pause", new object[]
			{
				paused
			});
			CBExternal.Log("Android : pause");
		}

		public static void destroy()
		{
			if (!CBExternal.checkInitialized())
			{
				return;
			}
			CBExternal._plugin.Call("destroy", new object[0]);
			CBExternal.initialized = false;
			CBExternal.Log("Android : destroy");
		}

		public static bool onBackPressed()
		{
			if (!CBExternal.checkInitialized())
			{
				return false;
			}
			bool result = CBExternal._plugin.Call<bool>("onBackPressed", new object[0]);
			CBExternal.Log("Android : onBackPressed");
			return result;
		}

		public static void trackInAppGooglePlayPurchaseEvent(string title, string description, string price, string currency, string productID, string purchaseData, string purchaseSignature)
		{
			CBExternal.Log("Android: trackInAppGooglePlayPurchaseEvent");
			CBExternal._plugin.Call("trackInAppGooglePlayPurchaseEvent", new object[]
			{
				title,
				description,
				price,
				currency,
				productID,
				purchaseData,
				purchaseSignature
			});
		}

		public static void trackInAppAmazonStorePurchaseEvent(string title, string description, string price, string currency, string productID, string userID, string purchaseToken)
		{
			CBExternal.Log("Android: trackInAppAmazonStorePurchaseEvent");
			CBExternal._plugin.Call("trackInAppAmazonStorePurchaseEvent", new object[]
			{
				title,
				description,
				price,
				currency,
				productID,
				userID,
				purchaseToken
			});
		}

		public static void setMediation(CBMediation mediator, string version)
		{
			CBExternal._plugin.Call("setMediation", new object[]
			{
				mediator.ToString(),
				version
			});
			CBExternal.Log("Android : setMediation to = " + mediator.ToString() + " " + version);
		}

		public static void restrictDataCollection(bool limit)
		{
			CBExternal.Log("Android: restrictDataCollection set to " + limit);
			CBExternal._plugin.Call("restrictDataCollection", new object[]
			{
				limit
			});
		}

		public static void setMuted(bool mute)
		{
			CBExternal.Log("Android: setMuted not supported on Android");
		}

		public static bool isWebViewEnabled()
		{
			return CBExternal._plugin.Call<bool>("isWebViewEnabled", new object[0]);
		}

		private static bool initialized;

		private static string _logTag = "ChartboostSDK";

		private static AndroidJavaObject _plugin;
	}
}
