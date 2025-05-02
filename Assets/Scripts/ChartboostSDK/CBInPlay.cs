// dnSpy decompiler from Assembly-CSharp.dll class: ChartboostSDK.CBInPlay
using System;
using UnityEngine;

namespace ChartboostSDK
{
	public class CBInPlay
	{
		public CBInPlay(AndroidJavaObject inPlayAd, AndroidJavaObject plugin)
		{
			this.androidInPlayAd = inPlayAd;
			this.appName = this.androidInPlayAd.Call<string>("getAppName", new object[0]);
			string s = plugin.Call<string>("getBitmapAsString", new object[]
			{
				this.androidInPlayAd.Call<AndroidJavaObject>("getAppIcon", new object[0])
			});
			this.appIcon = new Texture2D(4, 4);
			this.appIcon.LoadImage(Convert.FromBase64String(s));
		}

		public void show()
		{
			this.androidInPlayAd.Call("show", new object[0]);
		}

		public void click()
		{
			this.androidInPlayAd.Call("click", new object[0]);
		}

		~CBInPlay()
		{
		}

		public Texture2D appIcon;

		public string appName;

		private IntPtr inPlayUniqueId;

		private AndroidJavaObject androidInPlayAd;
	}
}
