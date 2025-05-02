// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: ControlFreak2.SuperTouchZone
using System;
using ControlFreak2.Internal;
using UnityEngine;

namespace ControlFreak2
{
	[ExecuteInEditMode]
	public class SuperTouchZone : TouchControl
	{
		public SuperTouchZone()
		{
			this.strictMultiTouchTime = 0.5f;
			this.customThresh = new MultiTouchGestureThresholds();
			this.fingers = new SuperTouchZone.FingerState[3];
			this.fingersOrdered = new SuperTouchZone.FingerState[3];
			for (int i = 0; i < 3; i++)
			{
				this.fingers[i] = new SuperTouchZone.FingerState(this);
				this.fingersOrdered[i] = this.fingers[i];
			}
			this.multiFingerTouch = new SuperTouchZone.MultiFingerTouch[3];
			for (int j = 0; j < this.multiFingerTouch.Length; j++)
			{
				this.multiFingerTouch[j] = new SuperTouchZone.MultiFingerTouch(j + 1, this);
			}
			this.multiFingerConfigs = new SuperTouchZone.MultiFingerTouchConfig[3];
			for (int k = 0; k < this.multiFingerConfigs.Length; k++)
			{
				this.multiFingerConfigs[k] = new SuperTouchZone.MultiFingerTouchConfig();
			}
			this.separateFingersAsEmuTouchesBinding = new EmuTouchBinding(null);
			this.twistAnalogBinding = new AxisBinding(null);
			this.twistDeltaBinding = new AxisBinding(null);
			this.twistLeftDigitalBinding = new DigitalBinding(null);
			this.twistRightDigitalBinding = new DigitalBinding(null);
			this.pinchAnalogBinding = new AxisBinding(null);
			this.pinchDeltaBinding = new AxisBinding(null);
			this.pinchDigitalBinding = new DigitalBinding(null);
			this.spreadDigitalBinding = new DigitalBinding(null);
			this.pinchScrollDeltaBinding = new ScrollDeltaBinding(null);
			this.twistScrollDeltaBinding = new ScrollDeltaBinding(null);
			this.pinchAnalogConfig = new AnalogConfig();
			this.twistAnalogConfig = new AnalogConfig();
			this.pinchDistCur = 0.1f;
			this.pinchDistPrev = 0.1f;
			this.pinchDistInitial = 0.1f;
			this.pinchDistQuietInitial = 0.1f;
			this.InitEmulatedFingers();
		}

		protected override void OnDestroyControl()
		{
			this.ResetControl();
		}

		protected override void OnInitControl()
		{
			for (int i = 0; i < this.multiFingerTouch.Length; i++)
			{
				this.multiFingerTouch[i].Init((i >= this.multiFingerConfigs.Length) ? null : this.multiFingerConfigs[i]);
			}
			this.SetTouchSmoothing(this.touchSmoothing);
			this.ResetControl();
		}

		public override void ResetControl()
		{
			this.ReleaseAllTouches();
			for (int i = 0; i < this.multiFingerTouch.Length; i++)
			{
				this.multiFingerTouch[i].Reset();
			}
			for (int j = 0; j < this.fingers.Length; j++)
			{
				this.fingers[j].Reset();
			}
			this.twistMoved = false;
			this.twistJustMoved = false;
			this.twistAngleCur = 0f;
			this.twistAngleInitial = 0f;
			this.twistAnglePrev = 0f;
			this.twistAngleQuietInitial = 0f;
			this.pinchMoved = false;
			this.pinchJustMoved = false;
			this.pinchDistCur = 0.1f;
			this.pinchDistPrev = 0.1f;
			this.pinchDistInitial = 0.1f;
			this.pinchDistQuietInitial = 0.1f;
			this.pinchScrollCur = 0;
			this.pinchScrollPrev = 0;
			this.twistScrollCur = 0;
			this.twistScrollPrev = 0;
		}

		public bool PressedRaw(int fingerCount)
		{
			SuperTouchZone.MultiFingerTouch multiFingerTouch = this.GetMultiFingerTouch(fingerCount);
			return multiFingerTouch != null && multiFingerTouch.touchScreen.PressedRaw();
		}

		public bool JustPressedRaw(int fingerCount)
		{
			SuperTouchZone.MultiFingerTouch multiFingerTouch = this.GetMultiFingerTouch(fingerCount);
			return multiFingerTouch != null && multiFingerTouch.touchScreen.JustPressedRaw();
		}

		public bool JustReleasedRaw(int fingerCount)
		{
			SuperTouchZone.MultiFingerTouch multiFingerTouch = this.GetMultiFingerTouch(fingerCount);
			return multiFingerTouch != null && multiFingerTouch.touchScreen.JustReleasedRaw();
		}

		public bool PressedNormal(int fingerCount)
		{
			SuperTouchZone.MultiFingerTouch multiFingerTouch = this.GetMultiFingerTouch(fingerCount);
			return multiFingerTouch != null && multiFingerTouch.touchScreen.PressedNormal();
		}

		public bool JustPressedNormal(int fingerCount)
		{
			SuperTouchZone.MultiFingerTouch multiFingerTouch = this.GetMultiFingerTouch(fingerCount);
			return multiFingerTouch != null && multiFingerTouch.touchScreen.JustPressedNormal();
		}

		public bool JustReleasedNormal(int fingerCount)
		{
			SuperTouchZone.MultiFingerTouch multiFingerTouch = this.GetMultiFingerTouch(fingerCount);
			return multiFingerTouch != null && multiFingerTouch.touchScreen.JustReleasedNormal();
		}

		public bool PressedLong(int fingerCount)
		{
			SuperTouchZone.MultiFingerTouch multiFingerTouch = this.GetMultiFingerTouch(fingerCount);
			return multiFingerTouch != null && multiFingerTouch.touchScreen.PressedLong();
		}

		public bool JustPressedLong(int fingerCount)
		{
			SuperTouchZone.MultiFingerTouch multiFingerTouch = this.GetMultiFingerTouch(fingerCount);
			return multiFingerTouch != null && multiFingerTouch.touchScreen.JustPressedLong();
		}

		public bool JustReleasedLong(int fingerCount)
		{
			SuperTouchZone.MultiFingerTouch multiFingerTouch = this.GetMultiFingerTouch(fingerCount);
			return multiFingerTouch != null && multiFingerTouch.touchScreen.JustReleasedLong();
		}

		public bool JustTapped(int fingerCount, int howManyTimes)
		{
			SuperTouchZone.MultiFingerTouch multiFingerTouch = this.GetMultiFingerTouch(fingerCount);
			return multiFingerTouch != null && multiFingerTouch.touchScreen.JustTapped(howManyTimes);
		}

		public bool JustLongTapped(int fingerCount)
		{
			SuperTouchZone.MultiFingerTouch multiFingerTouch = this.GetMultiFingerTouch(fingerCount);
			return multiFingerTouch != null && multiFingerTouch.touchScreen.JustLongTapped();
		}

		public bool SwipeActive(int fingerCount)
		{
			SuperTouchZone.MultiFingerTouch multiFingerTouch = this.GetMultiFingerTouch(fingerCount);
			return multiFingerTouch != null && multiFingerTouch.touchOriented.Swiped();
		}

		public bool SwipeJustActivated(int fingerCount)
		{
			SuperTouchZone.MultiFingerTouch multiFingerTouch = this.GetMultiFingerTouch(fingerCount);
			return multiFingerTouch != null && multiFingerTouch.touchOriented.JustSwiped();
		}

		public Vector2 GetCurPos(int fingerCount)
		{
			SuperTouchZone.MultiFingerTouch multiFingerTouch = this.GetMultiFingerTouch(fingerCount);
			return (multiFingerTouch != null) ? multiFingerTouch.touchScreen.GetCurPosRaw() : Vector2.zero;
		}

		public Vector2 GetStartPos(int fingerCount)
		{
			SuperTouchZone.MultiFingerTouch multiFingerTouch = this.GetMultiFingerTouch(fingerCount);
			return (multiFingerTouch != null) ? multiFingerTouch.touchScreen.GetStartPos() : Vector2.zero;
		}

		public Vector2 GetTapPos(int fingerCount)
		{
			SuperTouchZone.MultiFingerTouch multiFingerTouch = this.GetMultiFingerTouch(fingerCount);
			return (multiFingerTouch != null) ? multiFingerTouch.touchScreen.GetTapPos() : Vector2.zero;
		}

		public Vector2 GetRawSwipeVector(int fingerCount)
		{
			SuperTouchZone.MultiFingerTouch multiFingerTouch = this.GetMultiFingerTouch(fingerCount);
			return (multiFingerTouch != null) ? multiFingerTouch.touchOriented.GetSwipeVecRaw() : Vector2.zero;
		}

		public Vector2 GetRawSwipeDelta(int fingerCount)
		{
			SuperTouchZone.MultiFingerTouch multiFingerTouch = this.GetMultiFingerTouch(fingerCount);
			return (multiFingerTouch != null) ? multiFingerTouch.touchOriented.GetDeltaVecRaw() : Vector2.zero;
		}

		public Vector2 GetUnconstrainedSwipeVector(int fingerCount)
		{
			SuperTouchZone.MultiFingerTouch multiFingerTouch = this.GetMultiFingerTouch(fingerCount);
			return (multiFingerTouch != null) ? multiFingerTouch.touchOriented.GetSwipeVecSmooth() : Vector2.zero;
		}

		public Vector2 GetUnconstrainedSwipeDelta(int fingerCount)
		{
			SuperTouchZone.MultiFingerTouch multiFingerTouch = this.GetMultiFingerTouch(fingerCount);
			return (multiFingerTouch != null) ? multiFingerTouch.touchOriented.GetDeltaVecSmooth() : Vector2.zero;
		}

		public Vector2 GetSwipeVector(int fingerCount)
		{
			SuperTouchZone.MultiFingerTouch multiFingerTouch = this.GetMultiFingerTouch(fingerCount);
			return (multiFingerTouch != null) ? multiFingerTouch.touchOriented.GetConstrainedSwipeVec() : Vector2.zero;
		}

		public Vector2 GetSwipeDelta(int fingerCount)
		{
			SuperTouchZone.MultiFingerTouch multiFingerTouch = this.GetMultiFingerTouch(fingerCount);
			return (multiFingerTouch != null) ? multiFingerTouch.touchOriented.GetConstrainedDeltaVec() : Vector2.zero;
		}

		public Dir GetSwipeDir(int fingerCount)
		{
			SuperTouchZone.MultiFingerTouch multiFingerTouch = this.GetMultiFingerTouch(fingerCount);
			return (multiFingerTouch != null) ? multiFingerTouch.touchOriented.GetSwipeDirState().GetCur() : Dir.N;
		}

		public Vector2 GetScroll(int fingerCount)
		{
			SuperTouchZone.MultiFingerTouch multiFingerTouch = this.GetMultiFingerTouch(fingerCount);
			return (multiFingerTouch != null) ? multiFingerTouch.touchOriented.GetScroll() : Vector2.zero;
		}

		public Vector2 GetScrollDelta(int fingerCount)
		{
			SuperTouchZone.MultiFingerTouch multiFingerTouch = this.GetMultiFingerTouch(fingerCount);
			return (multiFingerTouch != null) ? multiFingerTouch.touchOriented.GetScrollDelta() : Vector2.zero;
		}

		public bool PinchActive()
		{
			return this.pinchMoved;
		}

		public bool PinchJustActivated()
		{
			return this.pinchJustMoved;
		}

		public float GetPinchScale()
		{
			return this.pinchDistCur / this.pinchDistInitial;
		}

		public float GetPinchScaleDelta()
		{
			return this.pinchDistCur / this.pinchDistPrev;
		}

		public float GetPinchDist()
		{
			return this.pinchDistCur - this.pinchDistInitial;
		}

		public float GetPinchDistDelta()
		{
			return this.pinchDistCur - this.pinchDistPrev;
		}

		public bool TwistActive()
		{
			return this.twistMoved;
		}

		public bool TwistJustActivated()
		{
			return this.twistJustMoved;
		}

		public float GetTwist()
		{
			return this.twistAngleCur;
		}

		public float GetTwistDelta()
		{
			return this.twistAngleCur - this.twistAnglePrev;
		}

		public int GetTwistScrollDelta()
		{
			return this.twistScrollCur - this.twistScrollPrev;
		}

		public int GetPinchScrollDelta()
		{
			return this.pinchScrollCur - this.pinchScrollPrev;
		}

		public float GetReleasedDuration(int fingerCount)
		{
			SuperTouchZone.MultiFingerTouch multiFingerTouch = this.GetMultiFingerTouch(fingerCount);
			return (multiFingerTouch != null) ? multiFingerTouch.touchScreen.GetReleasedDurationRaw() : 0f;
		}

		public void SetTouchSmoothing(float smPow)
		{
			this.touchSmoothing = Mathf.Clamp01(smPow);
			for (int i = 0; i < this.fingers.Length; i++)
			{
				this.fingers[i].touchScreen.SetSmoothingTime(this.touchSmoothing * 0.1f);
				this.fingers[i].touchOriented.SetSmoothingTime(this.touchSmoothing * 0.1f);
			}
			for (int j = 0; j < this.multiFingerTouch.Length; j++)
			{
				this.multiFingerTouch[j].touchScreen.SetSmoothingTime(this.touchSmoothing * 0.1f);
				this.multiFingerTouch[j].touchOriented.SetSmoothingTime(this.touchSmoothing * 0.1f);
			}
		}

		protected SuperTouchZone.MultiFingerTouch GetMultiFingerTouch(int fingerCount)
		{
			if (fingerCount <= 0 || fingerCount > this.multiFingerTouch.Length)
			{
				return null;
			}
			return this.multiFingerTouch[fingerCount - 1];
		}

		public MultiTouchGestureThresholds GetThresholds()
		{
			return this.customThresh;
		}

		private Vector2 GetAveragedFingerPos(SuperTouchZone.FingerState[] fingers, int count, bool orientedSpace, bool useSmoothPos)
		{
			count = Mathf.Clamp(count, 0, fingers.Length);
			Vector2 vector = Vector2.zero;
			Vector2 vector2 = Vector2.zero;
			for (int i = 0; i < count; i++)
			{
				TouchGestureBasicState touchGestureBasicState = (!orientedSpace) ? fingers[i].touchScreen : fingers[i].touchOriented;
				Vector2 vector3 = (!useSmoothPos) ? touchGestureBasicState.GetCurPosRaw() : touchGestureBasicState.GetCurPosSmooth();
				if (i == 0)
				{
					vector2 = (vector = vector3);
				}
				else
				{
					vector = Vector2.Min(vector, vector3);
					vector2 = Vector2.Max(vector2, vector3);
				}
			}
			return vector + (vector2 - vector) * 0.5f;
		}

		private void StartMultiFingerTouch(int fingerNum, SuperTouchZone.FingerState[] fingers, Vector2 startScreenPos, Vector2 startOrientedPos, bool quietMode)
		{
			for (int i = 0; i < this.multiFingerTouch.Length; i++)
			{
				SuperTouchZone.MultiFingerTouch multiFingerTouch = this.multiFingerTouch[i];
				if (multiFingerTouch.GetFingerCount() == fingerNum)
				{
					multiFingerTouch.Start(fingers, startScreenPos, startOrientedPos, quietMode, this.elapsedSinceFirstTouch);
					if (multiFingerTouch.GetFingerCount() == 2)
					{
						this.pinchDistQuietInitial = this.GetTwoFingerDist();
						this.twistAngleQuietInitial = this.GetTwoFingerAngle(0f);
					}
				}
				else
				{
					if (fingerNum > multiFingerTouch.GetFingerCount())
					{
						multiFingerTouch.End(true);
					}
					else
					{
						multiFingerTouch.EndAndReport();
					}
					if (fingerNum > 0 && multiFingerTouch.GetFingerCount() < fingerNum)
					{
						multiFingerTouch.touchScreen.CancelTap();
						multiFingerTouch.touchOriented.CancelTap();
					}
				}
			}
		}

		private void CancelTapsOnMultiFingersOtherThan(int howManyFingers)
		{
			for (int i = 0; i < this.multiFingerTouch.Length; i++)
			{
				SuperTouchZone.MultiFingerTouch multiFingerTouch = this.multiFingerTouch[i];
				if (multiFingerTouch.GetFingerCount() != howManyFingers)
				{
					multiFingerTouch.touchOriented.CancelTap();
					multiFingerTouch.touchScreen.CancelTap();
				}
			}
		}

		protected override void OnUpdateControl()
		{
			this.UpdateEmulatedFingers();
			this.rawFingersPressedPrev = this.rawFingersPressedCur;
			this.rawFingersPressedCur = 0;
			bool flag = false;
			for (int i = 0; i < this.fingers.Length; i++)
			{
				SuperTouchZone.FingerState fingerState = this.fingers[i];
				fingerState.Update();
				if (fingerState.touchScreen.PressedRaw())
				{
					this.fingersOrdered[this.rawFingersPressedCur] = fingerState;
					this.rawFingersPressedCur++;
				}
				if (fingerState.touchScreen.JustReleasedRaw())
				{
					flag = true;
				}
			}
			Vector2 averagedFingerPos = this.GetAveragedFingerPos(this.fingersOrdered, this.rawFingersPressedCur, false, false);
			Vector2 averagedFingerPos2 = this.GetAveragedFingerPos(this.fingersOrdered, this.rawFingersPressedCur, true, false);
			if (this.rawFingersPressedCur > 0 && base.rig != null)
			{
				base.rig.WakeTouchControlsUp();
			}
			if (this.rawFingersPressedCur == 0)
			{
				this.elapsedSinceFirstTouch = 0f;
				this.dontAllowNewFingers = false;
			}
			if (!this.strictMultiTouch || this.maxFingers <= 1)
			{
				if (this.rawFingersPressedCur != this.rawFingersPressedPrev)
				{
					this.elapsedSinceFirstTouch = 0f;
					this.StartMultiFingerTouch(this.rawFingersPressedCur, this.fingersOrdered, averagedFingerPos, averagedFingerPos2, false);
					this.dontAllowNewFingers = false;
					this.waitingForMoreFingers = false;
					if (this.rawFingersPressedCur < this.rawFingersPressedPrev)
					{
						SuperTouchZone.MultiFingerTouch multiFingerTouch = this.GetMultiFingerTouch(this.rawFingersPressedCur);
						if (multiFingerTouch != null)
						{
							multiFingerTouch.DisableTapUntilRelease();
						}
					}
				}
			}
			else if (this.rawFingersPressedCur != this.rawFingersPressedPrev || flag)
			{
				if (this.rawFingersPressedCur < this.rawFingersPressedPrev || flag)
				{
					this.dontAllowNewFingers = (this.rawFingersPressedCur > 0);
					this.StartMultiFingerTouch(0, this.fingersOrdered, averagedFingerPos, averagedFingerPos2, false);
				}
				else
				{
					if (this.rawFingersPressedPrev == 0)
					{
						this.waitingForMoreFingers = true;
						this.elapsedSinceFirstTouch = 0f;
					}
					this.StartMultiFingerTouch(this.rawFingersPressedCur, this.fingersOrdered, averagedFingerPos, averagedFingerPos2, this.strictMultiTouch);
				}
			}
			else if (this.strictMultiTouch && this.waitingForMoreFingers && this.rawFingersPressedCur > 0)
			{
				this.elapsedSinceFirstTouch += CFUtils.realDeltaTime;
				if (this.elapsedSinceFirstTouch > this.strictMultiTouchTime)
				{
					this.waitingForMoreFingers = false;
					this.dontAllowNewFingers = true;
				}
			}
			for (int j = 0; j < this.multiFingerTouch.Length; j++)
			{
				SuperTouchZone.MultiFingerTouch multiFingerTouch2 = this.multiFingerTouch[j];
				if (multiFingerTouch2.GetFingerCount() == this.rawFingersPressedCur)
				{
					multiFingerTouch2.SetPos(averagedFingerPos, averagedFingerPos2);
				}
				multiFingerTouch2.Update();
				if (multiFingerTouch2.touchScreen.JustPressedRaw())
				{
					this.CancelTapsOnMultiFingersOtherThan(multiFingerTouch2.GetFingerCount());
				}
			}
			this.UpdateTwistAndPinch(false);
			if (this.separateFingersAsEmuTouchesBinding.enabled)
			{
				for (int k = 0; k < this.fingers.Length; k++)
				{
					base.rig.SyncEmuTouch(this.fingers[k].touchScreen, ref this.fingers[k].emuTouchId);
				}
			}
			for (int l = 0; l < this.multiFingerTouch.Length; l++)
			{
				this.multiFingerTouch[l].SyncToRig();
			}
			MultiTouchGestureThresholds thresholds = this.GetThresholds();
			if (thresholds != null)
			{
				this.pinchScrollDeltaBinding.SyncScrollDelta(this.GetPinchScrollDelta(), base.rig);
				this.twistScrollDeltaBinding.SyncScrollDelta(this.GetTwistScrollDelta(), base.rig);
				if (this.PinchActive())
				{
					if (this.pinchAnalogBinding.enabled)
					{
						this.pinchAnalogBinding.SyncFloat(this.pinchAnalogConfig.GetAnalogVal(this.GetPinchDist() / thresholds.pinchAnalogRangePx), InputRig.InputSource.Analog, base.rig);
					}
					if (this.pinchDeltaBinding.enabled)
					{
						this.pinchDeltaBinding.SyncFloat(this.GetPinchDistDelta() / thresholds.pinchDeltaRangePx, InputRig.InputSource.NormalizedDelta, base.rig);
					}
					if (this.GetPinchDist() > thresholds.pinchDigitalThreshPx)
					{
						this.spreadDigitalBinding.Sync(true, base.rig, false);
					}
					else if (this.GetPinchDist() < thresholds.pinchDigitalThreshPx)
					{
						this.pinchDigitalBinding.Sync(true, base.rig, false);
					}
				}
				if (this.TwistActive())
				{
					if (this.twistAnalogBinding.enabled)
					{
						this.twistAnalogBinding.SyncFloat(this.twistAnalogConfig.GetAnalogVal(this.GetTwist() / thresholds.twistAnalogRange), InputRig.InputSource.Analog, base.rig);
					}
					if (this.twistDeltaBinding.enabled)
					{
						this.twistDeltaBinding.SyncFloat(this.GetTwistDelta() / thresholds.twistDeltaRange, InputRig.InputSource.NormalizedDelta, base.rig);
					}
					if (this.GetTwist() > thresholds.twistDigitalThresh)
					{
						this.twistRightDigitalBinding.Sync(true, base.rig, false);
					}
					if (this.GetTwist() < thresholds.twistDigitalThresh)
					{
						this.twistLeftDigitalBinding.Sync(true, base.rig, false);
					}
				}
			}
		}

		private float GetTwoFingerDist()
		{
			SuperTouchZone.MultiFingerTouch multiFingerTouch = this.GetMultiFingerTouch(2);
			SuperTouchZone.FingerState finger;
			SuperTouchZone.FingerState finger2;
			if (multiFingerTouch == null || (finger = multiFingerTouch.GetFinger(0)) == null || (finger2 = multiFingerTouch.GetFinger(1)) == null)
			{
				return 0f;
			}
			return Mathf.Max(0.1f, (finger.touchScreen.GetCurPosSmooth() - finger2.touchScreen.GetCurPosSmooth()).magnitude);
		}

		private float GetTwoFingerAngle(float defaultAngle)
		{
			SuperTouchZone.MultiFingerTouch multiFingerTouch = this.GetMultiFingerTouch(2);
			SuperTouchZone.FingerState finger;
			SuperTouchZone.FingerState finger2;
			if (multiFingerTouch == null || (finger = multiFingerTouch.GetFinger(0)) == null || (finger2 = multiFingerTouch.GetFinger(1)) == null)
			{
				return defaultAngle;
			}
			Vector2 vector = finger.touchScreen.GetCurPosSmooth() - finger2.touchScreen.GetCurPosSmooth();
			float sqrMagnitude = vector.sqrMagnitude;
			if (sqrMagnitude <= 0.1f)
			{
				return defaultAngle;
			}
			float target = CFUtils.VecToAngle(vector.normalized);
			return defaultAngle + Mathf.DeltaAngle(defaultAngle, target);
		}

		private void UpdateTwistAndPinch()
		{
			this.UpdateTwistAndPinch(false);
		}

		private void UpdateTwistAndPinch(bool lastUpdateMode)
		{
			if (lastUpdateMode)
			{
				return;
			}
			this.pinchDistPrev = this.pinchDistCur;
			this.twistAnglePrev = this.twistAngleCur;
			this.twistScrollPrev = this.twistScrollCur;
			this.pinchScrollPrev = this.pinchScrollCur;
			this.pinchJustMoved = false;
			this.twistJustMoved = false;
			MultiTouchGestureThresholds thresholds = this.GetThresholds();
			SuperTouchZone.MultiFingerTouch multiFingerTouch = this.GetMultiFingerTouch(2);
			if (multiFingerTouch == null)
			{
				return;
			}
			if (multiFingerTouch.touchScreen.JustPressedRaw())
			{
				this.pinchDistInitial = this.pinchDistQuietInitial;
				this.pinchDistPrev = (this.pinchDistCur = this.pinchDistInitial);
				this.pinchJustMoved = false;
				this.pinchMoved = false;
				this.twistAngleInitial = this.twistAngleQuietInitial;
				this.twistAngleCur = 0f;
				this.twistAnglePrev = 0f;
				this.twistJustMoved = false;
				this.twistMoved = false;
				this.twistScrollCur = (this.twistScrollPrev = 0);
				this.pinchScrollCur = (this.pinchScrollPrev = 0);
			}
			else if (!multiFingerTouch.touchScreen.PressedRaw())
			{
				this.pinchDistInitial = this.pinchDistCur;
				this.twistAngleInitial = this.twistAngleCur;
				this.twistJustMoved = false;
				this.twistMoved = false;
				this.pinchMoved = false;
				this.pinchJustMoved = false;
			}
			else if (multiFingerTouch.touchScreen.PressedRaw())
			{
				bool flag = false;
				bool flag2 = false;
				bool flag3 = multiFingerTouch.touchScreen.JustSwiped();
				this.pinchDistCur = this.GetTwoFingerDist();
				if (!this.pinchMoved && Mathf.Abs(this.pinchDistInitial - this.pinchDistCur) > thresholds.pinchDistThreshPx)
				{
					flag2 = true;
				}
				this.twistAngleCur = this.GetTwoFingerAngle(this.twistAngleCur) - this.twistAngleInitial;
				if (!this.twistMoved && this.pinchDistCur > thresholds.twistMinDistPx && Mathf.Abs(this.twistAngleCur) > thresholds.twistAngleThresh)
				{
					flag = true;
				}
				this.pinchScrollCur = CFUtils.GetScrollValue(this.pinchDistInitial - this.pinchDistCur, this.pinchScrollCur, thresholds.pinchScrollStepPx, thresholds.pinchScrollMagnet);
				this.twistScrollCur = CFUtils.GetScrollValue(this.twistAngleCur, this.twistScrollCur, thresholds.twistScrollStep, thresholds.twistScrollMagnet);
				bool flag4 = false;
				int num = 0;
				switch (this.gestureDetectionOrder)
				{
				case SuperTouchZone.GestureDetectionOrder.TwistPinchSwipe:
					num = 136;
					break;
				case SuperTouchZone.GestureDetectionOrder.TwistSwipePinch:
					num = 80;
					break;
				case SuperTouchZone.GestureDetectionOrder.PinchTwistSwipe:
					num = 129;
					break;
				case SuperTouchZone.GestureDetectionOrder.PinchSwipeTwist:
					num = 17;
					break;
				case SuperTouchZone.GestureDetectionOrder.SwipeTwistPinch:
					num = 66;
					break;
				case SuperTouchZone.GestureDetectionOrder.SwipePinchTwist:
					num = 10;
					break;
				}
				for (int i = 0; i < 3; i++)
				{
					int num2 = num >> i * 3 & 7;
					if (num2 != 0)
					{
						if (num2 != 1)
						{
							if (num2 == 2)
							{
								if (multiFingerTouch.touchScreen.Swiped() || flag3)
								{
									if (this.noTwistAfterDrag)
									{
										flag = false;
									}
									if (this.noPinchAfterDrag)
									{
										flag2 = false;
									}
								}
							}
						}
						else if (this.pinchMoved || flag2)
						{
							if (this.noDragAfterPinch)
							{
								flag4 = true;
							}
							if (this.noTwistAfterPinch)
							{
								flag = false;
							}
						}
					}
					else if (this.twistMoved || flag)
					{
						if (this.noDragAfterTwist)
						{
							flag4 = true;
						}
						if (this.noPinchAfterTwist)
						{
							flag2 = false;
						}
					}
				}
				if (flag4)
				{
					multiFingerTouch.touchScreen.BlockSwipe();
					multiFingerTouch.touchOriented.BlockSwipe();
					flag3 = false;
				}
				if (flag3)
				{
					this.OnTwoFingerDragStart();
				}
				if (flag2)
				{
					this.OnPinchStart();
				}
				if (flag)
				{
					this.OnTwistStart();
				}
			}
		}

		private void OnPinchStart()
		{
			if (!this.pinchMoved)
			{
				this.pinchMoved = true;
				this.pinchJustMoved = true;
				if (this.startDragWhenPinching)
				{
					this.OnTwoFingerDragStart();
				}
				if (this.startTwistWhenPinching)
				{
					this.OnTwistStart();
				}
			}
		}

		private void OnTwistStart()
		{
			if (!this.twistMoved)
			{
				this.twistMoved = true;
				this.twistJustMoved = true;
				if (this.startDragWhenTwisting)
				{
					this.OnTwoFingerDragStart();
				}
				if (this.startPinchWhenTwisting)
				{
					this.OnPinchStart();
				}
			}
		}

		private void OnTwoFingerDragStart()
		{
			SuperTouchZone.MultiFingerTouch multiFingerTouch = this.GetMultiFingerTouch(2);
			if (multiFingerTouch == null)
			{
				return;
			}
			if (!multiFingerTouch.touchScreen.Swiped())
			{
				multiFingerTouch.touchScreen.ForceSwipe();
				multiFingerTouch.touchOriented.ForceSwipe();
				if (this.startTwistWhenDragging)
				{
					this.OnTwistStart();
				}
				if (this.startPinchWhenDragging)
				{
					this.OnPinchStart();
				}
			}
		}

		private SuperTouchZone.FingerState GetFirstUnusedFinger(TouchObject newTouchObj)
		{
			if (this.strictMultiTouch && this.dontAllowNewFingers)
			{
				return null;
			}
			int num = Mathf.Min(this.maxFingers, this.fingers.Length);
			for (int i = 0; i < num; i++)
			{
				SuperTouchZone.FingerState fingerState = this.fingers[i];
				if (fingerState.touchObj == null)
				{
					return fingerState;
				}
			}
			return null;
		}

		private SuperTouchZone.FingerState GetFingerByTouch(TouchObject touchObj)
		{
			if (touchObj == null)
			{
				return null;
			}
			for (int i = 0; i < this.fingers.Length; i++)
			{
				if (touchObj == this.fingers[i].touchObj)
				{
					return this.fingers[i];
				}
			}
			return null;
		}

		public override bool CanBeActivatedByOtherControl(TouchControl c, TouchObject touchObj)
		{
			return base.CanBeActivatedByOtherControl(c, touchObj) && this.GetFirstUnusedFinger(touchObj) != null;
		}

		public override bool CanBeTouchedDirectly(TouchObject touchObj)
		{
			return base.CanBeTouchedDirectly(touchObj) && this.GetFirstUnusedFinger(touchObj) != null;
		}

		public override bool CanBeSwipedOverFromNothing(TouchObject touchObj)
		{
			return base.CanBeSwipedOverFromNothingDefault(touchObj) && this.GetFirstUnusedFinger(touchObj) != null;
		}

		public override bool CanBeSwipedOverFromRestrictedList(TouchObject touchObj)
		{
			return base.CanBeSwipedOverFromRestrictedListDefault(touchObj) && this.GetFirstUnusedFinger(touchObj) != null;
		}

		public override bool CanSwipeOverOthers(TouchObject touchObj)
		{
			if (this.swipeOverOthersMode == TouchControl.SwipeOverOthersMode.Disabled)
			{
				return false;
			}
			SuperTouchZone.FingerState fingerByTouch = this.GetFingerByTouch(touchObj);
			return fingerByTouch != null && base.CanSwipeOverOthersDefault(touchObj, touchObj, fingerByTouch.touchStartType);
		}

		public override bool OnTouchStart(TouchObject touch, TouchControl sender, TouchControl.TouchStartType touchStartType)
		{
			if (!base.IsInitialized)
			{
				return false;
			}
			SuperTouchZone.FingerState firstUnusedFinger = this.GetFirstUnusedFinger(touch);
			if (firstUnusedFinger == null)
			{
				return false;
			}
			if (this.emulatedFingers.OnSystemTouchStart(touch, sender, touchStartType))
			{
				return true;
			}
			if (!firstUnusedFinger.OnTouchStart(touch, 0f, touchStartType))
			{
				return false;
			}
			touch.AddControl(this);
			return true;
		}

		public override bool OnTouchEnd(TouchObject touch, TouchControl.TouchEndType touchEndType)
		{
			if (!base.IsInitialized)
			{
				return false;
			}
			if (this.emulatedFingers.OnSystemTouchEnd(touch, touchEndType))
			{
				return true;
			}
			for (int i = 0; i < this.fingers.Length; i++)
			{
				if (this.fingers[i].OnTouchEnd(touch, touchEndType))
				{
					return true;
				}
			}
			return false;
		}

		public override bool OnTouchMove(TouchObject touch)
		{
			if (!base.IsInitialized)
			{
				return false;
			}
			if (this.emulatedFingers.OnSystemTouchMove(touch))
			{
				return true;
			}
			for (int i = 0; i < this.fingers.Length; i++)
			{
				if (this.fingers[i].OnTouchMove(touch))
				{
					return true;
				}
			}
			return false;
		}

		public override bool OnTouchPressureChange(TouchObject touch)
		{
			for (int i = 0; i < this.fingers.Length; i++)
			{
				if (this.fingers[i].OnTouchPressureChange(touch))
				{
					return true;
				}
			}
			return false;
		}

		public override void ReleaseAllTouches()
		{
			if (!base.IsInitialized)
			{
				return;
			}
			for (int i = 0; i < this.fingers.Length; i++)
			{
				this.fingers[i].ReleaseTouch(true);
			}
			for (int j = 0; j < this.multiFingerTouch.Length; j++)
			{
				this.multiFingerTouch[j].End(true);
				this.multiFingerTouch[j].EndDrivingTouch(true);
			}
			this.emulatedFingers.EndMouseAndTouches(true);
			this.rawFingersPressedCur = 0;
		}

		protected override bool OnIsBoundToAxis(string axisName, InputRig rig)
		{
			for (int i = 0; i < Mathf.Min(this.maxFingers, 3); i++)
			{
				if (this.multiFingerConfigs[i].binding.IsBoundToAxis(axisName, rig))
				{
					return true;
				}
			}
			return this.twistAnalogBinding.IsBoundToAxis(axisName, rig) || this.twistDeltaBinding.IsBoundToAxis(axisName, rig) || this.twistLeftDigitalBinding.IsBoundToAxis(axisName, rig) || this.twistRightDigitalBinding.IsBoundToAxis(axisName, rig) || this.pinchAnalogBinding.IsBoundToAxis(axisName, rig) || this.pinchDeltaBinding.IsBoundToAxis(axisName, rig) || this.pinchDigitalBinding.IsBoundToAxis(axisName, rig) || this.spreadDigitalBinding.IsBoundToAxis(axisName, rig) || this.pinchScrollDeltaBinding.IsBoundToAxis(axisName, rig) || this.twistScrollDeltaBinding.IsBoundToAxis(axisName, rig);
		}

		protected override bool OnIsBoundToKey(KeyCode key, InputRig rig)
		{
			for (int i = 0; i < Mathf.Min(this.maxFingers, 3); i++)
			{
				if (this.multiFingerConfigs[i].binding.IsBoundToKey(key, rig))
				{
					return true;
				}
			}
			return this.twistAnalogBinding.IsBoundToKey(key, rig) || this.twistDeltaBinding.IsBoundToKey(key, rig) || this.twistLeftDigitalBinding.IsBoundToKey(key, rig) || this.twistRightDigitalBinding.IsBoundToKey(key, rig) || this.pinchAnalogBinding.IsBoundToKey(key, rig) || this.pinchDigitalBinding.IsBoundToKey(key, rig) || this.spreadDigitalBinding.IsBoundToKey(key, rig) || this.pinchScrollDeltaBinding.IsBoundToKey(key, rig) || this.twistScrollDeltaBinding.IsBoundToKey(key, rig);
		}

		public override bool IsUsingKeyForEmulation(KeyCode key)
		{
			return this.emuWithKeys && (key == this.emuKeyOneFinger || key == this.emuKeyTwoFingers || key == this.emuKeyThreeFingers || key == this.emuKeyPinch || key == this.emuKeySpread || key == this.emuKeyTwistL || key == this.emuKeyTwistR || key == this.emuKeySwipeU || key == this.emuKeySwipeR || key == this.emuKeySwipeD || key == this.emuKeySwipeL || key == this.emuMouseTwoFingersKey || key == this.emuMouseThreeFingersKey || key == this.emuMousePinchKey || key == this.emuMouseTwistKey);
		}

		protected override bool OnIsEmulatingTouches()
		{
			if (this.separateFingersAsEmuTouchesBinding.IsEmulatingTouches())
			{
				return true;
			}
			for (int i = 0; i < Mathf.Min(this.maxFingers, 3); i++)
			{
				if (this.multiFingerConfigs[i].binding.IsEmulatingTouches())
				{
					return true;
				}
			}
			return false;
		}

		protected override bool OnIsEmulatingMousePosition()
		{
			for (int i = 0; i < Mathf.Min(this.maxFingers, 3); i++)
			{
				if (this.multiFingerConfigs[i].binding.IsEmulatingMousePosition())
				{
					return true;
				}
			}
			return false;
		}

		protected override void OnGetSubBindingDescriptions(BindingDescriptionList descList, UnityEngine.Object undoObject, string parentMenuPath)
		{
			for (int i = 0; i < this.multiFingerConfigs.Length; i++)
			{
				if (descList.addUnusedBindings || i + 1 <= this.maxFingers)
				{
					descList.Add(this.multiFingerConfigs[i].binding, (i + 1).ToString() + "-Finger Touch", parentMenuPath, this);
				}
			}
			descList.Add(this.separateFingersAsEmuTouchesBinding, "Separate Fingers as Emu Touches", parentMenuPath, this);
			if (descList.addUnusedBindings || this.maxFingers >= 2)
			{
				descList.Add(this.pinchAnalogBinding, InputRig.InputSource.Analog, "Pinch (Analog)", parentMenuPath, this);
				descList.Add(this.pinchDeltaBinding, InputRig.InputSource.NormalizedDelta, "Pinch (Analog)", parentMenuPath, this);
				descList.Add(this.pinchDigitalBinding, "Pinch (Digital)", parentMenuPath, this);
				descList.Add(this.spreadDigitalBinding, "Spread (Digital)", parentMenuPath, this);
				descList.Add(this.twistAnalogBinding, InputRig.InputSource.Analog, "Twist (Analog)", parentMenuPath, this);
				descList.Add(this.twistDeltaBinding, InputRig.InputSource.NormalizedDelta, "Twist (Analog)", parentMenuPath, this);
				descList.Add(this.twistLeftDigitalBinding, "Twist Left (Digital)", parentMenuPath, this);
				descList.Add(this.twistRightDigitalBinding, "Twist Right (Digital)", parentMenuPath, this);
				descList.Add(this.pinchScrollDeltaBinding, "Pinch Scroll Delta", parentMenuPath, this);
				descList.Add(this.twistScrollDeltaBinding, "Twist Scroll Delta", parentMenuPath, this);
			}
		}

		public void DrawMarkerGUI()
		{
			this.emulatedFingers.DrawMarkers();
		}

		private void InitEmulatedFingers()
		{
			this.emulatedFingers = new SuperTouchZone.EmulatedFingerSystem(this);
		}

		private void ReleaseEmulatedFingers(bool cancel)
		{
			this.emulatedFingers.EndTouches(cancel);
		}

		private void UpdateEmulatedFingers()
		{
			this.emulatedFingers.Update();
		}

		public MultiTouchGestureThresholds customThresh;

		[Tooltip("How many fingers can be used on this zone at the same time?")]
		[Range(1f, 3f)]
		public int maxFingers = 3;

		public float touchSmoothing = 0.5f;

		public bool strictMultiTouch;

		public float strictMultiTouchTime;

		public bool freezeTwistWhenTooClose;

		public bool noPinchAfterDrag;

		public bool noPinchAfterTwist;

		public bool noTwistAfterDrag;

		public bool noTwistAfterPinch;

		public bool noDragAfterPinch;

		public bool noDragAfterTwist;

		public bool startPinchWhenTwisting;

		public bool startPinchWhenDragging;

		public bool startDragWhenPinching;

		public bool startDragWhenTwisting;

		public bool startTwistWhenDragging;

		public bool startTwistWhenPinching;

		public SuperTouchZone.GestureDetectionOrder gestureDetectionOrder;

		private SuperTouchZone.MultiFingerTouch[] multiFingerTouch;

		public SuperTouchZone.MultiFingerTouchConfig[] multiFingerConfigs;

		private SuperTouchZone.FingerState[] fingers;

		private SuperTouchZone.FingerState[] fingersOrdered;

		private int rawFingersPressedCur;

		private int rawFingersPressedPrev;

		private bool dontAllowNewFingers;

		private bool waitingForMoreFingers;

		private float elapsedSinceFirstTouch;

		private bool pinchMoved;

		private bool pinchJustMoved;

		private bool twistMoved;

		private bool twistJustMoved;

		private float pinchDistQuietInitial;

		private float pinchDistInitial;

		private float pinchDistCur;

		private float pinchDistPrev;

		private float twistAngleQuietInitial;

		private float twistAngleInitial;

		private float twistAngleCur;

		private float twistAnglePrev;

		private int twistScrollPrev;

		private int twistScrollCur;

		private int pinchScrollPrev;

		private int pinchScrollCur;

		public const int MAX_FINGERS = 3;

		private const float MIN_TWIST_FINGER_DIST_SQ = 0.1f;

		private const float MIN_PINCH_DIST = 0.1f;

		public EmuTouchBinding separateFingersAsEmuTouchesBinding;

		public AnalogConfig twistAnalogConfig;

		public AnalogConfig pinchAnalogConfig;

		public AxisBinding twistAnalogBinding;

		public AxisBinding twistDeltaBinding;

		public DigitalBinding twistRightDigitalBinding;

		public DigitalBinding twistLeftDigitalBinding;

		public ScrollDeltaBinding pinchScrollDeltaBinding;

		public ScrollDeltaBinding twistScrollDeltaBinding;

		public AxisBinding pinchAnalogBinding;

		public AxisBinding pinchDeltaBinding;

		public DigitalBinding pinchDigitalBinding;

		public DigitalBinding spreadDigitalBinding;

		public bool emuWithKeys;

		public KeyCode emuKeyOneFinger;

		public KeyCode emuKeyTwoFingers;

		public KeyCode emuKeyThreeFingers;

		public KeyCode emuKeySwipeU;

		public KeyCode emuKeySwipeR;

		public KeyCode emuKeySwipeD;

		public KeyCode emuKeySwipeL;

		public KeyCode emuKeyTwistR;

		public KeyCode emuKeyTwistL;

		public KeyCode emuKeyPinch;

		public KeyCode emuKeySpread;

		public KeyCode emuMouseTwoFingersKey = KeyCode.LeftControl;

		public KeyCode emuMouseTwistKey = KeyCode.LeftShift;

		public KeyCode emuMousePinchKey = KeyCode.LeftShift;

		public KeyCode emuMouseThreeFingersKey;

		public SuperTouchZone.EmuMouseAxis emuMousePinchAxis;

		public SuperTouchZone.EmuMouseAxis emuMouseTwistAxis = SuperTouchZone.EmuMouseAxis.Y;

		public float emuKeySwipeSpeed = 0.25f;

		public float emuKeyPinchSpeed = 0.25f;

		public float emuKeyTwistSpeed = 45f;

		public float mouseEmuTwistScreenFactor = 0.3f;

		private int multiFingerCountCur;

		private int multiFingerCountPrev;

		private const float MIN_EMU_FINGER_DIST_FACTOR = 0.1f;

		private const float MAX_EMU_FINGER_DIST_FACTOR = 0.9f;

		private SuperTouchZone.EmulatedFingerSystem emulatedFingers;

		public enum GestureDetectionOrder
		{
			TwistPinchSwipe,
			TwistSwipePinch,
			PinchTwistSwipe,
			PinchSwipeTwist,
			SwipeTwistPinch,
			SwipePinchTwist
		}

		public enum EmuMouseAxis
		{
			X,
			Y
		}

		protected class FingerState
		{
			public FingerState(SuperTouchZone zone)
			{
				this.zone = zone;
				this.touchObj = null;
				this.touchScreen = new TouchGestureBasicState();
				this.touchOriented = new TouchGestureBasicState();
				this.emuTouchId = -1;
			}

			public bool IsActive
			{
				get
				{
					return this.touchObj != null;
				}
			}

			public bool IsControlledByMouse()
			{
				return this.touchObj != null && this.touchObj.IsMouse();
			}

			public void Reset()
			{
				this.touchScreen.Reset();
				this.touchOriented.Reset();
			}

			public void Update()
			{
				this.touchScreen.Update();
				this.touchOriented.Update();
			}

			public bool OnTouchStart(TouchObject touchObj, float delay, TouchControl.TouchStartType touchStartType)
			{
				if (this.touchObj != null)
				{
					return false;
				}
				this.touchObj = touchObj;
				this.touchStartType = touchStartType;
				Vector2 vector = (touchStartType != TouchControl.TouchStartType.DirectPress) ? touchObj.screenPosCur : touchObj.screenPosStart;
				this.touchScreen.OnTouchStart(vector, touchObj.screenPosCur, delay, this.touchObj);
				this.touchOriented.OnTouchStart(this.zone.ScreenToOrientedPos(vector, touchObj.cam), this.zone.ScreenToOrientedPos(touchObj.screenPosCur, touchObj.cam), delay, this.touchObj);
				return true;
			}

			public bool OnTouchEnd(TouchObject touchObj, TouchControl.TouchEndType touchEndType)
			{
				if (this.touchObj != touchObj || this.touchObj == null)
				{
					return false;
				}
				this.touchScreen.OnTouchEnd(touchObj.screenPosCur, touchEndType != TouchControl.TouchEndType.Release);
				this.touchOriented.OnTouchEnd(this.zone.ScreenToOrientedPos(touchObj.screenPosCur, touchObj.cam), touchEndType != TouchControl.TouchEndType.Release);
				this.touchObj = null;
				return true;
			}

			public bool OnTouchMove(TouchObject touchObj)
			{
				if (this.touchObj != touchObj || this.touchObj == null)
				{
					return false;
				}
				this.touchScreen.OnTouchMove(touchObj.screenPosCur);
				this.touchOriented.OnTouchMove(this.zone.ScreenToOrientedPos(touchObj.screenPosCur, touchObj.cam));
				this.zone.CheckSwipeOff(touchObj, this.touchStartType);
				return true;
			}

			public bool OnTouchPressureChange(TouchObject touchObj)
			{
				if (this.touchObj != touchObj || this.touchObj == null)
				{
					return false;
				}
				this.touchScreen.OnTouchPressureChange(touchObj.GetPressure());
				this.touchOriented.OnTouchPressureChange(touchObj.GetPressure());
				return true;
			}

			public void ReleaseTouch(bool cancel)
			{
				if (this.touchObj != null)
				{
					this.touchObj.ReleaseControl(this.zone, (!cancel) ? TouchControl.TouchEndType.Release : TouchControl.TouchEndType.Cancel);
					this.touchObj = null;
				}
				this.touchScreen.OnTouchEnd(cancel);
				this.touchOriented.OnTouchEnd(cancel);
			}

			public SuperTouchZone zone;

			public TouchObject touchObj;

			public TouchControl.TouchStartType touchStartType;

			public int emuTouchId;

			public TouchGestureBasicState touchScreen;

			public TouchGestureBasicState touchOriented;
		}

		[Serializable]
		public class MultiFingerTouchConfig
		{
			public MultiFingerTouchConfig()
			{
				this.binding = new TouchGestureStateBinding(null);
				this.driveOtherControl = false;
				this.touchConfig = new TouchGestureConfig();
				this.swipeJoyConfig = new JoystickConfig();
			}

			public TouchGestureStateBinding binding;

			public bool driveOtherControl;

			public TouchControl controlToDriveOnRawPress;

			public TouchControl controlToDriveOnNormalPress;

			public TouchControl controlToDriveOnLongPress;

			public TouchControl controlToDriveOnNormalPressSwipe;

			public TouchControl controlToDriveOnNormalPressSwipeU;

			public TouchControl controlToDriveOnNormalPressSwipeR;

			public TouchControl controlToDriveOnNormalPressSwipeD;

			public TouchControl controlToDriveOnNormalPressSwipeL;

			public TouchControl controlToDriveOnLongPressSwipe;

			public TouchControl controlToDriveOnLongPressSwipeU;

			public TouchControl controlToDriveOnLongPressSwipeR;

			public TouchControl controlToDriveOnLongPressSwipeD;

			public TouchControl controlToDriveOnLongPressSwipeL;

			public TouchGestureConfig touchConfig;

			public JoystickConfig swipeJoyConfig;
		}

		protected class MultiFingerTouch
		{
			public MultiFingerTouch(int fingerCount, SuperTouchZone zone)
			{
				this.zone = zone;
				this.touchScreen = new TouchGestureState();
				this.touchOriented = new TouchGestureState();
				this.fingers = new SuperTouchZone.FingerState[fingerCount];
				this.drivingTouch = new TouchObject();
				this.swipeJoyState = new JoystickState(null);
				this.Reset();
			}

			public void Init(SuperTouchZone.MultiFingerTouchConfig config)
			{
				this.config = config;
				this.touchScreen.SetConfig((config == null) ? null : config.touchConfig);
				this.touchOriented.SetConfig((config == null) ? null : config.touchConfig);
				this.touchScreen.SetThresholds(this.zone.GetThresholds());
				this.touchOriented.SetThresholds(this.zone.GetThresholds());
				this.swipeJoyState.config = config.swipeJoyConfig;
			}

			public int GetFingerCount()
			{
				return this.fingers.Length;
			}

			public SuperTouchZone.FingerState GetFinger(int i)
			{
				return this.fingers[i];
			}

			public bool IsActive()
			{
				return this.active;
			}

			public bool IsQuiet()
			{
				return this.quietMode;
			}

			public void Reset()
			{
				this.touchScreen.Reset();
				this.touchOriented.Reset();
				this.swipeJoyState.Reset();
				this.active = false;
				this.quietMode = false;
			}

			public void EndDrivingTouch(bool cancel)
			{
				this.drivingTouch.End(cancel);
			}

			public void SyncToRig()
			{
				if (this.config == null)
				{
					return;
				}
				this.config.binding.SyncTouchState(this.touchScreen, this.touchOriented, this.swipeJoyState, this.zone.rig);
			}

			public void Start(SuperTouchZone.FingerState[] fingers, Vector2 startScreenPos, Vector2 startOrientedPos, bool quietMode, float quietAlreadyElapsed)
			{
				this.controlledByMouse = true;
				for (int i = 0; i < this.fingers.Length; i++)
				{
					this.fingers[i] = fingers[i];
					if (!this.fingers[i].IsControlledByMouse())
					{
						this.controlledByMouse = false;
					}
				}
				this.active = true;
				this.quietMode = quietMode;
				this.quietModeElapsed = quietAlreadyElapsed;
				this.posStartScreen = startScreenPos;
				this.posStartOriented = startOrientedPos;
				this.posCurScreen = startScreenPos;
				this.posCurOriented = startOrientedPos;
				if (!quietMode)
				{
					this.touchScreen.OnTouchStart(startScreenPos, startScreenPos, 0f, this.controlledByMouse, false, 1f);
					this.touchOriented.OnTouchStart(startOrientedPos, startOrientedPos, 0f, this.controlledByMouse, false, 1f);
				}
			}

			public void End(bool cancel)
			{
				if (!this.active)
				{
					return;
				}
				this.active = false;
				this.touchScreen.OnTouchEnd(cancel || this.quietMode);
				this.touchOriented.OnTouchEnd(cancel || this.quietMode);
			}

			public void EndAndReport()
			{
				if (!this.active)
				{
					return;
				}
				if (this.quietMode)
				{
					this.StartOfficially();
				}
				this.active = false;
				this.touchScreen.OnTouchEnd(false);
				this.touchOriented.OnTouchEnd(false);
			}

			public void SetPos(Vector2 screenPos, Vector2 orientedPos)
			{
				this.posCurScreen = screenPos;
				this.posCurOriented = orientedPos;
			}

			private bool IsPotentialTapPossible()
			{
				return this.active && this.quietMode;
			}

			public void CancelTap()
			{
				this.touchOriented.CancelTap();
				this.touchScreen.CancelTap();
			}

			public void DisableTapUntilRelease()
			{
				this.touchOriented.DisableTapUntilRelease();
				this.touchScreen.DisableTapUntilRelease();
			}

			public void StartOfficially()
			{
				if (!this.active || !this.quietMode)
				{
					return;
				}
				this.quietMode = false;
				this.touchScreen.OnTouchStart(this.posStartScreen, this.posCurScreen, this.quietModeElapsed, this.controlledByMouse, false, 1f);
				this.touchOriented.OnTouchStart(this.posStartOriented, this.posCurOriented, this.quietModeElapsed, this.controlledByMouse, false, 1f);
			}

			public void Update()
			{
				this.touchScreen.HoldDelayedEvents(this.IsPotentialTapPossible());
				this.touchOriented.HoldDelayedEvents(this.IsPotentialTapPossible());
				if (this.active)
				{
					for (int i = 0; i < this.fingers.Length; i++)
					{
						if (!this.fingers[i].touchScreen.PressedRaw())
						{
							this.EndAndReport();
							break;
						}
						if (this.fingers[i].touchScreen.Moved(this.zone.GetThresholds().tapMoveThreshPxSq))
						{
							this.touchScreen.MarkAsNonStatic();
							this.touchOriented.MarkAsNonStatic();
							if (this.quietMode)
							{
								this.StartOfficially();
							}
						}
					}
					if (this.active)
					{
						if (this.quietMode)
						{
							this.quietModeElapsed += CFUtils.realDeltaTime;
							if (this.quietModeElapsed > this.zone.strictMultiTouchTime)
							{
								this.StartOfficially();
							}
						}
						else
						{
							this.touchScreen.OnTouchMove(this.posCurScreen);
							this.touchOriented.OnTouchMove(this.posCurOriented);
						}
					}
				}
				this.touchScreen.Update();
				this.touchOriented.Update();
				if (this.config != null)
				{
					if (this.touchOriented.PressedRaw() && this.touchScreen.Swiped())
					{
						this.swipeJoyState.ApplyUnclampedVec(this.touchOriented.GetSwipeVecSmooth() / this.zone.GetThresholds().swipeJoystickRadPx);
					}
					this.swipeJoyState.Update();
					if (this.drivingTouch.IsOn())
					{
						if (!this.touchScreen.PressedRaw())
						{
							this.drivingTouch.End(false);
						}
						else
						{
							this.drivingTouch.MoveIfNeeded(this.touchScreen.GetCurPosRaw(), this.zone.GetCamera());
						}
					}
					else if (this.config.driveOtherControl)
					{
						if (this.config.controlToDriveOnRawPress != null)
						{
							if (this.touchScreen.JustPressedRaw())
							{
								this.StartDrivingControl(this.config.controlToDriveOnRawPress);
							}
						}
						else if (this.touchOriented.PressedLong())
						{
							if (this.config.controlToDriveOnLongPressSwipeU != null || this.config.controlToDriveOnLongPressSwipeD != null || this.config.controlToDriveOnLongPressSwipeR != null || this.config.controlToDriveOnLongPressSwipeL != null)
							{
								if (this.touchOriented.GetSwipeDirState4().GetCur() != Dir.N)
								{
									switch (this.touchOriented.GetSwipeDirState4().GetCur())
									{
									case Dir.U:
										this.StartDrivingControl(this.config.controlToDriveOnLongPressSwipeU);
										break;
									case Dir.R:
										this.StartDrivingControl(this.config.controlToDriveOnLongPressSwipeR);
										break;
									case Dir.D:
										this.StartDrivingControl(this.config.controlToDriveOnLongPressSwipeD);
										break;
									case Dir.L:
										this.StartDrivingControl(this.config.controlToDriveOnLongPressSwipeL);
										break;
									}
								}
							}
							else if (this.config.controlToDriveOnLongPressSwipe != null && this.touchOriented.Swiped())
							{
								this.StartDrivingControl(this.config.controlToDriveOnLongPressSwipe);
							}
							if (this.config.controlToDriveOnLongPress != null)
							{
								this.StartDrivingControl(this.config.controlToDriveOnLongPress);
							}
						}
						else if (this.touchOriented.PressedNormal())
						{
							if (this.config.controlToDriveOnNormalPressSwipeU != null || this.config.controlToDriveOnNormalPressSwipeD != null || this.config.controlToDriveOnNormalPressSwipeR != null || this.config.controlToDriveOnNormalPressSwipeL != null)
							{
								if (this.touchOriented.PressedNormal() && this.touchOriented.GetSwipeDirState4().GetCur() != Dir.N)
								{
									switch (this.touchOriented.GetSwipeDirState4().GetCur())
									{
									case Dir.U:
										this.StartDrivingControl(this.config.controlToDriveOnNormalPressSwipeU);
										break;
									case Dir.R:
										this.StartDrivingControl(this.config.controlToDriveOnNormalPressSwipeR);
										break;
									case Dir.D:
										this.StartDrivingControl(this.config.controlToDriveOnNormalPressSwipeD);
										break;
									case Dir.L:
										this.StartDrivingControl(this.config.controlToDriveOnNormalPressSwipeL);
										break;
									}
								}
							}
							else if (this.config.controlToDriveOnNormalPressSwipe != null && this.touchOriented.PressedNormal() && this.touchOriented.Swiped())
							{
								this.StartDrivingControl(this.config.controlToDriveOnNormalPressSwipe);
							}
							if (this.config.controlToDriveOnNormalPress != null && this.touchScreen.PressedNormal())
							{
								this.StartDrivingControl(this.config.controlToDriveOnNormalPress);
							}
						}
					}
				}
			}

			private void StartDrivingControl(TouchControl c)
			{
				if (c == null || !c.IsActive() || this.drivingTouch.IsOn())
				{
					return;
				}
				this.drivingTouch.Start(this.touchScreen.GetStartPos(), this.touchScreen.GetCurPosRaw(), this.zone.GetCamera(), false, false, 1f);
				if (!c.OnTouchStart(this.drivingTouch, this.zone, TouchControl.TouchStartType.ProxyPress))
				{
					this.drivingTouch.End(true);
				}
			}

			private SuperTouchZone zone;

			public TouchGestureState touchScreen;

			public TouchGestureState touchOriented;

			public float quietModeElapsed;

			private SuperTouchZone.FingerState[] fingers;

			public bool active;

			public bool quietMode;

			private Vector2 posStartScreen;

			private Vector2 posStartOriented;

			private Vector2 posCurScreen;

			private Vector2 posCurOriented;

			private bool controlledByMouse;

			private JoystickState swipeJoyState;

			private SuperTouchZone.MultiFingerTouchConfig config;

			private TouchObject drivingTouch;
		}

		private class EmulatedFingerObject : TouchObject
		{
			public EmulatedFingerObject(SuperTouchZone parentZone)
			{
				this.parentZone = parentZone;
			}

			public Vector2 emuPos;

			public SuperTouchZone parentZone;
		}

		private class EmulatedFingerSystem
		{
			public EmulatedFingerSystem(SuperTouchZone zone)
			{
				this.zone = zone;
				this.fingers = new SuperTouchZone.EmulatedFingerObject[3];
				for (int i = 0; i < this.fingers.Length; i++)
				{
					this.fingers[i] = new SuperTouchZone.EmulatedFingerObject(zone);
				}
			}

			public void EndMouseAndTouches(bool cancel)
			{
				if (this.mouseTouchObj != null)
				{
					this.mouseTouchObj.ReleaseControl(this.zone, (!cancel) ? TouchControl.TouchEndType.Release : TouchControl.TouchEndType.Cancel);
					this.mouseTouchObj = null;
				}
				this.EndTouches(cancel);
			}

			public void EndTouches(bool cancel)
			{
				this.curFingerNum = 0;
				for (int i = 0; i < this.fingers.Length; i++)
				{
					this.fingers[i].End(cancel);
				}
			}

			public bool OnSystemTouchStart(TouchObject touchObj, TouchControl sender, TouchControl.TouchStartType touchStartType)
			{
				if (sender != null && sender != this.zone)
				{
					return false;
				}
				if (this.mouseTouchObj != null)
				{
					return false;
				}
				SuperTouchZone.EmulatedFingerObject emulatedFingerObject = touchObj as SuperTouchZone.EmulatedFingerObject;
				if (emulatedFingerObject != null && emulatedFingerObject.parentZone == this.zone)
				{
					return false;
				}
				this.EndTouches(false);
				this.mouseTouchObj = touchObj;
				this.mouseTouchObj.AddControl(this.zone);
				int num = (!Input.GetKey(this.zone.emuMouseTwoFingersKey) && !Input.GetKey(this.zone.emuMouseTwistKey) && !Input.GetKey(this.zone.emuMousePinchKey)) ? ((!Input.GetKey(this.zone.emuMouseThreeFingersKey)) ? 1 : 3) : 2;
				this.lastMousePos = touchObj.screenPosCur;
				this.twistCur = 0f;
				this.pinchDistCur = 0.25f * (float)Mathf.Min(Screen.width, Screen.height);
				this.StartTouches(num, this.lastMousePos, touchObj.IsMouse());
				return true;
			}

			public bool OnSystemTouchEnd(TouchObject touchObj, TouchControl.TouchEndType touchEndType)
			{
				if (this.mouseTouchObj == null || this.mouseTouchObj != touchObj)
				{
					return false;
				}
				this.mouseTouchObj = null;
				this.EndTouches(touchEndType != TouchControl.TouchEndType.Release);
				return true;
			}

			public bool OnSystemTouchMove(TouchObject touchObj)
			{
				return this.mouseTouchObj != null && this.mouseTouchObj == touchObj;
			}

			public void StartTouches(int num, Vector2 center, bool isMouse)
			{
				this.EndTouches(false);
				if (num > this.fingers.Length)
				{
					num = this.fingers.Length;
				}
				this.curFingerNum = num;
				this.centerPos = center;
				for (int i = 0; i < num; i++)
				{
					SuperTouchZone.EmulatedFingerObject emulatedFingerObject = this.fingers[i];
					emulatedFingerObject.emuPos = this.GetFingerPos(i);
					emulatedFingerObject.Start(emulatedFingerObject.emuPos, emulatedFingerObject.emuPos, this.zone.GetCamera(), isMouse, false, 1f);
					this.zone.OnTouchStart(emulatedFingerObject, null, TouchControl.TouchStartType.DirectPress);
				}
			}

			public void Update()
			{
				float num = (float)Mathf.Min(Screen.width, Screen.height);
				if (this.mouseTouchObj != null)
				{
					Vector2 b = this.mouseTouchObj.screenPosCur - this.lastMousePos;
					this.lastMousePos = this.mouseTouchObj.screenPosCur;
					bool key = UnityEngine.Input.GetKey(this.zone.emuMouseTwistKey);
					bool flag = UnityEngine.Input.GetKey(this.zone.emuMousePinchKey);
					if (this.zone.emuMousePinchAxis == this.zone.emuMouseTwistAxis)
					{
						flag = false;
					}
					bool flag2 = UnityEngine.Input.GetKey(this.zone.emuMouseTwoFingersKey) || UnityEngine.Input.GetKey(this.zone.emuMouseThreeFingersKey) || (!key && !flag);
					if (key)
					{
						this.twistCur += b[(int)this.zone.emuMouseTwistAxis] * (90f / Mathf.Max(30f, this.zone.mouseEmuTwistScreenFactor * num));
					}
					if (flag)
					{
						this.pinchDistCur += b[(int)this.zone.emuMousePinchAxis];
					}
					if (flag2)
					{
						this.centerPos += b;
					}
				}
				else
				{
					Vector2 a = new Vector2((float)(((!Input.GetKey(this.zone.emuKeySwipeR)) ? 0 : 1) + ((!Input.GetKey(this.zone.emuKeySwipeL)) ? 0 : -1)), (float)(((!Input.GetKey(this.zone.emuKeySwipeU)) ? 0 : 1) + ((!Input.GetKey(this.zone.emuKeySwipeD)) ? 0 : -1)));
					float num2 = (float)(((!Input.GetKey(this.zone.emuKeyPinch)) ? 0 : -1) + ((!Input.GetKey(this.zone.emuKeySpread)) ? 0 : 1));
					float num3 = (float)(((!Input.GetKey(this.zone.emuKeyTwistL)) ? 0 : -1) + ((!Input.GetKey(this.zone.emuKeyTwistR)) ? 0 : 1));
					if (this.curFingerNum == 0)
					{
						int num4 = 0;
						if (a.sqrMagnitude > 0.0001f || UnityEngine.Input.GetKey(this.zone.emuKeyOneFinger))
						{
							num4 = 1;
						}
						else if (Mathf.Abs(num2) > 0.0001f || Mathf.Abs(num3) > 0.001f || UnityEngine.Input.GetKey(this.zone.emuKeyTwoFingers))
						{
							num4 = 2;
						}
						else if (UnityEngine.Input.GetKey(this.zone.emuKeyThreeFingers))
						{
							num4 = 3;
						}
						this.StartTouches(num4, this.zone.GetScreenSpaceCenter(this.zone.GetCamera()), false);
					}
					else if (a.sqrMagnitude < 0.0001f && Mathf.Abs(num2) < 0.0001f && Mathf.Abs(num3) < 0.001f && ((this.curFingerNum != 1) ? ((this.curFingerNum != 2) ? (this.curFingerNum != 3 || !Input.GetKey(this.zone.emuKeyThreeFingers)) : (!Input.GetKey(this.zone.emuKeyTwoFingers))) : (!Input.GetKey(this.zone.emuKeyOneFinger))))
					{
						this.EndTouches(false);
					}
					else
					{
						this.centerPos += a * (num * this.zone.emuKeySwipeSpeed * CFUtils.realDeltaTime);
						this.pinchDistCur += num2 * (num * this.zone.emuKeyPinchSpeed * CFUtils.realDeltaTime);
						this.pinchDistCur = Mathf.Clamp(this.pinchDistCur, num * 0.1f, num * 0.9f);
						this.twistCur += num3 * this.zone.emuKeyTwistSpeed * CFUtils.realDeltaTime;
					}
				}
				this.pinchDistCur = Mathf.Clamp(this.pinchDistCur, num * 0.1f, num * 0.9f);
				for (int i = 0; i < this.fingers.Length; i++)
				{
					SuperTouchZone.EmulatedFingerObject emulatedFingerObject = this.fingers[i];
					emulatedFingerObject.emuPos = this.GetFingerPos(i);
					if (emulatedFingerObject.IsOn())
					{
						emulatedFingerObject.MoveIfNeeded(emulatedFingerObject.emuPos, this.zone.GetCamera());
					}
				}
			}

			public void DrawMarkers()
			{
				Texture2D texture2D = null;
				Texture2D texture2D2 = null;
				Texture2D texture2D3 = null;
				if (TouchMarkerGUI.mInst != null)
				{
					texture2D = TouchMarkerGUI.mInst.fingerMarker;
					texture2D2 = TouchMarkerGUI.mInst.pinchHintMarker;
					texture2D3 = TouchMarkerGUI.mInst.twistHintMarker;
				}
				if (texture2D == null || texture2D2 == null || texture2D3 == null)
				{
					return;
				}
				float num = 32f;
				Matrix4x4 matrix = GUI.matrix;
				Color color = GUI.color;
				if (this.curFingerNum > 1)
				{
					bool key = UnityEngine.Input.GetKey(this.zone.emuMouseTwistKey);
					bool flag = UnityEngine.Input.GetKey(this.zone.emuMousePinchKey);
					if (this.zone.emuMousePinchAxis == this.zone.emuMouseTwistAxis)
					{
						flag = false;
					}
					float num2 = 0.6f;
					float num3 = 0.6f;
					if (key && flag)
					{
						float num4 = Time.unscaledTime / 2f;
						num2 *= Mathf.Clamp01(Mathf.Sin(num4 * 2f * 3.14159274f));
						num3 *= Mathf.Clamp01(Mathf.Sin((num4 + 0.5f) * 2f * 3.14159274f));
					}
					float num5 = 128f;
					Rect position = new Rect(-num5 * 0.5f, -num5 * 0.5f, num5, num5);
					Vector2 v = this.centerPos;
					v.y = (float)Screen.height - v.y;
					if (key)
					{
						GUI.color = new Color(1f, 1f, 1f, num2);
						GUI.matrix = Matrix4x4.TRS(v, Quaternion.Euler(0f, 0f, (float)((this.zone.emuMouseTwistAxis != SuperTouchZone.EmuMouseAxis.Y) ? 0 : -90)), Vector3.one);
						GUI.DrawTexture(position, texture2D3, ScaleMode.ScaleToFit);
					}
					if (flag)
					{
						GUI.color = new Color(1f, 1f, 1f, num3);
						GUI.matrix = Matrix4x4.TRS(v, Quaternion.Euler(0f, 0f, (float)((this.zone.emuMousePinchAxis != SuperTouchZone.EmuMouseAxis.Y) ? 0 : -90)), Vector3.one);
						GUI.DrawTexture(position, texture2D2, ScaleMode.ScaleToFit);
					}
				}
				GUI.color = new Color(1f, 1f, 1f, 0.5f);
				for (int i = 0; i < this.fingers.Length; i++)
				{
					if (this.fingers[i].IsOn())
					{
						Vector2 emuPos = this.fingers[i].emuPos;
						emuPos.y = (float)Screen.height - emuPos.y;
						GUI.matrix = Matrix4x4.TRS(emuPos, Quaternion.Euler(0f, 0f, this.GetFingerAngle(i)), Vector3.one);
						GUI.DrawTexture(new Rect(-(num * 0.5f), -num, num, num), texture2D);
					}
				}
				GUI.color = color;
				GUI.matrix = matrix;
			}

			private Vector2 GetFingerPos(int fingerId)
			{
				if (this.curFingerNum <= 1)
				{
					return this.centerPos;
				}
				float d = this.pinchDistCur * 0.5f;
				float f = this.GetFingerAngle(fingerId) * 0.0174532924f;
				return this.centerPos + new Vector2(Mathf.Sin(f), Mathf.Cos(f)) * d;
			}

			private float GetFingerAngle(int fingerId)
			{
				if (this.curFingerNum <= 1)
				{
					return 0f;
				}
				return 360f * ((float)fingerId / (float)this.curFingerNum) + this.twistCur;
			}

			public SuperTouchZone.EmulatedFingerObject[] fingers;

			private SuperTouchZone zone;

			private TouchObject mouseTouchObj;

			private Vector2 lastMousePos;

			private int curFingerNum;

			private Vector2 centerPos;

			private float twistCur;

			private float pinchDistCur;
		}
	}
}
