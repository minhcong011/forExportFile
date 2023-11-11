using UnityEngine;
using UnityStandardAssets.Utility;

// Token: 0x0200001B RID: 27
[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(AudioSource))]
public class FirstPersonController : MonoBehaviour
{
	// Token: 0x06000061 RID: 97 RVA: 0x00004518 File Offset: 0x00002718
	private void Start()
	{
		Debug.Log("FistPersonController");
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

	// Token: 0x06000062 RID: 98 RVA: 0x000045C8 File Offset: 0x000027C8
	private void Update()
	{
		if (VariblesGlobal.PositionUP)
		{
			this.m_RunSpeed = 4f + VariblesGlobal.EnergySpeed;
		}
		else
		{
			this.m_RunSpeed = 2f + VariblesGlobal.EnergySpeed / 2f;
		}
		this.RotateView();
		this.m_Jump = this.JumpAxis;
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

	// Token: 0x06000063 RID: 99 RVA: 0x0000469A File Offset: 0x0000289A
	private void PlayLandingSound()
	{
		this.m_AudioSource.clip = this.m_LandSound;
		this.m_AudioSource.Play();
		this.m_NextStep = this.m_StepCycle + 0.5f;
	}

	// Token: 0x06000064 RID: 100 RVA: 0x000046CC File Offset: 0x000028CC
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
				Debug.Log("#" + this.m_Jump.ToString());
				this.m_MoveDir.y = this.m_JumpSpeed;
				this.PlayJumpSound();
				this.m_Jump = false;
				this.m_Jumping = true;
				Debug.Log("#" + this.m_Jump.ToString());
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

	// Token: 0x06000065 RID: 101 RVA: 0x00004879 File Offset: 0x00002A79
	private void PlayJumpSound()
	{
		this.m_AudioSource.clip = this.m_JumpSound;
		this.m_AudioSource.Play();
	}

	// Token: 0x06000066 RID: 102 RVA: 0x00004898 File Offset: 0x00002A98
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

	// Token: 0x06000067 RID: 103 RVA: 0x0000494C File Offset: 0x00002B4C
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

	// Token: 0x06000068 RID: 104 RVA: 0x000049C4 File Offset: 0x00002BC4
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

	// Token: 0x06000069 RID: 105 RVA: 0x00004AC8 File Offset: 0x00002CC8
	private void GetInput(out float speed)
	{
		float x = this.RunAxis.x;
		float y = this.RunAxis.y;
		bool isWalking = this.m_IsWalking;
		speed = (this.m_IsWalking ? this.m_WalkSpeed : this.m_RunSpeed);
		this.m_Input = new Vector2(x, y);
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

	// Token: 0x0600006A RID: 106 RVA: 0x00004B8E File Offset: 0x00002D8E
	private void RotateView()
	{
		this.m_MouseLook.LookRotation(base.transform, this.m_Camera.transform);
	}

	// Token: 0x0600006B RID: 107 RVA: 0x00004BAC File Offset: 0x00002DAC
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

	// Token: 0x040000A9 RID: 169
	[SerializeField]
	private bool m_IsWalking;

	// Token: 0x040000AA RID: 170
	[SerializeField]
	private float m_WalkSpeed;

	// Token: 0x040000AB RID: 171
	[SerializeField]
	private float m_RunSpeed;

	// Token: 0x040000AC RID: 172
	[SerializeField]
	[Range(0f, 1f)]
	private float m_RunstepLenghten;

	// Token: 0x040000AD RID: 173
	[SerializeField]
	private float m_JumpSpeed;

	// Token: 0x040000AE RID: 174
	[SerializeField]
	private float m_StickToGroundForce;

	// Token: 0x040000AF RID: 175
	[SerializeField]
	private float m_GravityMultiplier;

	// Token: 0x040000B0 RID: 176
	public MouseLook m_MouseLook;

	// Token: 0x040000B1 RID: 177
	[SerializeField]
	private bool m_UseFovKick;

	// Token: 0x040000B2 RID: 178
	[SerializeField]
	private FOVKick m_FovKick = new FOVKick();

	// Token: 0x040000B3 RID: 179
	[SerializeField]
	private bool m_UseHeadBob;

	// Token: 0x040000B4 RID: 180
	[SerializeField]
	private CurveControlledBob m_HeadBob = new CurveControlledBob();

	// Token: 0x040000B5 RID: 181
	[SerializeField]
	private LerpControlledBob m_JumpBob = new LerpControlledBob();

	// Token: 0x040000B6 RID: 182
	[SerializeField]
	private float m_StepInterval;

	// Token: 0x040000B7 RID: 183
	[SerializeField]
	private AudioClip[] m_FootstepSounds;

	// Token: 0x040000B8 RID: 184
	[SerializeField]
	private AudioClip m_JumpSound;

	// Token: 0x040000B9 RID: 185
	[SerializeField]
	private AudioClip m_LandSound;

	// Token: 0x040000BA RID: 186
	private float TimeLeftenergy;

	// Token: 0x040000BB RID: 187
	private Camera m_Camera;

	// Token: 0x040000BC RID: 188
	private bool m_Jump;

	// Token: 0x040000BD RID: 189
	private float m_YRotation;

	// Token: 0x040000BE RID: 190
	private Vector2 m_Input;

	// Token: 0x040000BF RID: 191
	private Vector3 m_MoveDir = Vector3.zero;

	// Token: 0x040000C0 RID: 192
	private CharacterController m_CharacterController;

	// Token: 0x040000C1 RID: 193
	private CollisionFlags m_CollisionFlags;

	// Token: 0x040000C2 RID: 194
	private bool m_PreviouslyGrounded;

	// Token: 0x040000C3 RID: 195
	private Vector3 m_OriginalCameraPosition;

	// Token: 0x040000C4 RID: 196
	private float m_StepCycle;

	// Token: 0x040000C5 RID: 197
	private float m_NextStep;

	// Token: 0x040000C6 RID: 198
	private bool m_Jumping;

	// Token: 0x040000C7 RID: 199
	private AudioSource m_AudioSource;

	// Token: 0x040000C8 RID: 200
	[HideInInspector]
	public Vector2 RunAxis;

	// Token: 0x040000C9 RID: 201
	[HideInInspector]
	public bool JumpAxis;
}
