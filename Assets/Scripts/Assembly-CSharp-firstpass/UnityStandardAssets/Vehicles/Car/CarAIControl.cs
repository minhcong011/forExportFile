using UnityEngine;

namespace UnityStandardAssets.Vehicles.Car
{
	// Token: 0x0200003A RID: 58
	[RequireComponent(typeof(CarController))]
	public class CarAIControl : MonoBehaviour
	{
		// Token: 0x06000125 RID: 293 RVA: 0x00006DE1 File Offset: 0x00004FE1
		private void Awake()
		{
			this.m_CarController = base.GetComponent<CarController>();
			this.m_RandomPerlin = Random.value * 100f;
			this.m_Rigidbody = base.GetComponent<Rigidbody>();
		}

		// Token: 0x06000126 RID: 294 RVA: 0x00006E0C File Offset: 0x0000500C
		private void FixedUpdate()
		{
			if (this.m_Target == null || !this.m_Driving)
			{
				this.m_CarController.Move(0f, 0f, -1f, 1f);
				return;
			}
			Vector3 to = base.transform.forward;
			if (this.m_Rigidbody.velocity.magnitude > this.m_CarController.MaxSpeed * 0.1f)
			{
				to = this.m_Rigidbody.velocity;
			}
			float num = this.m_CarController.MaxSpeed;
			switch (this.m_BrakeCondition)
			{
			case CarAIControl.BrakeCondition.TargetDirectionDifference:
			{
				float b = Vector3.Angle(this.m_Target.forward, to);
				float a = this.m_Rigidbody.angularVelocity.magnitude * this.m_CautiousAngularVelocityFactor;
				float t = Mathf.InverseLerp(0f, this.m_CautiousMaxAngle, Mathf.Max(a, b));
				num = Mathf.Lerp(this.m_CarController.MaxSpeed, this.m_CarController.MaxSpeed * this.m_CautiousSpeedFactor, t);
				break;
			}
			case CarAIControl.BrakeCondition.TargetDistance:
			{
				Vector3 vector = this.m_Target.position - base.transform.position;
				float b2 = Mathf.InverseLerp(this.m_CautiousMaxDistance, 0f, vector.magnitude);
				float value = this.m_Rigidbody.angularVelocity.magnitude * this.m_CautiousAngularVelocityFactor;
				float t2 = Mathf.Max(Mathf.InverseLerp(0f, this.m_CautiousMaxAngle, value), b2);
				num = Mathf.Lerp(this.m_CarController.MaxSpeed, this.m_CarController.MaxSpeed * this.m_CautiousSpeedFactor, t2);
				break;
			}
			}
			Vector3 vector2 = this.m_Target.position;
			if (Time.time < this.m_AvoidOtherCarTime)
			{
				num *= this.m_AvoidOtherCarSlowdown;
				vector2 += this.m_Target.right * this.m_AvoidPathOffset;
			}
			else
			{
				vector2 += this.m_Target.right * (Mathf.PerlinNoise(Time.time * this.m_LateralWanderSpeed, this.m_RandomPerlin) * 2f - 1f) * this.m_LateralWanderDistance;
			}
			float num2 = (num < this.m_CarController.CurrentSpeed) ? this.m_BrakeSensitivity : this.m_AccelSensitivity;
			float num3 = Mathf.Clamp((num - this.m_CarController.CurrentSpeed) * num2, -1f, 1f);
			num3 *= 1f - this.m_AccelWanderAmount + Mathf.PerlinNoise(Time.time * this.m_AccelWanderSpeed, this.m_RandomPerlin) * this.m_AccelWanderAmount;
			Vector3 vector3 = base.transform.InverseTransformPoint(vector2);
			float steering = Mathf.Clamp(Mathf.Atan2(vector3.x, vector3.z) * 57.29578f * this.m_SteerSensitivity, -1f, 1f) * Mathf.Sign(this.m_CarController.CurrentSpeed);
			this.m_CarController.Move(steering, num3, num3, 0f);
			if (this.m_StopWhenTargetReached && vector3.magnitude < this.m_ReachTargetThreshold)
			{
				this.m_Driving = false;
			}
		}

		// Token: 0x06000127 RID: 295 RVA: 0x0000713C File Offset: 0x0000533C
		private void OnCollisionStay(Collision col)
		{
			if (col.rigidbody != null)
			{
				CarAIControl component = col.rigidbody.GetComponent<CarAIControl>();
				if (component != null)
				{
					this.m_AvoidOtherCarTime = Time.time + 1f;
					if (Vector3.Angle(base.transform.forward, component.transform.position - base.transform.position) < 90f)
					{
						this.m_AvoidOtherCarSlowdown = 0.5f;
					}
					else
					{
						this.m_AvoidOtherCarSlowdown = 1f;
					}
					Vector3 vector = base.transform.InverseTransformPoint(component.transform.position);
					float f = Mathf.Atan2(vector.x, vector.z);
					this.m_AvoidPathOffset = this.m_LateralWanderDistance * -Mathf.Sign(f);
				}
			}
		}

		// Token: 0x06000128 RID: 296 RVA: 0x0000720A File Offset: 0x0000540A
		public void SetTarget(Transform target)
		{
			this.m_Target = target;
			this.m_Driving = true;
		}

		// Token: 0x040000F6 RID: 246
		[SerializeField]
		[Range(0f, 1f)]
		private float m_CautiousSpeedFactor = 0.05f;

		// Token: 0x040000F7 RID: 247
		[SerializeField]
		[Range(0f, 180f)]
		private float m_CautiousMaxAngle = 50f;

		// Token: 0x040000F8 RID: 248
		[SerializeField]
		private float m_CautiousMaxDistance = 100f;

		// Token: 0x040000F9 RID: 249
		[SerializeField]
		private float m_CautiousAngularVelocityFactor = 30f;

		// Token: 0x040000FA RID: 250
		[SerializeField]
		private float m_SteerSensitivity = 0.05f;

		// Token: 0x040000FB RID: 251
		[SerializeField]
		private float m_AccelSensitivity = 0.04f;

		// Token: 0x040000FC RID: 252
		[SerializeField]
		private float m_BrakeSensitivity = 1f;

		// Token: 0x040000FD RID: 253
		[SerializeField]
		private float m_LateralWanderDistance = 3f;

		// Token: 0x040000FE RID: 254
		[SerializeField]
		private float m_LateralWanderSpeed = 0.1f;

		// Token: 0x040000FF RID: 255
		[SerializeField]
		[Range(0f, 1f)]
		private float m_AccelWanderAmount = 0.1f;

		// Token: 0x04000100 RID: 256
		[SerializeField]
		private float m_AccelWanderSpeed = 0.1f;

		// Token: 0x04000101 RID: 257
		[SerializeField]
		private CarAIControl.BrakeCondition m_BrakeCondition = CarAIControl.BrakeCondition.TargetDistance;

		// Token: 0x04000102 RID: 258
		[SerializeField]
		private bool m_Driving;

		// Token: 0x04000103 RID: 259
		[SerializeField]
		private Transform m_Target;

		// Token: 0x04000104 RID: 260
		[SerializeField]
		private bool m_StopWhenTargetReached;

		// Token: 0x04000105 RID: 261
		[SerializeField]
		private float m_ReachTargetThreshold = 2f;

		// Token: 0x04000106 RID: 262
		private float m_RandomPerlin;

		// Token: 0x04000107 RID: 263
		private CarController m_CarController;

		// Token: 0x04000108 RID: 264
		private float m_AvoidOtherCarTime;

		// Token: 0x04000109 RID: 265
		private float m_AvoidOtherCarSlowdown;

		// Token: 0x0400010A RID: 266
		private float m_AvoidPathOffset;

		// Token: 0x0400010B RID: 267
		private Rigidbody m_Rigidbody;

		// Token: 0x02000084 RID: 132
		public enum BrakeCondition
		{
			// Token: 0x040002E7 RID: 743
			NeverBrake,
			// Token: 0x040002E8 RID: 744
			TargetDirectionDifference,
			// Token: 0x040002E9 RID: 745
			TargetDistance
		}
	}
}
