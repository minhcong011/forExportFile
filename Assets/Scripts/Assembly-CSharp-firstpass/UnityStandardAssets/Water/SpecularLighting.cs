using System;
using UnityEngine;

namespace UnityStandardAssets.Water
{
	// Token: 0x02000029 RID: 41
	[RequireComponent(typeof(WaterBase))]
	[ExecuteInEditMode]
	public class SpecularLighting : MonoBehaviour
	{
		// Token: 0x06000096 RID: 150 RVA: 0x000050A5 File Offset: 0x000032A5
		public void Start()
		{
			this.m_WaterBase = (WaterBase)base.gameObject.GetComponent(typeof(WaterBase));
		}

		// Token: 0x06000097 RID: 151 RVA: 0x000050C8 File Offset: 0x000032C8
		public void Update()
		{
			if (!this.m_WaterBase)
			{
				this.m_WaterBase = (WaterBase)base.gameObject.GetComponent(typeof(WaterBase));
			}
			if (this.specularLight && this.m_WaterBase.sharedMaterial)
			{
				this.m_WaterBase.sharedMaterial.SetVector("_WorldLightDir", this.specularLight.transform.forward);
			}
		}

		// Token: 0x040000AC RID: 172
		public Transform specularLight;

		// Token: 0x040000AD RID: 173
		private WaterBase m_WaterBase;
	}
}
