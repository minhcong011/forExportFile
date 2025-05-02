// dnSpy decompiler from Assembly-CSharp.dll class: TutorialUIController
using System;
using ControlFreak2;
using UnityEngine;

public class TutorialUIController : MonoBehaviour
{
	private void Start()
	{
		this.timeClicked = Time.time;
	}

	public void ShowTutorial()
	{
		Time.timeScale = 0f;
		int currentTutorialStep = Constants.GetCurrentTutorialStep();
		this.DisableBtns();
		this.steps[currentTutorialStep - 1].SetActive(true);
		this.stepsTarget[currentTutorialStep - 1].SetActive(true);
	}

	private void DisableBtns()
	{
		for (int i = 0; i < this.stepsTarget.Length; i++)
		{
			TouchButton component = this.stepsTarget[i].GetComponent<TouchButton>();
			if (component != null)
			{
				component.disablingConditions.disableWhenTouchScreenInactive = false;
			}
			DynamicRegion component2 = this.stepsTarget[i].GetComponent<DynamicRegion>();
			if (component2 != null)
			{
				component2.disablingConditions.disableWhenTouchScreenInactive = false;
			}
			this.stepsTarget[i].SetActive(false);
		}
	}

	private void EnableBtns()
	{
		for (int i = 0; i < this.stepsTarget.Length; i++)
		{
			TouchButton component = this.stepsTarget[i].GetComponent<TouchButton>();
			if (component != null)
			{
				component.disablingConditions.disableWhenTouchScreenInactive = true;
			}
			DynamicRegion component2 = this.stepsTarget[i].GetComponent<DynamicRegion>();
			if (component2 != null)
			{
				component2.disablingConditions.disableWhenTouchScreenInactive = true;
			}
			this.stepsTarget[i].SetActive(true);
		}
	}

	public void ClickAt(string action)
	{
		Time.timeScale = 1f;
		if (Time.time - this.timeClicked < 0.4f)
		{
			return;
		}
		this.timeClicked = Time.time;
		this.DisableBtns();
		switch (action)
		{
		case "Step1":
			this.steps[0].SetActive(false);
			this.id = 1;
			base.Invoke("ActiveAfterDelay", 0.05f);
			Time.timeScale = 1f;
			break;
		case "Step2":
			this.steps[1].SetActive(false);
			this.id = 2;
			base.Invoke("ActiveAfterDelay", 0.05f);
			Time.timeScale = 1f;
			break;
		case "Step3":
			this.steps[2].SetActive(false);
			this.id = 3;
			base.Invoke("ActiveAfterDelay", 0.05f);
			Time.timeScale = 1f;
			break;
		case "Step4":
			this.steps[3].SetActive(false);
			this.id = 4;
			base.Invoke("ActiveAfterDelay", 0.05f);
			Time.timeScale = 1f;
			break;
		case "Step5":
			this.steps[4].SetActive(false);
			this.id = 5;
			base.Invoke("ActiveAfterDelay", 0.05f);
			Time.timeScale = 1f;
			break;
		case "Step6":
			this.steps[5].SetActive(false);
			this.id = 6;
			Time.timeScale = 1f;
			base.Invoke("ActiveAfterDelay", 0.05f);
			break;
		case "Step7":
			this.steps[6].SetActive(false);
			this.id = 7;
			base.Invoke("ActiveAfterDelay", 0.05f);
			Time.timeScale = 1f;
			break;
		case "Step8":
			this.steps[7].SetActive(false);
			this.id = 8;
			base.Invoke("ActiveAfterDelay", 0.05f);
			Time.timeScale = 1f;
			break;
		case "Step9":
			this.steps[8].SetActive(false);
			this.id = 9;
			base.Invoke("ActiveAfterDelay", 0.05f);
			Time.timeScale = 1f;
			break;
		case "Step10":
			this.steps[9].SetActive(false);
			this.id = 10;
			base.Invoke("ActiveAfterDelay", 0.05f);
			Time.timeScale = 1f;
			break;
		case "Step11":
			this.steps[10].SetActive(false);
			this.id = 11;
			Time.timeScale = 1f;
			Constants.setTutorialComplete();
			this.EnableBtns();
			break;
		}
	}

	public void ActiveAfterDelay()
	{
		this.steps[this.id].SetActive(true);
		this.stepsTarget[this.id].SetActive(true);
	}

	public GameObject[] steps;

	public GameObject[] stepsTarget;

	private int id;

	private float timeClicked;
}
