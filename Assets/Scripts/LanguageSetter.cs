// dnSpy decompiler from Assembly-CSharp.dll class: LanguageSetter
using System;
using Lean;
using UnityEngine;

public class LanguageSetter : MonoBehaviour
{
	private void Start()
	{
	}

	private void OnEnable()
	{
		this.setLanguage();
	}

	private void setLanguage()
	{
		string text = string.Empty;
		if (!PlayerPrefs.HasKey("Language"))
		{
			text = "English";
			PlayerPrefs.SetString("Language", text);
		}
		else
		{
			text = PlayerPrefs.GetString("Language", "English");
		}
		LeanLocalization.Instance.CurrentLanguage = text;
	}
}
