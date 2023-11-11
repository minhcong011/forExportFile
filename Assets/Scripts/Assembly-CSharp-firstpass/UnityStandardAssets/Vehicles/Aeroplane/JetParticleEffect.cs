using System;
using UnityEngine;

namespace UnityStandardAssets.Vehicles.Aeroplane
{
	// Token: 0x0200004C RID: 76
	[RequireComponent(typeof(ParticleSystem))]
	public class JetParticleEffect : MonoBehaviour
	{
		// Token: 0x060001A3 RID: 419 RVA: 0x000095C0 File Offset: 0x000077C0
		private void Start()
		{
			this.m_Jet = this.FindAeroplaneParent();
			this.m_System = base.GetComponent<ParticleSystem>();
			this.m_OriginalLifetime = this.m_System.main.startLifetime.constant;
			this.m_OriginalStartSize = this.m_System.main.startSize.constant;
			this.m_OriginalStartColor = this.m_System.main.startColor.color;
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x00009648 File Offset: 0x00007848
		private void Update()
		{
			ParticleSystem.MainModule main = this.m_System.main;
			main.startLifetime = Mathf.Lerp(0f, this.m_OriginalLifetime, this.m_Jet.Throttle);
			main.startSize = Mathf.Lerp(this.m_OriginalStartSize * 0.3f, this.m_OriginalStartSize, this.m_Jet.Throttle);
			main.startColor = Color.Lerp(this.minColour, this.m_OriginalStartColor, this.m_Jet.Throttle);
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x000096E0 File Offset: 0x000078E0
		private AeroplaneController FindAeroplaneParent()
		{
			Transform transform = base.transform;
			while (transform != null)
			{
				AeroplaneController component = transform.GetComponent<AeroplaneController>();
				if (!(component == null))
				{
					return component;
				}
				transform = transform.parent;
			}
			throw new Exception(" AeroplaneContoller not found in object hierarchy");
		}

		// Token: 0x040001A8 RID: 424
		public Color minColour;

		// Token: 0x040001A9 RID: 425
		private AeroplaneController m_Jet;

		// Token: 0x040001AA RID: 426
		private ParticleSystem m_System;

		// Token: 0x040001AB RID: 427
		private float m_OriginalStartSize;

		// Token: 0x040001AC RID: 428
		private float m_OriginalLifetime;

		// Token: 0x040001AD RID: 429
		private Color m_OriginalStartColor;
	}
}
