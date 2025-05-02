// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: ControlFreak2.TouchTrackPad
using System;
using ControlFreak2.Internal;
using UnityEngine;

namespace ControlFreak2
{
	public class TouchTrackPad : TouchControl
	{
		public TouchTrackPad()
		{
			this.touchSmoothing = 0.5f;
			this.touchState = new TouchGestureBasicState();
			this.pressBinding = new DigitalBinding(null);
			this.horzSwipeBinding = new AxisBinding("Mouse X", false, null);
			this.vertSwipeBinding = new AxisBinding("Mouse Y", false, null);
			this.emulateTouchPressure = true;
			this.touchPressureBinding = new AxisBinding(null);
		}

		public bool Pressed()
		{
			return this.touchState.PressedRaw();
		}

		public bool JustPressed()
		{
			return this.touchState.JustPressedRaw();
		}

		public bool JustReleased()
		{
			return this.touchState.JustReleasedRaw();
		}

		public bool IsTouchPressureSensitive()
		{
			return this.touchState.PressedRaw() && this.touchState.IsPressureSensitive();
		}

		public float GetTouchPressure()
		{
			return (!this.touchState.PressedRaw()) ? 0f : this.touchState.GetPressure();
		}

		public Vector2 GetSwipeDelta()
		{
			return this.touchState.GetDeltaVecSmooth();
		}

		public void SetTouchSmoothing(float smTime)
		{
			this.touchSmoothing = Mathf.Clamp01(smTime);
			this.touchState.SetSmoothingTime(this.touchSmoothing * 0.1f);
		}

		protected override void OnInitControl()
		{
			this.ResetControl();
			this.SetTouchSmoothing(this.touchSmoothing);
		}

		protected override void OnDestroyControl()
		{
			this.ResetControl();
		}

		public override void ResetControl()
		{
			this.ReleaseAllTouches();
			this.touchState.Reset();
		}

		protected override void OnUpdateControl()
		{
			if (this.touchObj != null && base.rig != null)
			{
				base.rig.WakeTouchControlsUp();
			}
			this.touchState.Update();
			if (base.IsActive())
			{
				this.SyncRigState();
			}
		}

		private void SyncRigState()
		{
			if (this.Pressed())
			{
				this.pressBinding.Sync(true, base.rig, false);
				if (this.IsTouchPressureSensitive())
				{
					this.touchPressureBinding.SyncFloat(this.GetTouchPressure(), InputRig.InputSource.Analog, base.rig);
				}
				else if (this.emulateTouchPressure)
				{
					this.touchPressureBinding.SyncFloat(1f, InputRig.InputSource.Digital, base.rig);
				}
			}
			Vector2 swipeDelta = this.GetSwipeDelta();
			this.horzSwipeBinding.SyncFloat(swipeDelta.x, InputRig.InputSource.TouchDelta, base.rig);
			this.vertSwipeBinding.SyncFloat(swipeDelta.y, InputRig.InputSource.TouchDelta, base.rig);
		}

		protected override bool OnIsBoundToAxis(string axisName, InputRig rig)
		{
			return this.pressBinding.IsBoundToAxis(axisName, rig) || this.touchPressureBinding.IsBoundToAxis(axisName, rig) || this.horzSwipeBinding.IsBoundToAxis(axisName, rig) || this.vertSwipeBinding.IsBoundToAxis(axisName, rig);
		}

		protected override bool OnIsBoundToKey(KeyCode key, InputRig rig)
		{
			return this.pressBinding.IsBoundToKey(key, rig) || this.touchPressureBinding.IsBoundToKey(key, rig) || this.horzSwipeBinding.IsBoundToKey(key, rig) || this.vertSwipeBinding.IsBoundToKey(key, rig);
		}

		protected override void OnGetSubBindingDescriptions(BindingDescriptionList descList, UnityEngine.Object undoObject, string parentMenuPath)
		{
			descList.Add(this.pressBinding, "Press", parentMenuPath, this);
			descList.Add(this.touchPressureBinding, InputRig.InputSource.Analog, "Touch Pressure", parentMenuPath, this);
			descList.Add(this.horzSwipeBinding, InputRig.InputSource.TouchDelta, "Horz. Swipe Delta", parentMenuPath, this);
			descList.Add(this.vertSwipeBinding, InputRig.InputSource.TouchDelta, "Vert. Swipe Delta", parentMenuPath, this);
		}

		public override bool CanBeTouchedDirectly(TouchObject touchObj)
		{
			return base.CanBeTouchedDirectly(touchObj) && this.touchObj == null;
		}

		public override bool CanBeActivatedByOtherControl(TouchControl c, TouchObject touchObj)
		{
			return base.CanBeActivatedByOtherControl(c, touchObj) && this.touchObj == null;
		}

		public override bool CanBeSwipedOverFromNothing(TouchObject touchObj)
		{
			return base.CanBeSwipedOverFromNothingDefault(touchObj) && this.touchObj == null;
		}

		public override bool CanBeSwipedOverFromRestrictedList(TouchObject touchObj)
		{
			return base.CanBeSwipedOverFromRestrictedListDefault(touchObj) && this.touchObj == null;
		}

		public override bool CanSwipeOverOthers(TouchObject touchObj)
		{
			return base.CanSwipeOverOthersDefault(touchObj, this.touchObj, this.touchStartType);
		}

		public override void ReleaseAllTouches()
		{
			if (this.touchObj != null)
			{
				this.touchObj.ReleaseControl(this, TouchControl.TouchEndType.Cancel);
				this.touchObj = null;
			}
		}

		public override bool OnTouchStart(TouchObject touchObj, TouchControl sender, TouchControl.TouchStartType touchStartType)
		{
			if (this.touchObj != null)
			{
				return false;
			}
			this.touchObj = touchObj;
			this.touchStartType = touchStartType;
			this.touchObj.AddControl(this);
			Vector2 vector = base.ScreenToOrientedPos(touchObj.screenPosStart, touchObj.cam);
			this.touchState.OnTouchStart(vector, vector, 0f, touchObj);
			return true;
		}

		public override bool OnTouchEnd(TouchObject touchObj, TouchControl.TouchEndType touchEndType)
		{
			if (this.touchObj == null || this.touchObj != touchObj)
			{
				return false;
			}
			this.touchObj = null;
			this.touchState.OnTouchEnd(touchEndType != TouchControl.TouchEndType.Release);
			return true;
		}

		public override bool OnTouchMove(TouchObject touchObj)
		{
			if (this.touchObj == null || this.touchObj != touchObj)
			{
				return false;
			}
			Vector2 pos = base.ScreenToOrientedPos(touchObj.screenPosCur, touchObj.cam);
			this.touchState.OnTouchMove(pos);
			base.CheckSwipeOff(touchObj, this.touchStartType);
			return true;
		}

		public override bool OnTouchPressureChange(TouchObject touchObj)
		{
			if (this.touchObj == null || this.touchObj != touchObj)
			{
				return false;
			}
			this.touchState.OnTouchPressureChange(touchObj.GetPressure());
			return true;
		}

		public float touchSmoothing;

		public DigitalBinding pressBinding;

		public AxisBinding horzSwipeBinding;

		public AxisBinding vertSwipeBinding;

		public bool emulateTouchPressure;

		public AxisBinding touchPressureBinding;

		private TouchObject touchObj;

		private TouchControl.TouchStartType touchStartType;

		private TouchGestureBasicState touchState;
	}
}
