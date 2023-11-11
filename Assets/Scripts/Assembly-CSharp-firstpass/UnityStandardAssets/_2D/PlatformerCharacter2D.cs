using System;
using UnityEngine;

namespace UnityStandardAssets._2D
{
	// Token: 0x02000062 RID: 98
	public class PlatformerCharacter2D : MonoBehaviour
	{
		// Token: 0x0600021A RID: 538 RVA: 0x0000C4BC File Offset: 0x0000A6BC
		private void Awake()
		{
			this.m_GroundCheck = base.transform.Find("GroundCheck");
			this.m_CeilingCheck = base.transform.Find("CeilingCheck");
			this.m_Anim = base.GetComponent<Animator>();
			this.m_Rigidbody2D = base.GetComponent<Rigidbody2D>();
		}

		// Token: 0x0600021B RID: 539 RVA: 0x0000C510 File Offset: 0x0000A710
		private void FixedUpdate()
		{
			this.m_Grounded = false;
			Collider2D[] array = Physics2D.OverlapCircleAll(this.m_GroundCheck.position, 0.2f, this.m_WhatIsGround);
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i].gameObject != base.gameObject)
				{
					this.m_Grounded = true;
				}
			}
			this.m_Anim.SetBool("Ground", this.m_Grounded);
			this.m_Anim.SetFloat("vSpeed", this.m_Rigidbody2D.velocity.y);
		}

		// Token: 0x0600021C RID: 540 RVA: 0x0000C5AC File Offset: 0x0000A7AC
		public void Move(float move, bool crouch, bool jump)
		{
			if (!crouch && this.m_Anim.GetBool("Crouch") && Physics2D.OverlapCircle(this.m_CeilingCheck.position, 0.01f, this.m_WhatIsGround))
			{
				crouch = true;
			}
			this.m_Anim.SetBool("Crouch", crouch);
			if (this.m_Grounded || this.m_AirControl)
			{
				move = (crouch ? (move * this.m_CrouchSpeed) : move);
				this.m_Anim.SetFloat("Speed", Mathf.Abs(move));
				this.m_Rigidbody2D.velocity = new Vector2(move * this.m_MaxSpeed, this.m_Rigidbody2D.velocity.y);
				if (move > 0f && !this.m_FacingRight)
				{
					this.Flip();
				}
				else if (move < 0f && this.m_FacingRight)
				{
					this.Flip();
				}
			}
			if (this.m_Grounded && jump && this.m_Anim.GetBool("Ground"))
			{
				this.m_Grounded = false;
				this.m_Anim.SetBool("Ground", false);
				this.m_Rigidbody2D.AddForce(new Vector2(0f, this.m_JumpForce));
			}
		}

		// Token: 0x0600021D RID: 541 RVA: 0x0000C6EC File Offset: 0x0000A8EC
		private void Flip()
		{
			this.m_FacingRight = !this.m_FacingRight;
			Vector3 localScale = base.transform.localScale;
			localScale.x *= -1f;
			base.transform.localScale = localScale;
		}

		// Token: 0x04000269 RID: 617
		[SerializeField]
		private float m_MaxSpeed = 10f;

		// Token: 0x0400026A RID: 618
		[SerializeField]
		private float m_JumpForce = 400f;

		// Token: 0x0400026B RID: 619
		[Range(0f, 1f)]
		[SerializeField]
		private float m_CrouchSpeed = 0.36f;

		// Token: 0x0400026C RID: 620
		[SerializeField]
		private bool m_AirControl;

		// Token: 0x0400026D RID: 621
		[SerializeField]
		private LayerMask m_WhatIsGround;

		// Token: 0x0400026E RID: 622
		private Transform m_GroundCheck;

		// Token: 0x0400026F RID: 623
		private const float k_GroundedRadius = 0.2f;

		// Token: 0x04000270 RID: 624
		private bool m_Grounded;

		// Token: 0x04000271 RID: 625
		private Transform m_CeilingCheck;

		// Token: 0x04000272 RID: 626
		private const float k_CeilingRadius = 0.01f;

		// Token: 0x04000273 RID: 627
		private Animator m_Anim;

		// Token: 0x04000274 RID: 628
		private Rigidbody2D m_Rigidbody2D;

		// Token: 0x04000275 RID: 629
		private bool m_FacingRight = true;
	}
}
