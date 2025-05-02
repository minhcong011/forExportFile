// dnSpy decompiler from Assembly-CSharp.dll class: Share
using System;
using UnityEngine;

public class Share : MonoBehaviour
{
	private void Start()
	{
		this.title = Constants.GAME_NAME;
		this.content = Constants.RATE_US_LINK;
		if (Application.platform != RuntimePlatform.Android)
		{
			return;
		}
		Share.sharePluginClass = new AndroidJavaClass("com.ari.tool.UnityAndroidTool");
		if (Share.sharePluginClass == null)
		{
			UnityEngine.Debug.Log("sharePluginClass is null");
		}
		else
		{
			UnityEngine.Debug.Log("sharePluginClass is not null");
		}
		Share.unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		Share.currActivity = Share.unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
	}

	private void Update()
	{
	}

	public static void CallShare(string handline, string subject, string text)
	{
		UnityEngine.Debug.Log("share call start");
		Share.sharePluginClass.CallStatic("share", new object[]
		{
			handline,
			subject,
			text
		});
		UnityEngine.Debug.Log("share call end");
	}

	public void CallToShare()
	{
		this.title = Constants.GAME_NAME;
		this.content = Constants.RATE_US_LINK;
		Share.CallShare(this.title, "Amazing", this.content);
	}

	public string title;

	public string content;

	private static AndroidJavaClass sharePluginClass;

	private static AndroidJavaClass unityPlayer;

	private static AndroidJavaObject currActivity;
}
