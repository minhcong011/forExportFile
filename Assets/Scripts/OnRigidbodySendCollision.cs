// dnSpy decompiler from Assembly-CSharp.dll class: OnRigidbodySendCollision
using System;
using UnityEngine;

public class OnRigidbodySendCollision : MonoBehaviour
{
	private void GetEffectSettingsComponent(Transform tr)
	{
		Transform parent = tr.parent;
		if (parent != null)
		{
			this.effectSettings = parent.GetComponentInChildren<EffectSettings>();
			if (this.effectSettings == null)
			{
				this.GetEffectSettingsComponent(parent.transform);
			}
		}
	}

	private void Start()
	{
		this.GetEffectSettingsComponent(base.transform);
	}

	private void OnCollisionEnter(Collision collision)
	{
		this.effectSettings.OnCollisionHandler(new CollisionInfo());
	}

	private EffectSettings effectSettings;
}
