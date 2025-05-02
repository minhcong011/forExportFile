// dnSpy decompiler from Assembly-CSharp.dll class: LevelDescriptionPanel
using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class LevelDescriptionPanel : MonoBehaviour
{
	private void Awake()
	{
		this.descriptionText.gameObject.SetActive(false);
		this.levelNumberScale.gameObject.SetActive(false);
	}

	private void Start()
	{
		float num = Constants.getSensitivity();
		this.sensitivity.value = num - 0.5f;
		Sequence s = DOTween.Sequence();
		s.Append(this.topBar.DOAnchorPos(new Vector2(this.topBar.anchoredPosition.x, -125f), 0.8f, false).SetEase(Ease.OutBounce).OnComplete(delegate
		{
			this.descriptionText.gameObject.SetActive(true);
			this.levelNumberScale.gameObject.SetActive(true);
			this.levelNumberScale.DOAnchorPos(new Vector2(65f, this.levelNumber.anchoredPosition.y), 0.6f, false).SetEase(Ease.Linear);
		}));
		s.Append(this.levelNumber.DOAnchorPos(new Vector2(0f, this.levelNumber.anchoredPosition.y), 0.6f, false).SetEase(Ease.Linear).OnComplete(delegate
		{
		}));
		s.AppendInterval(1.2f);
		s.Append(this.tapToContinue.DOAnchorPos(new Vector2(350f, this.tapToContinue.anchoredPosition.y), 0.7f, false).SetEase(Ease.Linear));
	}

	public void setDescription(string text)
	{
		this.descriptionText.text = text;
		this.levelHeading.text = string.Empty + Singleton<GameController>.Instance.SelectedLevel;
	}

	private void OnDisable()
	{
		MouseLooking[] array = UnityEngine.Object.FindObjectsOfType<MouseLooking>();
		for (int i = 0; i < array.Length; i++)
		{
			array[i].setSettingsSensitivity();
		}
		MouseLookingMachine[] array2 = UnityEngine.Object.FindObjectsOfType<MouseLookingMachine>();
		for (int j = 0; j < array.Length; j++)
		{
		}
	}

	private void Update()
	{
	}

	public void sensitivityChanged()
	{
		Constants.setSensitivity(this.sensitivity.value + 0.5f);
	}

	public Text descriptionText;

	public Text levelHeading;

	public Slider sensitivity;

	public RectTransform tapToContinue;

	public RectTransform topBar;

	public RectTransform levelNumber;

	public RectTransform levelNumberScale;
}
