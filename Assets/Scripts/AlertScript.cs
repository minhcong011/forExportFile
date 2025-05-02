// dnSpy decompiler from Assembly-CSharp.dll class: AlertScript
using System;
using UnityEngine;

public class AlertScript : MonoBehaviour
{
	private void Start()
	{
	}

	private void OnTriggerEnter(Collider collision)
	{
		if (collision.gameObject.tag == "Player" && !Singleton<GameController>.Instance.gameSceneController.canAlert)
		{
			Singleton<GameController>.Instance.gameSceneController.AlertEnemies();
		}
	}

	private void OnDestroy()
	{
		Singleton<GameController>.Instance.gameSceneController.canAlert = true;
	}
}
