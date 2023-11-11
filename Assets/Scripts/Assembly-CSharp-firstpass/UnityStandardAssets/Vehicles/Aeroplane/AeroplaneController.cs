using System;
using UnityEngine;

namespace UnityStandardAssets.Vehicles.Aeroplane
{
	// Token: 0x02000047 RID: 71
	[RequireComponent(typeof(Rigidbody))]
	public class AeroplaneController : MonoBehaviour
	{
		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600016E RID: 366 RVA: 0x0000884C File Offset: 0x00006A4C
		// (set) Token: 0x0600016F RID: 367 RVA: 0x00008854 File Offset: 0x00006A54
		public float Altitude { get; private set; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000170 RID: 368 RVA: 0x0000885D File Offset: 0x00006A5D
		// (set) Token: 0x06000171 RID: 369 RVA: 0x00008865 File Offset: 0x00006A65
		public float Throttle { get; private set; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000172 RID: 370 RVA: 0x0000886E File Offset: 0x00006A6E
		// (set) Token: 0x06000173 RID: 371 RVA: 0x00008876 File Offset: 0x00006A76
		public bool AirBrakes { get; private set; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000174 RID: 372 RVA: 0x0000887F File Offset: 0x00006A7F
		// (set) Token: 0x06000175 RID: 373 RVA: 0x00008887 File Offset: 0x00006A87
		public float ForwardSpeed { get; private set; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000176 RID: 374 RVA: 0x00008890 File Offset: 0x00006A90
		// (set) Token: 0x06000177 RID: 375 RVA: 0x00008898 File Offset: 0x00006A98
		public float EnginePower { get; private set; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000178 RID: 376 RVA: 0x000088A1 File Offset: 0x00006AA1
		public float MaxEnginePower
		{
			get
			{
				return this.m_MaxEnginePower;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000179 RID: 377 RVA: 0x000088A9 File Offset: 0x00006AA9
		// (set) Token: 0x0600017A RID: 378 RVA: 0x000088B1 File Offset: 0x00006AB1
		public float RollAngle { get; private set; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600017B RID: 379 RVA: 0x000088BA File Offset: 0x00006ABA
		// (set) Token: 0x0600017C RID: 380 RVA: 0x000088C2 File Offset: 0x00006AC2
		public float PitchAngle { get; private set; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600017D RID: 381 RVA: 0x000088CB File Offset: 0x00006ACB
		// (set) Token: 0x0600017E RID: 382 RVA: 0x000088D3 File Offset: 0x00006AD3
		public float RollInput { get; private set; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600017F RID: 383 RVA: 0x000088DC File Offset: 0x00006ADC
		// (set) Token: 0x06000180 RID: 384 RVA: 0x000088E4 File Offset: 0x00006AE4
		public float PitchInput { get; private set; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000181 RID: 385 RVA: 0x000088ED File Offset: 0x00006AED
		// (set) Token: 0x06000182 RID: 386 RVA: 0x000088F5 File Offset: 0x00006AF5
		public float YawInput { get; private set; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000183 RID: 387 RVA: 0x000088FE File Offset: 0x00006AFE
		// (set) Token: 0x06000184 RID: 388 RVA: 0x00008906 File Offset: 0x00006B06
		public float ThrottleInput { get; private set; }

		// Token: 0x06000185 RID: 389 RVA: 0x00008910 File Offset: 0x00006B10
		private void Start()
		{
			this.m_Rigidbody = base.GetComponent<Rigidbody>();
			this.m_OriginalDrag = this.m_Rigidbody.drag;
			this.m_OriginalAngularDrag = this.m_Rigidbody.angularDrag;
			for (int i = 0; i < base.transform.childCount; i++)
			{
				WheelCollider[] componentsInChildren = base.transform.GetChild(i).GetComponentsInChildren<WheelCollider>();
				for (int j = 0; j < componentsInChildren.Length; j++)
				{
					componentsInChildren[j].motorTorque = 0.18f;
				}
			}
		}

		// Token: 0x06000186 RID: 390 RVA: 0x00008990 File Offset: 0x00006B90
		public void Move(float rollInput, float pitchInput, float yawInput, float throttleInput, bool airBrakes)
		{
			this.RollInput = rollInput;
			this.PitchInput = pitchInput;
			this.YawInput = yawInput;
			this.ThrottleInput = throttleInput;
			this.AirBrakes = airBrakes;
			this.ClampInputs();
			this.CalculateRollAndPitchAngles();
			this.AutoLevel();
			this.CalculateForwardSpeed();
			this.ControlThrottle();
			this.CalculateDrag();
			this.CaluclateAerodynamicEffect();
			this.CalculateLinearForces();
			this.CalculateTorque();
			this.CalculateAltitude();
		}

		// Token: 0x06000187 RID: 391 RVA: 0x00008A00 File Offset: 0x00006C00
		private void ClampInputs()
		{
			this.RollInput = Mathf.Clamp(this.RollInput, -1f, 1f);
			this.PitchInput = Mathf.Clamp(this.PitchInput, -1f, 1f);
			this.YawInput = Mathf.Clamp(this.YawInput, -1f, 1f);
			this.ThrottleInput = Mathf.Clamp(this.ThrottleInput, -1f, 1f);
		}

		// Token: 0x06000188 RID: 392 RVA: 0x00008A7C File Offset: 0x00006C7C
		private void CalculateRollAndPitchAngles()
		{
			Vector3 forward = base.transform.forward;
			forward.y = 0f;
			if (forward.sqrMagnitude > 0f)
			{
				forward.Normalize();
				Vector3 vector = base.transform.InverseTransformDirection(forward);
				this.PitchAngle = Mathf.Atan2(vector.y, vector.z);
				Vector3 direction = Vector3.Cross(Vector3.up, forward);
				Vector3 vector2 = base.transform.InverseTransformDirection(direction);
				this.RollAngle = Mathf.Atan2(vector2.y, vector2.x);
			}
		}

		// Token: 0x06000189 RID: 393 RVA: 0x00008B0C File Offset: 0x00006D0C
		private void AutoLevel()
		{
			this.m_BankedTurnAmount = Mathf.Sin(this.RollAngle);
			if (this.RollInput == 0f)
			{
				this.RollInput = -this.RollAngle * this.m_AutoRollLevel;
			}
			if (this.PitchInput == 0f)
			{
				this.PitchInput = -this.PitchAngle * this.m_AutoPitchLevel;
				this.PitchInput -= Mathf.Abs(this.m_BankedTurnAmount * this.m_BankedTurnAmount * this.m_AutoTurnPitch);
			}
		}

		// Token: 0x0600018A RID: 394 RVA: 0x00008B94 File Offset: 0x00006D94
		private void CalculateForwardSpeed()
		{
			Vector3 vector = base.transform.InverseTransformDirection(this.m_Rigidbody.velocity);
			this.ForwardSpeed = Mathf.Max(0f, vector.z);
		}

		// Token: 0x0600018B RID: 395 RVA: 0x00008BD0 File Offset: 0x00006DD0
		private void ControlThrottle()
		{
			if (this.m_Immobilized)
			{
				this.ThrottleInput = -0.5f;
			}
			this.Throttle = Mathf.Clamp01(this.Throttle + this.ThrottleInput * Time.deltaTime * this.m_ThrottleChangeSpeed);
			this.EnginePower = this.Throttle * this.m_MaxEnginePower;
		}

		// Token: 0x0600018C RID: 396 RVA: 0x00008C28 File Offset: 0x00006E28
		private void CalculateDrag()
		{
			float num = this.m_Rigidbody.velocity.magnitude * this.m_DragIncreaseFactor;
			this.m_Rigidbody.drag = (this.AirBrakes ? ((this.m_OriginalDrag + num) * this.m_AirBrakesEffect) : (this.m_OriginalDrag + num));
			this.m_Rigidbody.angularDrag = this.m_OriginalAngularDrag * this.ForwardSpeed;
		}

		// Token: 0x0600018D RID: 397 RVA: 0x00008C94 File Offset: 0x00006E94
		private void CaluclateAerodynamicEffect()
		{
			if (this.m_Rigidbody.velocity.magnitude > 0f)
			{
				this.m_AeroFactor = Vector3.Dot(base.transform.forward, this.m_Rigidbody.velocity.normalized);
				this.m_AeroFactor *= this.m_AeroFactor;
				Vector3 velocity = Vector3.Lerp(this.m_Rigidbody.velocity, base.transform.forward * this.ForwardSpeed, this.m_AeroFactor * this.ForwardSpeed * this.m_AerodynamicEffect * Time.deltaTime);
				this.m_Rigidbody.velocity = velocity;
				this.m_Rigidbody.rotation = Quaternion.Slerp(this.m_Rigidbody.rotation, Quaternion.LookRotation(this.m_Rigidbody.velocity, base.transform.up), this.m_AerodynamicEffect * Time.deltaTime);
			}
		}

		// Token: 0x0600018E RID: 398 RVA: 0x00008D8C File Offset: 0x00006F8C
		private void CalculateLinearForces()
		{
			Vector3 vector = Vector3.zero;
			vector += this.EnginePower * base.transform.forward;
			Vector3 normalized = Vector3.Cross(this.m_Rigidbody.velocity, base.transform.right).normalized;
			float num = Mathf.InverseLerp(this.m_ZeroLiftSpeed, 0f, this.ForwardSpeed);
			float d = this.ForwardSpeed * this.ForwardSpeed * this.m_Lift * num * this.m_AeroFactor;
			vector += d * normalized;
			this.m_Rigidbody.AddForce(vector);
		}

		// Token: 0x0600018F RID: 399 RVA: 0x00008E30 File Offset: 0x00007030
		private void CalculateTorque()
		{
			Vector3 a = Vector3.zero;
			a += this.PitchInput * this.m_PitchEffect * base.transform.right;
			a += this.YawInput * this.m_YawEffect * base.transform.up;
			a += -this.RollInput * this.m_RollEffect * base.transform.forward;
			a += this.m_BankedTurnAmount * this.m_BankedTurnEffect * base.transform.up;
			this.m_Rigidbody.AddTorque(a * this.ForwardSpeed * this.m_AeroFactor);
		}

		// Token: 0x06000190 RID: 400 RVA: 0x00008EF8 File Offset: 0x000070F8
		private void CalculateAltitude()
		{
			Ray ray = new Ray(base.transform.position - Vector3.up * 10f, -Vector3.up);
			RaycastHit raycastHit;
			this.Altitude = (Physics.Raycast(ray, out raycastHit) ? (raycastHit.distance + 10f) : base.transform.position.y);
		}

		// Token: 0x06000191 RID: 401 RVA: 0x00008F64 File Offset: 0x00007164
		public void Immobilize()
		{
			this.m_Immobilized = true;
		}

		// Token: 0x06000192 RID: 402 RVA: 0x00008F6D File Offset: 0x0000716D
		public void Reset()
		{
			this.m_Immobilized = false;
		}

		// Token: 0x04000171 RID: 369
		[SerializeField]
		private float m_MaxEnginePower = 40f;

		// Token: 0x04000172 RID: 370
		[SerializeField]
		private float m_Lift = 0.002f;

		// Token: 0x04000173 RID: 371
		[SerializeField]
		private float m_ZeroLiftSpeed = 300f;

		// Token: 0x04000174 RID: 372
		[SerializeField]
		private float m_RollEffect = 1f;

		// Token: 0x04000175 RID: 373
		[SerializeField]
		private float m_PitchEffect = 1f;

		// Token: 0x04000176 RID: 374
		[SerializeField]
		private float m_YawEffect = 0.2f;

		// Token: 0x04000177 RID: 375
		[SerializeField]
		private float m_BankedTurnEffect = 0.5f;

		// Token: 0x04000178 RID: 376
		[SerializeField]
		private float m_AerodynamicEffect = 0.02f;

		// Token: 0x04000179 RID: 377
		[SerializeField]
		private float m_AutoTurnPitch = 0.5f;

		// Token: 0x0400017A RID: 378
		[SerializeField]
		private float m_AutoRollLevel = 0.2f;

		// Token: 0x0400017B RID: 379
		[SerializeField]
		private float m_AutoPitchLevel = 0.2f;

		// Token: 0x0400017C RID: 380
		[SerializeField]
		private float m_AirBrakesEffect = 3f;

		// Token: 0x0400017D RID: 381
		[SerializeField]
		private float m_ThrottleChangeSpeed = 0.3f;

		// Token: 0x0400017E RID: 382
		[SerializeField]
		private float m_DragIncreaseFactor = 0.001f;

		// Token: 0x0400018A RID: 394
		private float m_OriginalDrag;

		// Token: 0x0400018B RID: 395
		private float m_OriginalAngularDrag;

		// Token: 0x0400018C RID: 396
		private float m_AeroFactor;

		// Token: 0x0400018D RID: 397
		private bool m_Immobilized;

		// Token: 0x0400018E RID: 398
		private float m_BankedTurnAmount;

		// Token: 0x0400018F RID: 399
		private Rigidbody m_Rigidbody;

		// Token: 0x04000190 RID: 400
		private WheelCollider[] m_WheelColliders;
	}
}
