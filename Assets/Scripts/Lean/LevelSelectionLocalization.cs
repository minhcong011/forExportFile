// dnSpy decompiler from Assembly-CSharp.dll class: Lean.LevelSelectionLocalization
using System;
using UnityEngine;

namespace Lean
{
	public class LevelSelectionLocalization : MonoBehaviour
	{
		private void Start()
		{
			LeanLocalization.Instance.SetLanguage(PlayerPrefs.GetString("Language"));
		}

		private void Update()
		{
		}

		public void LevelSelectionFunction(string value)
		{
			if (value != null)
			{
				if (!(value == "MissionSelection"))
				{
					if (!(value == "MainMenu"))
					{
						if (value == "LevelSelection")
						{
							UnityEngine.SceneManagement.SceneManager.LoadScene("LevelSelection");
						}
					}
					else
					{
						UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
					}
				}
				else
				{
					UnityEngine.SceneManagement.SceneManager.LoadScene("MissionSelection");
				}
			}
		}
	}
}
