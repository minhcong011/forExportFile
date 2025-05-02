// dnSpy decompiler from Assembly-CSharp.dll class: ResetPositionOnDiactivated
using System;
using UnityEngine;

public class ResetPositionOnDiactivated : MonoBehaviour
{
	private void Start()
	{
		this.EffectSettings.EffectDeactivated += this.EffectSettings_EffectDeactivated;
	}

	private void EffectSettings_EffectDeactivated(object sender, EventArgs e)
	{
		base.transform.localPosition = Vector3.zero;
	}

	public EffectSettings EffectSettings;
}
