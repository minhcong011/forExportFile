// dnSpy decompiler from Assembly-CSharp.dll class: SplashSceneLoader
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SplashSceneLoader : MonoBehaviour
{
	private void Start()
	{
		if (this.loadingSlider != null)
		{
			this.loadingSlider.value = 0.5f;
		}
		base.Invoke("LoadGame", 2f);
	}

	private void LoadGame()
	{
		if (this.loadingSlider != null)
		{
			this.loadingSlider.value = 1f;
		}
		if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name != "MenuTowTractor")
		{
			SceneManager.LoadScene("MenuTowTractor");
		}
	}

	public Slider loadingSlider;
}
