// dnSpy decompiler from Assembly-CSharp.dll class: MenuHandler
using System;
using UnityEngine;
using UnityEngine.UI;

public class MenuHandler : MonoBehaviour
{
	private void Start()
	{
	}

	private void Update()
	{
		if (UnityEngine.Input.GetKeyUp(KeyCode.Escape))
		{
			Application.Quit();
		}
		if (this.Init != null)
		{
			this.filler.fillAmount = this.Init.getProgress();
		}
	}

	public void StartController()
	{
		this.Init = UnityEngine.Object.FindObjectOfType<Initializer>();
		this.Init.Initialize();
	}

	public GameObject MainMenuObj;

	public Image filler;

	private Initializer Init;
}
