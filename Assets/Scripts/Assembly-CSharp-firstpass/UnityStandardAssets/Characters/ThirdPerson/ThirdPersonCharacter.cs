using System;
using UnityEngine;

namespace UnityStandardAssets.Characters.ThirdPerson
{
	// Token: 0x02000051 RID: 81
	[RequireComponent(typeof(Rigidbody))]
	[RequireComponent(typeof(CapsuleCollider))]
	[RequireComponent(typeof(Animator))]
	public class ThirdPersonCharacter : MonoBehaviour
	{
		// Token: 0x060001B9 RID: 441 RVA: 0x00009AEC File Offset: 0x00007CEC
		private void Start()
		{
			this.m_Animator = base.GetComponent<Animator>();
			this.m_Rigidbody = base.GetComponent<Rigidbody>();
			this.m_Capsule = base.GetComponent<CapsuleCollider>();
			this.m_CapsuleHeight = this.m_Capsule.height;
			this.m_CapsuleCenter = this.m_Capsule.center;
			this.m_Rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
			this.m_OrigGroundCheckDistance = this.m_GroundCheckDistance;
		}

		// Token: 0x060001BA RID: 442 RVA: 0x00009B58 File Offset: 0x00007D58
		public void Move(Vector3 move, bool crouch, bool jump)
		{
			if (move.magnitude > 1f)
			{
				move.Normalize();
			}
			move = base.transform.InverseTransformDirection(move);
			this.CheckGroundStatus();
			move = Vector3.ProjectOnPlane(move, this.m_GroundNormal);
			this.m_TurnAmount = Mathf.Atan2(move.x, move.z);
			this.m_ForwardAmount = move.z;
			this.ApplyExtraTurnRotation();
			if (this.m_IsGrounded)
			{
				this.HandleGroundedMovement(crouch, jump);
			}
			else
			{
				this.HandleAirborneMovement();
			}
			this.ScaleCapsuleForCrouching(crouch);
			this.PreventStandingInLowHeadroom();
			this.UpdateAnimator(move);
		}

		// Token: 0x060001BB RID: 443 RVA: 0x00009BF4 File Offset: 0x00007DF4
		private void ScaleCapsuleForCrouching(bool crouch)
		{
			if (this.m_IsGrounded && crouch)
			{
				if (this.m_Crouching)
				{
					return;
				}
				this.m_Capsule.height = this.m_Capsule.height / 2f;
				this.m_Capsule.center = this.m_Capsule.center / 2f;
				this.m_Crouching = true;
				return;
			}
			else
			{
				Ray ray = new Ray(this.m_Rigidbody.position + Vector3.up * this.m_Capsule.radius * 0.5f, Vector3.up);
				float maxDistance = this.m_CapsuleHeight - this.m_Capsule.radius * 0.5f;
				if (Physics.SphereCast(ray, this.m_Capsule.radius * 0.5f, maxDistance, -1, QueryTriggerInteraction.Ignore))
				{
					this.m_Crouching = true;
					return;
				}
				this.m_Capsule.height = this.m_CapsuleHeight;
				this.m_Capsule.center = this.m_CapsuleCenter;
				this.m_Crouching = false;
				return;
			}
		}

		// Token: 0x060001BC RID: 444 RVA: 0x00009CF8 File Offset: 0x00007EF8
		private void PreventStandingInLowHeadroom()
		{
			if (!this.m_Crouching)
			{
				Ray ray = new Ray(this.m_Rigidbody.position + Vector3.up * this.m_Capsule.radius * 0.5f, Vector3.up);
				float maxDistance = this.m_CapsuleHeight - this.m_Capsule.radius * 0.5f;
				if (Physics.SphereCast(ray, this.m_Capsule.radius * 0.5f, maxDistance, -1, QueryTriggerInteraction.Ignore))
				{
					this.m_Crouching = true;
				}
			}
		}

		// Token: 0x060001BD RID: 445 RVA: 0x00009D84 File Offset: 0x00007F84
		private void UpdateAnimator(Vector3 move)
		{
			this.m_Animator.SetFloat("Forward", this.m_ForwardAmount, 0.1f, Time.deltaTime);
			this.m_Animator.SetFloat("Turn", this.m_TurnAmount, 0.1f, Time.deltaTime);
			this.m_Animator.SetBool("Crouch", this.m_Crouching);
			this.m_Animator.SetBool("OnGround", this.m_IsGrounded);
			if (!this.m_IsGrounded)
			{
				this.m_Animator.SetFloat("Jump", this.m_Rigidbody.velocity.y);
			}
			float value = (float)((Mathf.Repeat(this.m_Animator.GetCurrentAnimatorStateInfo(0).normalizedTime + this.m_RunCycleLegOffset, 1f) < 0.5f) ? 1 : -1) * this.m_ForwardAmount;
			if (this.m_IsGrounded)
			{
				this.m_Animator.SetFloat("JumpLeg", value);
			}
			if (this.m_IsGrounded && move.magnitude > 0f)
			{
				this.m_Animator.speed = this.m_AnimSpeedMultiplier;
				return;
			}
			this.m_Animator.speed = 1f;
		}

		// Token: 0x060001BE RID: 446 RVA: 0x00009EB0 File Offset: 0x000080B0
		private void HandleAirborneMovement()
		{
			Vector3 force = Physics.gravity * this.m_GravityMultiplier - Physics.gravity;
			this.m_Rigidbody.AddForce(force);
			this.m_GroundCheckDistance = ((this.m_Rigidbody.velocity.y < 0f) ? this.m_OrigGroundCheckDistance : 0.01f);
		}

		// Token: 0x060001BF RID: 447 RVA: 0x00009F10 File Offset: 0x00008110
		private void HandleGroundedMovement(bool crouch, bool jump)
		{
			if (jump && !crouch && this.m_Animator.GetCurrentAnimatorStateInfo(0).IsName("Grounded"))
			{
				this.m_Rigidbody.velocity = new Vector3(this.m_Rigidbody.velocity.x, this.m_JumpPower, this.m_Rigidbody.velocity.z);
				this.m_IsGrounded = false;
				this.m_Animator.applyRootMotion = false;
				this.m_GroundCheckDistance = 0.1f;
			}
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x00009F94 File Offset: 0x00008194
		private void ApplyExtraTurnRotation()
		{
			float num = Mathf.Lerp(this.m_StationaryTurnSpeed, this.m_MovingTurnSpeed, this.m_ForwardAmount);
			base.transform.Rotate(0f, this.m_TurnAmount * num * Time.deltaTime, 0f);
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x00009FDC File Offset: 0x000081DC
		public void OnAnimatorMove()
		{
			if (this.m_IsGrounded && Time.deltaTime > 0f)
			{
				Vector3 velocity = this.m_Animator.deltaPosition * this.m_MoveSpeedMultiplier / Time.deltaTime;
				velocity.y = this.m_Rigidbody.velocity.y;
				this.m_Rigidbody.velocity = velocity;
			}
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x0000A044 File Offset: 0x00008244
		private void CheckGroundStatus()
		{
			RaycastHit raycastHit;
			if (Physics.Raycast(base.transform.position + Vector3.up * 0.1f, Vector3.down, out raycastHit, this.m_GroundCheckDistance))
			{
				this.m_GroundNormal = raycastHit.normal;
				this.m_IsGrounded = true;
				this.m_Animator.applyRootMotion = true;
				return;
			}
			this.m_IsGrounded = false;
			this.m_GroundNormal = Vector3.up;
			this.m_Animator.applyRootMotion = false;
		}

		// Token: 0x040001C2 RID: 450
		[SerializeField]
		private float m_MovingTurnSpeed = 360f;

		// Token: 0x040001C3 RID: 451
		[SerializeField]
		private float m_StationaryTurnSpeed = 180f;

		// Token: 0x040001C4 RID: 452
		[SerializeField]
		private float m_JumpPower = 12f;

		// Token: 0x040001C5 RID: 453
		[Range(1f, 4f)]
		[SerializeField]
		private float m_GravityMultiplier = 2f;

		// Token: 0x040001C6 RID: 454
		[SerializeField]
		private float m_RunCycleLegOffset = 0.2f;

		// Token: 0x040001C7 RID: 455
		[SerializeField]
		private float m_MoveSpeedMultiplier = 1f;

		// Token: 0x040001C8 RID: 456
		[SerializeField]
		private float m_AnimSpeedMultiplier = 1f;

		// Token: 0x040001C9 RID: 457
		[SerializeField]
		private float m_GroundCheckDistance = 0.1f;

		// Token: 0x040001CA RID: 458
		private Rigidbody m_Rigidbody;

		// Token: 0x040001CB RID: 459
		private Animator m_Animator;

		// Token: 0x040001CC RID: 460
		private bool m_IsGrounded;

		// Token: 0x040001CD RID: 461
		private float m_OrigGroundCheckDistance;

		// Token: 0x040001CE RID: 462
		private const float k_Half = 0.5f;

		// Token: 0x040001CF RID: 463
		private float m_TurnAmount;

		// Token: 0x040001D0 RID: 464
		private float m_ForwardAmount;

		// Token: 0x040001D1 RID: 465
		private Vector3 m_GroundNormal;

		// Token: 0x040001D2 RID: 466
		private float m_CapsuleHeight;

		// Token: 0x040001D3 RID: 467
		private Vector3 m_CapsuleCenter;

		// Token: 0x040001D4 RID: 468
		private CapsuleCollider m_Capsule;

		// Token: 0x040001D5 RID: 469
		private bool m_Crouching;
	}
}
