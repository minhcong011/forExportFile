using System;
using UnityEngine;

namespace UnityStandardAssets.Vehicles.Aeroplane
{
	// Token: 0x02000048 RID: 72
	public class AeroplaneControlSurfaceAnimator : MonoBehaviour
	{
		// Token: 0x06000194 RID: 404 RVA: 0x00009028 File Offset: 0x00007228
		private void Start()
		{
			this.m_Plane = base.GetComponent<AeroplaneController>();
			foreach (AeroplaneControlSurfaceAnimator.ControlSurface controlSurface in this.m_ControlSurfaces)
			{
				controlSurface.originalLocalRotation = controlSurface.transform.localRotation;
			}
		}

		// Token: 0x06000195 RID: 405 RVA: 0x0000906C File Offset: 0x0000726C
		private void Update()
		{
			foreach (AeroplaneControlSurfaceAnimator.ControlSurface controlSurface in this.m_ControlSurfaces)
			{
				switch (controlSurface.type)
				{
				case AeroplaneControlSurfaceAnimator.ControlSurface.Type.Aileron:
				{
					Quaternion rotation = Quaternion.Euler(controlSurface.amount * this.m_Plane.RollInput, 0f, 0f);
					this.RotateSurface(controlSurface, rotation);
					break;
				}
				case AeroplaneControlSurfaceAnimator.ControlSurface.Type.Elevator:
				{
					Quaternion rotation2 = Quaternion.Euler(controlSurface.amount * -this.m_Plane.PitchInput, 0f, 0f);
					this.RotateSurface(controlSurface, rotation2);
					break;
				}
				case AeroplaneControlSurfaceAnimator.ControlSurface.Type.Rudder:
				{
					Quaternion rotation3 = Quaternion.Euler(0f, controlSurface.amount * this.m_Plane.YawInput, 0f);
					this.RotateSurface(controlSurface, rotation3);
					break;
				}
				case AeroplaneControlSurfaceAnimator.ControlSurface.Type.RuddervatorNegative:
				{
					float num = this.m_Plane.YawInput - this.m_Plane.PitchInput;
					Quaternion rotation4 = Quaternion.Euler(0f, 0f, controlSurface.amount * num);
					this.RotateSurface(controlSurface, rotation4);
					break;
				}
				case AeroplaneControlSurfaceAnimator.ControlSurface.Type.RuddervatorPositive:
				{
					float num2 = this.m_Plane.YawInput + this.m_Plane.PitchInput;
					Quaternion rotation5 = Quaternion.Euler(0f, 0f, controlSurface.amount * num2);
					this.RotateSurface(controlSurface, rotation5);
					break;
				}
				}
			}
		}

		// Token: 0x06000196 RID: 406 RVA: 0x000091CC File Offset: 0x000073CC
		private void RotateSurface(AeroplaneControlSurfaceAnimator.ControlSurface surface, Quaternion rotation)
		{
			Quaternion b = surface.originalLocalRotation * rotation;
			surface.transform.localRotation = Quaternion.Slerp(surface.transform.localRotation, b, this.m_Smoothing * Time.deltaTime);
		}

		// Token: 0x04000191 RID: 401
		[SerializeField]
		private float m_Smoothing = 5f;

		// Token: 0x04000192 RID: 402
		[SerializeField]
		private AeroplaneControlSurfaceAnimator.ControlSurface[] m_ControlSurfaces;

		// Token: 0x04000193 RID: 403
		private AeroplaneController m_Plane;

		// Token: 0x02000089 RID: 137
		[Serializable]
		public class ControlSurface
		{
			// Token: 0x040002FB RID: 763
			public Transform transform;

			// Token: 0x040002FC RID: 764
			public float amount;

			// Token: 0x040002FD RID: 765
			public AeroplaneControlSurfaceAnimator.ControlSurface.Type type;

			// Token: 0x040002FE RID: 766
			[HideInInspector]
			public Quaternion originalLocalRotation;

			// Token: 0x02000090 RID: 144
			public enum Type
			{
				// Token: 0x04000319 RID: 793
				Aileron,
				// Token: 0x0400031A RID: 794
				Elevator,
				// Token: 0x0400031B RID: 795
				Rudder,
				// Token: 0x0400031C RID: 796
				RuddervatorNegative,
				// Token: 0x0400031D RID: 797
				RuddervatorPositive
			}
		}
	}
}
