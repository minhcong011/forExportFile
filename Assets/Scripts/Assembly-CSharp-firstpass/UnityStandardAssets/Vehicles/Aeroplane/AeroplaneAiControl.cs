using UnityEngine;

namespace UnityStandardAssets.Vehicles.Aeroplane
{
	// Token: 0x02000045 RID: 69
	[RequireComponent(typeof(AeroplaneController))]
	public class AeroplaneAiControl : MonoBehaviour
	{
		// Token: 0x06000166 RID: 358 RVA: 0x0000836F File Offset: 0x0000656F
		private void Awake()
		{
			this.m_AeroplaneController = base.GetComponent<AeroplaneController>();
			this.m_RandomPerlin = Random.Range(0f, 100f);
		}

		// Token: 0x06000167 RID: 359 RVA: 0x00008392 File Offset: 0x00006592
		public void Reset()
		{
			this.m_TakenOff = false;
		}

		// Token: 0x06000168 RID: 360 RVA: 0x0000839C File Offset: 0x0000659C
		private void FixedUpdate()
		{
			if (this.m_Target != null)
			{
				Vector3 position = this.m_Target.position + base.transform.right * (Mathf.PerlinNoise(Time.time * this.m_LateralWanderSpeed, this.m_RandomPerlin) * 2f - 1f) * this.m_LateralWanderDistance;
				Vector3 vector = base.transform.InverseTransformPoint(position);
				float num = Mathf.Atan2(vector.x, vector.z);
				float num2 = (Mathf.Clamp(-Mathf.Atan2(vector.y, vector.z), -this.m_MaxClimbAngle * 0.0174532924f, this.m_MaxClimbAngle * 0.0174532924f) - this.m_AeroplaneController.PitchAngle) * this.m_PitchSensitivity;
				float num3 = Mathf.Clamp(num, -this.m_MaxRollAngle * 0.0174532924f, this.m_MaxRollAngle * 0.0174532924f);
				float num4 = 0f;
				float num5 = 0f;
				if (!this.m_TakenOff)
				{
					if (this.m_AeroplaneController.Altitude > this.m_TakeoffHeight)
					{
						this.m_TakenOff = true;
					}
				}
				else
				{
					num4 = num;
					num5 = -(this.m_AeroplaneController.RollAngle - num3) * this.m_RollSensitivity;
				}
				float num6 = 1f + this.m_AeroplaneController.ForwardSpeed * this.m_SpeedEffect;
				num5 *= num6;
				num2 *= num6;
				num4 *= num6;
				this.m_AeroplaneController.Move(num5, num2, num4, 0.5f, false);
				return;
			}
			this.m_AeroplaneController.Move(0f, 0f, 0f, 0f, false);
		}

		// Token: 0x06000169 RID: 361 RVA: 0x0000853E File Offset: 0x0000673E
		public void SetTarget(Transform target)
		{
			this.m_Target = target;
		}

		// Token: 0x04000158 RID: 344
		[SerializeField]
		private float m_RollSensitivity = 0.2f;

		// Token: 0x04000159 RID: 345
		[SerializeField]
		private float m_PitchSensitivity = 0.5f;

		// Token: 0x0400015A RID: 346
		[SerializeField]
		private float m_LateralWanderDistance = 5f;

		// Token: 0x0400015B RID: 347
		[SerializeField]
		private float m_LateralWanderSpeed = 0.11f;

		// Token: 0x0400015C RID: 348
		[SerializeField]
		private float m_MaxClimbAngle = 45f;

		// Token: 0x0400015D RID: 349
		[SerializeField]
		private float m_MaxRollAngle = 45f;

		// Token: 0x0400015E RID: 350
		[SerializeField]
		private float m_SpeedEffect = 0.01f;

		// Token: 0x0400015F RID: 351
		[SerializeField]
		private float m_TakeoffHeight = 20f;

		// Token: 0x04000160 RID: 352
		[SerializeField]
		private Transform m_Target;

		// Token: 0x04000161 RID: 353
		private AeroplaneController m_AeroplaneController;

		// Token: 0x04000162 RID: 354
		private float m_RandomPerlin;

		// Token: 0x04000163 RID: 355
		private bool m_TakenOff;
	}
}
