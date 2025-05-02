// dnSpy decompiler from Assembly-CSharp.dll class: Lean.MissionSelectionLocalization
using System;
using UnityEngine;

namespace Lean
{
	public class MissionSelectionLocalization : MonoBehaviour
	{
		private void Awake()
		{
			LeanLocalization.Instance.SetLanguage(PlayerPrefs.GetString("Language"));
		}

		private void Start()
		{
		}

		private void Update()
		{
		}

		public void MissionSelectionFunction(string value)
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
