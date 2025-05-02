// dnSpy decompiler from Assembly-CSharp.dll class: SceneLoader
using System;
using System.Collections;
using UnityEngine;

public class SceneLoader : MonoBehaviour
{
	private void Start()
	{
		base.StartCoroutine(this.loadScene());
	}

	private IEnumerator loadScene()
	{
		yield return new WaitForSeconds(0.2f);
		this.async = Application.LoadLevelAdditiveAsync("GamePlay");
		while (this.async.progress < 1f)
		{
			yield return null;
		}
		for (int i = 0; i < this.objsToDelete.Length; i++)
		{
			UnityEngine.Object.Destroy(this.objsToDelete[i]);
		}
		base.gameObject.SetActive(false);
		yield break;
	}

	private void Update()
	{
	}

	private AsyncOperation async;

	public GameObject[] objsToDelete;
}
