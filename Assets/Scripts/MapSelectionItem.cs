// dnSpy decompiler from Assembly-CSharp.dll class: MapSelectionItem
using System;
using System.Collections;
using UnityEngine;

public class MapSelectionItem : MonoBehaviour
{
	private void Awake()
	{
		this.levelItemInMapArray = base.GetComponentsInChildren<LevelSelectionItem>();
		for (int i = 0; i < this.levelItemInMapArray.Length; i++)
		{
			this.levelItemInMapArray[i].SetInitials(this.missionId);
			this.levelItemInMapArray[i].gameObject.SetActive(false);
		}
		this.isInitialised = true;
	}

	public void ShowMap()
	{
		base.StartCoroutine(this.PlayInCoroutine());
	}

	public IEnumerator PlayInCoroutine()
	{
		for (int i = 0; i < this.levelItemInMapArray.Length; i++)
		{
			this.levelItemInMapArray[i].gameObject.SetActive(true);
			yield return new WaitForSeconds(this.duration);
		}
		yield return new WaitForSeconds(0.01f);
		yield break;
	}

	public void UnlockLevels(int count)
	{
		if (this.levelItemInMapArray.Length <= 0)
		{
			this.levelItemInMapArray = base.GetComponentsInChildren<LevelSelectionItem>();
		}
		for (int i = 0; i < this.levelItemInMapArray.Length; i++)
		{
			if (i < count)
			{
				this.levelItemInMapArray[i].UnlockLevel();
			}
			else
			{
				this.levelItemInMapArray[i].LockLevel();
			}
		}
	}

	public void setAllLocked()
	{
		if (this.levelItemInMapArray.Length <= 0)
		{
			this.levelItemInMapArray = base.GetComponentsInChildren<LevelSelectionItem>();
		}
		for (int i = 0; i < this.levelItemInMapArray.Length; i++)
		{
			this.levelItemInMapArray[i].LockLevel();
		}
	}

	public void setAllUnLocked()
	{
		if (this.levelItemInMapArray.Length <= 0)
		{
			this.levelItemInMapArray = base.GetComponentsInChildren<LevelSelectionItem>();
		}
		for (int i = 0; i < this.levelItemInMapArray.Length; i++)
		{
			this.levelItemInMapArray[i].UnlockLevel();
		}
	}

	public void SetAsSelected(int id)
	{
		this.levelItemInMapArray[id - 1].SetAsSelected();
	}

	public void SetAsNormal(int id)
	{
		this.levelItemInMapArray[id - 1].SetAsNormal();
	}

	private void OnDisable()
	{
		for (int i = 0; i < this.levelItemInMapArray.Length; i++)
		{
			this.levelItemInMapArray[i].gameObject.SetActive(false);
		}
	}

	public int missionId = 1;

	public LevelSelectionItem[] levelItemInMapArray;

	public float duration = 0.15f;

	private bool isInitialised;
}
