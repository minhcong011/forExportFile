/*using System;
using UnityEngine;

namespace UnityStandardAssets.Characters.FirstPerson
{
	// Token: 0x020000AF RID: 175
	[RequireComponent(typeof(Rigidbody))]
	[RequireComponent(typeof(CapsuleCollider))]
	public class RigidbodyFirstPersonController : MonoBehaviour
	{
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000321 RID: 801 RVA: 0x000136EF File Offset: 0x000118EF
		public Vector3 Velocity
		{
			get
			{
				return this.m_RigidBody.velocity;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000322 RID: 802 RVA: 0x000136FC File Offset: 0x000118FC
		public bool Grounded
		{
			get
			{
				return this.m_IsGrounded;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000323 RID: 803 RVA: 0x00013704 File Offset: 0x00011904
		public bool Jumping
		{
			get
			{
				return this.m_Jumping;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000324 RID: 804 RVA: 0x0001370C File Offset: 0x0001190C
		public bool Running
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000325 RID: 805 RVA: 0x0001370F File Offset: 0x0001190F
		private void Start()
		{
			this.m_RigidBody = base.GetComponent<Rigidbody>();
			this.m_Capsule = base.GetComponent<CapsuleCollider>();
			this.mouseLook.Init(base.transform, this.cam.transform);
		}

		// Token: 0x06000326 RID: 806 RVA: 0x00013745 File Offset: 0x00011945
		private void Update()
		{
			this.RotateView();
			if (this.JumpAxis && !this.m_Jump)
			{
				this.m_Jump = true;
			}
		}

		// Token: 0x06000327 RID: 807 RVA: 0x00013764 File Offset: 0x00011964
		private void FixedUpdate()
		{
			this.GroundCheck();
			Vector2 input = this.GetInput();
			if ((Mathf.Abs(input.x) > 1.401298E-45f || Mathf.Abs(input.y) > 1.401298E-45f) && (this.advancedSettings.airControl || this.m_IsGrounded))
			{
				Vector3 vector = this.cam.transform.forward * input.y + this.cam.transform.right * input.x;
				vector = Vector3.ProjectOnPlane(vector, this.m_GroundContactNormal).normalized;
				vector.x *= this.movementSettings.CurrentTargetSpeed;
				vector.z *= this.movementSettings.CurrentTargetSpeed;
				vector.y *= this.movementSettings.CurrentTargetSpeed;
				if (this.m_RigidBody.velocity.sqrMagnitude < this.movementSettings.CurrentTargetSpeed * this.movementSettings.CurrentTargetSpeed)
				{
					this.m_RigidBody.AddForce(vector * this.SlopeMultiplier(), ForceMode.Impulse);
				}
			}
			if (this.m_IsGrounded)
			{
				this.m_RigidBody.drag = 5f;
				if (this.m_Jump)
				{
					this.m_RigidBody.drag = 0f;
					this.m_RigidBody.velocity = new Vector3(this.m_RigidBody.velocity.x, 0f, this.m_RigidBody.velocity.z);
					this.m_RigidBody.AddForce(new Vector3(0f, this.movementSettings.JumpForce, 0f), ForceMode.Impulse);
					this.m_Jumping = true;
				}
				if (!this.m_Jumping && Mathf.Abs(input.x) < 1.401298E-45f && Mathf.Abs(input.y) < 1.401298E-45f && this.m_RigidBody.velocity.magnitude < 1f)
				{
					this.m_RigidBody.Sleep();
				}
			}
			else
			{
				this.m_RigidBody.drag = 0f;
				if (this.m_PreviouslyGrounded && !this.m_Jumping)
				{
					this.StickToGroundHelper();
				}
			}
			this.m_Jump = false;
		}

		// Token: 0x06000328 RID: 808 RVA: 0x000139B4 File Offset: 0x00011BB4
		private float SlopeMultiplier()
		{
			float time = Vector3.Angle(this.m_GroundContactNormal, Vector3.up);
			return this.movementSettings.SlopeCurveModifier.Evaluate(time);
		}

		// Token: 0x06000329 RID: 809 RVA: 0x000139E4 File Offset: 0x00011BE4
		private void StickToGroundHelper()
		{
			RaycastHit raycastHit;
			if (Physics.SphereCast(base.transform.position, this.m_Capsule.radius * (1f - this.advancedSettings.shellOffset), Vector3.down, out raycastHit, this.m_Capsule.height / 2f - this.m_Capsule.radius + this.advancedSettings.stickToGroundHelperDistance, -1, QueryTriggerInteraction.Ignore) && Mathf.Abs(Vector3.Angle(raycastHit.normal, Vector3.up)) < 85f)
			{
				this.m_RigidBody.velocity = Vector3.ProjectOnPlane(this.m_RigidBody.velocity, raycastHit.normal);
			}
		}

		// Token: 0x0600032A RID: 810 RVA: 0x00013A94 File Offset: 0x00011C94
		private Vector2 GetInput()
		{
			Vector2 vector = new Vector2
			{
				x = this.RunAxis.x,
				y = this.RunAxis.y
			};
			this.movementSettings.UpdateDesiredTargetSpeed(vector);
			return vector;
		}

		// Token: 0x0600032B RID: 811 RVA: 0x00013ADC File Offset: 0x00011CDC
		private void RotateView()
		{
			if (Mathf.Abs(Time.timeScale) < 1.401298E-45f)
			{
				return;
			}
			float y = base.transform.eulerAngles.y;
			this.mouseLook.LookRotation(base.transform, this.cam.transform);
			if (this.m_IsGrounded || this.advancedSettings.airControl)
			{
				Quaternion rotation = Quaternion.AngleAxis(base.transform.eulerAngles.y - y, Vector3.up);
				this.m_RigidBody.velocity = rotation * this.m_RigidBody.velocity;
			}
		}

		// Token: 0x0600032C RID: 812 RVA: 0x00013B78 File Offset: 0x00011D78
		private void GroundCheck()
		{
			this.m_PreviouslyGrounded = this.m_IsGrounded;
			RaycastHit raycastHit;
			if (Physics.SphereCast(base.transform.position, this.m_Capsule.radius * (1f - this.advancedSettings.shellOffset), Vector3.down, out raycastHit, this.m_Capsule.height / 2f - this.m_Capsule.radius + this.advancedSettings.groundCheckDistance, -1, QueryTriggerInteraction.Ignore))
			{
				this.m_IsGrounded = true;
				this.m_GroundContactNormal = raycastHit.normal;
			}
			else
			{
				this.m_IsGrounded = false;
				this.m_GroundContactNormal = Vector3.up;
			}
			if (!this.m_PreviouslyGrounded && this.m_IsGrounded && this.m_Jumping)
			{
				this.m_Jumping = false;
			}
		}

		// Token: 0x04000358 RID: 856
		public Camera cam;

		// Token: 0x04000359 RID: 857
		public RigidbodyFirstPersonController.MovementSettings movementSettings = new RigidbodyFirstPersonController.MovementSettings();

		// Token: 0x0400035A RID: 858
		public MouseLook mouseLook = new MouseLook();

		// Token: 0x0400035B RID: 859
		public RigidbodyFirstPersonController.AdvancedSettings advancedSettings = new RigidbodyFirstPersonController.AdvancedSettings();

		// Token: 0x0400035C RID: 860
		private Rigidbody m_RigidBody;

		// Token: 0x0400035D RID: 861
		private CapsuleCollider m_Capsule;

		// Token: 0x0400035E RID: 862
		private float m_YRotation;

		// Token: 0x0400035F RID: 863
		private Vector3 m_GroundContactNormal;

		// Token: 0x04000360 RID: 864
		private bool m_Jump;

		// Token: 0x04000361 RID: 865
		private bool m_PreviouslyGrounded;

		// Token: 0x04000362 RID: 866
		private bool m_Jumping;

		// Token: 0x04000363 RID: 867
		private bool m_IsGrounded;

		// Token: 0x04000364 RID: 868
		[HideInInspector]
		public Vector2 RunAxis;

		// Token: 0x04000365 RID: 869
		[HideInInspector]
		public bool JumpAxis;

		// Token: 0x020000D9 RID: 217
		[Serializable]
		public class MovementSettings
		{
			// Token: 0x060004F8 RID: 1272 RVA: 0x000178E4 File Offset: 0x00015AE4
			public void UpdateDesiredTargetSpeed(Vector2 input)
			{
				if (input == Vector2.zero)
				{
					return;
				}
				if (input.x > 0f || input.x < 0f)
				{
					this.CurrentTargetSpeed = this.StrafeSpeed;
				}
				if (input.y < 0f)
				{
					this.CurrentTargetSpeed = this.BackwardSpeed;
				}
				if (input.y > 0f)
				{
					this.CurrentTargetSpeed = this.ForwardSpeed;
				}
			}

			// Token: 0x04000419 RID: 1049
			public float ForwardSpeed = 8f;

			// Token: 0x0400041A RID: 1050
			public float BackwardSpeed = 4f;

			// Token: 0x0400041B RID: 1051
			public float StrafeSpeed = 4f;

			// Token: 0x0400041C RID: 1052
			public float RunMultiplier = 2f;

			// Token: 0x0400041D RID: 1053
			public KeyCode RunKey = KeyCode.LeftShift;

			// Token: 0x0400041E RID: 1054
			public float JumpForce = 30f;

			// Token: 0x0400041F RID: 1055
			public AnimationCurve SlopeCurveModifier = new AnimationCurve(new Keyframe[]
			{
				new Keyframe(-90f, 1f),
				new Keyframe(0f, 1f),
				new Keyframe(90f, 0f)
			});

			// Token: 0x04000420 RID: 1056
			[HideInInspector]
			public float CurrentTargetSpeed = 8f;
		}

		// Token: 0x020000DA RID: 218
		[Serializable]
		public class AdvancedSettings
		{
			// Token: 0x04000421 RID: 1057
			public float groundCheckDistance = 0.01f;

			// Token: 0x04000422 RID: 1058
			public float stickToGroundHelperDistance = 0.5f;

			// Token: 0x04000423 RID: 1059
			public float slowDownRate = 20f;

			// Token: 0x04000424 RID: 1060
			public bool airControl;

			// Token: 0x04000425 RID: 1061
			[Tooltip("set it to 0.1 or more if you get stuck in wall")]
			public float shellOffset;
		}
	}
}
*/