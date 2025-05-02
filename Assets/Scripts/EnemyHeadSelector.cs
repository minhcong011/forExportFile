// dnSpy decompiler from Assembly-CSharp.dll class: EnemyHeadSelector
using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHeadSelector : MonoBehaviour
{
	private void Awake()
	{
		int num = UnityEngine.Random.Range(0, this.heads.Count);
		for (int i = 0; i < this.heads.Count; i++)
		{
			this.heads[i].SetActive(false);
		}
		if (this.heads.Count > 0)
		{
			this.heads[num].SetActive(true);
		}
		for (int j = 0; j < this.bodies.Count; j++)
		{
			this.bodies[j].SetActive(false);
			if (this.isTextureChange && this.bodyTextures.Length > 0)
			{
				num = UnityEngine.Random.Range(0, this.bodyTextures.Length);
				this.bodies[j].GetComponent<SkinnedMeshRenderer>().material.mainTexture = this.bodyTextures[num];
			}
		}
		num = UnityEngine.Random.Range(0, this.bodies.Count);
		if (this.bodies.Count > 0)
		{
			this.bodies[num].SetActive(true);
		}
	}

	private void Update()
	{
	}

	public List<GameObject> heads;

	public List<GameObject> bodies;

	public bool isTextureChange;

	public Texture[] bodyTextures;

	public SkinnedMeshRenderer[] bodyMats;
}
