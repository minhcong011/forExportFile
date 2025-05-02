// dnSpy decompiler from Assembly-CSharp.dll class: ChartboostSDK.CBSettings
using System;
using UnityEngine;

namespace ChartboostSDK
{
	public class CBSettings : ScriptableObject
	{
		private static CBSettings Instance
		{
			get
			{
				if (CBSettings.instance == null)
				{
					CBSettings.instance = (Resources.Load("ChartboostSettings") as CBSettings);
					if (CBSettings.instance == null)
					{
						CBSettings.instance = ScriptableObject.CreateInstance<CBSettings>();
					}
				}
				return CBSettings.instance;
			}
		}

		public static void setAppId(string appId, string appSignature)
		{
			if (CBSettings.Instance.selectedAndroidPlatformIndex == 0)
			{
				UnityEngine.Debug.Log("Overriding Google AppId: " + appId);
				CBSettings.Instance.SetAndroidAppId(appId);
				CBSettings.Instance.SetAndroidAppSecret(appSignature);
			}
			else
			{
				UnityEngine.Debug.Log("Overriding Amazon AppId: " + appId);
				CBSettings.Instance.SetAmazonAppId(appId);
				CBSettings.Instance.SetAmazonAppSecret(appSignature);
			}
		}

		public void SetAndroidPlatformIndex(int index)
		{
			if (this.selectedAndroidPlatformIndex != index)
			{
				this.selectedAndroidPlatformIndex = index;
				CBSettings.DirtyEditor();
			}
		}

		public int SelectedAndroidPlatformIndex
		{
			get
			{
				return this.selectedAndroidPlatformIndex;
			}
		}

		public string[] AndroidPlatformLabels
		{
			get
			{
				return this.androidPlatformLabels;
			}
			set
			{
				if (!this.androidPlatformLabels.Equals(value))
				{
					this.androidPlatformLabels = value;
					CBSettings.DirtyEditor();
				}
			}
		}

		public void SetIOSAppId(string id)
		{
			if (!CBSettings.Instance.iOSAppId.Equals(id))
			{
				CBSettings.Instance.iOSAppId = id;
				CBSettings.DirtyEditor();
			}
		}

		public static string getIOSAppId()
		{
			if (CBSettings.Instance.iOSAppId.Equals("CB_IOS_APP_ID"))
			{
				CBSettings.CredentialsWarning("CHARTBOOST: You are using the Chartboost {0} example {1}! Go to the Chartboost dashboard and replace these with an App ID & App Signature from your account! If you need help, check out answers.chartboost.com", "IOS", "App ID");
				return "4f21c409cd1cb2fb7000001b";
			}
			if (CBSettings.Instance.iOSAppId.Equals(string.Empty))
			{
				CBSettings.CredentialsWarning("CHARTBOOST: You are using an empty string for the {0} {1}! Go to the Chartboost dashboard and replace these with an App ID & App Signature from your account! If you need help, check out answers.chartboost.com", "IOS", "App ID");
			}
			return CBSettings.Instance.iOSAppId;
		}

		public void SetIOSAppSecret(string secret)
		{
			if (!CBSettings.Instance.iOSAppSecret.Equals(secret))
			{
				CBSettings.Instance.iOSAppSecret = secret;
				CBSettings.DirtyEditor();
			}
		}

		public static string getIOSAppSecret()
		{
			if (CBSettings.Instance.iOSAppSecret.Equals("CB_IOS_APP_SIGNATURE"))
			{
				CBSettings.CredentialsWarning("CHARTBOOST: You are using the Chartboost {0} example {1}! Go to the Chartboost dashboard and replace these with an App ID & App Signature from your account! If you need help, check out answers.chartboost.com", "IOS", "App Signature");
				return "92e2de2fd7070327bdeb54c15a5295309c6fcd2d";
			}
			if (CBSettings.Instance.iOSAppSecret.Equals(string.Empty))
			{
				CBSettings.CredentialsWarning("CHARTBOOST: You are using an empty string for the {0} {1}! Go to the Chartboost dashboard and replace these with an App ID & App Signature from your account! If you need help, check out answers.chartboost.com", "IOS", "App Signature");
			}
			return CBSettings.Instance.iOSAppSecret;
		}

		public void SetAndroidAppId(string id)
		{
			if (!CBSettings.Instance.androidAppId.Equals(id))
			{
				CBSettings.Instance.androidAppId = id;
				CBSettings.DirtyEditor();
			}
		}

		public static string getAndroidAppId()
		{
			if (CBSettings.Instance.androidAppId.Equals("CB_ANDROID_APP_ID"))
			{
				CBSettings.CredentialsWarning("CHARTBOOST: You are using the Chartboost {0} example {1}! Go to the Chartboost dashboard and replace these with an App ID & App Signature from your account! If you need help, check out answers.chartboost.com", "Android", "App ID");
				return "4f7b433509b6025804000002";
			}
			if (CBSettings.Instance.androidAppId.Equals(string.Empty))
			{
				CBSettings.CredentialsWarning("CHARTBOOST: You are using an empty string for the {0} {1}! Go to the Chartboost dashboard and replace these with an App ID & App Signature from your account! If you need help, check out answers.chartboost.com", "Android", "App ID");
			}
			return CBSettings.Instance.androidAppId;
		}

		public void SetAndroidAppSecret(string secret)
		{
			if (!CBSettings.Instance.androidAppSecret.Equals(secret))
			{
				CBSettings.Instance.androidAppSecret = secret;
				CBSettings.DirtyEditor();
			}
		}

		public static string getAndroidAppSecret()
		{
			if (CBSettings.Instance.androidAppSecret.Equals("CB_ANDROID_APP_SIGNATURE"))
			{
				CBSettings.CredentialsWarning("CHARTBOOST: You are using the Chartboost {0} example {1}! Go to the Chartboost dashboard and replace these with an App ID & App Signature from your account! If you need help, check out answers.chartboost.com", "Android", "App Signature");
				return "dd2d41b69ac01b80f443f5b6cf06096d457f82bd";
			}
			if (CBSettings.Instance.androidAppSecret.Equals(string.Empty))
			{
				CBSettings.CredentialsWarning("CHARTBOOST: You are using an empty string for the {0} {1}! Go to the Chartboost dashboard and replace these with an App ID & App Signature from your account! If you need help, check out answers.chartboost.com", "Android", "App Signature");
			}
			return CBSettings.Instance.androidAppSecret;
		}

		public void SetAmazonAppId(string id)
		{
			if (!CBSettings.Instance.amazonAppId.Equals(id))
			{
				CBSettings.Instance.amazonAppId = id;
				CBSettings.DirtyEditor();
			}
		}

		public static string getAmazonAppId()
		{
			if (CBSettings.Instance.amazonAppId.Equals("CB_AMAZON_APP_ID"))
			{
				CBSettings.CredentialsWarning("CHARTBOOST: You are using the Chartboost {0} example {1}! Go to the Chartboost dashboard and replace these with an App ID & App Signature from your account! If you need help, check out answers.chartboost.com", "Amazon", "App ID");
				return "542ca35d1873da32dbc90488";
			}
			if (CBSettings.Instance.amazonAppId.Equals(string.Empty))
			{
				CBSettings.CredentialsWarning("CHARTBOOST: You are using an empty string for the {0} {1}! Go to the Chartboost dashboard and replace these with an App ID & App Signature from your account! If you need help, check out answers.chartboost.com", "Amazon", "App ID");
			}
			return CBSettings.Instance.amazonAppId;
		}

		public void SetAmazonAppSecret(string secret)
		{
			if (!CBSettings.Instance.amazonAppSecret.Equals(secret))
			{
				CBSettings.Instance.amazonAppSecret = secret;
				CBSettings.DirtyEditor();
			}
		}

		public static string getAmazonAppSecret()
		{
			if (CBSettings.Instance.amazonAppSecret.Equals("CB_AMAZON_APP_SIGNATURE"))
			{
				CBSettings.CredentialsWarning("CHARTBOOST: You are using the Chartboost {0} example {1}! Go to the Chartboost dashboard and replace these with an App ID & App Signature from your account! If you need help, check out answers.chartboost.com", "Amazon", "App Signature");
				return "90654a340386c9fb8de33315e4210d7c09989c43";
			}
			if (CBSettings.Instance.amazonAppSecret.Equals(string.Empty))
			{
				CBSettings.CredentialsWarning("CHARTBOOST: You are using an empty string for the {0} {1}! Go to the Chartboost dashboard and replace these with an App ID & App Signature from your account! If you need help, check out answers.chartboost.com", "Amazon", "App Signature");
			}
			return CBSettings.Instance.amazonAppSecret;
		}

		public static string getSelectAndroidAppId()
		{
			if (CBSettings.Instance.selectedAndroidPlatformIndex == 0)
			{
				return CBSettings.getAndroidAppId();
			}
			return CBSettings.getAmazonAppId();
		}

		public static string getSelectAndroidAppSecret()
		{
			if (CBSettings.Instance.selectedAndroidPlatformIndex == 0)
			{
				return CBSettings.getAndroidAppSecret();
			}
			return CBSettings.getAmazonAppSecret();
		}

		public static void enableLogging(bool enabled)
		{
			CBSettings.Instance.isLoggingEnabled = enabled;
			CBSettings.DirtyEditor();
		}

		public static bool isLogging()
		{
			return CBSettings.Instance.isLoggingEnabled;
		}

		private static void DirtyEditor()
		{
		}

		private static void CredentialsWarning(string warning, string platform, string field)
		{
			if (!CBSettings.credentialsWarning)
			{
				CBSettings.credentialsWarning = true;
				UnityEngine.Debug.LogWarning(string.Format(warning, platform, field));
			}
		}

		public static void resetSettings()
		{
			if (CBSettings.Instance.iOSAppId.Equals("4f21c409cd1cb2fb7000001b"))
			{
				CBSettings.Instance.SetIOSAppId("CB_IOS_APP_ID");
			}
			if (CBSettings.Instance.iOSAppSecret.Equals("92e2de2fd7070327bdeb54c15a5295309c6fcd2d"))
			{
				CBSettings.Instance.SetIOSAppSecret("CB_IOS_APP_SIGNATURE");
			}
			if (CBSettings.Instance.androidAppId.Equals("4f7b433509b6025804000002"))
			{
				CBSettings.Instance.SetAndroidAppId("CB_ANDROID_APP_ID");
			}
			if (CBSettings.Instance.androidAppSecret.Equals("dd2d41b69ac01b80f443f5b6cf06096d457f82bd"))
			{
				CBSettings.Instance.SetAndroidAppSecret("CB_ANDROID_APP_SIGNATURE");
			}
			if (CBSettings.Instance.amazonAppId.Equals("542ca35d1873da32dbc90488"))
			{
				CBSettings.Instance.SetAmazonAppId("CB_AMAZON_APP_ID");
			}
			if (CBSettings.Instance.amazonAppSecret.Equals("90654a340386c9fb8de33315e4210d7c09989c43"))
			{
				CBSettings.Instance.SetAmazonAppSecret("CB_AMAZON_APP_SIGNATURE");
			}
		}

		private const string cbSettingsAssetName = "ChartboostSettings";

		private const string cbSettingsPath = "Chartboost/Resources";

		private const string cbSettingsAssetExtension = ".asset";

		private const string iOSExampleAppIDLabel = "CB_IOS_APP_ID";

		private const string iOSExampleAppSignatureLabel = "CB_IOS_APP_SIGNATURE";

		private const string iOSExampleAppID = "4f21c409cd1cb2fb7000001b";

		private const string iOSExampleAppSignature = "92e2de2fd7070327bdeb54c15a5295309c6fcd2d";

		private const string androidExampleAppIDLabel = "CB_ANDROID_APP_ID";

		private const string androidExampleAppSignatureLabel = "CB_ANDROID_APP_SIGNATURE";

		private const string androidExampleAppID = "4f7b433509b6025804000002";

		private const string androidExampleAppSignature = "dd2d41b69ac01b80f443f5b6cf06096d457f82bd";

		private const string amazonExampleAppIDLabel = "CB_AMAZON_APP_ID";

		private const string amazonExampleAppSignatureLabel = "CB_AMAZON_APP_SIGNATURE";

		private const string amazonExampleAppID = "542ca35d1873da32dbc90488";

		private const string amazonExampleAppSignature = "90654a340386c9fb8de33315e4210d7c09989c43";

		private const string credentialsWarningDefaultFormat = "CHARTBOOST: You are using the Chartboost {0} example {1}! Go to the Chartboost dashboard and replace these with an App ID & App Signature from your account! If you need help, check out answers.chartboost.com";

		private const string credentialsWarningEmptyFormat = "CHARTBOOST: You are using an empty string for the {0} {1}! Go to the Chartboost dashboard and replace these with an App ID & App Signature from your account! If you need help, check out answers.chartboost.com";

		private const string credentialsWarningIOS = "IOS";

		private const string credentialsWarningAndroid = "Android";

		private const string credentialsWarningAmazon = "Amazon";

		private const string credentialsWarningAppID = "App ID";

		private const string credentialsWarningAppSignature = "App Signature";

		private static bool credentialsWarning;

		private static CBSettings instance;

		[SerializeField]
		public string iOSAppId = "CB_IOS_APP_ID";

		[SerializeField]
		public string iOSAppSecret = "CB_IOS_APP_SIGNATURE";

		[SerializeField]
		public string androidAppId = "CB_ANDROID_APP_ID";

		[SerializeField]
		public string androidAppSecret = "CB_ANDROID_APP_SIGNATURE";

		[SerializeField]
		public string amazonAppId = "CB_AMAZON_APP_ID";

		[SerializeField]
		public string amazonAppSecret = "CB_AMAZON_APP_SIGNATURE";

		[SerializeField]
		public bool isLoggingEnabled;

		[SerializeField]
		public string[] androidPlatformLabels = new string[]
		{
			"Google Play",
			"Amazon"
		};

		[SerializeField]
		public int selectedAndroidPlatformIndex;
	}
}
