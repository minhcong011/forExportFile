// dnSpy decompiler from Assembly-CSharp.dll class: LevelOrganiser
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelOrganiser : MonoBehaviour
{
	private void Awake()
	{
		this.allController = UnityEngine.Object.FindObjectOfType<AllGameControllers>();
		this.setTotalMetaCounts();
		if (this.locationWavesMeta.totalLocations > 0)
		{
			this.currentLocationMeta = this.locationWavesMeta.getCurrentLocationMeta();
		}
	}

	public void setMetaFromJson(Stage stage)
	{
		if (stage == null)
		{
			return;
		}
		LevelOrganiserMeta levelOrganiserMeta = stage.levelOrganiserMeta;
		this.refLevelOrganiserMeta = levelOrganiserMeta;
		if (levelOrganiserMeta == null)
		{
			UnityEngine.Debug.Log("LevelOrganiserMeta meta is null");
			return;
		}
		UnityEngine.Debug.Log("LevelOrganiserMeta meta is coming " + levelOrganiserMeta.shouldReadMeta);
		if (levelOrganiserMeta.shouldReadMeta)
		{
			this.levelFailedStrings = stage.levelFailedStrings;
			if (levelOrganiserMeta.soldiersMeta != null)
			{
			}
		}
	}

	private void Start()
	{
		this.levelFailedStrings = new List<string>();
		this.levelFailedStrings.Add("You Lost. Try to use MediPacks");
	}

	public void setTotalMetaCounts()
	{
		foreach (SingleLocationWaveMeta singleLocationWaveMeta in this.locationWavesMeta.locationsMeta)
		{
			singleLocationWaveMeta.InitFillIndexes();
		}
	}

	public void EnableDisableChunks()
	{
		for (int i = 0; i < this.chunksToDisable.Count; i++)
		{
			this.chunksToDisable[i].SetActive(false);
		}
		for (int j = 0; j < this.chunksToEnable.Count; j++)
		{
			this.chunksToEnable[j].SetActive(true);
		}
	}

	public void InitEnemies()
	{
		this.ManageAndGenerateWaves();
		this.allController.setInitialMissiles(this.initialMissilesCount);
		this.EnableDisableChunks();
	}

	public void freeIndex(int id, int enemyType = 0)
	{
		switch (enemyType)
		{
		case 0:
			this.currentLocationMeta.soldierEnemiesMeta.positionFillIndex[id] = false;
			break;
		case 1:
			this.currentLocationMeta.tankParams.positionFillIndex[id] = false;
			break;
		case 2:
			this.currentLocationMeta.HeliParams.positionFillIndex[id] = false;
			break;
		}
	}

	public void UpdateCurrentLocation()
	{
		this.locationWavesMeta.currentLocationId++;
		this.currentLocationMeta = this.locationWavesMeta.getCurrentLocationMeta();
	}

	public void ManageAndGenerateWaves()
	{
		GameSceneController gameSceneController = Singleton<GameController>.Instance.gameSceneController;
		if (this.currentLocationMeta == null)
		{
			UnityEngine.Debug.Log("Level Organiser :: ManageAndGenerateWaves : currentLocationMeta is null");
			return;
		}
		if (this.locationWavesMeta.currentLocationId <= this.locationWavesMeta.totalLocations && !this.currentLocationMeta.isLocationCleared && this.currentLocationMeta.GetGeneratedEnemiesCountInThisLocation() <= this.currentLocationMeta.totalCount)
		{
			int num = gameSceneController.missionLocationProgress.getCurrentLocation().listOfTask[0];
			EnemyLevelMeta soldierEnemiesMeta = this.currentLocationMeta.soldierEnemiesMeta;
			if (soldierEnemiesMeta.generatedCount < soldierEnemiesMeta.enemiesCount && soldierEnemiesMeta.currentWave < soldierEnemiesMeta.waveGenerationAtCount.Count && soldierEnemiesMeta.waveGenerationAtCount[soldierEnemiesMeta.currentWave] == num)
			{
				base.StartCoroutine(this.generateSoldierEnemiesWithNewWaves());
			}
			int num2 = gameSceneController.missionLocationProgress.getCurrentLocation().listOfTask[1];
			EnemyLevelMeta tankParams = this.currentLocationMeta.tankParams;
			if (tankParams.generatedCount < tankParams.enemiesCount && tankParams.waveGenerationAtCount[tankParams.currentWave] == num2)
			{
				base.StartCoroutine(this.generateTanksNew());
			}
			int num3 = gameSceneController.missionLocationProgress.getCurrentLocation().listOfTask[2];
			EnemyLevelMeta heliParams = this.currentLocationMeta.HeliParams;
			if (heliParams.generatedCount < heliParams.enemiesCount && heliParams.waveGenerationAtCount[heliParams.currentWave] == num3)
			{
				base.StartCoroutine(this.generateHeliWithNewWaves());
			}
		}
	}

	public IEnumerator generateSoldierEnemies()
	{
		EnemyLevelMeta soldiersMeta = this.currentLocationMeta.soldierEnemiesMeta;
		List<EnemyMeta> tempmeta = new List<EnemyMeta>();
		bool initialWave = true;
		if (soldiersMeta.currentWave == 0)
		{
			initialWave = true;
			for (int j = 0; j < soldiersMeta.initialMeta.Count; j++)
			{
				EnemyMeta enemyMeta = new EnemyMeta();
				enemyMeta.CopyCOnstructor(soldiersMeta.initialMeta[j]);
				tempmeta.Add(enemyMeta);
			}
		}
		else
		{
			initialWave = false;
			for (int k = 0; k < soldiersMeta.wavesMeta.Count; k++)
			{
				EnemyMeta enemyMeta2 = new EnemyMeta();
				enemyMeta2.CopyCOnstructor(soldiersMeta.wavesMeta[k]);
				tempmeta.Add(enemyMeta2);
			}
		}
		int i = 0;
		while (i < soldiersMeta.enemiesCountInWave[soldiersMeta.currentWave] && soldiersMeta.generatedCount < soldiersMeta.enemiesCount)
		{
			bool val = true;
			int r = UnityEngine.Random.Range(0, tempmeta.Count);
			int checkCount = 0;
			while (val && !initialWave)
			{
				r = UnityEngine.Random.Range(0, tempmeta.Count);
				if (!soldiersMeta.positionFillIndex[r])
				{
					soldiersMeta.positionFillIndex[r] = true;
					val = false;
				}
				checkCount++;
				if (checkCount > 10)
				{
					val = false;
				}
			}
			Transform point = tempmeta[r].enemyTransform;
			string objName = tempmeta[r].modelId;
			string[] objName2 = objName.Split(new char[]
			{
				'_'
			});
			GameObject enemy = UnityEngine.Object.Instantiate(Resources.Load("EnemyModels/" + objName2[0] + "/" + objName2[1])) as GameObject;
			enemy.transform.eulerAngles = point.eulerAngles;
			enemy.GetComponent<hoMove>().pathContainer = tempmeta[r].path;
			enemy.transform.localPosition = point.localPosition;
			if (initialWave)
			{
				enemy.GetComponent<EnemySoldier>().setInitials(tempmeta[r], -1);
				tempmeta.RemoveAt(r);
			}
			else
			{
				enemy.GetComponent<EnemySoldier>().setInitials(tempmeta[r], r);
			}
			soldiersMeta.generatedCount++;
			yield return new WaitForSeconds(0.01f);
			i++;
		}
		soldiersMeta.currentWave++;
		tempmeta.Clear();
		yield break;
	}

	public IEnumerator generateSoldierEnemiesWithNewWaves()
	{
		EnemyLevelMeta soldiersMeta = this.currentLocationMeta.soldierEnemiesMeta;
		List<EnemyMeta> tempmeta = new List<EnemyMeta>();
		for (int i = 0; i < soldiersMeta.perWaveMeta[soldiersMeta.currentWave].wavesMeta.Count; i++)
		{
			EnemyMeta enemyMeta = new EnemyMeta();
			enemyMeta.CopyCOnstructor(soldiersMeta.perWaveMeta[soldiersMeta.currentWave].wavesMeta[i]);
			tempmeta.Add(enemyMeta);
		}
		if (soldiersMeta.currentWave != 0)
		{
			int num = UnityEngine.Random.Range(0, 2);
			if (num == 0 || num == 1)
			{
				int id = UnityEngine.Random.Range(0, 6);
				Singleton<GameController>.Instance.soundController.playVoice(id, null);
			}
		}
		yield return new WaitForSeconds(0.05f);
		for (int j = 0; j < soldiersMeta.enemiesCountInWave[soldiersMeta.currentWave]; j++)
		{
			int num2 = UnityEngine.Random.Range(0, tempmeta.Count);
			Transform enemyTransform = tempmeta[num2].enemyTransform;
			string modelId = tempmeta[num2].modelId;
			string[] array = modelId.Split(new char[]
			{
				'_'
			});
			GameObject gameObject = UnityEngine.Object.Instantiate(Resources.Load("EnemyModels/" + array[0] + "/" + array[1])) as GameObject;
			gameObject.transform.eulerAngles = enemyTransform.eulerAngles;
			gameObject.GetComponent<hoMove>().pathContainer = tempmeta[num2].path;
			gameObject.transform.localPosition = enemyTransform.localPosition;
			gameObject.GetComponent<EnemySoldier>().setInitials(tempmeta[num2], num2);
			tempmeta.RemoveAt(num2);
			soldiersMeta.generatedCount++;
		}
		soldiersMeta.currentWave++;
		tempmeta.Clear();
		yield break;
	}

	public IEnumerator generateTanksNew()
	{
		EnemyLevelMeta tanksMeta = this.currentLocationMeta.tankParams;
		List<EnemyMeta> tempmeta = new List<EnemyMeta>();
		bool initialWave = true;
		if (tanksMeta.currentWave == 0)
		{
			initialWave = true;
			for (int j = 0; j < tanksMeta.wavesMeta.Count; j++)
			{
				EnemyMeta enemyMeta = new EnemyMeta();
				enemyMeta.CopyCOnstructor(tanksMeta.wavesMeta[j]);
				tempmeta.Add(enemyMeta);
			}
		}
		else
		{
			initialWave = false;
			for (int k = 0; k < tanksMeta.wavesMeta.Count; k++)
			{
				EnemyMeta enemyMeta2 = new EnemyMeta();
				enemyMeta2.CopyCOnstructor(tanksMeta.wavesMeta[k]);
				tempmeta.Add(enemyMeta2);
			}
		}
		yield return new WaitForSeconds(0.1f);
		for (int i = 0; i < tanksMeta.enemiesCountInWave[tanksMeta.currentWave]; i++)
		{
			bool val = true;
			int r = UnityEngine.Random.Range(0, tempmeta.Count);
			int checkCount = 0;
			while (val && !initialWave)
			{
				r = UnityEngine.Random.Range(0, tempmeta.Count);
				if (!tanksMeta.positionFillIndex[r])
				{
					tanksMeta.positionFillIndex[r] = true;
					val = false;
				}
				checkCount++;
				if (checkCount > 10)
				{
					val = false;
				}
			}
			Transform point = tempmeta[r].enemyTransform;
			string objName = tempmeta[r].modelId;
			string[] objName2 = objName.Split(new char[]
			{
				'_'
			});
			GameObject enemy = UnityEngine.Object.Instantiate(Resources.Load("EnemyModels/" + objName2[0] + "/" + objName2[1])) as GameObject;
			enemy.transform.eulerAngles = point.eulerAngles;
			enemy.GetComponent<hoMove>().pathContainer = tempmeta[r].path;
			enemy.transform.localPosition = point.localPosition;
			enemy.GetComponent<DestructionVehicle>().stopPoint = tempmeta[r].stopPoint;
			enemy.GetComponent<DestructionVehicle>().setInitials(tempmeta[r], r);
			tanksMeta.generatedCount++;
			if (initialWave)
			{
				tempmeta.RemoveAt(r);
			}
			yield return new WaitForSeconds(0.3f);
		}
		tanksMeta.currentWave++;
		tempmeta.Clear();
		yield break;
	}

	public IEnumerator generateHeliWithNewWaves()
	{
		EnemyLevelMeta heliMeta = this.currentLocationMeta.HeliParams;
		List<EnemyMeta> tempmeta = new List<EnemyMeta>();
		for (int i = 0; i < heliMeta.perWaveMeta[heliMeta.currentWave].wavesMeta.Count; i++)
		{
			EnemyMeta enemyMeta = new EnemyMeta();
			enemyMeta.CopyCOnstructor(heliMeta.perWaveMeta[heliMeta.currentWave].wavesMeta[i]);
			tempmeta.Add(enemyMeta);
		}
		yield return new WaitForSeconds(0.05f);
		for (int j = 0; j < heliMeta.enemiesCountInWave[heliMeta.currentWave]; j++)
		{
			int num = UnityEngine.Random.Range(0, tempmeta.Count);
			Transform enemyTransform = tempmeta[num].enemyTransform;
			string modelId = tempmeta[num].modelId;
			string[] array = modelId.Split(new char[]
			{
				'_'
			});
			GameObject gameObject = UnityEngine.Object.Instantiate(Resources.Load("EnemyModels/" + array[0] + "/Helicopter" + array[1])) as GameObject;
			gameObject.transform.eulerAngles = enemyTransform.eulerAngles;
			gameObject.GetComponent<hoMove>().pathContainer = tempmeta[num].path;
			gameObject.transform.localPosition = enemyTransform.localPosition;
			DestructionVehicle component = gameObject.GetComponent<DestructionVehicle>();
			if (component != null)
			{
				component.stopPoint = tempmeta[num].stopPoint;
				component.setInitials(tempmeta[num], num);
			}
			tempmeta.RemoveAt(num);
			heliMeta.generatedCount++;
		}
		heliMeta.currentWave++;
		tempmeta.Clear();
		yield break;
	}

	public IEnumerator generateHelicoptersNew()
	{
		UnityEngine.Debug.Log("coming in heli ");
		EnemyLevelMeta helisMeta = this.currentLocationMeta.HeliParams;
		List<EnemyMeta> tempmeta = new List<EnemyMeta>();
		bool initialWave = true;
		if (helisMeta.currentWave == 0)
		{
			initialWave = true;
			for (int j = 0; j < helisMeta.initialMeta.Count; j++)
			{
				EnemyMeta enemyMeta = new EnemyMeta();
				enemyMeta.CopyCOnstructor(helisMeta.initialMeta[j]);
				tempmeta.Add(enemyMeta);
			}
		}
		else
		{
			initialWave = false;
			for (int k = 0; k < helisMeta.wavesMeta.Count; k++)
			{
				EnemyMeta enemyMeta2 = new EnemyMeta();
				enemyMeta2.CopyCOnstructor(helisMeta.wavesMeta[k]);
				tempmeta.Add(enemyMeta2);
			}
		}
		yield return new WaitForSeconds(0.1f);
		for (int i = 0; i < helisMeta.enemiesCountInWave[helisMeta.currentWave]; i++)
		{
			bool val = true;
			int r = UnityEngine.Random.Range(0, tempmeta.Count);
			int checkCount = 0;
			while (val && !initialWave)
			{
				r = UnityEngine.Random.Range(0, tempmeta.Count);
				if (!helisMeta.positionFillIndex[r])
				{
					helisMeta.positionFillIndex[r] = true;
					val = false;
				}
				checkCount++;
				if (checkCount > 10)
				{
					val = false;
				}
			}
			Transform point = tempmeta[r].enemyTransform;
			string objName = tempmeta[r].modelId;
			string[] objName2 = objName.Split(new char[]
			{
				'_'
			});
			GameObject enemy = UnityEngine.Object.Instantiate(Resources.Load("EnemyModels/" + objName2[0] + "/Helicopter" + objName2[1])) as GameObject;
			enemy.transform.eulerAngles = point.eulerAngles;
			enemy.GetComponent<hoMove>().pathContainer = tempmeta[r].path;
			enemy.transform.localPosition = point.localPosition;
			enemy.GetComponent<DestructionVehicle>().stopPoint = tempmeta[r].stopPoint;
			enemy.GetComponent<DestructionVehicle>().setInitials(tempmeta[r], r);
			helisMeta.generatedCount++;
			yield return new WaitForSeconds(0.3f);
		}
		helisMeta.currentWave++;
		tempmeta.Clear();
		yield break;
	}

	public IEnumerator generateEnemiesSpecialWave()
	{
		yield return new WaitForSeconds(0.1f);
		yield break;
	}

	public string getRandomfailedString()
	{
		string result = "Value Is Null";
		if (this.levelFailedStrings.Count > 0)
		{
			int index = UnityEngine.Random.Range(0, this.levelFailedStrings.Count);
			result = this.levelFailedStrings[index];
		}
		return result;
	}

	public void setRandomPlayerPosition(Transform t)
	{
		int index = UnityEngine.Random.Range(0, this.playerSpawningPoints.Count);
		t.localPosition = this.playerSpawningPoints[index].localPosition;
		t.localEulerAngles = this.playerSpawningPoints[index].localEulerAngles;
		GameObject gameObject = GameObject.FindGameObjectWithTag("LookObject");
		if (gameObject != null)
		{
			gameObject.transform.localPosition = new Vector3(0f, 0.6f, 0f);
		}
		UnityEngine.Debug.Log("rot " + this.playerSpawningPoints[index].localEulerAngles);
		int num = 0;
		while (num < this.allController.allControllers.Length && num < this.controllerPositions.Count)
		{
			this.allController.setControllerPosition(this.controllerPositions[num], num);
			num++;
		}
	}

	public void SetPlayerPosition()
	{
		Transform startPositionOnIndex = this.currentLocationMeta.playerLevelMeta.GetStartPositionOnIndex(0);
		this.allController.setControllerPosition(startPositionOnIndex, this.controllerId);
		PlayerMovementAnimationController component = this.allController.getCurrentController().gameObject.GetComponent<PlayerMovementAnimationController>();
		if (component != null)
		{
			component.setPath(this.currentLocationMeta.playerLevelMeta.path[0]);
		}
	}

	private void setPosAferTime()
	{
		PlayerEventListener component = this.allController.getCurrentController().gameObject.GetComponent<PlayerEventListener>();
		if (component)
		{
			component.SetHeliPos(this.playerCopterPos);
		}
	}

	public bool isIntroNeeded;

	public int controllerId;

	public bool isControllerMoveable = true;

	public float levelTime = 300f;

	public string levelDescription = "Kill the lion !!";

	private List<string> levelFailedStrings;

	public List<Transform> playerSpawningPoints;

	public AirStrikeController airStrikeController;

	public LocationWavesMeta locationWavesMeta;

	public List<Transform> controllerPositions;

	private AllGameControllers allController;

	public int[] initialMissilesCount = new int[5];

	public Guns gun;

	private LevelOrganiserMeta refLevelOrganiserMeta;

	public List<Transform> playerPositionsArray;

	public int playerPosIndex = 1;

	public List<GameObject> chunksToDisable;

	public List<GameObject> chunksToEnable;

	public GameObject IntroScene;

	public Transform playerCopterPos;

	private SingleLocationWaveMeta currentLocationMeta;
}
