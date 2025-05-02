// dnSpy decompiler from Assembly-CSharp.dll class: PromotionalSplashUIController
using System;
using UnityEngine;

public class PromotionalSplashUIController : MonoBehaviour
{
	private void Awake()
	{
		this.splashPromoItem = this.splashPromoRoot.GetComponentsInChildren<GamePromoItem>();
		if (this.gamesPromoRoot != null)
		{
			this.gamePromoItems = this.gamesPromoRoot.GetComponentsInChildren<GamePromoItem>();
		}
	}

	private void Start()
	{
		if (!this.showFeature && this.splashPromoRoot != null)
		{
			this.splashPromoRoot.SetActive(false);
		}
		if (!this.showHotGames && this.gamesPromoRoot != null)
		{
			this.gamesPromoRoot.SetActive(false);
		}
		this.refSplashScreen = Singleton<GameController>.Instance.promotionalSplashes;
		this.SetInitials();
		if (this.showFeature && this.refSplashScreen != null)
		{
			base.InvokeRepeating("ChangeFeaturedImages", 4f, 3f);
		}
	}

	private void ChangeFeaturedImages()
	{
		int index = UnityEngine.Random.Range(0, this.refSplashScreen.featuredGamesList.Count);
		this.splashPromoItem[0].SetInitials(this.refSplashScreen.featuredGamesList[index]);
	}

	private void SetInitials()
	{
		if (this.refSplashScreen == null)
		{
			return;
		}
		int num = 0;
		while (num < this.splashPromoItem.Length && num < this.refSplashScreen.featuredGamesList.Count)
		{
			this.splashPromoItem[num].SetInitials(this.refSplashScreen.featuredGamesList[num]);
			num++;
		}
		int num2 = 0;
		while (num2 < this.gamePromoItems.Length && num2 < this.refSplashScreen.hotGamesList.Count)
		{
			this.gamePromoItems[num2].SetInitials(this.refSplashScreen.hotGamesList[num2]);
			num2++;
		}
	}

	private void Update()
	{
	}

	public PromotionalSplashScreenController refSplashScreen;

	public GameObject splashPromoRoot;

	public GameObject gamesPromoRoot;

	public GamePromoItem[] gamePromoItems;

	public GamePromoItem[] splashPromoItem;

	public bool showFeature;

	public bool showHotGames;
}
