// dnSpy decompiler from Assembly-CSharp.dll class: EnemiesController
using System;
using UnityEngine;

public class EnemiesController : MonoBehaviour
{
	private void Start()
	{
		if (this.value == 0)
		{
			for (int i = 0; i < 5; i++)
			{
				this.enemies[i].SetActive(true);
			}
		}
		else if (this.value == 1)
		{
			this.enemies[0].SetActive(true);
			this.enemies[1].SetActive(true);
		}
	}

	private void Update()
	{
	}

	public GameObject[] enemies;

	public int value;
}
