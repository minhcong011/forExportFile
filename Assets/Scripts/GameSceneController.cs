// dnSpy decompiler from Assembly-CSharp.dll class: GameSceneController
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneController : MonoBehaviour
{
	public IEnumerator LoadLightMap(int index)
	{
		yield return new WaitForSeconds(0.5f);
		int totalMaps = 8;
		if (index == 1)
		{
			totalMaps = 8;
		}
		LightmapData[] lightmapData = new LightmapData[totalMaps];
		UnityEngine.Debug.Log("Coming");
		for (int i = 0; i < totalMaps; i++)
		{
			lightmapData[i] = new LightmapData();
			lightmapData[i].lightmapColor = (Resources.Load(string.Concat(new object[]
			{
				"LightMaps/Level",
				index,
				"/Lightmap-",
				i,
				"_comp_light"
			}), typeof(Texture2D)) as Texture2D);
		}
		LightmapSettings.lightmaps = lightmapData;
		yield break;
	}

	private void Awake()
	{
		this.uiController = UnityEngine.Object.FindObjectOfType<UIController>();
		Singleton<GameController>.Instance.gameSceneController = this;
		this.selectedMission = Singleton<GameController>.Instance.SelectedMission;
		this.selectedlevel = Singleton<GameController>.Instance.SelectedLevel;
		this.currentLevelMeta = UnityEngine.Object.FindObjectOfType<MissionManager>().missionOrganisersArray[this.selectedMission - 1].levelsArray[this.selectedlevel - 1];
		StoreManager x = UnityEngine.Object.FindObjectOfType<StoreManager>();
		if (x == null)
		{
			this.DummyManager.AddComponent<StoreManager>();
		}
		ScoreManager x2 = UnityEngine.Object.FindObjectOfType<ScoreManager>();
		if (x2 == null)
		{
			this.DummyManager.AddComponent<ScoreManager>();
		}
		if (this.selectedMission == 1)
		{
			if (this.selectedlevel <= 4)
			{
				this.sceneval = "Scene_1";
			}
			else
			{
				this.sceneval = "Scene_2";
			}
		}
		else if (Singleton<GameController>.Instance.SelectedMission == 2)
		{
			this.sceneval = "Scene_2";
		}
		else
		{
			this.sceneval = "Scene_2";
		}
		this.sceneControllerSource = base.GetComponent<AudioSource>();
	}

	private IEnumerator LoadScene()
	{
		yield return new WaitForSeconds(0.4f);
		SceneManager.LoadScene(this.sceneval, LoadSceneMode.Additive);
		yield return new WaitForSeconds(1.2f);
		SceneManager.SetActiveScene(SceneManager.GetSceneByName(this.sceneval));
		yield break;
	}

	private void Start()
	{
		this.uiController.showLoading();
		if (Singleton<GameController>.Instance.soundController != null)
		{
			Singleton<GameController>.Instance.soundController.PlayInGameMusic();
		}
		this.currentLevelMeta.gameObject.SetActive(true);
		this.totalEnemiesToKill = this.currentLevelMeta.locationWavesMeta.totalLevelEnemies;
		this.totalTime = this.currentLevelMeta.levelTime;
		this.isIntroNeeded = this.currentLevelMeta.isIntroNeeded;
		Singleton<GameController>.Instance.airStrikeController = this.currentLevelMeta.airStrikeController;
		int selectedArmour = Singleton<GameController>.Instance.storeManager.GetSelectedArmour();
		if (selectedArmour != -1)
		{
			switch (selectedArmour)
			{
			case 0:
				this.armourHealth = 100f;
				break;
			case 1:
				this.armourHealth = 120f;
				break;
			case 2:
				this.armourHealth = 140f;
				break;
			case 3:
				this.armourHealth = 160f;
				break;
			}
		}
		this.missionLocationProgress = new MissionLocationTask(this.currentLevelMeta.locationWavesMeta, false);
		this.missionLocationTasks = new MissionLocationTask(this.currentLevelMeta.locationWavesMeta, true);
		this.uiController.setLoadingSliderVal(1f);
		base.StartCoroutine(this.SetInitialParameters());
		UKAdsManager adsManager = Singleton<GameController>.Instance.adsManager;
		if (adsManager != null)
		{
			adsManager.HideBanner("AdMob");
		}
		this.lastShakeTime = Time.time;
		this.uiController.setMissionStatsCount(0, this.currentLevelMeta.locationWavesMeta.totalLevelEnemies);
		base.StartCoroutine(this.LoadScene());
	}

	private IEnumerator SetInitialParameters()
	{
		yield return new WaitForSeconds(0.4f);
		if (!this.isIntroNeeded)
		{
			this.setPlayer();
		}
		this.setGunsParameters();
		yield return new WaitForSeconds(0.4f);
		this.currentLevelMeta.InitEnemies();
		yield return new WaitForSeconds(0.2f);
		this.showIntro();
		yield return new WaitForSeconds(1.4f);
		yield return new WaitForSeconds(0.1f);
		this.uiController.setLoadingSliderVal(1f);
		yield return new WaitForSeconds(0.6f);
		this.uiController.HideLoading();
		if (!this.introInProgress)
		{
			this.skipIntro();
			this.uiController.ShowWithOutIntroPanel();
			if (Singleton<GameController>.Instance.ukGameDataLoader != null)
			{
				LevelFromInspector levelFromInspector = Singleton<GameController>.Instance.ukGameDataLoader.GetMissionWithIndex(Singleton<GameController>.Instance.SelectedMission).GetLevelFromInspector(this.selectedlevel);
				if (levelFromInspector != null)
				{
					this.uiController.ShowDescriptionPanel(levelFromInspector.lvlBriefings);
				}
				else
				{
					this.uiController.ShowDescriptionPanel(this.currentLevelMeta.levelDescription);
				}
			}
		}
		yield return new WaitForSeconds(1f);
		yield break;
	}

	private void setPlayer()
	{
		int controllerId = this.currentLevelMeta.controllerId;
		AllGameControllers allGameControllers = UnityEngine.Object.FindObjectOfType<AllGameControllers>();
		if (allGameControllers != null)
		{
			allGameControllers.setCurrentControllerId(controllerId);
		}
		this.uiController.ShowCurrentControllerJoystick(this.currentLevelMeta.isControllerMoveable);
		this.currentLevelMeta.SetPlayerPosition();
	}

	public void setGunsParameters()
	{
		AllGameControllers allGameControllers = UnityEngine.Object.FindObjectOfType<AllGameControllers>();
		weaponselector component = allGameControllers.getCurrentController().GetComponent<weaponselector>();
		if (component != null)
		{
			if (this.currentLevelMeta.gun.gunItems.Count > 0)
			{
				List<GunItem> gunItems = this.currentLevelMeta.gun.gunItems;
				component.SetDefaultGuns(gunItems);
			}
			else
			{
				component.setUnlockedGuns();
			}
		}
	}

	public void showIntro()
	{
		if (this.isIntroNeeded)
		{
			this.introInProgress = true;
			this.introSceneObject = this.currentLevelMeta.IntroScene;
			this.introSceneObject.SetActive(true);
		}
	}

	public void skipIntro()
	{
		this.introInProgress = false;
		this.camSwitcher.SendMessage("DoFade", SendMessageOptions.DontRequireReceiver);
		Stage stageWithMissionandStageId = Singleton<MissionReader>.Instance.getStageWithMissionandStageId(this.selectedMission, this.selectedlevel);
		if (stageWithMissionandStageId != null)
		{
			this.uiController.ShowDescriptionPanel(stageWithMissionandStageId.stageBriefings);
		}
		else
		{
			this.uiController.ShowDescriptionPanel(this.currentLevelMeta.levelDescription);
		}
		base.Invoke("DestroyIntroScene", 1.5f);
	}

	public void DestroyIntroScene()
	{
		if (this.introSceneObject != null)
		{
			UnityEngine.Object.Destroy(this.introSceneObject);
		}
	}

	public void startGame()
	{
		this.startTime = Time.time;
		base.Invoke("startTouches", 0.3f);
		base.InvokeRepeating("showTime", 0.5f, 0.4f);
		if (Constants.isMusicOn())
		{
			base.GetComponent<AudioSource>().Play();
		}
		int num = UnityEngine.Random.Range(3, 7);
		base.StartCoroutine(this.checkTutorial());
		this.DestroyIntroScene();
		GameObject gameObject = GameObject.FindGameObjectWithTag("WeaponManager");
		if (gameObject != null)
		{
			gameObject.SendMessage("showCrossHairFunc", SendMessageOptions.DontRequireReceiver);
		}
		EnemySoldier[] array = UnityEngine.Object.FindObjectsOfType<EnemySoldier>();
		foreach (EnemySoldier enemySoldier in array)
		{
			enemySoldier.StartWalking();
		}
		DestructionVehicle[] array3 = UnityEngine.Object.FindObjectsOfType<DestructionVehicle>();
		foreach (DestructionVehicle destructionVehicle in array3)
		{
			destructionVehicle.StartMoving();
		}
		Singleton<GameController>.Instance.eventManager.GameStarted();
		if (this.selectedlevel == 1)
		{
		}
		if (this.playerCopterSource != null)
		{
			this.playerCopterSource.Play();
		}
	}

	public void MovePlayerAtStarting()
	{
		AllGameControllers allGameControllers = UnityEngine.Object.FindObjectOfType<AllGameControllers>();
		if (allGameControllers != null)
		{
			PlayerEventListener component = allGameControllers.getCurrentController().GetComponent<PlayerEventListener>();
			if (component != null)
			{
				component.MovePlayer(this.currentLevelMeta.locationWavesMeta.getCurrentLocationMeta().playerLevelMeta.path[0]);
			}
		}
	}

	public void PlayerMoveEnd()
	{
		this.uiController.EnableCurrentControllerUI();
	}

	private void showPop()
	{
		PopUpBase popUpBase = new PopUpBase();
		popUpBase.description = "uk cheeta " + this.ifv;
		popUpBase.screenStayTime = 2f;
		popUpBase.isCancelBtn = true;
		Singleton<GameController>.Instance.uiPopUpManager.ShowPopUp("LargePopUp", popUpBase);
		this.ifv++;
	}

	private void startTouches()
	{
		this.isGameStarted = true;
	}

	private IEnumerator checkTutorial()
	{
		yield return new WaitForSeconds(1f);
		if (!Constants.isTutorialCompleted())
		{
			int currentTutorialStep = Constants.GetCurrentTutorialStep();
			TutorialUIController tutorialUIController = UnityEngine.Object.FindObjectOfType<TutorialUIController>();
			if (tutorialUIController != null)
			{
				tutorialUIController.ShowTutorial();
			}
		}
		yield break;
	}

	public void AlertEnemies()
	{
		this.canAlert = true;
		Singleton<GameController>.Instance.eventManager.BulletFired();
		int num = UnityEngine.Random.Range(0, 2);
		if (num == 0 || num == 1)
		{
			int id = UnityEngine.Random.Range(3, 5);
			Singleton<GameController>.Instance.soundController.playVoice(id, null);
		}
		float time = (float)UnityEngine.Random.Range(4, 8);
		base.InvokeRepeating("PlayVoices", time, 8f);
	}

	private void PlayVoices()
	{
		if (!this.isGameStarted || this.isGameOver || this.isGameCompleted)
		{
			return;
		}
		int num = UnityEngine.Random.Range(0, 2);
		if (num == 0 || num == 1)
		{
			int id = UnityEngine.Random.Range(9, 17);
			if (this.sceneControllerSource != null)
			{
				Singleton<GameController>.Instance.soundController.playVoice(id, this.sceneControllerSource);
			}
			else
			{
				Singleton<GameController>.Instance.soundController.playVoice(id, null);
			}
		}
	}

	public void IncrementEnemiesCountWithTypeNew(int type, int bodyId = 0)
	{
		Singleton<GameController>.Instance.scoreManager.SetScores(type);
		if (bodyId == 1)
		{
			this.totalHeadShots++;
			this.ShowShotBadge("Head");
		}
		int currentLocationId = this.currentLevelMeta.locationWavesMeta.currentLocationId;
		switch (type)
		{
		case 0:
		{
			List<int> listOfTask = this.missionLocationProgress.getCurrentLocation().listOfTask;
			(listOfTask)[0] = listOfTask[0] + 1;
			break;
		}
		case 1:
		{
			List<int> listOfTask = this.missionLocationProgress.getCurrentLocation().listOfTask;
			(listOfTask )[1] = listOfTask[1] + 1;
			break;
		}
		case 2:
		{
			List<int> listOfTask = this.missionLocationProgress.getLocationWithIndex(currentLocationId).listOfTask;
			(listOfTask)[2] = listOfTask[2] + 1;
			break;
		}
		case 4:
		{
			List<int> listOfTask = this.missionLocationProgress.getLocationWithIndex(currentLocationId).listOfTask;
			(listOfTask )[4] = listOfTask[4] + 1;
			break;
		}
		case 5:
			this.missionLocationProgress.getLocationWithIndex(currentLocationId).specialObjectiveTask++;
			break;
		}
		UnityEngine.Debug.Log("IncrementEnemiesCountWithType ");
		if (this.isGameOver || this.isGameCompleted || !this.isGameStarted || this.introInProgress)
		{
			UnityEngine.Debug.Log("Ahoo .SceneController :: Sorry GameOver");
			return;
		}
		int num = UnityEngine.Random.Range(0, 4);
		if (num == 0 || num == 1)
		{
			int id = UnityEngine.Random.Range(6, 9);
			Singleton<GameController>.Instance.soundController.playVoice(id, null);
		}
		this.currentLevelMeta.ManageAndGenerateWaves();
		this.CheckGameProgressNew();
	}

	public void CheckGameProgressNew()
	{
		bool currentLocIsCleared = this.missionLocationProgress.getCurrentLocIsCleared(this.missionLocationTasks);
		bool flag = false;
		int num = 0;
		for (int i = 0; i < this.missionLocationProgress.getCurrentLocation().listOfTask.Count; i++)
		{
			num += this.missionLocationProgress.getCurrentLocation().listOfTask[i];
		}
		int totalLevelEnemies = this.currentLevelMeta.locationWavesMeta.totalLevelEnemies;
		float missionStats = (float)num / (float)totalLevelEnemies;
		this.uiController.setMissionStats(missionStats);
		this.uiController.setMissionStatsCount(num, totalLevelEnemies);
		if (currentLocIsCleared)
		{
			flag = this.missionLocationProgress.getIsCleared(this.missionLocationTasks);
			if (!flag)
			{
				this.LocationCleared();
			}
			UnityEngine.Debug.Log("GameSCeneController :: CheckGameProgressNew : Level Cleared ");
		}
		if (flag)
		{
			this.isGameCompleted = true;
			base.Invoke("GameComplete", 2f);
		}
	}

	public void LocationCleared()
	{
		base.StartCoroutine(this.PerformLocationClearSpecificFunctionality());
	}

	private IEnumerator PerformLocationClearSpecificFunctionality()
	{
		yield return new WaitForSeconds(1f);
		PopUpBase b = new PopUpBase();
		b.description = "Location Cleared";
		b.screenStayTime = 2f;
		Singleton<GameController>.Instance.uiPopUpManager.ShowPopUp("MediumPopUp", b);
		yield return new WaitForSeconds(2f);
		int action = this.currentLevelMeta.locationWavesMeta.getCurrentLocationMeta().locationClearedAction;
		if (action != 0)
		{
			this.currentLevelMeta.GetComponent<LevelCameraAnimationController>().setAnimation();
		}
		this.missionLocationProgress.incrementCurrentLocation();
		this.currentLevelMeta.UpdateCurrentLocation();
		this.currentLevelMeta.ManageAndGenerateWaves();
		yield break;
	}

	public void endSpecificAnimationOperation()
	{
		this.currentLevelMeta.GetComponent<LevelCameraAnimationController>().animationComplete();
	}

	public void ShowShotBadge(string part)
	{
		if (part == "Head")
		{
			this.uiController.showShotTypeText("+1 HeadShot !!!");
		}
		else
		{
			this.uiController.showShotTypeText("BodyShot !!!");
		}
	}

	public void AnimalPointed()
	{
		this.animalInDangerCount++;
		if (this.animalInDangerCount > 0)
		{
			this.uiController.ShowDangerAlert();
		}
	}

	public void AnimalInSafe()
	{
		this.animalInDangerCount--;
		if (this.animalInDangerCount <= 0)
		{
			this.animalInDangerCount = 0;
			this.uiController.HideDangerAlert();
		}
	}

	public void makePositionFree(int index, int enemyType = 0)
	{
	}

	public void healthCollectedIncrement()
	{
		this.PlayerHealth = 100f;
		this.uiController.setHealth(this.PlayerHealth);
	}

	public void RefillHealth()
	{
		this.PlayerHealth = 100f;
		this.uiController.setHealth(this.PlayerHealth);
	}

	public void SetPlayersHealth(float h)
	{
		if (this.isGameOver || this.isGameCompleted)
		{
			return;
		}
		if (this.isArmourSelected)
		{
			this.armourHealth -= h;
			if (this.armourHealth <= 0f)
			{
				this.isArmourSelected = false;
				this.uiController.setArmourHealth(0f);
				this.uiController.hideArmourBar();
				this.uiController.ShowRunTimePopup("Armour Ended!!");
			}
			else
			{
				this.uiController.setArmourHealth(this.armourHealth);
				if (Time.time - this.lastShakeTime > 5f)
				{
					this.lastShakeTime = Time.time;
				}
			}
			return;
		}
		this.PlayerHealth -= h;
		if (this.PlayerHealth < 90f)
		{
			this.showDamageEffect();
		}
		if (this.PlayerHealth <= 0f)
		{
			this.uiController.setHealth(0f);
			this.gameOverMode = 1;
			base.Invoke("GameOverAccordingToMode", 0.1f);
		}
		else
		{
			this.uiController.setHealth(this.PlayerHealth);
			if (Time.time - this.lastShakeTime > 5f && UnityEngine.Object.FindObjectOfType<CameraShake>())
			{
				UnityEngine.Object.FindObjectOfType<CameraShake>().ShakeCamera(0.6f, 0.5f);
				this.lastShakeTime = Time.time;
			}
		}
		if (Constants.isSoundOn())
		{
			base.GetComponent<AudioSource>().PlayOneShot(this.playerHitSound, 0.7f);
		}
	}

	private void showDamageEffect()
	{
		this.uiController.showDamageEffect();
	}

	public void GameComplete()
	{
		if (Constants.isMusicOn())
		{
			base.GetComponent<AudioSource>().PlayOneShot(this.CompleteSound, 1f);
		}
		GameObject gameObject = GameObject.FindGameObjectWithTag("WeaponManager");
		if (gameObject != null)
		{
			gameObject.SendMessage("HideCrossHairFunc", SendMessageOptions.DontRequireReceiver);
		}
		this.isGameCompleted = true;
		base.StartCoroutine(this.winingSequence());
		UKAdsManager adsManager = Singleton<GameController>.Instance.adsManager;
		if (adsManager != null)
		{
		}
	}

	private IEnumerator winingSequence()
	{
		yield return new WaitForSeconds(0.5f);
		AllGameControllers allControllers = UnityEngine.Object.FindObjectOfType<AllGameControllers>();
		if (allControllers != null)
		{
			PlayerEventListener component = allControllers.getCurrentController().GetComponent<PlayerEventListener>();
			if (component != null)
			{
			}
		}
		yield return new WaitForSeconds(0.6f);
		this.uiController.showCompletePopUp();
		yield return new WaitForSeconds(2f);
		this.uiController.ShowLevelCompleteUI();
		UKAdsManager ads = Singleton<GameController>.Instance.adsManager;
		int r = UnityEngine.Random.Range(0, 3);
		if (ads != null)
		{
			ads.showInterstitial("LevelClear");
		}
		yield break;
	}

	private bool canShowRetryOption()
	{
		return true;
	}

	public void GameOverAccordingToMode()
	{
		Singleton<GameController>.Instance.eventManager.GameOver();
		if (Singleton<GameController>.Instance.soundController != null)
		{
			Singleton<GameController>.Instance.soundController.PlayGameOver();
		}
		this.initialGameOver();
		switch (this.gameOverMode)
		{
		case 1:
			if (!this.canShowRetryOption())
			{
				this.FinalGameOver();
			}
			else
			{
				this.showRetryScreenWithMode();
			}
			break;
		case 2:
			if (!this.canShowRetryOption())
			{
				this.FinalGameOver();
			}
			else
			{
				this.showRetryScreenWithMode();
			}
			break;
		case 3:
		case 4:
		case 5:
			this.FinalGameOver();
			break;
		}
	}

	private void showRetryScreenWithMode()
	{
		this.uiController.showRetryPopUp();
	}

	public void retryGame(float t = 0f)
	{
		int num = this.gameOverMode;
		if (num != 1)
		{
			if (num != 2)
			{
				if (num != 3)
				{
				}
			}
			else
			{
				this.startTime += 100f + t;
				this.isGameOver = false;
				base.InvokeRepeating("showTime", 0.5f, 0.2f);
				Singleton<GameController>.Instance.eventManager.GameRetried();
				GameObject gameObject = GameObject.FindGameObjectWithTag("WeaponManager");
				if (gameObject != null)
				{
					gameObject.SendMessage("showCrossHairFunc", SendMessageOptions.DontRequireReceiver);
				}
			}
		}
		else
		{
			this.PlayerHealth = 100f;
			this.isGameOver = false;
			this.startTime += t;
			base.InvokeRepeating("showTime", 0.5f, 0.2f);
			this.uiController.setHealth(this.PlayerHealth);
			Singleton<GameController>.Instance.eventManager.GameRetried();
			GameObject gameObject2 = GameObject.FindGameObjectWithTag("WeaponManager");
			if (gameObject2 != null)
			{
				gameObject2.SendMessage("showCrossHairFunc", SendMessageOptions.DontRequireReceiver);
			}
			this.currentLevelMeta.ManageAndGenerateWaves();
			this.CheckGameProgressNew();
		}
	}

	private void initialGameOver()
	{
		GameObject gameObject = GameObject.FindGameObjectWithTag("WeaponManager");
		if (gameObject != null)
		{
			gameObject.SendMessage("HideCrossHairFunc", SendMessageOptions.DontRequireReceiver);
		}
		base.CancelInvoke("showTime");
		this.isGameOver = true;
	}

	public void FinalGameOver()
	{
		base.StartCoroutine(this.waitToShowUI(this.gameOverMode));
	}

	public void GameOver(int mode)
	{
		if (Singleton<GameController>.Instance.soundController != null)
		{
			Singleton<GameController>.Instance.soundController.PlayGameOver();
		}
		GameObject gameObject = GameObject.FindGameObjectWithTag("WeaponManager");
		if (gameObject != null)
		{
			gameObject.SendMessage("HideCrossHairFunc", SendMessageOptions.DontRequireReceiver);
		}
		PlayerEventListener component = Singleton<GameController>.Instance.refAllController.getCurrentController().GetComponent<PlayerEventListener>();
		if (component != null)
		{
		}
		base.CancelInvoke("showTime");
		this.isGameOver = true;
		this.uiController.DisabLeAllControls();
		base.StartCoroutine(this.waitToShowUI(mode));
	}

	private IEnumerator waitToShowUI(int mode)
	{
		this.uiController.showFailedPopUp();
		yield return new WaitForSeconds(2.5f);
		this.uiController.ShowLevelFailedUI();
		UKAdsManager ads = Singleton<GameController>.Instance.adsManager;
		int r = UnityEngine.Random.Range(0, 3);
		if (ads != null)
		{
			ads.showInterstitial("LevelClear");
		}
		yield return new WaitForSeconds(1f);
		yield break;
	}

	public void showTime()
	{
		this.uiController.setTime(this.getTimeString(this.elapsedTime), false);
		if (this.remainingTime <= 10f)
		{
		}
	}

	public void addCoins(int coinsToAdd)
	{
		this.collectedPts += coinsToAdd;
	}

	private void Update()
	{
		if (!this.isGameStarted || this.isGameOver || this.isGameCompleted)
		{
			return;
		}
		if (this.isGameStarted)
		{
			this.elapsedTime = Time.time - this.startTime;
			this.remainingTime = this.totalTime - this.elapsedTime;
			if (this.remainingTime <= 0f)
			{
				this.uiController.setTime(this.getTimeString(0f), false);
				this.isGameOver = true;
				this.gameOverMode = 2;
				base.Invoke("GameOverAccordingToMode", 0.1f);
				base.CancelInvoke("showTime");
			}
		}
	}

	private void GameOver1()
	{
		this.GameOver(0);
	}

	private string getTimeString(float t)
	{
		int num = Mathf.FloorToInt(t);
		string result = string.Empty;
		if (num / 60 > 0)
		{
			result = string.Concat(new object[]
			{
				"0",
				num / 60,
				" : ",
				num % 60
			});
			if (num % 60 < 10)
			{
				result = string.Concat(new object[]
				{
					"0",
					num / 60,
					" : 0",
					num % 60
				});
			}
		}
		else
		{
			result = " 00 : " + num;
			if (num < 10)
			{
				result = "00 : 0" + num;
			}
		}
		return result;
	}

	public void StartRecording()
	{
	}

	public void StopRecording()
	{
	}

	public float startTime;

	public float elapsedTime;

	public float remainingTime;

	public float totalTime = 300f;

	public bool isGameOver;

	public bool isGameStarted;

	public bool isGameCompleted;

	public bool isIntroNeeded;

	public bool introInProgress;

	public bool canAlert;

	public float PlayerHealth = 100f;

	public int collectedPts;

	public LevelOrganiser currentLevelMeta;

	public GameObject camSwitcher;

	public AudioClip CompleteSound;

	public AudioClip FailedSound;

	public AudioClip playerHitSound;

	public int forTestingLevels = 1;

	public int forTestingMissions = 1;

	public bool isArmourSelected;

	public float armourHealth = 100f;

	public GameObject DummyManager;

	public GameObject introSceneObject;

	private float lastShakeTime;

	public EndSequenceAnimationController endSequence;

	public int gameOverMode = 1;

	public int animalInDangerCount;

	public GameObject[] environments;

	public MissionLocationTask missionLocationProgress;

	public MissionLocationTask missionLocationTasks;

	private int selectedMission = 1;

	private int selectedlevel = 1;

	private int totalEnemiesToKill = 1;

	private UIController uiController;

	public int totalHeadShots;

	public AudioSource playerCopterSource;

	public AudioSource sceneControllerSource;

	public string sceneval = string.Empty;

	private int ifv;
}
