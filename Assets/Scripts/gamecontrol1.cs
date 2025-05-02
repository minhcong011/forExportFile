// dnSpy decompiler from Assembly-CSharp.dll class: gamecontrol1
using System;
using ControlFreak2;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gamecontrol1 : MonoBehaviour
{
	private void Start()
	{
	}

	private void Update()
	{
		if (CF2Input.GetButtonDown("Quit"))
		{
			Application.Quit();
		}
	}

	public void restart()
	{
		SceneManager.LoadScene("corridors");
	}
}
