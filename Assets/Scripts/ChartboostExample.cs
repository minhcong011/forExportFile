// dnSpy decompiler from Assembly-CSharp.dll class: ChartboostExample
using System;
using System.Collections.Generic;
using ChartboostSDK;
using UnityEngine;

public class ChartboostExample : MonoBehaviour
{
	private void OnEnable()
	{
		this.SetupDelegates();
	}

	private void Start()
	{
		this.delegateHistory = new List<string>();
		Chartboost.setAutoCacheAds(this.autocache);
		this.AddLog("Is Initialized: " + Chartboost.isInitialized());
	}

	private void SetupDelegates()
	{
		Chartboost.didInitialize += this.didInitialize;
		Chartboost.didFailToLoadInterstitial += this.didFailToLoadInterstitial;
		Chartboost.didDismissInterstitial += this.didDismissInterstitial;
		Chartboost.didCloseInterstitial += this.didCloseInterstitial;
		Chartboost.didClickInterstitial += this.didClickInterstitial;
		Chartboost.didCacheInterstitial += this.didCacheInterstitial;
		Chartboost.shouldDisplayInterstitial += this.shouldDisplayInterstitial;
		Chartboost.didDisplayInterstitial += this.didDisplayInterstitial;
		Chartboost.didFailToRecordClick += this.didFailToRecordClick;
		Chartboost.didFailToLoadRewardedVideo += this.didFailToLoadRewardedVideo;
		Chartboost.didDismissRewardedVideo += this.didDismissRewardedVideo;
		Chartboost.didCloseRewardedVideo += this.didCloseRewardedVideo;
		Chartboost.didClickRewardedVideo += this.didClickRewardedVideo;
		Chartboost.didCacheRewardedVideo += this.didCacheRewardedVideo;
		Chartboost.shouldDisplayRewardedVideo += this.shouldDisplayRewardedVideo;
		Chartboost.didCompleteRewardedVideo += this.didCompleteRewardedVideo;
		Chartboost.didDisplayRewardedVideo += this.didDisplayRewardedVideo;
		Chartboost.didPauseClickForConfirmation += this.didPauseClickForConfirmation;
		Chartboost.willDisplayVideo += this.willDisplayVideo;
	}

	private void Update()
	{
		this.UpdateScrolling();
		this.frameCount++;
		if (this.frameCount > 30)
		{
			this.hasInterstitial = Chartboost.hasInterstitial(CBLocation.Default);
			this.hasRewardedVideo = Chartboost.hasRewardedVideo(CBLocation.Default);
			this.frameCount = 0;
		}
	}

	private void UpdateScrolling()
	{
		if (UnityEngine.Input.touchCount != 1)
		{
			return;
		}
		Touch touch = Input.touches[0];
		if (touch.phase == TouchPhase.Began)
		{
			this.beginFinger = touch.position;
			this.beginPanel = this.scrollPosition;
		}
		if (touch.phase == TouchPhase.Moved)
		{
			this.deltaFingerY = touch.position.y - this.beginFinger.y;
			float y = this.beginPanel.y + this.deltaFingerY / this.scale;
			this.latestPanel = this.beginPanel;
			this.latestPanel.y = y;
			this.scrollPosition = this.latestPanel;
		}
	}

	private void AddLog(string text)
	{
		UnityEngine.Debug.Log(text);
		this.delegateHistory.Insert(0, text + "\n");
		int count = this.delegateHistory.Count;
		if (count > 20)
		{
			this.delegateHistory.RemoveRange(20, count - 20);
		}
	}

	private void OnGUI()
	{
		float num = (float)Screen.width;
		float num2 = (float)Screen.height;
		float a = num / 240f;
		float b = num2 / 210f;
		float num3 = Mathf.Min(6f, Mathf.Min(a, b));
		if (this.scale != num3)
		{
			this.scale = num3;
			this.guiScale = new Vector3(this.scale, this.scale, 1f);
		}
		GUI.matrix = Matrix4x4.Scale(this.guiScale);
		this.ELEMENT_WIDTH = (int)(num / this.scale) - 30;
		float height = (float)this.REQUIRED_HEIGHT;
		this.scrollRect = new Rect(0f, (float)this.BANNER_HEIGHT, (float)(this.ELEMENT_WIDTH + 30), num2 / this.scale - (float)this.BANNER_HEIGHT);
		this.scrollArea = new Rect(-10f, (float)this.BANNER_HEIGHT, (float)this.ELEMENT_WIDTH, height);
		this.LayoutHeader();
		if (this.activeAgeGate)
		{
			GUI.ModalWindow(1, new Rect(0f, 0f, (float)Screen.width, (float)Screen.height), new GUI.WindowFunction(this.LayoutAgeGate), "Age Gate");
			return;
		}
		this.scrollPosition = GUI.BeginScrollView(this.scrollRect, this.scrollPosition, this.scrollArea);
		this.LayoutButtons();
		this.LayoutToggles();
		GUI.EndScrollView();
	}

	private void LayoutHeader()
	{
		GUILayout.Label(this.logo, new GUILayoutOption[]
		{
			GUILayout.Height(30f),
			GUILayout.Width((float)(this.ELEMENT_WIDTH + 20))
		});
		string text = string.Empty;
		foreach (string str in this.delegateHistory)
		{
			text += str;
		}
		GUILayout.TextArea(text, new GUILayoutOption[]
		{
			GUILayout.Height(70f),
			GUILayout.Width((float)(this.ELEMENT_WIDTH + 20))
		});
	}

	private void LayoutToggles()
	{
		GUILayout.Space(5f);
		GUILayout.Label("Options:", new GUILayoutOption[0]);
		this.showInterstitial = GUILayout.Toggle(this.showInterstitial, "Should Display Interstitial", new GUILayoutOption[0]);
		this.showRewardedVideo = GUILayout.Toggle(this.showRewardedVideo, "Should Display Rewarded Video", new GUILayoutOption[0]);
		if (GUILayout.Toggle(this.ageGate, "Should Pause for AgeGate", new GUILayoutOption[0]) != this.ageGate)
		{
			this.ageGate = !this.ageGate;
			Chartboost.setShouldPauseClickForConfirmation(this.ageGate);
		}
		if (GUILayout.Toggle(this.autocache, "Auto cache ads", new GUILayoutOption[0]) != this.autocache)
		{
			this.autocache = !this.autocache;
			Chartboost.setAutoCacheAds(this.autocache);
		}
	}

	private void LayoutButtons()
	{
		GUILayout.Space(5f);
		GUILayout.Label("Has Interstitial: " + this.hasInterstitial, new GUILayoutOption[0]);
		if (GUILayout.Button("Cache Interstitial", new GUILayoutOption[]
		{
			GUILayout.Width((float)this.ELEMENT_WIDTH)
		}))
		{
			Chartboost.cacheInterstitial(CBLocation.Default);
		}
		if (GUILayout.Button("Show Interstitial", new GUILayoutOption[]
		{
			GUILayout.Width((float)this.ELEMENT_WIDTH)
		}))
		{
			Chartboost.showInterstitial(CBLocation.Default);
		}
		GUILayout.Space(5f);
		GUILayout.Label("Has Rewarded Video: " + this.hasRewardedVideo, new GUILayoutOption[0]);
		if (GUILayout.Button("Cache Rewarded Video", new GUILayoutOption[]
		{
			GUILayout.Width((float)this.ELEMENT_WIDTH)
		}))
		{
			Chartboost.cacheRewardedVideo(CBLocation.Default);
		}
		if (GUILayout.Button("Show Rewarded Video", new GUILayoutOption[]
		{
			GUILayout.Width((float)this.ELEMENT_WIDTH)
		}))
		{
			Chartboost.showRewardedVideo(CBLocation.Default);
		}
		GUILayout.Space(5f);
		GUILayout.Label("Post install events:", new GUILayoutOption[0]);
		if (GUILayout.Button("Send PIA Main Level Event", new GUILayoutOption[]
		{
			GUILayout.Width((float)this.ELEMENT_WIDTH)
		}))
		{
			Chartboost.trackLevelInfo("Test Data", CBLevelType.HIGHEST_LEVEL_REACHED, 1, "Test Send mail level Information");
		}
		if (GUILayout.Button("Send PIA Sub Level Event", new GUILayoutOption[]
		{
			GUILayout.Width((float)this.ELEMENT_WIDTH)
		}))
		{
			Chartboost.trackLevelInfo("Test Data", CBLevelType.HIGHEST_LEVEL_REACHED, 1, 2, "Test Send sub level Information");
		}
		if (GUILayout.Button("Track IAP", new GUILayoutOption[]
		{
			GUILayout.Width((float)this.ELEMENT_WIDTH)
		}))
		{
			this.TrackIAP();
		}
	}

	private void LayoutAgeGate(int windowID)
	{
		GUILayout.Space((float)this.BANNER_HEIGHT);
		GUILayout.Label("Want to pass the age gate?", new GUILayoutOption[0]);
		GUILayout.BeginHorizontal(new GUILayoutOption[]
		{
			GUILayout.Width((float)this.ELEMENT_WIDTH)
		});
		if (GUILayout.Button("YES", new GUILayoutOption[0]))
		{
			Chartboost.didPassAgeGate(true);
			this.activeAgeGate = false;
		}
		if (GUILayout.Button("NO", new GUILayoutOption[0]))
		{
			Chartboost.didPassAgeGate(false);
			this.activeAgeGate = false;
		}
		GUILayout.EndHorizontal();
	}

	private void OnDisable()
	{
		Chartboost.didInitialize -= this.didInitialize;
		Chartboost.didFailToLoadInterstitial -= this.didFailToLoadInterstitial;
		Chartboost.didDismissInterstitial -= this.didDismissInterstitial;
		Chartboost.didCloseInterstitial -= this.didCloseInterstitial;
		Chartboost.didClickInterstitial -= this.didClickInterstitial;
		Chartboost.didCacheInterstitial -= this.didCacheInterstitial;
		Chartboost.shouldDisplayInterstitial -= this.shouldDisplayInterstitial;
		Chartboost.didDisplayInterstitial -= this.didDisplayInterstitial;
		Chartboost.didFailToRecordClick -= this.didFailToRecordClick;
		Chartboost.didFailToLoadRewardedVideo -= this.didFailToLoadRewardedVideo;
		Chartboost.didDismissRewardedVideo -= this.didDismissRewardedVideo;
		Chartboost.didCloseRewardedVideo -= this.didCloseRewardedVideo;
		Chartboost.didClickRewardedVideo -= this.didClickRewardedVideo;
		Chartboost.didCacheRewardedVideo -= this.didCacheRewardedVideo;
		Chartboost.shouldDisplayRewardedVideo -= this.shouldDisplayRewardedVideo;
		Chartboost.didCompleteRewardedVideo -= this.didCompleteRewardedVideo;
		Chartboost.didDisplayRewardedVideo -= this.didDisplayRewardedVideo;
		Chartboost.didPauseClickForConfirmation -= this.didPauseClickForConfirmation;
		Chartboost.willDisplayVideo -= this.willDisplayVideo;
	}

	private void didInitialize(bool status)
	{
		this.AddLog(string.Format("didInitialize: {0}", status));
	}

	private void didFailToLoadInterstitial(CBLocation location, CBImpressionError error)
	{
		this.AddLog(string.Format("didFailToLoadInterstitial: {0} at location {1}", error, location));
	}

	private void didDismissInterstitial(CBLocation location)
	{
		this.AddLog("didDismissInterstitial: " + location);
	}

	private void didCloseInterstitial(CBLocation location)
	{
		this.AddLog("didCloseInterstitial: " + location);
	}

	private void didClickInterstitial(CBLocation location)
	{
		this.AddLog("didClickInterstitial: " + location);
	}

	private void didCacheInterstitial(CBLocation location)
	{
		this.AddLog("didCacheInterstitial: " + location);
	}

	private bool shouldDisplayInterstitial(CBLocation location)
	{
		this.AddLog(string.Concat(new object[]
		{
			"shouldDisplayInterstitial @",
			location,
			" : ",
			this.showInterstitial
		}));
		return this.showInterstitial;
	}

	private void didDisplayInterstitial(CBLocation location)
	{
		this.AddLog("didDisplayInterstitial: " + location);
	}

	private void didFailToRecordClick(CBLocation location, CBClickError error)
	{
		this.AddLog(string.Format("didFailToRecordClick: {0} at location: {1}", error, location));
	}

	private void didFailToLoadRewardedVideo(CBLocation location, CBImpressionError error)
	{
		this.AddLog(string.Format("didFailToLoadRewardedVideo: {0} at location {1}", error, location));
	}

	private void didDismissRewardedVideo(CBLocation location)
	{
		this.AddLog("didDismissRewardedVideo: " + location);
	}

	private void didCloseRewardedVideo(CBLocation location)
	{
		this.AddLog("didCloseRewardedVideo: " + location);
	}

	private void didClickRewardedVideo(CBLocation location)
	{
		this.AddLog("didClickRewardedVideo: " + location);
	}

	private void didCacheRewardedVideo(CBLocation location)
	{
		this.AddLog("didCacheRewardedVideo: " + location);
	}

	private bool shouldDisplayRewardedVideo(CBLocation location)
	{
		this.AddLog(string.Concat(new object[]
		{
			"shouldDisplayRewardedVideo @",
			location,
			" : ",
			this.showRewardedVideo
		}));
		return this.showRewardedVideo;
	}

	private void didCompleteRewardedVideo(CBLocation location, int reward)
	{
		this.AddLog(string.Format("didCompleteRewardedVideo: reward {0} at location {1}", reward, location));
	}

	private void didDisplayRewardedVideo(CBLocation location)
	{
		this.AddLog("didDisplayRewardedVideo: " + location);
	}

	private void didPauseClickForConfirmation()
	{
	}

	private void willDisplayVideo(CBLocation location)
	{
		this.AddLog("willDisplayVideo: " + location);
	}

	private void TrackIAP()
	{
		UnityEngine.Debug.Log("TrackIAP");
		Chartboost.trackInAppGooglePlayPurchaseEvent("SampleItem", "TestPurchase", "0.99", "USD", "ProductID", "PurchaseData", "PurchaseSignature");
	}

	public Texture2D logo;

	public Vector2 scrollPosition = Vector2.zero;

	private List<string> delegateHistory;

	private bool hasInterstitial;

	private bool hasRewardedVideo;

	private int frameCount;

	private bool ageGate;

	private bool autocache = true;

	private bool activeAgeGate;

	private bool showInterstitial = true;

	private bool showRewardedVideo = true;

	private int BANNER_HEIGHT = 110;

	private int REQUIRED_HEIGHT = 650;

	private int ELEMENT_WIDTH = 190;

	private Rect scrollRect;

	private Rect scrollArea;

	private Vector3 guiScale;

	private float scale;

	private Vector2 beginFinger;

	private float deltaFingerY;

	private Vector2 beginPanel;

	private Vector2 latestPanel;
}
