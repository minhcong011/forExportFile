// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: ControlFreak2.Demos.Characters.DualStickShooterCharaAnimator
using System;
using UnityEngine;

namespace ControlFreak2.Demos.Characters
{
	public class DualStickShooterCharaAnimator : MonoBehaviour
	{
		private void OnEnable()
		{
			if (this.animator == null)
			{
				this.animator = base.GetComponent<Animator>();
			}
			if (this.charaMotor == null)
			{
				this.charaMotor = base.GetComponent<DualStickShooterCharaMotor>();
			}
		}

		private void Update()
		{
			if (this.animator == null || this.charaMotor == null)
			{
				return;
			}
			Vector2 vector = new Vector2(this.charaMotor.GetLocalDir().x, this.charaMotor.GetLocalDir().z);
			if (!string.IsNullOrEmpty(this.speedParamName))
			{
				this.animator.SetFloat(this.speedParamName, vector.magnitude);
			}
			if (!string.IsNullOrEmpty(this.forwardBackwardParamName))
			{
				this.animator.SetFloat(this.forwardBackwardParamName, vector.y);
			}
			if (!string.IsNullOrEmpty(this.strafeParamName))
			{
				this.animator.SetFloat(this.strafeParamName, vector.x);
			}
		}

		public DualStickShooterCharaMotor charaMotor;

		public Animator animator;

		public string forwardBackwardParamName = "Vertical";

		public string strafeParamName = "Horizontal";

		public string speedParamName = "Speed";
	}
}
