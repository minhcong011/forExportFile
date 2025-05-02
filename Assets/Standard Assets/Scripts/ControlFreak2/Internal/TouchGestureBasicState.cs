// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: ControlFreak2.Internal.TouchGestureBasicState
using System;
using UnityEngine;

namespace ControlFreak2.Internal
{
	public class TouchGestureBasicState
	{
		public virtual void Reset()
		{
			this.pressCur = false;
			this.pressPrev = false;
			this.isPressureSensitive = false;
			this.pressureCur = 1f;
			this.prepollState = false;
			this.pollReleased = false;
			this.pollReleasedAsCancel = false;
			this.pollStartedPress = false;
			this.pollStartPressureSensitive = false;
			this.pollStartPressure = 1f;
			this.pollCurPressure = 1f;
			this.curDist = 0f;
			this.extremeDistCurSq = 0f;
			this.extremeDistPrevSq = 0f;
			this.releasedDist = 0f;
			this.extremeDistPerAxisCur = Vector2.zero;
			this.extremeDistPerAxisPrev = Vector2.zero;
			this.posCurRaw = Vector2.zero;
			this.posCurSmooth = Vector2.zero;
			this.posPrevRaw = Vector2.zero;
			this.posPrevSmooth = Vector2.zero;
			this.relEndPos = Vector2.zero;
			this.relDur = 0f;
			this.releasedDist = 0f;
			this.relStartPos = Vector2.zero;
		}

		public void SetSmoothingTime(float smt)
		{
			this.smoothingTime = smt;
		}

		public bool PressedRaw()
		{
			return this.pressCur;
		}

		public bool JustPressedRaw()
		{
			return this.pressCur && !this.pressPrev;
		}

		public bool JustReleasedRaw()
		{
			return !this.pressCur && this.pressPrev;
		}

		public Vector2 GetCurPosRaw()
		{
			return this.posCurRaw;
		}

		public Vector2 GetCurPosSmooth()
		{
			return this.posCurSmooth;
		}

		public Vector2 GetStartPos()
		{
			return this.posStart;
		}

		public Vector2 GetReleasedStartPos()
		{
			return this.relStartPos;
		}

		public Vector2 GetReleasedEndPos()
		{
			return this.relEndPos;
		}

		public Vector2 GetSwipeVecRaw()
		{
			return this.posCurRaw - this.posStart;
		}

		public Vector2 GetSwipeVecSmooth()
		{
			return this.posCurSmooth - this.posStart;
		}

		public Vector2 GetDeltaVecRaw()
		{
			return this.posCurRaw - this.posPrevRaw;
		}

		public Vector2 GetDeltaVecSmooth()
		{
			return this.posCurSmooth - this.posPrevSmooth;
		}

		public bool Moved(float threshSquared)
		{
			return this.extremeDistCurSq > threshSquared;
		}

		public bool JustMoved(float threshSquared)
		{
			return this.extremeDistCurSq > threshSquared && this.extremeDistPrevSq <= threshSquared;
		}

		public bool IsControlledByMouse()
		{
			return this.controlledByMouse;
		}

		public bool IsPressureSensitive()
		{
			return this.PressedRaw() && this.isPressureSensitive;
		}

		public float GetPressure()
		{
			return (!this.PressedRaw()) ? 0f : this.pressureCur;
		}

		public virtual void Update()
		{
			this.InternalUpdate();
			this.InternalPostUpdate();
		}

		protected void InternalUpdate()
		{
			this.posPrevRaw = this.posCurRaw;
			this.posPrevSmooth = this.posCurSmooth;
			this.pressPrev = this.pressCur;
			this.extremeDistPrevSq = this.extremeDistCurSq;
			this.extremeDistPerAxisPrev = this.extremeDistPerAxisCur;
			if (this.pollReleased && this.prepollState)
			{
				this.posCurRaw = this.pollReleasePos;
				this.posCurSmooth = this.posCurRaw;
				this.OnRelease(this.pollReleasePos, this.pollReleasedAsCancel);
				this.pollReleased = false;
				this.prepollState = this.pressCur;
				if (this.pollStartedPress)
				{
					this.pollPressStartDelay += CFUtils.realDeltaTime;
				}
			}
			else if (this.pollStartedPress && !this.prepollState)
			{
				this.OnPress(this.pollPressStartPos, this.pollCurPos, this.pollPressStartDelay, this.pollStartedByMouse, this.pollStartPressureSensitive, this.pollStartPressure);
				this.pollStartedPress = false;
				this.prepollState = this.pressCur;
			}
			else
			{
				this.posCurRaw = this.pollCurPos;
				this.posCurSmooth = CFUtils.SmoothTowardsVec2(this.posCurSmooth, this.posCurRaw, this.smoothingTime, CFUtils.realDeltaTime, 0f, 0.75f);
				this.pressureCur = this.pollCurPressure;
			}
			if (this.pressCur)
			{
				this.CheckMovement(false);
			}
		}

		protected void InternalPostUpdate()
		{
			this.elapsedSincePress += CFUtils.realDeltaTime;
			this.elapsedSinceRelease += CFUtils.realDeltaTime;
			this.elapsedSinceLastMove += CFUtils.realDeltaTime;
		}

		public void OnTouchStart(Vector2 startPos, Vector2 curPos, float timeElapsed, TouchObject touchObj)
		{
			this.OnTouchStart(startPos, curPos, timeElapsed, touchObj.IsMouse(), touchObj.IsPressureSensitive(), touchObj.GetPressure());
		}

		public void OnTouchStart(Vector2 startPos, Vector2 curPos, float timeElapsed, bool controlledByMouse, bool isPressureSensitive, float pressure)
		{
			if (!this.prepollState)
			{
				this.pollReleased = false;
			}
			else if (!this.pollReleased)
			{
				return;
			}
			this.pollStartedPress = true;
			this.pollPressStartPos = startPos;
			this.pollPressStartDelay = timeElapsed;
			this.pollStartedByMouse = controlledByMouse;
			this.pollStartPressureSensitive = isPressureSensitive;
			this.pollStartPressure = pressure;
			this.pollCurPos = curPos;
		}

		public void OnTouchPressureChange(float pressure)
		{
			this.isPressureSensitive = true;
			this.pollCurPressure = pressure;
		}

		public void OnTouchMove(Vector2 pos)
		{
			this.pollCurPos = pos;
		}

		public void OnTouchMoveByDelta(Vector2 delta)
		{
			this.pollCurPos += delta;
		}

		public void OnTouchEnd(Vector2 pos, bool cancel)
		{
			if (this.prepollState)
			{
				if (this.pollReleased)
				{
					return;
				}
			}
			else if (!this.pollStartedPress)
			{
				return;
			}
			this.pollReleased = true;
			this.pollReleasedAsCancel = cancel;
			this.pollReleasePos = pos;
		}

		public void OnTouchEnd(bool cancel)
		{
			this.OnTouchEnd(this.pollCurPos, cancel);
		}

		protected virtual void OnPress(Vector2 startPos, Vector2 pos, float delay, bool startedByMouse, bool isPressureSensitive = false, float pressure = 1f)
		{
			this.elapsedSincePress = delay;
			this.posStart = startPos;
			this.posPrevRaw = startPos;
			this.posPrevSmooth = this.posPrevRaw;
			this.posCurRaw = pos;
			this.posCurSmooth = pos;
			this.pressCur = true;
			this.controlledByMouse = startedByMouse;
			this.isPressureSensitive = isPressureSensitive;
			this.pressureCur = pressure;
			this.extremeDistCurSq = 0f;
			this.extremeDistPrevSq = 0f;
			this.extremeDistPerAxisCur = (this.extremeDistPerAxisPrev = Vector2.zero);
			this.elapsedSinceLastMove = 0f;
		}

		protected virtual void OnRelease(Vector2 pos, bool cancel)
		{
			this.elapsedSinceRelease = 0f;
			this.posCurRaw = pos;
			this.posCurSmooth = pos;
			this.pressCur = false;
			this.CheckMovement(true);
			this.relStartPos = this.posStart;
			this.relEndPos = pos;
			this.relDur = this.elapsedSincePress;
			this.relDistSq = (this.relEndPos - this.relStartPos).sqrMagnitude;
			this.relExtremeDistSq = this.extremeDistCurSq;
			this.relExtremeDistPerAxis = this.extremeDistPerAxisCur;
		}

		protected virtual void CheckMovement(bool itsFinalUpdate)
		{
			Vector2 vector = this.posCurRaw - this.posStart;
			this.extremeDistCurSq = Mathf.Max(this.extremeDistCurSq, vector.sqrMagnitude);
			this.extremeDistPerAxisCur = Vector2.Max(this.extremeDistPerAxisCur, new Vector2(Mathf.Abs(vector.x), Mathf.Abs(vector.y)));
		}

		protected bool controlledByMouse;

		protected bool pollStartedByMouse;

		protected bool isPressureSensitive;

		protected bool pollStartPressureSensitive;

		protected float pressureCur;

		protected float pollStartPressure;

		protected bool pressCur;

		protected bool pressPrev;

		protected float releasedDur;

		protected float elapsedSincePress;

		protected float elapsedSinceRelease;

		protected float curDist;

		protected float releasedDist;

		protected Vector2 posCurRaw;

		protected Vector2 posPrevRaw;

		protected Vector2 posCurSmooth;

		protected Vector2 posPrevSmooth;

		protected Vector2 posNext;

		protected Vector2 posStart;

		protected float elapsedSinceLastMove;

		protected Vector2 relStartPos;

		protected Vector2 relEndPos;

		protected Vector2 relExtremeDistPerAxis;

		protected float relDur;

		protected float relExtremeDistSq;

		protected float relDistSq;

		protected float extremeDistCurSq;

		protected float extremeDistPrevSq;

		protected Vector2 extremeDistPerAxisCur;

		protected Vector2 extremeDistPerAxisPrev;

		protected float smoothingTime;

		protected bool prepollState;

		protected bool pollStartedPress;

		protected bool pollReleased;

		protected bool pollReleasedAsCancel;

		protected float pollCurPressure;

		protected Vector2 pollCurPos;

		protected Vector2 pollPressStartPos;

		protected Vector2 pollReleasePos;

		protected float pollPressStartDelay;
	}
}
