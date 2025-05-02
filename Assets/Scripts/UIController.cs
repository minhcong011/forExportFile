// dnSpy decompiler from Assembly-CSharp.dll class: UIController
using System;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
	private void Awake()
	{
		this.refAllGameControllers = UnityEngine.Object.FindObjectOfType<AllGameControllers>();
	}

	private void Start()
	{
		Time.timeScale = 1f;
		this.gernadePressTime = Time.time;
		int @int = PlayerPrefs.GetInt("HealthCounts", 1);
		this.DisabLeAllControls();
		this.UpdateAllLabels();
		this.initialiseUI();
		this.setInitialMissionStats();
	}

	private void initialiseUI()
	{
		int selectedArmour = Singleton<GameController>.Instance.storeManager.GetSelectedArmour();
		if (selectedArmour != -1)
		{
			this.playerArmourButton.SetActive(true);
			this.hideArmourBar();
			switch (selectedArmour)
			{
			case 0:
				this.armourVal = (float)Singleton<GameController>.Instance.storeManager.armourPackItemCount[0];
				break;
			case 1:
				this.armourVal = (float)Singleton<GameController>.Instance.storeManager.armourPackItemCount[1];
				break;
			case 2:
				this.armourVal = (float)Singleton<GameController>.Instance.storeManager.armourPackItemCount[2];
				break;
			case 3:
				this.armourVal = (float)Singleton<GameController>.Instance.storeManager.armourPackItemCount[3];
				break;
			}
		}
		else
		{
			this.hideArmourBar();
		}
	}

	public void DragDown()
	{
		MonoBehaviour.print("AAhoo");
		this.count++;
		Singleton<GameController>.Instance.canTouch = true;
	}

	public void showReloadingBtn()
	{
		this.reloadingBtn.SetActive(true);
	}

	public void hideReloadingBtn()
	{
		this.reloadingBtn.SetActive(false);
	}

	public void ShowDangerAlert()
	{
		this.dangerAlert.SetActive(true);
		base.InvokeRepeating("playAlertSound", 0f, 3.5f);
	}

	public void HideDangerAlert()
	{
		this.dangerAlert.SetActive(false);
		base.CancelInvoke("playAlertSound");
	}

	private void playAlertSound()
	{
		Singleton<GameController>.Instance.soundController.PlayAnimalAlert();
	}

	public void DragUp()
	{
		if (this.count > 1)
		{
			this.count--;
		}
		else
		{
			Singleton<GameController>.Instance.canTouch = false;
			this.count = 0;
		}
	}

	public void setLoadingSliderVal(float val)
	{
	}

	public void showArmourBar()
	{
		Singleton<GameController>.Instance.gameSceneController.isArmourSelected = true;
		this.playerArmourSlider.transform.parent.gameObject.SetActive(true);
	}

	public void hideArmourBar()
	{
		this.playerArmourSlider.transform.parent.gameObject.SetActive(false);
	}

	public void showLoading()
	{
		this.loading.SetActive(true);
	}

	public void HideLoading()
	{
		this.loading.SetActive(false);
	}

	public void ShowIntroPanel()
	{
		this.introPanel.SetActive(true);
	}

	public void ShowDescriptionPanel(string text)
	{
		Time.timeScale = 1f;
		this.descriptionPanel.GetComponent<LevelDescriptionPanel>().setDescription(text);
		this.descriptionPanel.SetActive(true);
		int currentControllerId = this.refAllGameControllers.getCurrentControllerId();
		this.allControllersUI[currentControllerId].SetActive(false);
	}

	public void hideDescriptionpanel()
	{
		Time.timeScale = 1f;
		this.enableMainControls();
		int currentControllerId = this.refAllGameControllers.getCurrentControllerId();
		JoystickEnabler component = this.allControllersUI[currentControllerId].GetComponent<JoystickEnabler>();
		if (component != null)
		{
			component.enableJoystick();
		}
		Singleton<GameController>.Instance.gameSceneController.startGame();
		this.UpdateAllLabels();
		this.descriptionPanel.SetActive(false);
	}

	public void ShowLevelNumb()
	{
		this.lvlNumb.text = " #  " + Singleton<GameController>.Instance.SelectedLevel;
	}

	public void ShowWithOutIntroPanel()
	{
		this.introPanel.SetActive(false);
		this.withoutIntro.SetActive(true);
	}

	public void ButtonClicks(string method)
	{
		switch (method)
		{
		case "SkipIntro":
			this.ShowWithOutIntroPanel();
			Singleton<GameController>.Instance.gameSceneController.skipIntro();
			if (Singleton<GameController>.Instance.soundController != null)
			{
				Singleton<GameController>.Instance.soundController.PlayButtonClick();
			}
			break;
		case "FireDown":
			this.isFire = true;
			break;
		case "FireUp":
			this.isFire = false;
			break;
		case "GunChange":
			this.ChangeGun();
			break;
		case "RocketFired":
			UnityEngine.Debug.Log("skip");
			this.RocketFired(1);
			break;
		case "GernadeFired":
			if (Time.time - this.gernadePressTime > 2f)
			{
				this.checkGernadeCountTofire();
			}
			break;
		case "RocketFiredMachine":
			UnityEngine.Debug.Log("RocketFiredMachine");
			this.RocketFired(2);
			break;
		case "AirStrike":
			this.AirstrikeCalled();
			UnityEngine.Debug.Log("AirStrike");
			break;
		case "MediPackTapped":
			this.MediPackTapped();
			UnityEngine.Debug.Log("MediPackTapped");
			break;
		case "Menu":
			Singleton<GameController>.Instance.soundController.PlayButtonClick();
			UnityEngine.Debug.Log("skip");
			UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
			break;
		case "Pause":
		{
			GameSceneController gameSceneController = Singleton<GameController>.Instance.gameSceneController;
			if (gameSceneController.isGameOver || gameSceneController.isGameCompleted || !gameSceneController.isGameStarted)
			{
				return;
			}
			if (Singleton<GameController>.Instance.soundController != null)
			{
				Singleton<GameController>.Instance.soundController.PlayButtonClick();
			}
			UnityEngine.Debug.Log("Pause");
			this.ShowPauseUI();
			break;
		}
		case "ArmourPressed":
		{
			UnityEngine.Debug.Log("Armor");
			this.showArmourBar();
			this.playerArmourButton.SetActive(false);
			int selectedArmour = Singleton<GameController>.Instance.storeManager.GetSelectedArmour();
			PlayerPrefs.SetInt("armour_selected", -1);
			Singleton<GameController>.Instance.storeManager.SetSelectedArmourNone();
			Singleton<GameController>.Instance.storeManager.consumeArmour(selectedArmour);
			Singleton<GameController>.Instance.soundController.PlayStoreProduct();
			break;
		}
		}
	}

	public void checkGernadeCountTofire()
	{
		if (this.gernadesVal > 0)
		{
			this.gernadePressTime = Time.time;
			UnityEngine.Debug.Log("GernadeFired");
			this.playerAnimator.PlayGernadeAnimation();
			base.Invoke("GernadeFired", 0.5f);
			this.gernadesVal--;
		}
		this.updateGernadeCount();
	}

	private void GernadeFired()
	{
		this.playerAnimator.GetComponent<GernadeThrow>().ThroughIt();
	}

	private void ChangeGun()
	{
		if (this.gunId == 0)
		{
			this.gunId = 1;
			this.guns[0].SetActive(false);
			this.guns[1].SetActive(true);
			this.playerAnimator.enableGun(this.gunId);
		}
		else
		{
			this.gunId = 0;
			this.guns[1].SetActive(false);
			this.guns[0].SetActive(true);
			this.playerAnimator.enableGun(this.gunId);
		}
	}

	public void enableMainControls()
	{
		this.DisabLeAllControls();
		this.introPanel.SetActive(false);
		for (int i = 0; i < this.refAllGameControllers.allControllers.Length; i++)
		{
			this.refAllGameControllers.allControllers[i].gameObject.SetActive(false);
		}
		this.refAllGameControllers.setCurrentControllerId(Singleton<GameController>.Instance.gameSceneController.currentLevelMeta.controllerId);
		int currentControllerId = this.refAllGameControllers.getCurrentControllerId();
		this.allControllersUI[currentControllerId].SetActive(true);
	}

	public void DisabLeAllControls()
	{
		for (int i = 0; i < this.allControllersUI.Length; i++)
		{
			this.allControllersUI[i].SetActive(false);
		}
		this.DisabLeAllButtonClick();
	}

	public void DisableCurrentControllerUI()
	{
		int currentControllerId = this.refAllGameControllers.getCurrentControllerId();
		this.allControllersUI[currentControllerId].SetActive(false);
	}

	public void EnableCurrentControllerUI()
	{
		int currentControllerId = this.refAllGameControllers.getCurrentControllerId();
		this.allControllersUI[currentControllerId].SetActive(true);
	}

	public void ShowCurrentControllerJoystick(bool show = true)
	{
		int currentControllerId = this.refAllGameControllers.getCurrentControllerId();
		PlayerSpecificUIControls component = this.allControllersUI[currentControllerId].GetComponent<PlayerSpecificUIControls>();
		if (component != null)
		{
			component.showJoystick(show);
		}
		if (this.refNewWeapon != null)
		{
			this.refNewWeapon.ShowJoystick(show);
		}
	}

	public void DisabLeAllButtonClick()
	{
		this.ButtonClicks("FireUp");
	}

	public void RocketFired(int refId)
	{
		if (refId != 1)
		{
			if (refId == 2)
			{
				this.refAllGameControllers.fire(0);
			}
		}
		else
		{
			this.checkAvailableRockets();
		}
	}

	public void RocketFiredFormLimited()
	{
		this.checkAvailableRocketsWithInitialCount();
	}

	private void checkAvailableRocketsWithInitialCount()
	{
		int num = PlayerPrefs.GetInt("missilesCount", 1);
		int initialMissileCount = this.refAllGameControllers.getCurrentController().initialMissileCount;
		int num2 = num + initialMissileCount;
		if (num2 <= 0)
		{
			this.ShowStorePopUp(1);
		}
		else if (initialMissileCount > 0)
		{
			this.refAllGameControllers.fire(0);
			this.refAllGameControllers.getCurrentController().initialMissileCount--;
		}
		else
		{
			num--;
			if (num <= 0)
			{
				num = 0;
			}
			PlayerPrefs.SetInt("missilesCount", num);
			this.refAllGameControllers.fire(0);
		}
		this.updateLimitedMissileCount();
	}

	private void checkAvailableRockets()
	{
		int num = PlayerPrefs.GetInt("missilesCount", 1);
		if (num <= 0)
		{
			this.ShowStorePopUp(1);
		}
		else
		{
			num--;
			if (num <= 0)
			{
				num = 0;
			}
			PlayerPrefs.SetInt("missilesCount", num);
			this.refAllGameControllers.fire(0);
		}
		this.updateMissileCount();
	}

	public void changeController()
	{
		int num = this.refAllGameControllers.getCurrentControllerId();
		num++;
		this.WeaponSwitcher(num);
	}

	public void UpdateAllLabels()
	{
		this.updateMissileCount();
		this.updateAirStrikeCount();
		this.updateMediPackCount();
		this.updateLimitedMissileCount();
		this.updateGernadeCount();
		this.initialiseUI();
	}

	public void updateGernadeCount()
	{
		if (this.gernadesVal < 0)
		{
			this.gernadesVal = 0;
		}
		this.gernadesCount.text = this.gernadesVal.ToString();
	}

	public void updateMissileCount()
	{
	}

	public void updateLimitedMissileCount()
	{
		int missileCount = Singleton<GameController>.Instance.storeManager.GetMissileCount();
		int initialMissileCount = this.refAllGameControllers.getCurrentController().initialMissileCount;
		int num = missileCount + initialMissileCount;
		this.limitedMissilesCountText.text = num.ToString();
	}

	public void updateAirStrikeCount()
	{
		int airStrikeCount = Singleton<GameController>.Instance.storeManager.GetAirStrikeCount();
		this.airStrikeCountText.text = airStrikeCount.ToString();
	}

	public void updateMediPackCount()
	{
		int num = Singleton<GameController>.Instance.storeManager.GetMediPackCount();
		this.mediPackCount.text = num.ToString();
	}

	public void AirstrikeCalled()
	{
		if (Singleton<GameController>.Instance.gameSceneController.isGameOver || Singleton<GameController>.Instance.gameSceneController.isGameCompleted)
		{
			return;
		}
		int airStrikeCount = Singleton<GameController>.Instance.storeManager.GetAirStrikeCount();
		if (airStrikeCount <= 0)
		{
			this.ShowStorePopUp(2);
		}
		else
		{
			Singleton<GameController>.Instance.airStrikeController.ShowAirstrike();
		}
		this.updateAirStrikeCount();
	}

	public void MediPackTapped()
	{
		int num = Singleton<GameController>.Instance.storeManager.GetMediPackCount();
		if (num <= 0)
		{
			GameObject gameObject = Singleton<GameController>.Instance.uiPopUpManager.NotEnoughText("Not Enough Items !!!");
		}
		else
		{
			num--;
			if (num <= 0)
			{
			}
			Singleton<GameController>.Instance.gameSceneController.RefillHealth();
			Singleton<GameController>.Instance.soundController.PlayStoreProduct();
			Singleton<GameController>.Instance.storeManager.GiveMediPack(-1);
		}
		this.updateMediPackCount();
	}

	public void ShowStorePopUp(int id)
	{
		Time.timeScale = 0f;
		this.storePopUpUI.SetActive(true);
	}

	public void storePopUpCancel(int id)
	{
		Time.timeScale = 1f;
		this.storePopUpUI.SetActive(false);
	}

	public void showStore()
	{
		this.storeUI.SetActive(true);
	}

	public void hideStore()
	{
		this.UpdateAllLabels();
		this.storeUI.SetActive(false);
		Time.timeScale = 1f;
	}

	public void WeaponSwitcher(int idToSwitch)
	{
		int currentControllerId = this.refAllGameControllers.getCurrentControllerId();
		if (currentControllerId == idToSwitch)
		{
			idToSwitch = 0;
		}
		this.DisabLeAllControls();
		this.refAllGameControllers.setCurrentControllerId(idToSwitch);
		currentControllerId = this.refAllGameControllers.getCurrentControllerId();
		this.allControllersUI[currentControllerId].SetActive(true);
	}

	private void resetAllControls()
	{
	}

	public void setTime(string t, bool alert = false)
	{
		this.gameTime.text = t;
		if (alert)
		{
		}
	}

	public void setHealth(float h)
	{
		this.playerHealthSlider.GetComponent<Image>().fillAmount = h / 100f;
		if (!this.healthDown && h < 20f)
		{
			this.healthDown = true;
			base.InvokeRepeating("alarmHealth", 0f, 0.5f);
		}
		if (this.healthDown && h > 21f)
		{
			this.healthDown = false;
			base.CancelInvoke("alarmHealth");
		}
	}

	private void alarmHealth()
	{
	}

	public void showDamageEffect()
	{
		this.playerDamageEffect.Showeffect();
	}

	public void showBloodEffect()
	{
		this.playerBloodEffect.Showeffect();
	}

	public void setArmourHealth(float h)
	{
		this.playerArmourSlider.GetComponent<Image>().fillAmount = h / this.armourVal;
		if (h < 0f)
		{
			this.hideArmourBar();
		}
	}

	public void setInitialMissionStats()
	{
		this.missionStats.GetComponent<Image>().fillAmount = 0f;
		this.ShowLevelNumb();
	}

	public void setMissionStats(float h)
	{
		this.missionStats.GetComponent<Image>().fillAmount = h;
	}

	public void setMissionStatsCount(int current, int tot)
	{
		this.levelProgressCount.text = current + string.Empty;
		this.LevelProgressTotalCount.text = "/  " + tot;
	}

	public void showShotTypeText(string t)
	{
		this.shotTextBadge.transform.parent.gameObject.SetActive(true);
		this.shotTextBadge.GetComponent<Text>().text = t;
		base.CancelInvoke("disableShotText");
		base.Invoke("disableShotText", 1f);
	}

	private void disableShotText()
	{
		this.shotTextBadge.transform.parent.gameObject.SetActive(false);
	}

	private void disableBadge()
	{
		this.enemyBadge.gameObject.SetActive(false);
	}

	public void showReloadingGunFiller()
	{
		this.reloadingFiller.SetActive(true);
	}

	public void setCountOfTargets(int currentC, int totalCount)
	{
	}

	public void ShowPauseUI()
	{
		Time.timeScale = 0f;
		this.pauseUI.SetActive(true);
		UKAdsManager adsManager = Singleton<GameController>.Instance.adsManager;
		if (adsManager != null)
		{
			adsManager.showInterstitial("Pause");
		}
	}

	public void showRetryPopUp()
	{
		this.retryUI.SetActive(true);
	}

	public void showCompletePopUp()
	{
		this.DisabLeAllControls();
		this.fader.SetActive(true);
	}

	public void showFailedPopUp()
	{
		this.fader.SetActive(true);
	}

	public void ShowRunTimePopup(int id)
	{
		this.runtimePopUp.showPopUp(id);
		this.runtimePopUp.gameObject.SetActive(true);
	}

	public void ShowRunTimePopup(string msg)
	{
		this.runtimePopUp.showPopUp(msg);
		this.runtimePopUp.gameObject.SetActive(true);
	}

	public void ShowRunTimePopup(string msg, GameObject g)
	{
		this.runtimePopUp.showPopUp(msg, g);
		this.runtimePopUp.gameObject.SetActive(true);
	}

	public void ShowLevelCompleteUI()
	{
		this.DisabLeAllControls();
		this.runtimePopUp.gameObject.SetActive(false);
		this.resetAllControls();
		this.ButtonClicks("FireUp");
		GameObject gameObject = GameObject.FindGameObjectWithTag("WeaponManager");
		if (gameObject != null)
		{
			gameObject.SendMessage("fireUp", SendMessageOptions.DontRequireReceiver);
		}
		this.levelCompleteUI.SendMessage("MarkComplete", SendMessageOptions.DontRequireReceiver);
		this.levelCompleteUI.SetActive(true);
		base.GetComponent<AudioSource>().Stop();
		base.CancelInvoke("playAlertSound");
	}

	public void ShowLevelFailedUI()
	{
		this.DisabLeAllControls();
		this.runtimePopUp.gameObject.SetActive(false);
		this.ButtonClicks("FireUp");
		GameObject gameObject = GameObject.FindGameObjectWithTag("WeaponManager");
		if (gameObject != null)
		{
			gameObject.SendMessage("fireUp", SendMessageOptions.DontRequireReceiver);
		}
		this.levelFailedUI.SetActive(true);
		LevelFailedUI component = this.levelFailedUI.GetComponent<LevelFailedUI>();
		if (component != null)
		{
			component.setFailedStringAccordingToMode();
		}
		base.GetComponent<AudioSource>().Stop();
		base.CancelInvoke("playAlertSound");
	}

	public void GiveGift(int id)
	{
		Time.timeScale = 0f;
		this.GiftUI.ShowGift(id);
	}

	public void GiveGiftVideo(int id)
	{
		Time.timeScale = 0f;
		this.GiftUI.ShowGiftAfterVideo(id);
	}

	public void GiftCB()
	{
		Time.timeScale = 1f;
		this.updateUIAfterGift();
	}

	private void updateUIAfterGift()
	{
		this.UpdateAllLabels();
	}

	public void AddAmmos()
	{
		WeaponAmmoManagerCS weaponAmmoManagerCS = UnityEngine.Object.FindObjectOfType<WeaponAmmoManagerCS>();
		int totalCashCount = Singleton<GameController>.Instance.storeManager.GetTotalCashCount();
		if (weaponAmmoManagerCS != null && totalCashCount > 100)
		{
			weaponAmmoManagerCS.AddAmmos(20);
			Singleton<GameController>.Instance.storeManager.consumeCash(100);
		}
	}

	public void AlertBullets(bool alert)
	{
		if (alert)
		{
			this.AddBulletsBtn.SetActive(true);
			Singleton<GameController>.Instance.soundController.playVoice(8, null);
		}
		else
		{
			this.AddBulletsBtn.SetActive(false);
		}
	}

	public void OpenInApp()
	{
		Time.timeScale = 0f;
		this.storeUI.SetActive(true);
		this.storeUI.SendMessage("OpenInApp", true, SendMessageOptions.DontRequireReceiver);
	}

	public void MovementTapped(string direction)
	{
		PlayerMovementController playerMovementController = UnityEngine.Object.FindObjectOfType<PlayerMovementController>();
		if (playerMovementController == null)
		{
			return;
		}
		if (direction != null)
		{
			if (!(direction == "Left"))
			{
				if (direction == "Right")
				{
					playerMovementController.RightPressed();
				}
			}
			else
			{
				playerMovementController.leftPressed();
			}
		}
	}

	public void crouchTapped()
	{
		PlayerMovementController playerMovementController = UnityEngine.Object.FindObjectOfType<PlayerMovementController>();
		if (playerMovementController == null)
		{
			return;
		}
		playerMovementController.crouchPressed();
	}

	public void Update()
	{
		if (Time.timeScale == 0f)
		{
		}
		if (this.isFire)
		{
			this.refAllGameControllers.fire(1);
		}
	}

	public bool isFire;

	private float gernadePressTime;

	public Text missilesCountText;

	public Text limitedMissilesCountText;

	public Text airStrikeCountText;

	public Text mediPackCount;

	public Text gernadesCount;

	public Text levelProgressCount;

	public Text LevelProgressTotalCount;

	public Text lvlNumb;

	public GameObject loading;

	private bool isAlarm;

	public GameObject[] allControllersUI;

	public GameObject introPanel;

	private AllGameControllers refAllGameControllers;

	public GameObject playerHealthSlider;

	public GameObject playerArmourSlider;

	public GameObject playerArmourButton;

	public GameObject missionStats;

	public GameObject withoutIntro;

	public GameObject descriptionPanel;

	public GameObject levelCompleteUI;

	public GameObject levelFailedUI;

	public GameObject retryUI;

	public RunTimePopHandler runtimePopUp;

	public GameObject videoCongrats;

	public GameObject pauseUI;

	public GiftUIController GiftUI;

	public GameObject storeUI;

	public GameObject storeCanvasBlocker;

	public GameObject storePopUpUI;

	public GameObject AddBulletsBtn;

	public PlayerAnimationHandler playerAnimator;

	public GameObject[] guns;

	public int gunId;

	private float armourVal = 100f;

	public int gernadesVal = 5;

	public PlayerDamageEffect playerDamageEffect;

	public PlayerBloodEffect playerBloodEffect;

	public GameObject fader;

	public GameObject reloadingFiller;

	public GameObject reloadingBtn;

	public GameObject dangerAlert;

	public Text gameTime;

	public CanvasGroup enemyBadge;

	public CanvasGroup shotTextBadge;

	private bool healthDown;

	public NewFPSWeaponsUIController refNewWeapon;

	private int count;

	private bool alreadyBulletsAlert;
}
