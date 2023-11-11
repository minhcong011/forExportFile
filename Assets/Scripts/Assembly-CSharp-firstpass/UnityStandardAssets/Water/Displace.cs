using System;
using UnityEngine;

namespace UnityStandardAssets.Water
{
	// Token: 0x02000025 RID: 37
	[ExecuteInEditMode]
	[RequireComponent(typeof(WaterBase))]
	public class Displace : MonoBehaviour
	{
		// Token: 0x0600007F RID: 127 RVA: 0x0000480A File Offset: 0x00002A0A
		public void Awake()
		{
			if (base.enabled)
			{
				this.OnEnable();
				return;
			}
			this.OnDisable();
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00004821 File Offset: 0x00002A21
		public void OnEnable()
		{
			Shader.EnableKeyword("WATER_VERTEX_DISPLACEMENT_ON");
			Shader.DisableKeyword("WATER_VERTEX_DISPLACEMENT_OFF");
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00004837 File Offset: 0x00002A37
		public void OnDisable()
		{
			Shader.EnableKeyword("WATER_VERTEX_DISPLACEMENT_OFF");
			Shader.DisableKeyword("WATER_VERTEX_DISPLACEMENT_ON");
		}
	}
}
