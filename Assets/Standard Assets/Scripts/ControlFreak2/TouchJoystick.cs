// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: ControlFreak2.TouchJoystick
using System;
using ControlFreak2.Internal;
using UnityEngine;

namespace ControlFreak2
{
	public class TouchJoystick : DynamicTouchControl
	{
		public TouchJoystick()
		{
			this.joyStateBinding = new JoystickStateBinding(null);
			this.pressBinding = new DigitalBinding(null);
			this.emulateTouchPressure = true;
			this.touchPressureBinding = new AxisBinding(null);
			this.centerWhenFollowing = false;
			this.config = new JoystickConfig();
			this.state = new JoystickState(this.config);
		}

		protected override void OnInitControl()
		{
			base.OnInitControl();
			this.ResetControl();
		}

		public Vector2 GetVector()
		{
			return this.state.GetVector();
		}

		public Vector2 GetVectorRaw()
		{
			return this.state.GetVectorRaw();
		}

		public Dir GetDir()
		{
			return this.state.GetDir();
		}

		public JoystickState GetState()
		{
			return this.state;
		}

		public override void ResetControl()
		{
			base.ResetControl();
			this.ReleaseAllTouches();
			this.touchStateWorld.Reset();
			this.touchStateScreen.Reset();
			this.touchStateOriented.Reset();
			this.state.Reset();
		}

		protected override void OnUpdateControl()
		{
			base.OnUpdateControl();
			if (this.touchStateWorld.PressedRaw())
			{
				this.state.ApplyUnclampedVec(base.WorldToNormalizedPos(this.touchStateWorld.GetCurPosSmooth(), base.GetOriginOffset()));
			}
			this.state.Update();
			if (base.IsActive())
			{
				this.SyncRigState();
			}
		}

		protected override bool OnIsBoundToAxis(string axisName, InputRig rig)
		{
			return this.pressBinding.IsBoundToAxis(axisName, rig) || this.touchPressureBinding.IsBoundToAxis(axisName, rig) || this.joyStateBinding.IsBoundToAxis(axisName, rig);
		}

		protected override bool OnIsBoundToKey(KeyCode key, InputRig rig)
		{
			return this.pressBinding.IsBoundToKey(key, rig) || this.touchPressureBinding.IsBoundToKey(key, rig) || this.joyStateBinding.IsBoundToKey(key, rig);
		}

		protected override void OnGetSubBindingDescriptions(BindingDescriptionList descList, UnityEngine.Object undoObject, string parentMenuPath)
		{
			descList.Add(this.pressBinding, "Press", parentMenuPath, this);
			descList.Add(this.touchPressureBinding, InputRig.InputSource.Analog, "Touch Pressure", parentMenuPath, this);
			descList.Add(this.joyStateBinding, "Joy State", parentMenuPath, this);
		}

		public override bool IsUsingKeyForEmulation(KeyCode key)
		{
			return false;
		}

		private void SyncRigState()
		{
			if (base.Pressed())
			{
				this.pressBinding.Sync(true, base.rig, false);
				if (base.IsTouchPressureSensitive())
				{
					this.touchPressureBinding.SyncFloat(base.GetTouchPressure(), InputRig.InputSource.Analog, base.rig);
				}
				else if (this.emulateTouchPressure)
				{
					this.touchPressureBinding.SyncFloat(1f, InputRig.InputSource.Digital, base.rig);
				}
			}
			this.joyStateBinding.SyncJoyState(this.state, base.rig);
		}

		public JoystickConfig config;

		private JoystickState state;

		public DigitalBinding pressBinding;

		public JoystickStateBinding joyStateBinding;

		public bool emulateTouchPressure;

		public AxisBinding touchPressureBinding;
	}
}
