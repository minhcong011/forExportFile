// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: ControlFreak2.TouchButton
using System;
using ControlFreak2.Internal;
using UnityEngine;

namespace ControlFreak2
{
	public class TouchButton : DynamicTouchControl, IBindingContainer
	{
		public TouchButton()
		{
			this.pressBinding = new DigitalBinding(null);
			this.toggleOnlyBinding = new DigitalBinding(null);
			this.emulateTouchPressure = true;
			this.touchPressureBinding = new AxisBinding(null);
			this.centerWhenFollowing = true;
			this.toggleOnAction = TouchButton.ToggleOnAction.OnPress;
			this.toggleOffAction = TouchButton.ToggleOffAction.OnRelease;
			this.autoToggleOff = false;
			this.autoToggleOffTimeOut = 1f;
		}

		public bool Toggled()
		{
			return this.toggledCur;
		}

		public bool JustToggled()
		{
			return this.toggledCur && !this.toggledPrev;
		}

		public bool JustUntoggled()
		{
			return !this.toggledCur && this.toggledPrev;
		}

		public bool PressedOrToggled()
		{
			return base.Pressed() || this.Toggled();
		}

		protected override void OnInitControl()
		{
			base.OnInitControl();
			if (this.toggle && this.linkToggleToRigSwitch && base.rig != null)
			{
				this.ChangeToggleState(base.rig.GetSwitchState(this.toggleRigSwitchName, ref this.toggleRigSwitchId, false), false);
			}
			this.ResetControl();
		}

		public override void ResetControl()
		{
			base.ResetControl();
			this.ReleaseAllTouches();
			this.touchStateWorld.Reset();
			this.touchStateScreen.Reset();
			this.touchStateOriented.Reset();
		}

		protected override void OnUpdateControl()
		{
			base.OnUpdateControl();
			this.toggledPrev = this.toggledCur;
			if (!this.toggle)
			{
				this.toggledCur = false;
			}
			else
			{
				bool flag = false;
				if (!this.Toggled())
				{
					if (this.toggleOnAction == TouchButton.ToggleOnAction.OnPress)
					{
						if (this.touchStateWorld.JustPressedRaw())
						{
							flag = true;
							this.curTouchToggledOn = true;
						}
					}
					else if (this.toggleOnAction == TouchButton.ToggleOnAction.OnRelease && !this.curTouchToggledOn && this.touchStateWorld.JustReleasedRaw())
					{
						flag = true;
						this.curTouchToggledOn = false;
					}
				}
				else if (this.toggleOffAction == TouchButton.ToggleOffAction.OnPress)
				{
					if (this.touchStateWorld.JustPressedRaw())
					{
						flag = true;
						this.curTouchToggledOn = true;
					}
				}
				else if (this.toggleOffAction == TouchButton.ToggleOffAction.OnRelease && !this.curTouchToggledOn && this.touchStateWorld.JustReleasedRaw())
				{
					flag = true;
					this.curTouchToggledOn = false;
				}
				if (!this.touchStateWorld.PressedRaw())
				{
					this.curTouchToggledOn = false;
				}
				if (flag)
				{
					this.ChangeToggleState(!this.toggledCur, true);
				}
				else if (this.toggle && this.linkToggleToRigSwitch)
				{
					this.ChangeToggleState(base.rig.rigSwitches.GetSwitchState(this.toggleRigSwitchName, ref this.toggleRigSwitchId, this.toggledCur), false);
				}
				if (this.Toggled() && (this.autoToggleOff || this.toggleOffAction == TouchButton.ToggleOffAction.OnTimeout))
				{
					if (base.Pressed())
					{
						this.elapsedSinceToggled = 0f;
					}
					else if ((this.elapsedSinceToggled += CFUtils.realDeltaTime) > this.autoToggleOffTimeOut)
					{
						this.ChangeToggleState(false, true);
					}
				}
			}
			if (base.IsActive())
			{
				this.SyncRigState();
			}
		}

		private void ChangeToggleState(bool toggleState, bool syncRigSwitch)
		{
			this.toggledCur = toggleState;
			this.elapsedSinceToggled = 0f;
			if (syncRigSwitch && this.linkToggleToRigSwitch && base.rig != null)
			{
				base.rig.rigSwitches.SetSwitchState(this.toggleRigSwitchName, ref this.toggleRigSwitchId, toggleState);
			}
		}

		private void SyncRigState()
		{
			if (this.PressedOrToggled())
			{
				this.pressBinding.Sync(true, base.rig, false);
			}
			this.toggleOnlyBinding.Sync(this.Toggled(), base.rig, false);
			if (base.Pressed())
			{
				if (base.IsTouchPressureSensitive())
				{
					this.touchPressureBinding.SyncFloat(base.GetTouchPressure(), InputRig.InputSource.Analog, base.rig);
				}
				else if (this.emulateTouchPressure)
				{
					this.touchPressureBinding.SyncFloat(1f, InputRig.InputSource.Digital, base.rig);
				}
			}
		}

		protected override void OnGetSubBindingDescriptions(BindingDescriptionList descList, UnityEngine.Object undoObject, string parentMenuPath)
		{
			descList.Add(this.pressBinding, "Press", parentMenuPath, this);
			descList.Add(this.touchPressureBinding, InputRig.InputSource.Analog, "Touch Pressure", parentMenuPath, this);
			if (this.toggle || descList.addUnusedBindings)
			{
				descList.Add(this.toggleOnlyBinding, "Toggle", parentMenuPath, this);
			}
		}

		protected override bool OnIsBoundToAxis(string axisName, InputRig rig)
		{
			return this.pressBinding.IsBoundToAxis(axisName, rig) || this.touchPressureBinding.IsBoundToAxis(axisName, rig) || this.toggleOnlyBinding.IsBoundToAxis(axisName, rig);
		}

		protected override bool OnIsBoundToKey(KeyCode key, InputRig rig)
		{
			return this.pressBinding.IsBoundToKey(key, rig) || this.touchPressureBinding.IsBoundToKey(key, rig) || this.toggleOnlyBinding.IsBoundToKey(key, rig);
		}

		public override void ReleaseAllTouches()
		{
			base.ReleaseAllTouches();
			this.ChangeToggleState(false, this.toggleOffWhenHiding);
		}

		public bool toggle;

		public TouchButton.ToggleOnAction toggleOnAction;

		public TouchButton.ToggleOffAction toggleOffAction;

		public bool toggleOffWhenHiding;

		public bool autoToggleOff;

		public float autoToggleOffTimeOut;

		public bool linkToggleToRigSwitch;

		public string toggleRigSwitchName;

		private int toggleRigSwitchId;

		public DigitalBinding pressBinding;

		public DigitalBinding toggleOnlyBinding;

		public bool emulateTouchPressure;

		public AxisBinding touchPressureBinding;

		private bool toggledCur;

		private bool toggledPrev;

		private bool curTouchToggledOn;

		private float elapsedSinceToggled;

		public enum ToggleOnAction
		{
			OnPress,
			OnRelease
		}

		public enum ToggleOffAction
		{
			OnPress,
			OnRelease,
			OnTimeout
		}
	}
}
