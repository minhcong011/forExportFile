using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Utility;

namespace UnityStandardAssets.Characters.FirstPerson
{
	// Token: 0x02000053 RID: 83
	[RequireComponent(typeof(CharacterController))]
	[RequireComponent(typeof(AudioSource))]
	public class FirstPersonController : MonoBehaviour
	{
		// Token: 0x060001C8 RID: 456 RVA: 0x0000A260 File Offset: 0x00008460
		private void Start()
		{
			this.m_CharacterController = base.GetComponent<CharacterController>();
			this.m_Camera = Camera.main;
			this.m_OriginalCameraPosition = this.m_Camera.transform.localPosition;
			this.m_FovKick.Setup(this.m_Camera);
			this.m_HeadBob.Setup(this.m_Camera, this.m_StepInterval);
			this.m_StepCycle = 0f;
			this.m_NextStep = this.m_StepCycle / 2f;
			this.m_Jumping = false;
			this.m_AudioSource = base.GetComponent<AudioSource>();
			this.m_MouseLook.Init(base.transform, this.m_Camera.transform);
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x0000A310 File Offset: 0x00008510
		private void Update()
		{
			this.RotateView();
			if (!this.m_Jump)
			{
				this.m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");
			}
			if (!this.m_PreviouslyGrounded && this.m_CharacterController.isGrounded)
			{
				base.StartCoroutine(this.m_JumpBob.DoBobCycle());
				this.PlayLandingSound();
				this.m_MoveDir.y = 0f;
				this.m_Jumping = false;
			}
			if (!this.m_CharacterController.isGrounded && !this.m_Jumping && this.m_PreviouslyGrounded)
			{
				this.m_MoveDir.y = 0f;
			}
			this.m_PreviouslyGrounded = this.m_CharacterController.isGrounded;
		}

		// Token: 0x060001CA RID: 458 RVA: 0x0000A3BD File Offset: 0x000085BD
		private void PlayLandingSound()
		{
			this.m_AudioSource.clip = this.m_LandSound;
			this.m_AudioSource.Play();
			this.m_NextStep = this.m_StepCycle + 0.5f;
		}

		// Token: 0x060001CB RID: 459 RVA: 0x0000A3F0 File Offset: 0x000085F0
		private void FixedUpdate()
		{
			float num;
			this.GetInput(out num);
			Vector3 vector = base.transform.forward * this.m_Input.y + base.transform.right * this.m_Input.x;
			RaycastHit raycastHit;
			Physics.SphereCast(base.transform.position, this.m_CharacterController.radius, Vector3.down, out raycastHit, this.m_CharacterController.height / 2f, -1, QueryTriggerInteraction.Ignore);
			vector = Vector3.ProjectOnPlane(vector, raycastHit.normal).normalized;
			this.m_MoveDir.x = vector.x * num;
			this.m_MoveDir.z = vector.z * num;
			if (this.m_CharacterController.isGrounded)
			{
				this.m_MoveDir.y = -this.m_StickToGroundForce;
				if (this.m_Jump)
				{
					this.m_MoveDir.y = this.m_JumpSpeed;
					this.PlayJumpSound();
					this.m_Jump = false;
					this.m_Jumping = true;
				}
			}
			else
			{
				this.m_MoveDir += Physics.gravity * this.m_GravityMultiplier * Time.fixedDeltaTime;
			}
			this.m_CollisionFlags = this.m_CharacterController.Move(this.m_MoveDir * Time.fixedDeltaTime);
			this.ProgressStepCycle(num);
			this.UpdateCameraPosition(num);
			this.m_MouseLook.UpdateCursorLock();
		}

		// Token: 0x060001CC RID: 460 RVA: 0x0000A566 File Offset: 0x00008766
		private void PlayJumpSound()
		{
			this.m_AudioSource.clip = this.m_JumpSound;
			this.m_AudioSource.Play();
		}

		// Token: 0x060001CD RID: 461 RVA: 0x0000A584 File Offset: 0x00008784
		private void ProgressStepCycle(float speed)
		{
			if (this.m_CharacterController.velocity.sqrMagnitude > 0f && (this.m_Input.x != 0f || this.m_Input.y != 0f))
			{
				this.m_StepCycle += (this.m_CharacterController.velocity.magnitude + speed * (this.m_IsWalking ? 1f : this.m_RunstepLenghten)) * Time.fixedDeltaTime;
			}
			if (this.m_StepCycle <= this.m_NextStep)
			{
				return;
			}
			this.m_NextStep = this.m_StepCycle + this.m_StepInterval;
			this.PlayFootStepAudio();
		}

		// Token: 0x060001CE RID: 462 RVA: 0x0000A638 File Offset: 0x00008838
		private void PlayFootStepAudio()
		{
			if (!this.m_CharacterController.isGrounded)
			{
				return;
			}
			int num = Random.Range(1, this.m_FootstepSounds.Length);
			this.m_AudioSource.clip = this.m_FootstepSounds[num];
			this.m_AudioSource.PlayOneShot(this.m_AudioSource.clip);
			this.m_FootstepSounds[num] = this.m_FootstepSounds[0];
			this.m_FootstepSounds[0] = this.m_AudioSource.clip;
		}

		// Token: 0x060001CF RID: 463 RVA: 0x0000A6B0 File Offset: 0x000088B0
		private void UpdateCameraPosition(float speed)
		{
			if (!this.m_UseHeadBob)
			{
				return;
			}
			Vector3 localPosition;
			if (this.m_CharacterController.velocity.magnitude > 0f && this.m_CharacterController.isGrounded)
			{
				this.m_Camera.transform.localPosition = this.m_HeadBob.DoHeadBob(this.m_CharacterController.velocity.magnitude + speed * (this.m_IsWalking ? 1f : this.m_RunstepLenghten));
				localPosition = this.m_Camera.transform.localPosition;
				localPosition.y = this.m_Camera.transform.localPosition.y - this.m_JumpBob.Offset();
			}
			else
			{
				localPosition = this.m_Camera.transform.localPosition;
				localPosition.y = this.m_OriginalCameraPosition.y - this.m_JumpBob.Offset();
			}
			this.m_Camera.transform.localPosition = localPosition;
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x0000A7B4 File Offset: 0x000089B4
		private void GetInput(out float speed)
		{
			float axis = CrossPlatformInputManager.GetAxis("Horizontal");
			float axis2 = CrossPlatformInputManager.GetAxis("Vertical");
			bool isWalking = this.m_IsWalking;
			speed = (this.m_IsWalking ? this.m_WalkSpeed : this.m_RunSpeed);
			this.m_Input = new Vector2(axis, axis2);
			if (this.m_Input.sqrMagnitude > 1f)
			{
				this.m_Input.Normalize();
			}
			if (this.m_IsWalking != isWalking && this.m_UseFovKick && this.m_CharacterController.velocity.sqrMagnitude > 0f)
			{
				base.StopAllCoroutines();
				base.StartCoroutine((!this.m_IsWalking) ? this.m_FovKick.FOVKickUp() : this.m_FovKick.FOVKickDown());
			}
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x0000A878 File Offset: 0x00008A78
		private void RotateView()
		{
			this.m_MouseLook.LookRotation(base.transform, this.m_Camera.transform);
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x0000A898 File Offset: 0x00008A98
		private void OnControllerColliderHit(ControllerColliderHit hit)
		{
			Rigidbody attachedRigidbody = hit.collider.attachedRigidbody;
			if (this.m_CollisionFlags == CollisionFlags.Below)
			{
				return;
			}
			if (attachedRigidbody == null || attachedRigidbody.isKinematic)
			{
				return;
			}
			attachedRigidbody.AddForceAtPosition(this.m_CharacterController.velocity * 0.1f, hit.point, ForceMode.Impulse);
		}

		// Token: 0x040001DB RID: 475
		[SerializeField]
		private bool m_IsWalking;

		// Token: 0x040001DC RID: 476
		[SerializeField]
		private float m_WalkSpeed;

		// Token: 0x040001DD RID: 477
		[SerializeField]
		private float m_RunSpeed;

		// Token: 0x040001DE RID: 478
		[SerializeField]
		[Range(0f, 1f)]
		private float m_RunstepLenghten;

		// Token: 0x040001DF RID: 479
		[SerializeField]
		private float m_JumpSpeed;

		// Token: 0x040001E0 RID: 480
		[SerializeField]
		private float m_StickToGroundForce;

		// Token: 0x040001E1 RID: 481
		[SerializeField]
		private float m_GravityMultiplier;

		// Token: 0x040001E2 RID: 482
		[SerializeField]
		private MouseLook m_MouseLook;

		// Token: 0x040001E3 RID: 483
		[SerializeField]
		private bool m_UseFovKick;

		// Token: 0x040001E4 RID: 484
		[SerializeField]
		private FOVKick m_FovKick = new FOVKick();

		// Token: 0x040001E5 RID: 485
		[SerializeField]
		private bool m_UseHeadBob;

		// Token: 0x040001E6 RID: 486
		[SerializeField]
		private CurveControlledBob m_HeadBob = new CurveControlledBob();

		// Token: 0x040001E7 RID: 487
		[SerializeField]
		private LerpControlledBob m_JumpBob = new LerpControlledBob();

		// Token: 0x040001E8 RID: 488
		[SerializeField]
		private float m_StepInterval;

		// Token: 0x040001E9 RID: 489
		[SerializeField]
		private AudioClip[] m_FootstepSounds;

		// Token: 0x040001EA RID: 490
		[SerializeField]
		private AudioClip m_JumpSound;

		// Token: 0x040001EB RID: 491
		[SerializeField]
		private AudioClip m_LandSound;

		// Token: 0x040001EC RID: 492
		private Camera m_Camera;

		// Token: 0x040001ED RID: 493
		private bool m_Jump;

		// Token: 0x040001EE RID: 494
		private float m_YRotation;

		// Token: 0x040001EF RID: 495
		private Vector2 m_Input;

		// Token: 0x040001F0 RID: 496
		private Vector3 m_MoveDir = Vector3.zero;

		// Token: 0x040001F1 RID: 497
		private CharacterController m_CharacterController;

		// Token: 0x040001F2 RID: 498
		private CollisionFlags m_CollisionFlags;

		// Token: 0x040001F3 RID: 499
		private bool m_PreviouslyGrounded;

		// Token: 0x040001F4 RID: 500
		private Vector3 m_OriginalCameraPosition;

		// Token: 0x040001F5 RID: 501
		private float m_StepCycle;

		// Token: 0x040001F6 RID: 502
		private float m_NextStep;

		// Token: 0x040001F7 RID: 503
		private bool m_Jumping;

		// Token: 0x040001F8 RID: 504
		private AudioSource m_AudioSource;
	}
}
