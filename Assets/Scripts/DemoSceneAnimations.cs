// dnSpy decompiler from Assembly-CSharp.dll class: DemoSceneAnimations
using System;
using UnityEngine;

public class DemoSceneAnimations : MonoBehaviour
{
	public void OnButtonClick(string doAction)
	{
		if (doAction != null)
		{
			if (!(doAction == "LevelComplete"))
			{
				if (!(doAction == "objective"))
				{
					if (doAction == "gift")
					{
						this.gift.SetActive(true);
					}
				}
				else
				{
					this.objective.SetActive(true);
				}
			}
			else
			{
				this.LevelComplete.SetActive(true);
				this.LevelComplete.SendMessage("Awake", SendMessageOptions.DontRequireReceiver);
			}
		}
	}

	public GameObject LevelComplete;

	public GameObject objective;

	public GameObject gift;
}
