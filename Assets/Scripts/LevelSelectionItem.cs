// dnSpy decompiler from Assembly-CSharp.dll class: LevelSelectionItem
using System;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectionItem : MonoBehaviour
{
	private void Awake()
	{
		this.refLevelSelection = UnityEngine.Object.FindObjectOfType<LevelSelectionUI>();
	}

	public void SetInitials(int missionNumb)
	{
		this.missionNumber = missionNumb;
	}

	private void OnDisable()
	{
		this.SetAsNormal();
	}

	private void Start()
	{
		this.lvlNumbtxt.text = this.lvlNumb.ToString();
		this.lvlNumbtxt.enabled = true;
	}

	public void UnlockLevel()
	{
		this.lockImage.gameObject.SetActive(false);
		this.lvlNumbtxt.gameObject.SetActive(true);
		this.islocked = false;
		this.SetAsNormal();
	}

	public void LockLevel()
	{
		if (this.refLevelSelection == null)
		{
			this.refLevelSelection = UnityEngine.Object.FindObjectOfType<LevelSelectionUI>();
		}
		this.lockImage.gameObject.SetActive(true);
		this.lvlNumbtxt.gameObject.SetActive(false);
		this.islocked = true;
		this.thumb.sprite = this.refLevelSelection.lockedLvl;
	}

	public void SetAsSelected()
	{
		if (this.refLevelSelection == null)
		{
			this.refLevelSelection = UnityEngine.Object.FindObjectOfType<LevelSelectionUI>();
		}
		if (!this.islocked)
		{
			this.thumb.sprite = this.refLevelSelection.selectedSprite;
		}
	}

	public void SetAsNormal()
	{
		if (this.refLevelSelection == null)
		{
			this.refLevelSelection = UnityEngine.Object.FindObjectOfType<LevelSelectionUI>();
		}
		if (!this.islocked)
		{
			this.thumb.sprite = this.refLevelSelection.clearedLevel;
		}
		else
		{
			this.thumb.sprite = this.refLevelSelection.lockedLvl;
		}
	}

	public void LevelClicked()
	{
		if (this.refLevelSelection == null)
		{
			UnityEngine.Debug.LogError("No UI");
			return;
		}
		if (this.lvlNumb == 0)
		{
			UnityEngine.Debug.LogError("level selection :: level number not setted");
		}
		this.refLevelSelection.LevelClicked(this.missionNumber, this.lvlNumb);
	}

	public int lvlNumb;

	public int missionNumber;

	public Text lvlNumbtxt;

	public Image thumb;

	public Image lockImage;

	public LevelSelectionUI refLevelSelection;

	public bool islocked = true;
}
