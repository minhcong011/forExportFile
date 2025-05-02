// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: ControlFreak2.DynamicRegion
using System;

namespace ControlFreak2
{
	public class DynamicRegion : TouchControl
	{
		public DynamicRegion()
		{
			this.ignoreFingerRadius = true;
		}

		public DynamicTouchControl GetTargetControl()
		{
			return this.targetControl;
		}

		public void SetTargetControl(DynamicTouchControl targetControl)
		{
			if (this.targetControl == targetControl)
			{
				return;
			}
			if (this.targetControl != null)
			{
				if (this.targetControl.GetDynamicRegion() == this)
				{
					return;
				}
				this.targetControl.OnLinkToDynamicRegion(null);
			}
			this.targetControl = targetControl;
			this.targetControl.OnLinkToDynamicRegion(this);
		}

		protected override void OnInitControl()
		{
			this.ResetControl();
		}

		protected override void OnUpdateControl()
		{
		}

		protected override void OnDestroyControl()
		{
		}

		public override void ResetControl()
		{
			this.ReleaseAllTouches();
		}

		public override void ReleaseAllTouches()
		{
		}

		public override bool OnTouchStart(TouchObject touch, TouchControl sender, TouchControl.TouchStartType touchStartType)
		{
			return this.targetControl != null && this.targetControl.OnTouchStart(touch, this, TouchControl.TouchStartType.ProxyPress);
		}

		public override bool OnTouchEnd(TouchObject touch, TouchControl.TouchEndType touchEndType)
		{
			return false;
		}

		public override bool OnTouchMove(TouchObject touch)
		{
			return false;
		}

		public override bool OnTouchPressureChange(TouchObject touch)
		{
			return false;
		}

		public override bool CanBeTouchedDirectly(TouchObject touchObj)
		{
			return base.CanBeTouchedDirectly(touchObj) && this.targetControl != null && this.targetControl.CanBeActivatedByDynamicRegion();
		}

		public override bool CanBeSwipedOverFromNothing(TouchObject touchObj)
		{
			return false;
		}

		public override bool CanBeSwipedOverFromRestrictedList(TouchObject touchObj)
		{
			return base.CanBeSwipedOverFromRestrictedListDefault(touchObj) && this.targetControl != null && this.targetControl.CanBeActivatedByDynamicRegion();
		}

		public override bool CanSwipeOverOthers(TouchObject touchObj)
		{
			return false;
		}

		public override bool CanBeActivatedByOtherControl(TouchControl c, TouchObject touchObj)
		{
			return base.CanBeActivatedByOtherControl(c, touchObj) && this.targetControl != null && this.targetControl.CanBeActivatedByDynamicRegion();
		}

		[NonSerialized]
		private DynamicTouchControl targetControl;
	}
}
