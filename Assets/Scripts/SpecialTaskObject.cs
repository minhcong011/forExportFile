// dnSpy decompiler from Assembly-CSharp.dll class: SpecialTaskObject
using System;
using UnityEngine;

public class SpecialTaskObject : MonoBehaviour
{
	private void Start()
	{
	}

	private void Update()
	{
	}

	public void ApplyDamage(float d)
	{
		this.health -= d;
		if (this.health <= 0f)
		{
			this.health = 0f;
			Singleton<GameController>.Instance.gameSceneController.IncrementEnemiesCountWithTypeNew(5, 0);
		}
	}

	public float health;
}
