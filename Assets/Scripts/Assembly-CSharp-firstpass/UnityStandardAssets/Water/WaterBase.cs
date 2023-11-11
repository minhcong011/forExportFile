using System;
using UnityEngine;

namespace UnityStandardAssets.Water
{
	// Token: 0x0200002C RID: 44
	[ExecuteInEditMode]
	public class WaterBase : MonoBehaviour
	{
		// Token: 0x060000A3 RID: 163 RVA: 0x00005D5C File Offset: 0x00003F5C
		public void UpdateShader()
		{
			if (this.waterQuality > WaterQuality.Medium)
			{
				this.sharedMaterial.shader.maximumLOD = 501;
			}
			else if (this.waterQuality > WaterQuality.Low)
			{
				this.sharedMaterial.shader.maximumLOD = 301;
			}
			else
			{
				this.sharedMaterial.shader.maximumLOD = 201;
			}
			if (!SystemInfo.SupportsRenderTextureFormat(RenderTextureFormat.Depth))
			{
				this.edgeBlend = false;
			}
			if (this.edgeBlend)
			{
				Shader.EnableKeyword("WATER_EDGEBLEND_ON");
				Shader.DisableKeyword("WATER_EDGEBLEND_OFF");
				if (Camera.main)
				{
					Camera.main.depthTextureMode |= DepthTextureMode.Depth;
					return;
				}
			}
			else
			{
				Shader.EnableKeyword("WATER_EDGEBLEND_OFF");
				Shader.DisableKeyword("WATER_EDGEBLEND_ON");
			}
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00005E1C File Offset: 0x0000401C
		public void WaterTileBeingRendered(Transform tr, Camera currentCam)
		{
			if (currentCam && this.edgeBlend)
			{
				currentCam.depthTextureMode |= DepthTextureMode.Depth;
			}
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00005E3C File Offset: 0x0000403C
		public void Update()
		{
			if (this.sharedMaterial)
			{
				this.UpdateShader();
			}
		}

		// Token: 0x040000C0 RID: 192
		public Material sharedMaterial;

		// Token: 0x040000C1 RID: 193
		public WaterQuality waterQuality = WaterQuality.High;

		// Token: 0x040000C2 RID: 194
		public bool edgeBlend = true;
	}
}
