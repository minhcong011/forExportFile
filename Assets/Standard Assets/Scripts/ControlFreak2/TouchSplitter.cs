// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: ControlFreak2.TouchSplitter
using System;
using System.Collections.Generic;

namespace ControlFreak2
{
	public class TouchSplitter : TouchControl
	{
		public TouchSplitter()
		{
			this.ignoreFingerRadius = true;
			this.targetControlList = new List<TouchControl>(4);
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
			for (int i = 0; i < this.targetControlList.Count; i++)
			{
				TouchControl touchControl = this.targetControlList[i];
				if (touchControl != null)
				{
					touchControl.ReleaseAllTouches();
				}
			}
		}

		public override bool OnTouchStart(TouchObject touch, TouchControl sender, TouchControl.TouchStartType touchStartType)
		{
			bool result = false;
			for (int i = 0; i < this.targetControlList.Count; i++)
			{
				TouchControl touchControl = this.targetControlList[i];
				if (touchControl != null && touchControl.OnTouchStart(touch, this, TouchControl.TouchStartType.ProxyPress))
				{
					result = true;
				}
			}
			return result;
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
			if (!base.CanBeTouchedDirectly(touchObj))
			{
				return false;
			}
			for (int i = 0; i < this.targetControlList.Count; i++)
			{
				TouchControl touchControl = this.targetControlList[i];
				if (!(touchControl == null))
				{
					if (touchControl.CanBeActivatedByOtherControl(this, touchObj))
					{
						return true;
					}
				}
			}
			return false;
		}

		public override bool CanSwipeOverOthers(TouchObject touchObj)
		{
			return false;
		}

		public override bool CanBeSwipedOverFromNothing(TouchObject touchObj)
		{
			return base.CanBeSwipedOverFromNothingDefault(touchObj);
		}

		public override bool CanBeSwipedOverFromRestrictedList(TouchObject touchObj)
		{
			return base.CanBeSwipedOverFromRestrictedListDefault(touchObj);
		}

		public List<TouchControl> targetControlList;
	}
}
