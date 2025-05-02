// dnSpy decompiler from Assembly-CSharp.dll class: SetUpRenderers
using System;
using UnityEngine;

public class SetUpRenderers : MonoBehaviour
{
	public void SetupViewWeaponRenderer()
	{
		if (!this.shouldRender)
		{
			return;
		}
		foreach (Renderer renderer in this.renderers)
		{
			renderer.gameObject.layer = this.FPV_Renderer_LayerIndex;
		}
	}

	public void SetupRenderer()
	{
		if (!this.shouldRender)
		{
			return;
		}
		foreach (Renderer renderer in this.renderers)
		{
			renderer.gameObject.layer = this.TPV_Renderer_LOCAL_LayerIndex;
		}
	}

	public Renderer[] renderers;

	public int FPV_Renderer_LayerIndex = 12;

	public int TPV_Renderer_LOCAL_LayerIndex = 9;

	public int TPV_Renderer_REMOTE_LayerIndex = 11;

	public bool shouldRender = true;
}
