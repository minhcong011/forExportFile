// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: ControlFreak2.Internal.TouchControlAnimatorBase
using System;
using UnityEngine;

namespace ControlFreak2.Internal
{
	[ExecuteInEditMode]
	public abstract class TouchControlAnimatorBase : ComponentBase
	{
		public TouchControlAnimatorBase(Type sourceType)
		{
			this.sourceType = sourceType;
		}

		protected abstract void OnUpdateAnimator(bool skipAnim);

		public void UpdateAnimator(bool skipAnim)
		{
			if (this.sourceControl == null)
			{
				return;
			}
			this.OnUpdateAnimator(skipAnim);
		}

		public void SetSourceControl(TouchControl c)
		{
			if (this.sourceControl != null)
			{
				this.sourceControl.RemoveAnimator(this);
			}
			if (c != null && !c.CanBeUsed())
			{
				c = null;
			}
			this.sourceControl = c;
			if (this.sourceControl != null)
			{
				this.sourceControl.AddAnimator(this);
			}
		}

		public Type GetSourceControlType()
		{
			return this.sourceType;
		}

		public TouchControl FindAutoSource()
		{
			return (TouchControl)base.GetComponentInParent(this.sourceType);
		}

		public void AutoConnectToSource()
		{
			TouchControl x = this.FindAutoSource();
			if (x != null)
			{
				this.SetSourceControl(x);
			}
		}

		public bool IsIllegallyAttachedToSource()
		{
			return this.sourceControl != null && this.sourceControl.gameObject == base.gameObject;
		}

		public void InvalidateHierarchy()
		{
			if (this.autoConnectToSource && this.sourceControl == null)
			{
				this.SetSourceControl(this.FindAutoSource());
			}
		}

		protected override void OnInitComponent()
		{
			if (this.autoConnectToSource || this.sourceControl == null)
			{
				this.SetSourceControl(this.FindAutoSource());
			}
		}

		protected override void OnDestroyComponent()
		{
		}

		protected override void OnEnableComponent()
		{
		}

		protected override void OnDisableComponent()
		{
		}

		public bool autoConnectToSource;

		public TouchControl sourceControl;

		protected Type sourceType;
	}
}
