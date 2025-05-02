// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: ControlFreak2.Demos.RPG.LevelData
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ControlFreak2.Demos.RPG
{
	public class LevelData : MonoBehaviour
	{
		public LevelData()
		{
			this.interactiveObjectList = new List<InteractiveObjectBase>();
		}

		public InteractiveObjectBase FindInteractiveObjectFor(CharacterAction chara)
		{
			InteractiveObjectBase interactiveObjectBase = null;
			float num = 0f;
			for (int i = 0; i < this.interactiveObjectList.Count; i++)
			{
				InteractiveObjectBase interactiveObjectBase2 = this.interactiveObjectList[i];
				if (!(interactiveObjectBase2 == null) && interactiveObjectBase2.IsNear(chara))
				{
					float sqrMagnitude = (chara.transform.position - interactiveObjectBase2.transform.position).sqrMagnitude;
					if (interactiveObjectBase == null || sqrMagnitude < num)
					{
						interactiveObjectBase = interactiveObjectBase2;
						num = sqrMagnitude;
					}
				}
			}
			return interactiveObjectBase;
		}

		public List<InteractiveObjectBase> interactiveObjectList;
	}
}
