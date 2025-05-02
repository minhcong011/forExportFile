// dnSpy decompiler from Assembly-CSharp.dll class: Initializer
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Initializer : MonoBehaviour
{
	private void Start()
	{
		if (!Initializer.isCreated)
		{
			UnityEngine.Object.DontDestroyOnLoad(this);
			Initializer.isCreated = true;
		}
		else
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	private void Update()
	{
		if (UnityEngine.Input.GetKeyUp(KeyCode.Escape))
		{
			base.StartCoroutine(this.LoadLevel(0));
		}
	}

	public void Initialize()
	{
		base.StartCoroutine(this.LoadLevel(1));
	}

	private IEnumerator LoadLevel(int levelNo)
	{
		this.asyncOp = SceneManager.LoadSceneAsync("testlevel");
		Utils1.CLog(">> ", "Loading...\t\t~GameDonar", "yellow");
		while (!this.asyncOp.isDone)
		{
			yield return null;
		}
		Utils1.CLog("* ", "Level loaded.\t\t~GameDonar", "green");
		if (levelNo == 1)
		{
			this.InstantiateMyPlayer();
		}
		else if (levelNo == 0)
		{
		}
		yield break;
	}

	private void InstantiateMyPlayer()
	{
		GameObject[] array = GameObject.FindGameObjectsWithTag("SpawnPoint");
		Transform transform = array[UnityEngine.Random.Range(0, array.Length)].transform;
		GameObject gameObject = UnityEngine.Object.Instantiate(Resources.Load("Prefabs/Players/Player_CT_Sady"), transform.position, transform.rotation) as GameObject;
		Utils1.CLog("* ", "Player has been initialized.\t~GameDonar", "green");
		gameObject.GetComponent<SetUpRenderers>().SetupRenderer();
	}

	public float getProgress()
	{
		if (this.asyncOp != null)
		{
			return this.asyncOp.progress;
		}
		return 0f;
	}

	public static bool isCreated;

	private AsyncOperation asyncOp;
}
