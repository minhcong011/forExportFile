// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: ControlFreak2.TouchSteeringWheel
using System;
using ControlFreak2.Internal;
using UnityEngine;

namespace ControlFreak2
{
	public class TouchSteeringWheel : DynamicTouchControl
	{
		public TouchSteeringWheel()
		{
			this.analogConfig = new AnalogConfig();
			this.analogConfig.analogDeadZone = 0f;
			this.touchSmoothing = 0.1f;
			this.centerOnDirectTouch = false;
			this.centerWhenFollowing = false;
			this.wheelMode = TouchSteeringWheel.WheelMode.Swipe;
			this.pressBinding = new DigitalBinding(null);
			this.analogTurnBinding = new AxisBinding("Horizontal", false, null);
			this.turnLeftBinding = new DigitalBinding(KeyCode.None, true, "Horizontal", true, false, null);
			this.turnRightBinding = new DigitalBinding(KeyCode.None, true, "Horizontal", false, false, null);
			this.emulateTouchPressure = true;
			this.touchPressureBinding = new AxisBinding(null);
		}

		public float GetValue()
		{
			return this.valCur;
		}

		public float GetValueDelta()
		{
			return this.valCur - this.valPrev;
		}

		protected override void OnInitControl()
		{
			base.OnInitControl();
			this.ResetControl();
		}

		public override void ResetControl()
		{
			base.ResetControl();
			this.ReleaseAllTouches();
			this.touchStateWorld.Reset();
			this.touchStateScreen.Reset();
			this.touchStateOriented.Reset();
			this.valCur = 0f;
			this.valPrev = 0f;
			this.rawValCur = 0f;
			this.startRawVal = 0f;
			this.startVec = Vector2.zero;
			this.angleIsSafe = false;
			this.startAngle = 0f;
			this.curAngle = 0f;
			this.angleDelta = 0f;
		}

		protected override void OnUpdateControl()
		{
			base.OnUpdateControl();
			this.valPrev = this.valCur;
			if (this.touchStateWorld.JustPressedRaw())
			{
				this.startRawVal = this.rawValCur;
				if (this.wheelMode == TouchSteeringWheel.WheelMode.Swipe)
				{
					if (this.physicalMode)
					{
						this.startVec = this.touchStateOriented.GetCurPosSmooth();
					}
					else
					{
						this.startVec = base.WorldToNormalizedPos(this.touchStateWorld.GetStartPos(), base.GetOriginOffset());
					}
				}
				else
				{
					this.startVec = base.WorldToNormalizedPos(this.touchStateWorld.GetStartPos(), base.GetOriginOffset());
					this.angleIsSafe = false;
					this.curAngle = 0f;
					this.startAngle = 0f;
					this.angleDelta = 0f;
				}
			}
			if (this.touchStateWorld.PressedRaw())
			{
				float num;
				if (this.wheelMode == TouchSteeringWheel.WheelMode.Swipe)
				{
					if (this.physicalMode)
					{
						num = (this.touchStateOriented.GetCurPosSmooth().x - this.startVec.x) / (this.physicalMoveRangeCm * CFScreen.dpcm * 0.5f);
					}
					else
					{
						num = base.WorldToNormalizedPos(this.touchStateWorld.GetCurPosSmooth(), base.GetOriginOffset()).x - this.startVec.x;
					}
				}
				else
				{
					Vector3 v = base.WorldToNormalizedPos(this.touchStateWorld.GetCurPosSmooth(), base.GetOriginOffset());
					if (v.sqrMagnitude < this.turnModeDeadZone * this.turnModeDeadZone)
					{
						this.angleIsSafe = false;
					}
					else
					{
						this.curAngle = this.GetWheelAngle(v, this.curAngle);
						if (!this.angleIsSafe)
						{
							this.startAngle = this.curAngle;
							this.startRawVal = this.rawValCur;
							this.angleIsSafe = true;
						}
					}
					this.angleDelta = CFUtils.SmartDeltaAngle(this.startAngle, this.curAngle, this.angleDelta);
					this.angleDelta = Mathf.Clamp(this.angleDelta, -this.maxTurnAngle - 360f, this.maxTurnAngle + 360f);
					num = this.angleDelta / this.maxTurnAngle;
				}
				float b = this.startRawVal + num;
				this.rawValCur = CFUtils.MoveTowards(this.rawValCur, b, (!this.limitTurnSpeed) ? 0f : this.minTurnTime, CFUtils.realDeltaTime, 0.001f);
			}
			else
			{
				this.rawValCur = CFUtils.MoveTowards(this.rawValCur, 0f, (!this.limitTurnSpeed) ? 0f : this.maxReturnTime, CFUtils.realDeltaTime, 0.001f);
			}
			this.rawValCur = Mathf.Clamp(this.rawValCur, -1f, 1f);
			this.valCur = this.analogConfig.GetAnalogVal(this.rawValCur);
			if (base.IsActive())
			{
				this.SyncRigState();
			}
		}

		private void SyncRigState()
		{
			if (base.Pressed() || this.sendInputWhileReturning)
			{
				this.analogTurnBinding.SyncFloat(this.GetValue(), InputRig.InputSource.Analog, base.rig);
			}
			if (base.Pressed())
			{
				this.pressBinding.Sync(base.Pressed(), base.rig, false);
				if (this.GetValue() <= -this.analogConfig.digitalEnterThresh)
				{
					this.turnLeftBinding.Sync(true, base.rig, false);
				}
				else if (this.GetValue() >= this.analogConfig.digitalEnterThresh)
				{
					this.turnRightBinding.Sync(true, base.rig, false);
				}
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

		private float GetWheelAngle(Vector2 np, float fallbackAngle)
		{
			if (np.sqrMagnitude < this.turnModeDeadZone * this.turnModeDeadZone)
			{
				return fallbackAngle;
			}
			np.Normalize();
			return Mathf.Atan2(np.x, np.y) * 57.29578f;
		}

		protected override bool OnIsBoundToAxis(string axisName, InputRig rig)
		{
			return this.analogTurnBinding.IsBoundToAxis(axisName, rig) || this.pressBinding.IsBoundToAxis(axisName, rig) || this.touchPressureBinding.IsBoundToAxis(axisName, rig) || this.turnLeftBinding.IsBoundToAxis(axisName, rig) || this.turnRightBinding.IsBoundToAxis(axisName, rig);
		}

		protected override bool OnIsBoundToKey(KeyCode key, InputRig rig)
		{
			return this.analogTurnBinding.IsBoundToKey(key, rig) || this.pressBinding.IsBoundToKey(key, rig) || this.touchPressureBinding.IsBoundToKey(key, rig) || this.turnLeftBinding.IsBoundToKey(key, rig) || this.turnRightBinding.IsBoundToKey(key, rig);
		}

		protected override void OnGetSubBindingDescriptions(BindingDescriptionList descList, UnityEngine.Object undoObject, string parentMenuPath)
		{
			descList.Add(this.pressBinding, "Press", parentMenuPath, this);
			descList.Add(this.touchPressureBinding, InputRig.InputSource.Analog, "Touch Pressure", parentMenuPath, this);
			descList.Add(this.analogTurnBinding, InputRig.InputSource.Analog, "Analog Turn", parentMenuPath, this);
			descList.Add(this.turnLeftBinding, "Turn Left", parentMenuPath, this);
			descList.Add(this.turnRightBinding, "Turn Right", parentMenuPath, this);
		}

		public TouchSteeringWheel.WheelMode wheelMode;

		public bool limitTurnSpeed;

		public float minTurnTime = 0.05f;

		public float maxReturnTime = 0.25f;

		public float maxTurnAngle = 60f;

		public float turnModeDeadZone = 0.05f;

		public bool physicalMode;

		public float physicalMoveRangeCm = 2f;

		public bool sendInputWhileReturning;

		public AnalogConfig analogConfig;

		public DigitalBinding pressBinding;

		public AxisBinding analogTurnBinding;

		public DigitalBinding turnRightBinding;

		public DigitalBinding turnLeftBinding;

		public bool emulateTouchPressure;

		public AxisBinding touchPressureBinding;

		private Vector2 pressOrigin;

		private float rawValCur;

		private float valCur;

		private float valPrev;

		private Vector2 startVec;

		private float startRawVal;

		private float startAngle;

		private float curAngle;

		private float angleDelta;

		private bool angleIsSafe;

		public enum WheelMode
		{
			Swipe,
			Turn
		}
	}
}
