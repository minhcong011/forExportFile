using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets.Characters.FirstPerson
{
	// Token: 0x02000056 RID: 86
	[RequireComponent(typeof(Rigidbody))]
	[RequireComponent(typeof(CapsuleCollider))]
	public class RigidbodyFirstPersonController : MonoBehaviour
	{
		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060001DE RID: 478 RVA: 0x0000AD2F File Offset: 0x00008F2F
		public Vector3 Velocity
		{
			get
			{
				return this.m_RigidBody.velocity;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060001DF RID: 479 RVA: 0x0000AD3C File Offset: 0x00008F3C
		public bool Grounded
		{
			get
			{
				return this.m_IsGrounded;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060001E0 RID: 480 RVA: 0x0000AD44 File Offset: 0x00008F44
		public bool Jumping
		{
			get
			{
				return this.m_Jumping;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060001E1 RID: 481 RVA: 0x0000AD4C File Offset: 0x00008F4C
		public bool Running
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x0000AD4F File Offset: 0x00008F4F
		private void Start()
		{
			this.m_RigidBody = base.GetComponent<Rigidbody>();
			this.m_Capsule = base.GetComponent<CapsuleCollider>();
			this.mouseLook.Init(base.transform, this.cam.transform);
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x0000AD85 File Offset: 0x00008F85
		private void Update()
		{
			this.RotateView();
			if (CrossPlatformInputManager.GetButtonDown("Jump") && !this.m_Jump)
			{
				this.m_Jump = true;
			}
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x0000ADA8 File Offset: 0x00008FA8
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

		// Token: 0x060001E5 RID: 485 RVA: 0x0000AFF8 File Offset: 0x000091F8
		private float SlopeMultiplier()
		{
			float time = Vector3.Angle(this.m_GroundContactNormal, Vector3.up);
			return this.movementSettings.SlopeCurveModifier.Evaluate(time);
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x0000B028 File Offset: 0x00009228
		private void StickToGroundHelper()
		{
			RaycastHit raycastHit;
			if (Physics.SphereCast(base.transform.position, this.m_Capsule.radius * (1f - this.advancedSettings.shellOffset), Vector3.down, out raycastHit, this.m_Capsule.height / 2f - this.m_Capsule.radius + this.advancedSettings.stickToGroundHelperDistance, -1, QueryTriggerInteraction.Ignore) && Mathf.Abs(Vector3.Angle(raycastHit.normal, Vector3.up)) < 85f)
			{
				this.m_RigidBody.velocity = Vector3.ProjectOnPlane(this.m_RigidBody.velocity, raycastHit.normal);
			}
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x0000B0D8 File Offset: 0x000092D8
		private Vector2 GetInput()
		{
			Vector2 vector = new Vector2
			{
				x = CrossPlatformInputManager.GetAxis("Horizontal"),
				y = CrossPlatformInputManager.GetAxis("Vertical")
			};
			this.movementSettings.UpdateDesiredTargetSpeed(vector);
			return vector;
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x0000B120 File Offset: 0x00009320
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

		// Token: 0x060001E9 RID: 489 RVA: 0x0000B1BC File Offset: 0x000093BC
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

		// Token: 0x0400020C RID: 524
		public Camera cam;

		// Token: 0x0400020D RID: 525
		public RigidbodyFirstPersonController.MovementSettings movementSettings = new RigidbodyFirstPersonController.MovementSettings();

		// Token: 0x0400020E RID: 526
		public MouseLook mouseLook = new MouseLook();

		// Token: 0x0400020F RID: 527
		public RigidbodyFirstPersonController.AdvancedSettings advancedSettings = new RigidbodyFirstPersonController.AdvancedSettings();

		// Token: 0x04000210 RID: 528
		private Rigidbody m_RigidBody;

		// Token: 0x04000211 RID: 529
		private CapsuleCollider m_Capsule;

		// Token: 0x04000212 RID: 530
		private float m_YRotation;

		// Token: 0x04000213 RID: 531
		private Vector3 m_GroundContactNormal;

		// Token: 0x04000214 RID: 532
		private bool m_Jump;

		// Token: 0x04000215 RID: 533
		private bool m_PreviouslyGrounded;

		// Token: 0x04000216 RID: 534
		private bool m_Jumping;

		// Token: 0x04000217 RID: 535
		private bool m_IsGrounded;

		// Token: 0x0200008B RID: 139
		[Serializable]
		public class MovementSettings
		{
			// Token: 0x06000295 RID: 661 RVA: 0x0000D6D0 File Offset: 0x0000B8D0
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

			// Token: 0x04000302 RID: 770
			public float ForwardSpeed = 8f;

			// Token: 0x04000303 RID: 771
			public float BackwardSpeed = 4f;

			// Token: 0x04000304 RID: 772
			public float StrafeSpeed = 4f;

			// Token: 0x04000305 RID: 773
			public float RunMultiplier = 2f;

			// Token: 0x04000306 RID: 774
			public KeyCode RunKey = KeyCode.LeftShift;

			// Token: 0x04000307 RID: 775
			public float JumpForce = 30f;

			// Token: 0x04000308 RID: 776
			public AnimationCurve SlopeCurveModifier = new AnimationCurve(new Keyframe[]
			{
				new Keyframe(-90f, 1f),
				new Keyframe(0f, 1f),
				new Keyframe(90f, 0f)
			});

			// Token: 0x04000309 RID: 777
			[HideInInspector]
			public float CurrentTargetSpeed = 8f;
		}

		// Token: 0x0200008C RID: 140
		[Serializable]
		public class AdvancedSettings
		{
			// Token: 0x0400030A RID: 778
			public float groundCheckDistance = 0.01f;

			// Token: 0x0400030B RID: 779
			public float stickToGroundHelperDistance = 0.5f;

			// Token: 0x0400030C RID: 780
			public float slowDownRate = 20f;

			// Token: 0x0400030D RID: 781
			public bool airControl;

			// Token: 0x0400030E RID: 782
			[Tooltip("set it to 0.1 or more if you get stuck in wall")]
			public float shellOffset;
		}
	}
}
