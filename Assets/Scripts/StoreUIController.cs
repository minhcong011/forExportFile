// dnSpy decompiler from Assembly-CSharp.dll class: StoreUIController
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class StoreUIController : MonoBehaviour
{
	private void Awake()
	{
		if (UnityEngine.Object.FindObjectOfType<StoreManager>() == null)
		{
			base.gameObject.AddComponent<StoreManager>();
		}
		this.refStoreManager = UnityEngine.Object.FindObjectOfType<StoreManager>();
		this.SetInitialValues();
		if (this.storeScreens.Length > 0)
		{
			this.storeScreens[this.storeScreenId].SetActive(true);
		}
		if (UnityEngine.Object.FindObjectOfType<UIScreensHandler>() != null && this.continueBtn != null)
		{
			this.continueBtn.SetActive(true);
		}
	}

	private void OnEnable()
	{
		this.SetInitialValues();
		if (this.storeScreens.Length > 0)
		{
			this.storeScreens[this.storeScreenId].SetActive(true);
		}
		EventManager.UIUpdate += this.updateEventCB;
		if (Singleton<GameController>.Instance.shopOpened)
		{
			this.continueBtn.SetActive(false);
			Singleton<GameController>.Instance.shopOpened = false;
		}
		else
		{
			this.continueBtn.SetActive(true);
		}
		UKAdsManager adsManager = Singleton<GameController>.Instance.adsManager;
		if (adsManager != null)
		{
			adsManager.showInterstitial("StoreSelection");
		}
	}

	private IEnumerator alphaMove()
	{
		yield return new WaitForSeconds(0.1f);
		for (int i = 0; i < 20; i++)
		{
			this.cg.alpha += 0.06f;
			yield return new WaitForSeconds(0.025f);
		}
		yield break;
	}

	private void OnDisable()
	{
		if (this.storeScreens.Length > 0)
		{
			this.storeScreens[this.storeScreenId].SetActive(false);
			this.storeScreenId = 1;
		}
		for (int i = 0; i < this.storeScreens.Length; i++)
		{
			this.storeScreens[i].SetActive(false);
		}
		EventManager.UIUpdate -= this.updateEventCB;
	}

	public void updateTotalCoinsText()
	{
		this.totalCoins.text = this.refStoreManager.GetTotalCoinsCount().ToString();
	}

	public void updateTotalMediPacksText()
	{
		this.mediPackTotalCount.text = this.refStoreManager.GetMediPackCount().ToString();
	}

	public void updateTotalMissilePacksText()
	{
		this.missilePackTotalCount.text = this.refStoreManager.GetMissileCount().ToString();
	}

	public void updateTotalAirstrikeText()
	{
		this.airStrikePackTotalCount.text = this.refStoreManager.GetAirStrikeCount().ToString();
	}

	private void SetInitialValues()
	{
		this.updateTotalMediPacksText();
		this.updateTotalMissilePacksText();
		this.updateTotalAirstrikeText();
		for (int i = 0; i < this.selectedPanel.Length; i++)
		{
			this.selectedPanel[i].SetActive(false);
		}
		this.selectedPanel[this.storeScreenId].SetActive(true);
		this.mediPackSingleCost.text = this.refStoreManager.mediPackCoinPrice[this.mediPackIndex].ToString();
		this.mediPackSingleValue.text = this.refStoreManager.mediPackItemCount[this.mediPackIndex].ToString();
		for (int j = 0; j < this.mediItemsBig.Length; j++)
		{
			this.mediItemsBig[j].SetActive(false);
		}
		this.mediItemsBig[this.mediPackIndex].SetActive(true);
		this.setSelectedMediPack();
		this.SetInitialForArmour();
		this.inAppSingleCost.text = (float)this.refStoreManager.coinsPackPrice[this.inAppIndex] + 0.99f + " $";
		this.inAppSingleValue.text = this.refStoreManager.coinsPackItemCount[this.inAppIndex].ToString();
		for (int k = 0; k < this.inAppItemsBig.Length; k++)
		{
			this.inAppItemsBig[k].SetActive(false);
		}
		this.inAppItemsBig[this.inAppIndex].SetActive(true);
		this.setGoldSelectedItem();
	}

	private void SetInitialForArmour()
	{
		for (int i = 0; i < this.armourEquipBtns.Length; i++)
		{
			if (this.refStoreManager.isArmourPurchased(i))
			{
				this.armourEquipBtns[i].gameObject.SetActive(true);
				this.armourEquipBtns[i].sprite = this.armourEquippedTextures[0];
				this.makeEquippedTextEnable(0);
			}
		}
		int selectedArmour = this.refStoreManager.GetSelectedArmour();
		if (this.refStoreManager.isArmourPurchased(this.armourPackIndex))
		{
			if (selectedArmour != -1 && this.armourPackIndex == selectedArmour)
			{
				this.armourEquipBtnNew.gameObject.SetActive(true);
				this.armourEquipBtnNew.sprite = this.armourEquippedTextures[1];
				this.makeEquippedTextEnable(1);
			}
			else if (selectedArmour != -1)
			{
				this.armourEquipBtnNew.gameObject.SetActive(true);
				this.armourEquipBtnNew.sprite = this.armourEquippedTextures[0];
				this.makeEquippedTextEnable(0);
			}
		}
		else
		{
			this.armourEquipBtnNew.gameObject.SetActive(true);
			this.armourEquipBtnNew.sprite = this.armourEquippedTextures[2];
			this.makeEquippedTextEnable(2);
		}
		this.armourPackSingleCost.text = this.refStoreManager.armourPackCoinPrice[this.armourPackIndex].ToString();
		this.armourSingleValue.text = this.refStoreManager.armourPackItemCount[this.armourPackIndex].ToString();
		for (int j = 0; j < this.armourItemsBig.Length; j++)
		{
			this.armourItemsBig[j].SetActive(false);
		}
		this.armourItemsBig[this.armourPackIndex].SetActive(true);
		this.setSelectedArmourPack();
	}

	public void MainScreenitemClicked(int id)
	{
		this.storeScreens[this.storeScreenId].SetActive(false);
		this.previousScreenId = this.storeScreenId;
		this.storeScreenId = id;
		this.storeScreens[this.storeScreenId].SetActive(true);
		if (Singleton<GameController>.Instance.soundController != null)
		{
			Singleton<GameController>.Instance.soundController.PlayButtonClick();
		}
		for (int i = 0; i < this.selectedPanel.Length; i++)
		{
			this.selectedPanel[i].SetActive(false);
		}
		this.selectedPanel[this.storeScreenId].SetActive(true);
	}

	public void MediPackRightClicked()
	{
		UnityEngine.Debug.Log(" medi pack " + this.mediPackIndex);
		this.mediPackIndex++;
		if (this.mediPackIndex > this.refStoreManager.mediPackItemCount.Length - 1)
		{
			this.mediPackIndex = 0;
		}
		this.MediPackClicked(this.mediPackIndex);
	}

	public void MediPackLeftClicked()
	{
		this.mediPackIndex--;
		if (this.mediPackIndex < 0)
		{
			this.mediPackIndex = this.refStoreManager.mediPackItemCount.Length - 1;
		}
		this.MediPackClicked(this.mediPackIndex);
	}

	public void MediPackClicked(int id)
	{
		this.mediPackIndex = id;
		this.mediPackSingleCost.text = this.refStoreManager.mediPackCoinPrice[this.mediPackIndex].ToString();
		this.mediPackSingleValue.text = this.refStoreManager.mediPackItemCount[this.mediPackIndex].ToString();
		for (int i = 0; i < this.mediItemsBig.Length; i++)
		{
			this.mediItemsBig[i].SetActive(false);
		}
		this.mediItemsBig[this.mediPackIndex].SetActive(true);
		this.setSelectedMediPack();
		Singleton<GameController>.Instance.soundController.PlayButtonClick();
	}

	public void MediPackBuyClicked()
	{
		int totalCashCount = this.refStoreManager.GetTotalCashCount();
		int num = this.refStoreManager.mediPackCoinPrice[this.mediPackIndex];
		int quantity = this.refStoreManager.mediPackItemCount[this.mediPackIndex];
		if (num <= totalCashCount)
		{
			this.refStoreManager.consumeCash(num);
			this.refStoreManager.GiveMediPack(quantity);
			Singleton<GameController>.Instance.soundController.PlayStoreProduct();
		}
		else
		{
			this.notEnoughCoins();
		}
		Singleton<GameController>.Instance.eventManager.UpdateStoreUI();
		Singleton<GameController>.Instance.soundController.PlayButtonClick();
	}

	public void MissilePackClicked(int id)
	{
		int totalCoinsCount = this.refStoreManager.GetTotalCoinsCount();
		int num = this.refStoreManager.missilePackCoinPrice[id];
		int quantity = this.refStoreManager.missilePackItemCount[id];
		if (num <= totalCoinsCount)
		{
			this.refStoreManager.consumeCoins(num);
			this.refStoreManager.GiveMissile(quantity);
			Singleton<GameController>.Instance.soundController.PlayStoreProduct();
		}
		else
		{
			this.notEnoughCoins();
		}
		this.UpdateUI();
	}

	private void setSelectedMediPack()
	{
		for (int i = 0; i < this.mediSelectedItem.Length; i++)
		{
			this.mediSelectedItem[i].SetActive(false);
		}
		this.mediSelectedItem[this.mediPackIndex].SetActive(true);
	}

	public void AirStrikePackClicked(int id)
	{
		int totalCoinsCount = this.refStoreManager.GetTotalCoinsCount();
		int num = this.refStoreManager.airStrikePackCoinPrice[id];
		int quantity = this.refStoreManager.airStrikePackItemCount[id];
		if (num <= totalCoinsCount)
		{
			this.refStoreManager.consumeCoins(num);
			this.refStoreManager.GiveAirStrike(quantity);
			Singleton<GameController>.Instance.soundController.PlayStoreProduct();
		}
		else
		{
			this.notEnoughCoins();
		}
		this.UpdateUI();
	}

	public void ArmourRightClicked()
	{
		this.armourPackIndex++;
		if (this.armourPackIndex > 2)
		{
			this.armourPackIndex = 0;
		}
		this.ArmourPackClicked(this.armourPackIndex);
	}

	public void ArmourPackLeftClicked()
	{
		this.armourPackIndex--;
		if (this.armourPackIndex < 0)
		{
			this.armourPackIndex = 2;
		}
		this.ArmourPackClicked(this.armourPackIndex);
	}

	public void ArmourPackClicked(int id)
	{
		this.armourPackIndex = id;
		this.armourPackSingleCost.text = this.refStoreManager.armourPackCoinPrice[this.armourPackIndex].ToString();
		this.armourSingleValue.text = this.refStoreManager.armourPackItemCount[this.armourPackIndex].ToString();
		for (int i = 0; i < this.armourItemsBig.Length; i++)
		{
			this.armourItemsBig[i].SetActive(false);
		}
		this.armourItemsBig[this.armourPackIndex].SetActive(true);
		this.setSelectedArmourPack();
		int selectedArmour = this.refStoreManager.GetSelectedArmour();
		if (this.refStoreManager.isArmourPurchased(this.armourPackIndex))
		{
			if (selectedArmour == this.armourPackIndex)
			{
				this.armourEquipBtnNew.gameObject.SetActive(true);
				this.armourEquipBtnNew.sprite = this.armourEquippedTextures[1];
				this.makeEquippedTextEnable(1);
			}
			else
			{
				this.armourEquipBtnNew.gameObject.SetActive(true);
				this.armourEquipBtnNew.sprite = this.armourEquippedTextures[0];
				this.makeEquippedTextEnable(0);
			}
		}
		else
		{
			this.armourEquipBtnNew.gameObject.SetActive(true);
			this.armourEquipBtnNew.sprite = this.armourEquippedTextures[2];
			this.makeEquippedTextEnable(2);
		}
		Singleton<GameController>.Instance.soundController.PlayButtonClick();
	}

	public void ArmourPackBuyClicked(int id)
	{
		int selectedArmour = this.refStoreManager.GetSelectedArmour();
		if (selectedArmour == this.armourPackIndex)
		{
			return;
		}
		if (this.refStoreManager.isArmourPurchased(this.armourPackIndex))
		{
			if (selectedArmour != -1)
			{
				this.armourEquipBtns[selectedArmour].sprite = this.armourEquippedTextures[0];
				this.makeEquippedTextEnable(0);
			}
			this.armourEquipBtnNew.gameObject.SetActive(true);
			this.armourEquipBtnNew.sprite = this.armourEquippedTextures[1];
			this.makeEquippedTextEnable(1);
			this.refStoreManager.SetSelectedArmour(this.armourPackIndex);
			return;
		}
		int totalCashCount = this.refStoreManager.GetTotalCashCount();
		int num = this.refStoreManager.armourPackCoinPrice[this.armourPackIndex];
		int num2 = this.refStoreManager.armourPackItemCount[this.armourPackIndex];
		if (num <= totalCashCount)
		{
			this.refStoreManager.consumeCash(num);
			this.refStoreManager.setArmourPurchased(this.armourPackIndex);
			if (this.refStoreManager.isArmourPurchased(this.armourPackIndex))
			{
				this.armourEquipBtns[id].gameObject.SetActive(true);
				this.armourEquipBtns[id].sprite = this.armourEquippedTextures[1];
				this.makeEquippedTextEnable(1);
				if (selectedArmour != -1)
				{
					this.armourEquipBtns[selectedArmour].sprite = this.armourEquippedTextures[0];
					this.makeEquippedTextEnable(0);
				}
				this.armourEquipBtnNew.gameObject.SetActive(true);
				this.armourEquipBtnNew.sprite = this.armourEquippedTextures[1];
				this.makeEquippedTextEnable(1);
				this.refStoreManager.SetSelectedArmour(this.armourPackIndex);
				Singleton<GameController>.Instance.soundController.PlayStoreProduct();
				this.UpdateUI();
				Singleton<GameController>.Instance.eventManager.UpdateStoreUI();
				return;
			}
		}
		else
		{
			this.notEnoughCoins();
		}
		this.UpdateUI();
		Singleton<GameController>.Instance.eventManager.UpdateStoreUI();
		Singleton<GameController>.Instance.soundController.PlayButtonClick();
	}

	private void makeEquippedTextEnable(int numb)
	{
		for (int i = 0; i < this.armourEquppedText.Length; i++)
		{
			this.armourEquppedText[i].SetActive(false);
		}
		this.armourEquppedText[numb].SetActive(true);
	}

	private void setSelectedArmourPack()
	{
		for (int i = 0; i < this.armourSelectedItem.Length; i++)
		{
			this.armourSelectedItem[i].SetActive(false);
		}
		this.armourSelectedItem[this.armourPackIndex].SetActive(true);
	}

	public void notEnoughCoins()
	{
		this.InstantiateNotEnoughPanel();
	}

	public void InstantiateNotEnoughPanel()
	{
		GameObject gameObject = UnityEngine.Object.Instantiate(Resources.Load("NotEnoughCashUI")) as GameObject;
		gameObject.transform.SetParent(base.gameObject.transform.parent);
		gameObject.GetComponent<RectTransform>().offsetMax = new Vector2(0f, 0f);
		gameObject.GetComponent<RectTransform>().offsetMin = new Vector2(0f, 0f);
		gameObject.GetComponent<RectTransform>().anchoredPosition3D = Vector3.zero;
		gameObject.transform.localScale = Vector3.one;
	}

	public void UpdateUI()
	{
		this.updateTotalMediPacksText();
		this.updateTotalMissilePacksText();
		this.updateTotalAirstrikeText();
	}

	public void StoreClicked()
	{
		this.storeScreens[this.storeScreenId].SetActive(false);
		this.previousScreenId = this.storeScreenId;
		this.storeScreenId = 4;
		this.storeScreens[this.storeScreenId].SetActive(true);
		UIScreensHandler uiscreensHandler = UnityEngine.Object.FindObjectOfType<UIScreensHandler>();
		if (uiscreensHandler != null)
		{
			uiscreensHandler.setScreen(11);
			Singleton<GameController>.Instance.soundController.PlayButtonClick();
		}
	}

	private void updateEventCB()
	{
		this.UpdateUI();
	}

	public void BackClicked()
	{
		if (Singleton<GameController>.Instance.soundController)
		{
			Singleton<GameController>.Instance.soundController.PlayButtonClick();
		}
		switch (this.storeScreenId)
		{
		case 0:
		case 1:
		case 2:
		case 3:
		case 4:
		case 6:
		{
			base.gameObject.SetActive(false);
			UIScreensHandler uiscreensHandler = UnityEngine.Object.FindObjectOfType<UIScreensHandler>();
			if (uiscreensHandler != null)
			{
				uiscreensHandler.setScreen(0);
			}
			if (this.checkForTimescale)
			{
				Time.timeScale = 1f;
				this.checkForTimescale = false;
			}
			break;
		}
		case 5:
			this.storeScreens[this.storeScreenId].SetActive(false);
			this.storeScreenId = this.previousScreenId;
			this.storeScreens[this.storeScreenId].SetActive(true);
			break;
		case 7:
			this.storeScreens[this.storeScreenId].SetActive(false);
			this.storeScreenId = 0;
			this.storeScreens[this.storeScreenId].SetActive(true);
			break;
		}
	}

	public void ContinuePressed()
	{
		if (this.isGamePlay)
		{
			this.BackClicked();
			return;
		}
		Singleton<GameController>.Instance.soundController.PlayButtonClick();
		this.startClicked();
	}

	public void startClicked()
	{
		Singleton<GameController>.Instance.soundController.PlayButtonClick();
		if (this.loadingGamePlay != null)
		{
			this.loadingGamePlay.LoadScene();
		}
	}

	public void OpenInApp(bool checkTime = false)
	{
		this.notEnoughCoins();
		if (checkTime)
		{
			this.checkForTimescale = true;
		}
	}

	public void InAppClicked(int id)
	{
		if (this.inAppIndex == id)
		{
			return;
		}
		this.inAppIndex = id;
		this.inAppSingleCost.text = (float)this.refStoreManager.coinsPackPrice[this.inAppIndex] + 0.99f + " $";
		this.inAppSingleValue.text = this.refStoreManager.coinsPackItemCount[this.inAppIndex].ToString();
		for (int i = 0; i < this.inAppItemsBig.Length; i++)
		{
			this.inAppItemsBig[i].SetActive(false);
		}
		this.inAppItemsBig[this.inAppIndex].SetActive(true);
		this.setGoldSelectedItem();
		Singleton<GameController>.Instance.soundController.PlayButtonClick();
	}

	private void setGoldSelectedItem()
	{
		for (int i = 0; i < this.goldPacksSelected.Length; i++)
		{
			this.goldPacksSelected[i].SetActive(false);
		}
		this.goldPacksSelected[this.inAppIndex].SetActive(true);
	}

	public void inAppBuyClicked()
	{
		OpenIABPurchaseHandler openIABPurchaseHandler = UnityEngine.Object.FindObjectOfType<OpenIABPurchaseHandler>();
		switch (this.inAppIndex)
		{
		case 0:
			if (openIABPurchaseHandler != null)
			{
				openIABPurchaseHandler.PurchasePressed(0);
			}
			break;
		case 1:
			if (openIABPurchaseHandler != null)
			{
				openIABPurchaseHandler.PurchasePressed(1);
			}
			break;
		case 2:
			if (openIABPurchaseHandler != null)
			{
				openIABPurchaseHandler.PurchasePressed(2);
			}
			break;
		case 3:
			if (openIABPurchaseHandler != null)
			{
				openIABPurchaseHandler.PurchasePressed(3);
			}
			break;
		}
		Singleton<GameController>.Instance.soundController.PlayButtonClick();
	}

	private void Update()
	{
		if (UnityEngine.Input.GetKeyDown(KeyCode.Escape))
		{
			this.BackClicked();
		}
	}

	public GameObject[] storeScreens;

	public Text totalCoins;

	public Text mediPackTotalCount;

	public Text mediPackSingleCost;

	public Text mediPackSingleValue;

	public GameObject[] mediItemsBig;

	public GameObject[] mediSelectedItem;

	private int mediPackIndex;

	public Text missilePackTotalCount;

	public Text airStrikePackTotalCount;

	public Sprite[] armourEquippedTextures;

	public GameObject[] armourEquppedText;

	public Image[] armourEquipBtns;

	public Image armourEquipBtnNew;

	public Text armourPackSingleCost;

	public Text armourSingleValue;

	public GameObject[] armourItemsBig;

	public GameObject[] armourSelectedItem;

	private int armourPackIndex;

	public Text inAppSingleCost;

	public Text inAppSingleValue;

	public GameObject[] inAppItemsBig;

	private int inAppIndex;

	public GameObject[] goldPacksSelected;

	public GameObject[] selectedPanel;

	public int storeScreenId;

	private int previousScreenId;

	[HideInInspector]
	public StoreManager refStoreManager;

	public GameObject continueBtn;

	private bool checkForTimescale;

	public CanvasGroup cg;

	public bool isGamePlay;

	public GameObject backBtn;

	public LoadingGamePlay loadingGamePlay;
}
