// dnSpy decompiler from Assembly-CSharp.dll class: DailyBonusController
using System;
using System.Collections;
using UnityEngine;

public class DailyBonusController : MonoBehaviour
{
	public void ChoiceRandom(bool isRegular = true)
	{
		int num = ProbabilityController.ChoiceRandom(this.BonusItems);
		this.BonusGanado = num;
		switch (this.BonusGanado)
		{
		case 0:
			UnityEngine.Debug.Log(num);
			break;
		case 1:
			UnityEngine.Debug.Log(num);
			break;
		case 2:
			UnityEngine.Debug.Log(num);
			break;
		case 3:
			UnityEngine.Debug.Log(num);
			break;
		case 4:
			UnityEngine.Debug.Log(num);
			break;
		case 5:
			UnityEngine.Debug.Log(num);
			break;
		case 6:
			UnityEngine.Debug.Log(num);
			break;
		case 7:
			UnityEngine.Debug.Log(num);
			break;
		}
		this.isRegularSpin = isRegular;
		this.id = this.BonusItems[num].ID;
		base.StartCoroutine(this.RotateToAngle(new Vector3(0f, 0f, 1f), this.Arrow.rotation.eulerAngles.z, this.Arrow.rotation.eulerAngles.z + ((float)(360 * UnityEngine.Random.Range(5, 9)) - this.Arrow.rotation.eulerAngles.z) + this.BonusItems[num].Angle, 2f, new Action(this.EndRandomChoice)));
	}

	public void EndRandomChoice()
	{
		this.ChoiceEndedSound.Play();
		DailyLoginUIController dailyLoginUIController = UnityEngine.Object.FindObjectOfType<DailyLoginUIController>();
		if (dailyLoginUIController != null)
		{
			dailyLoginUIController.spinComplete(this.id, this.isRegularSpin);
		}
	}

	private IEnumerator RotateToAngle(Vector3 rotateAxis, float currentAngle, float targetAngleValue, float speed = 1f, Action endFired = null)
	{
		float itemSoundAngle = currentAngle + (float)(360 / this.BonusItems.Length);
		float step;
		for (;;)
		{
			step = (targetAngleValue - currentAngle + speed) * Time.deltaTime * 0.5f;
			if (currentAngle + step > targetAngleValue)
			{
				break;
			}
			currentAngle += step;
			if (currentAngle >= itemSoundAngle)
			{
				this.SpineSound.Play();
				itemSoundAngle = currentAngle + (float)(360 / this.BonusItems.Length);
			}
			this.Arrow.Rotate(rotateAxis, step);
			yield return new WaitForSeconds(0.02f);
		}
		step = targetAngleValue - currentAngle;
		this.Arrow.Rotate(rotateAxis, step);
		if (endFired != null)
		{
			endFired();
		}
		yield break;
	}

	public BonusItem[] BonusItems;

	public RectTransform Arrow;

	public AudioSource SpineSound;

	public AudioSource ChoiceEndedSound;

	public int BonusGanado;

	private bool isRegularSpin = true;

	private int id;
}
