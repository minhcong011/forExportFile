using System;
using UnityEngine;

namespace UnityStandardAssets.Water
{
	// Token: 0x02000024 RID: 36
	[ExecuteInEditMode]
	public class WaterBasic : MonoBehaviour
	{
		// Token: 0x0600007D RID: 125 RVA: 0x00004754 File Offset: 0x00002954
		private void Update()
		{
			Renderer component = base.GetComponent<Renderer>();
			if (!component)
			{
				return;
			}
			Material sharedMaterial = component.sharedMaterial;
			if (!sharedMaterial)
			{
				return;
			}
			Vector4 vector = sharedMaterial.GetVector("WaveSpeed");
			float @float = sharedMaterial.GetFloat("_WaveScale");
			float num = Time.time / 20f;
			Vector4 vector2 = vector * (num * @float);
			Vector4 value = new Vector4(Mathf.Repeat(vector2.x, 1f), Mathf.Repeat(vector2.y, 1f), Mathf.Repeat(vector2.z, 1f), Mathf.Repeat(vector2.w, 1f));
			sharedMaterial.SetVector("_WaveOffset", value);
		}
	}
}
