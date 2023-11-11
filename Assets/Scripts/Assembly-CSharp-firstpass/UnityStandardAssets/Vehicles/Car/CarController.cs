using System;
using UnityEngine;

namespace UnityStandardAssets.Vehicles.Car
{
	// Token: 0x0200003E RID: 62
	public class CarController : MonoBehaviour
	{
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000130 RID: 304 RVA: 0x000076FB File Offset: 0x000058FB
		// (set) Token: 0x06000131 RID: 305 RVA: 0x00007703 File Offset: 0x00005903
		public bool Skidding { get; private set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000132 RID: 306 RVA: 0x0000770C File Offset: 0x0000590C
		// (set) Token: 0x06000133 RID: 307 RVA: 0x00007714 File Offset: 0x00005914
		public float BrakeInput { get; private set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000134 RID: 308 RVA: 0x0000771D File Offset: 0x0000591D
		public float CurrentSteerAngle
		{
			get
			{
				return this.m_SteerAngle;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000135 RID: 309 RVA: 0x00007728 File Offset: 0x00005928
		public float CurrentSpeed
		{
			get
			{
				return this.m_Rigidbody.velocity.magnitude * 2.23693633f;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000136 RID: 310 RVA: 0x0000774E File Offset: 0x0000594E
		public float MaxSpeed
		{
			get
			{
				return this.m_Topspeed;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000137 RID: 311 RVA: 0x00007756 File Offset: 0x00005956
		// (set) Token: 0x06000138 RID: 312 RVA: 0x0000775E File Offset: 0x0000595E
		public float Revs { get; private set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000139 RID: 313 RVA: 0x00007767 File Offset: 0x00005967
		// (set) Token: 0x0600013A RID: 314 RVA: 0x0000776F File Offset: 0x0000596F
		public float AccelInput { get; private set; }

		// Token: 0x0600013B RID: 315 RVA: 0x00007778 File Offset: 0x00005978
		private void Start()
		{
			this.m_WheelMeshLocalRotations = new Quaternion[4];
			for (int i = 0; i < 4; i++)
			{
				this.m_WheelMeshLocalRotations[i] = this.m_WheelMeshes[i].transform.localRotation;
			}
			this.m_WheelColliders[0].attachedRigidbody.centerOfMass = this.m_CentreOfMassOffset;
			this.m_MaxHandbrakeTorque = float.MaxValue;
			this.m_Rigidbody = base.GetComponent<Rigidbody>();
			this.m_CurrentTorque = this.m_FullTorqueOverAllWheels - this.m_TractionControl * this.m_FullTorqueOverAllWheels;
		}

		// Token: 0x0600013C RID: 316 RVA: 0x00007804 File Offset: 0x00005A04
		private void GearChanging()
		{
			float num = Mathf.Abs(this.CurrentSpeed / this.MaxSpeed);
			float num2 = 1f / (float)CarController.NoOfGears * (float)(this.m_GearNum + 1);
			float num3 = 1f / (float)CarController.NoOfGears * (float)this.m_GearNum;
			if (this.m_GearNum > 0 && num < num3)
			{
				this.m_GearNum--;
			}
			if (num > num2 && this.m_GearNum < CarController.NoOfGears - 1)
			{
				this.m_GearNum++;
			}
		}

		// Token: 0x0600013D RID: 317 RVA: 0x0000788C File Offset: 0x00005A8C
		private static float CurveFactor(float factor)
		{
			return 1f - (1f - factor) * (1f - factor);
		}

		// Token: 0x0600013E RID: 318 RVA: 0x00007687 File Offset: 0x00005887
		private static float ULerp(float from, float to, float value)
		{
			return (1f - value) * from + value * to;
		}

		// Token: 0x0600013F RID: 319 RVA: 0x000078A4 File Offset: 0x00005AA4
		private void CalculateGearFactor()
		{
			float num = 1f / (float)CarController.NoOfGears;
			float b = Mathf.InverseLerp(num * (float)this.m_GearNum, num * (float)(this.m_GearNum + 1), Mathf.Abs(this.CurrentSpeed / this.MaxSpeed));
			this.m_GearFactor = Mathf.Lerp(this.m_GearFactor, b, Time.deltaTime * 5f);
		}

		// Token: 0x06000140 RID: 320 RVA: 0x00007908 File Offset: 0x00005B08
		private void CalculateRevs()
		{
			this.CalculateGearFactor();
			float num = (float)this.m_GearNum / (float)CarController.NoOfGears;
			float from = CarController.ULerp(0f, this.m_RevRangeBoundary, CarController.CurveFactor(num));
			float to = CarController.ULerp(this.m_RevRangeBoundary, 1f, num);
			this.Revs = CarController.ULerp(from, to, this.m_GearFactor);
		}

		// Token: 0x06000141 RID: 321 RVA: 0x00007968 File Offset: 0x00005B68
		public void Move(float steering, float accel, float footbrake, float handbrake)
		{
			for (int i = 0; i < 4; i++)
			{
				Vector3 position;
				Quaternion rotation;
				this.m_WheelColliders[i].GetWorldPose(out position, out rotation);
				this.m_WheelMeshes[i].transform.position = position;
				this.m_WheelMeshes[i].transform.rotation = rotation;
			}
			steering = Mathf.Clamp(steering, -1f, 1f);
			accel = (this.AccelInput = Mathf.Clamp(accel, 0f, 1f));
			footbrake = (this.BrakeInput = -1f * Mathf.Clamp(footbrake, -1f, 0f));
			handbrake = Mathf.Clamp(handbrake, 0f, 1f);
			this.m_SteerAngle = steering * this.m_MaximumSteerAngle;
			this.m_WheelColliders[0].steerAngle = this.m_SteerAngle;
			this.m_WheelColliders[1].steerAngle = this.m_SteerAngle;
			this.SteerHelper();
			this.ApplyDrive(accel, footbrake);
			this.CapSpeed();
			if (handbrake > 0f)
			{
				float brakeTorque = handbrake * this.m_MaxHandbrakeTorque;
				this.m_WheelColliders[2].brakeTorque = brakeTorque;
				this.m_WheelColliders[3].brakeTorque = brakeTorque;
			}
			this.CalculateRevs();
			this.GearChanging();
			this.AddDownForce();
			this.CheckForWheelSpin();
			this.TractionControl();
		}

		// Token: 0x06000142 RID: 322 RVA: 0x00007AAC File Offset: 0x00005CAC
		private void CapSpeed()
		{
			float num = this.m_Rigidbody.velocity.magnitude;
			SpeedType speedType = this.m_SpeedType;
			if (speedType != SpeedType.MPH)
			{
				if (speedType != SpeedType.KPH)
				{
					return;
				}
				num *= 3.6f;
				if (num > this.m_Topspeed)
				{
					this.m_Rigidbody.velocity = this.m_Topspeed / 3.6f * this.m_Rigidbody.velocity.normalized;
				}
			}
			else
			{
				num *= 2.23693633f;
				if (num > this.m_Topspeed)
				{
					this.m_Rigidbody.velocity = this.m_Topspeed / 2.23693633f * this.m_Rigidbody.velocity.normalized;
					return;
				}
			}
		}

		// Token: 0x06000143 RID: 323 RVA: 0x00007B60 File Offset: 0x00005D60
		private void ApplyDrive(float accel, float footbrake)
		{
			switch (this.m_CarDriveType)
			{
			case CarDriveType.FrontWheelDrive:
			{
				float motorTorque = accel * (this.m_CurrentTorque / 2f);
				this.m_WheelColliders[0].motorTorque = (this.m_WheelColliders[1].motorTorque = motorTorque);
				break;
			}
			case CarDriveType.RearWheelDrive:
			{
				float motorTorque = accel * (this.m_CurrentTorque / 2f);
				this.m_WheelColliders[2].motorTorque = (this.m_WheelColliders[3].motorTorque = motorTorque);
				break;
			}
			case CarDriveType.FourWheelDrive:
			{
				float motorTorque = accel * (this.m_CurrentTorque / 4f);
				for (int i = 0; i < 4; i++)
				{
					this.m_WheelColliders[i].motorTorque = motorTorque;
				}
				break;
			}
			}
			for (int j = 0; j < 4; j++)
			{
				if (this.CurrentSpeed > 5f && Vector3.Angle(base.transform.forward, this.m_Rigidbody.velocity) < 50f)
				{
					this.m_WheelColliders[j].brakeTorque = this.m_BrakeTorque * footbrake;
				}
				else if (footbrake > 0f)
				{
					this.m_WheelColliders[j].brakeTorque = 0f;
					this.m_WheelColliders[j].motorTorque = -this.m_ReverseTorque * footbrake;
				}
			}
		}

		// Token: 0x06000144 RID: 324 RVA: 0x00007CA0 File Offset: 0x00005EA0
		private void SteerHelper()
		{
			for (int i = 0; i < 4; i++)
			{
				WheelHit wheelHit;
				this.m_WheelColliders[i].GetGroundHit(out wheelHit);
				if (wheelHit.normal == Vector3.zero)
				{
					return;
				}
			}
			if (Mathf.Abs(this.m_OldRotation - base.transform.eulerAngles.y) < 10f)
			{
				Quaternion rotation = Quaternion.AngleAxis((base.transform.eulerAngles.y - this.m_OldRotation) * this.m_SteerHelper, Vector3.up);
				this.m_Rigidbody.velocity = rotation * this.m_Rigidbody.velocity;
			}
			this.m_OldRotation = base.transform.eulerAngles.y;
		}

		// Token: 0x06000145 RID: 325 RVA: 0x00007D5C File Offset: 0x00005F5C
		private void AddDownForce()
		{
			this.m_WheelColliders[0].attachedRigidbody.AddForce(-base.transform.up * this.m_Downforce * this.m_WheelColliders[0].attachedRigidbody.velocity.magnitude);
		}

		// Token: 0x06000146 RID: 326 RVA: 0x00007DB8 File Offset: 0x00005FB8
		private void CheckForWheelSpin()
		{
			for (int i = 0; i < 4; i++)
			{
				WheelHit wheelHit;
				this.m_WheelColliders[i].GetGroundHit(out wheelHit);
				if (Mathf.Abs(wheelHit.forwardSlip) >= this.m_SlipLimit || Mathf.Abs(wheelHit.sidewaysSlip) >= this.m_SlipLimit)
				{
					this.m_WheelEffects[i].EmitTyreSmoke();
					if (!this.AnySkidSoundPlaying())
					{
						this.m_WheelEffects[i].PlayAudio();
					}
				}
				else
				{
					if (this.m_WheelEffects[i].PlayingAudio)
					{
						this.m_WheelEffects[i].StopAudio();
					}
					this.m_WheelEffects[i].EndSkidTrail();
				}
			}
		}

		// Token: 0x06000147 RID: 327 RVA: 0x00007E5C File Offset: 0x0000605C
		private void TractionControl()
		{
			switch (this.m_CarDriveType)
			{
			case CarDriveType.FrontWheelDrive:
			{
				WheelHit wheelHit;
				this.m_WheelColliders[0].GetGroundHit(out wheelHit);
				this.AdjustTorque(wheelHit.forwardSlip);
				this.m_WheelColliders[1].GetGroundHit(out wheelHit);
				this.AdjustTorque(wheelHit.forwardSlip);
				return;
			}
			case CarDriveType.RearWheelDrive:
			{
				WheelHit wheelHit;
				this.m_WheelColliders[2].GetGroundHit(out wheelHit);
				this.AdjustTorque(wheelHit.forwardSlip);
				this.m_WheelColliders[3].GetGroundHit(out wheelHit);
				this.AdjustTorque(wheelHit.forwardSlip);
				return;
			}
			case CarDriveType.FourWheelDrive:
				for (int i = 0; i < 4; i++)
				{
					WheelHit wheelHit;
					this.m_WheelColliders[i].GetGroundHit(out wheelHit);
					this.AdjustTorque(wheelHit.forwardSlip);
				}
				return;
			default:
				return;
			}
		}

		// Token: 0x06000148 RID: 328 RVA: 0x00007F24 File Offset: 0x00006124
		private void AdjustTorque(float forwardSlip)
		{
			if (forwardSlip >= this.m_SlipLimit && this.m_CurrentTorque >= 0f)
			{
				this.m_CurrentTorque -= 10f * this.m_TractionControl;
				return;
			}
			this.m_CurrentTorque += 10f * this.m_TractionControl;
			if (this.m_CurrentTorque > this.m_FullTorqueOverAllWheels)
			{
				this.m_CurrentTorque = this.m_FullTorqueOverAllWheels;
			}
		}

		// Token: 0x06000149 RID: 329 RVA: 0x00007F94 File Offset: 0x00006194
		private bool AnySkidSoundPlaying()
		{
			for (int i = 0; i < 4; i++)
			{
				if (this.m_WheelEffects[i].PlayingAudio)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x04000125 RID: 293
		[SerializeField]
		private CarDriveType m_CarDriveType = CarDriveType.FourWheelDrive;

		// Token: 0x04000126 RID: 294
		[SerializeField]
		private WheelCollider[] m_WheelColliders = new WheelCollider[4];

		// Token: 0x04000127 RID: 295
		[SerializeField]
		private GameObject[] m_WheelMeshes = new GameObject[4];

		// Token: 0x04000128 RID: 296
		[SerializeField]
		private WheelEffects[] m_WheelEffects = new WheelEffects[4];

		// Token: 0x04000129 RID: 297
		[SerializeField]
		private Vector3 m_CentreOfMassOffset;

		// Token: 0x0400012A RID: 298
		[SerializeField]
		private float m_MaximumSteerAngle;

		// Token: 0x0400012B RID: 299
		[Range(0f, 1f)]
		[SerializeField]
		private float m_SteerHelper;

		// Token: 0x0400012C RID: 300
		[Range(0f, 1f)]
		[SerializeField]
		private float m_TractionControl;

		// Token: 0x0400012D RID: 301
		[SerializeField]
		private float m_FullTorqueOverAllWheels;

		// Token: 0x0400012E RID: 302
		[SerializeField]
		private float m_ReverseTorque;

		// Token: 0x0400012F RID: 303
		[SerializeField]
		private float m_MaxHandbrakeTorque;

		// Token: 0x04000130 RID: 304
		[SerializeField]
		private float m_Downforce = 100f;

		// Token: 0x04000131 RID: 305
		[SerializeField]
		private SpeedType m_SpeedType;

		// Token: 0x04000132 RID: 306
		[SerializeField]
		private float m_Topspeed = 200f;

		// Token: 0x04000133 RID: 307
		[SerializeField]
		private static int NoOfGears = 5;

		// Token: 0x04000134 RID: 308
		[SerializeField]
		private float m_RevRangeBoundary = 1f;

		// Token: 0x04000135 RID: 309
		[SerializeField]
		private float m_SlipLimit;

		// Token: 0x04000136 RID: 310
		[SerializeField]
		private float m_BrakeTorque;

		// Token: 0x04000137 RID: 311
		private Quaternion[] m_WheelMeshLocalRotations;

		// Token: 0x04000138 RID: 312
		private Vector3 m_Prevpos;

		// Token: 0x04000139 RID: 313
		private Vector3 m_Pos;

		// Token: 0x0400013A RID: 314
		private float m_SteerAngle;

		// Token: 0x0400013B RID: 315
		private int m_GearNum;

		// Token: 0x0400013C RID: 316
		private float m_GearFactor;

		// Token: 0x0400013D RID: 317
		private float m_OldRotation;

		// Token: 0x0400013E RID: 318
		private float m_CurrentTorque;

		// Token: 0x0400013F RID: 319
		private Rigidbody m_Rigidbody;

		// Token: 0x04000140 RID: 320
		private const float k_ReversingThreshold = 0.01f;
	}
}
