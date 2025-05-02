// dnSpy decompiler from Assembly-CSharp.dll class: PromotionalSplashScreenController
using System;
using System.Collections.Generic;
using UnityEngine;

public class PromotionalSplashScreenController : MonoBehaviour
{
	private void Awake()
	{
		if (!PromotionalSplashScreenController.isCreated)
		{
			Singleton<GameController>.Instance.promotionalSplashes = this;
			PromotionalSplashScreenController.isCreated = true;
			UnityEngine.Object.DontDestroyOnLoad(this);
		}
		else
		{
			UnityEngine.Object.DestroyImmediate(base.gameObject);
		}
	}

	[SerializeField]
	public List<GameIcons> featuredGamesList;

	public List<GameIcons> hotGamesList;

	public List<string> gamesList;

	private static bool isCreated;

	public PromotionalSplashScreenController.videoCallBack rewardedVideoCB;

	public delegate void videoCallBack(int type);
}
