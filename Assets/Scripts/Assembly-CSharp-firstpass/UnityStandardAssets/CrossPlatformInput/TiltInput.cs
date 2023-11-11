using System;
using UnityEngine;

namespace UnityStandardAssets.CrossPlatformInput
{
	// Token: 0x02000034 RID: 52
	public class TiltInput : MonoBehaviour
	{
		// Token: 0x060000E3 RID: 227 RVA: 0x00006594 File Offset: 0x00004794
		private void OnEnable()
		{
			if (this.mapping.type == TiltInput.AxisMapping.MappingType.NamedAxis)
			{
				this.m_SteerAxis = new CrossPlatformInputManager.VirtualAxis(this.mapping.axisName);
				CrossPlatformInputManager.RegisterVirtualAxis(this.m_SteerAxis);
			}
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x000065C4 File Offset: 0x000047C4
		private void Update()
		{
			float value = 0f;
			if (Input.acceleration != Vector3.zero)
			{
				TiltInput.AxisOptions axisOptions = this.tiltAroundAxis;
				if (axisOptions != TiltInput.AxisOptions.ForwardAxis)
				{
					if (axisOptions == TiltInput.AxisOptions.SidewaysAxis)
					{
						value = Mathf.Atan2(Input.acceleration.z, -Input.acceleration.y) * 57.29578f + this.centreAngleOffset;
					}
				}
				else
				{
					value = Mathf.Atan2(Input.acceleration.x, -Input.acceleration.y) * 57.29578f + this.centreAngleOffset;
				}
			}
			float num = Mathf.InverseLerp(-this.fullTiltAngle, this.fullTiltAngle, value) * 2f - 1f;
			switch (this.mapping.type)
			{
			case TiltInput.AxisMapping.MappingType.NamedAxis:
				this.m_SteerAxis.Update(num);
				return;
			case TiltInput.AxisMapping.MappingType.MousePositionX:
				CrossPlatformInputManager.SetVirtualMousePositionX(num * (float)Screen.width);
				return;
			case TiltInput.AxisMapping.MappingType.MousePositionY:
				CrossPlatformInputManager.SetVirtualMousePositionY(num * (float)Screen.width);
				return;
			case TiltInput.AxisMapping.MappingType.MousePositionZ:
				CrossPlatformInputManager.SetVirtualMousePositionZ(num * (float)Screen.width);
				return;
			default:
				return;
			}
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x000066C3 File Offset: 0x000048C3
		private void OnDisable()
		{
			this.m_SteerAxis.Remove();
		}

		// Token: 0x040000D9 RID: 217
		public TiltInput.AxisMapping mapping;

		// Token: 0x040000DA RID: 218
		public TiltInput.AxisOptions tiltAroundAxis;

		// Token: 0x040000DB RID: 219
		public float fullTiltAngle = 25f;

		// Token: 0x040000DC RID: 220
		public float centreAngleOffset;

		// Token: 0x040000DD RID: 221
		private CrossPlatformInputManager.VirtualAxis m_SteerAxis;

		// Token: 0x02000080 RID: 128
		public enum AxisOptions
		{
			// Token: 0x040002DA RID: 730
			ForwardAxis,
			// Token: 0x040002DB RID: 731
			SidewaysAxis
		}

		// Token: 0x02000081 RID: 129
		[Serializable]
		public class AxisMapping
		{
			// Token: 0x040002DC RID: 732
			public TiltInput.AxisMapping.MappingType type;

			// Token: 0x040002DD RID: 733
			public string axisName;

			// Token: 0x0200008F RID: 143
			public enum MappingType
			{
				// Token: 0x04000314 RID: 788
				NamedAxis,
				// Token: 0x04000315 RID: 789
				MousePositionX,
				// Token: 0x04000316 RID: 790
				MousePositionY,
				// Token: 0x04000317 RID: 791
				MousePositionZ
			}
		}
	}
}
