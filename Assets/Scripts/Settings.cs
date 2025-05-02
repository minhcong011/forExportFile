// dnSpy decompiler from Assembly-CSharp.dll class: Settings
using System;
using UnityEngine;

public class Settings : MonoBehaviour
{
	private void Start()
	{
		this.SetResolution(this.startIndex, false);
		this.volume = Mathf.Clamp(this.volume, 0f, 1f);
		AudioListener.volume = this.volume;
	}

	public void SetResolution(int index, bool fs)
	{
		Screen.SetResolution(this.resolutions[index].width, this.resolutions[index].height, fs);
		this.currentResolution = this.resolutions[index];
	}

	public string GetResolutionInString(ScreenResolution input)
	{
		return input.width.ToString() + "x" + input.height.ToString();
	}

	public CustomInput customInput;

	public ScreenResolution[] resolutions;

	public int startIndex;

	public float volume = 0.3f;

	[HideInInspector]
	public ScreenResolution currentResolution;
}
