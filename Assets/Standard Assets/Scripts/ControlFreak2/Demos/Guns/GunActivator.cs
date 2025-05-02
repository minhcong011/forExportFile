// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: ControlFreak2.Demos.Guns.GunActivator
using System;
using UnityEngine;

namespace ControlFreak2.Demos.Guns
{
	public class GunActivator : MonoBehaviour
	{
		private void Update()
		{
			bool triggerState = (this.key != KeyCode.None && CF2Input.GetKey(this.key)) || (!string.IsNullOrEmpty(this.buttonName) && CF2Input.GetButton(this.buttonName));
			for (int i = 0; i < this.gunList.Length; i++)
			{
				Gun gun = this.gunList[i];
				if (gun != null)
				{
					gun.SetTriggerState(triggerState);
				}
			}
		}

		public Gun[] gunList;

		public string buttonName = "Fire1";

		public KeyCode key;
	}
}
