using System;
using UnityEngine;

namespace UnityStandardAssets.Water
{
	// Token: 0x0200002D RID: 45
	[ExecuteInEditMode]
	public class WaterTile : MonoBehaviour
	{
		// Token: 0x060000A7 RID: 167 RVA: 0x00005E67 File Offset: 0x00004067
		public void Start()
		{
			this.AcquireComponents();
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00005E70 File Offset: 0x00004070
		private void AcquireComponents()
		{
			if (!this.reflection)
			{
				if (base.transform.parent)
				{
					this.reflection = base.transform.parent.GetComponent<PlanarReflection>();
				}
				else
				{
					this.reflection = base.transform.GetComponent<PlanarReflection>();
				}
			}
			if (!this.waterBase)
			{
				if (base.transform.parent)
				{
					this.waterBase = base.transform.parent.GetComponent<WaterBase>();
					return;
				}
				this.waterBase = base.transform.GetComponent<WaterBase>();
			}
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00005F0C File Offset: 0x0000410C
		public void OnWillRenderObject()
		{
			if (this.reflection)
			{
				this.reflection.WaterTileBeingRendered(base.transform, Camera.current);
			}
			if (this.waterBase)
			{
				this.waterBase.WaterTileBeingRendered(base.transform, Camera.current);
			}
		}

		// Token: 0x040000C3 RID: 195
		public PlanarReflection reflection;

		// Token: 0x040000C4 RID: 196
		public WaterBase waterBase;
	}
}
