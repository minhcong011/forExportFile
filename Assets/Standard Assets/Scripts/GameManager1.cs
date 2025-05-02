// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: GameManager1
using System;
using UnityEngine;

public class GameManager1 : MonoBehaviour
{
	private void Awake()
	{
		if (PlayerPrefs.GetString("Music", "no") == "yes")
		{
			GameManager1.isMusicMute = true;
		}
		else
		{
			GameManager1.isMusicMute = false;
		}
		if (PlayerPrefs.GetString("Audio", "no") == "yes")
		{
			GameManager1.isSoundMute = true;
		}
		else
		{
			GameManager1.isSoundMute = false;
		}
	}

	public static bool isMusicMute;

	public static bool isSoundMute;
}
