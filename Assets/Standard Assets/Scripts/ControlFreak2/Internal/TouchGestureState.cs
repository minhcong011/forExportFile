// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: ControlFreak2.Internal.TouchGestureState
using System;
using UnityEngine;

namespace ControlFreak2.Internal
{
	public class TouchGestureState : TouchGestureBasicState
	{
		public TouchGestureState(TouchGestureThresholds thresholds, TouchGestureConfig config)
		{
			this.BasicConstructor(thresholds, config);
		}

		public TouchGestureState()
		{
			this.BasicConstructor(null, null);
		}

		private void BasicConstructor(TouchGestureThresholds thresholds, TouchGestureConfig config)
		{
			this.config = config;
			this.thresh = thresholds;
			this.swipeDirState = new DirectionState();
			this.swipeDirState4 = new DirectionState();
			this.swipeDirState8 = new DirectionState();
			this.Reset();
		}

		public void SetThresholds(TouchGestureThresholds thresh)
		{
			this.thresh = thresh;
		}

		public void SetConfig(TouchGestureConfig config)
		{
			this.config = config;
		}

		public override void Reset()
		{
			base.Reset();
			this.swipeConstraint = TouchGestureConfig.DirConstraint.None;
			this.scrollConstraint = TouchGestureConfig.DirConstraint.None;
			this.swipeDirConstraint = TouchGestureConfig.DirConstraint.None;
			this.constrainedVecCur = Vector2.zero;
			this.constrainedVecPrev = Vector2.zero;
			this.tapCount = 0;
			this.tapCandidateCount = 0;
			this.elapsedSincePress = 0f;
			this.elapsedSinceRelease = 0f;
			this.tapCandidateReleased = false;
			this.moved = false;
			this.justMoved = false;
			this.justTapped = false;
			this.justLongTapped = false;
			this.cleanPressCur = false;
			this.cleanPressPrev = false;
			this.holdPressCur = false;
			this.holdPressPrev = false;
			this.flushTapsFlag = false;
			this.nonStaticFlag = false;
			this.cancelTapFlag = false;
			this.disableTapUntilReleaseFlag = false;
			this.swipeDirState.Reset();
			this.swipeDirState4.Reset();
			this.swipeDirState8.Reset();
		}

		public bool PressedNormal()
		{
			return this.cleanPressCur;
		}

		public bool JustPressedNormal()
		{
			return this.cleanPressCur && !this.cleanPressPrev;
		}

		public bool JustReleasedNormal()
		{
			return !this.cleanPressCur && this.cleanPressPrev;
		}

		public bool PressedLong()
		{
			return this.holdPressCur;
		}

		public bool JustPressedLong()
		{
			return this.holdPressCur && !this.holdPressPrev;
		}

		public bool JustReleasedLong()
		{
			return !this.holdPressCur && this.holdPressPrev;
		}

		public bool JustTapped(int howManyTimes)
		{
			return this.justTapped && this.tapCount == howManyTimes;
		}

		public Vector2 GetTapPos()
		{
			return this.tapPos;
		}

		public bool JustLongTapped()
		{
			return this.justLongTapped;
		}

		public DirectionState GetSwipeDirState()
		{
			return this.swipeDirState;
		}

		public DirectionState GetSwipeDirState4()
		{
			return this.swipeDirState4;
		}

		public DirectionState GetSwipeDirState8()
		{
			return this.swipeDirState8;
		}

		public Vector2 GetSegmentOrigin()
		{
			return this.segmentOrigin;
		}

		public bool JustSwiped()
		{
			return this.justMoved;
		}

		public bool Swiped()
		{
			return this.moved;
		}

		public Vector2 GetConstrainedSwipeVec()
		{
			return this.constrainedVecCur;
		}

		public Vector2 GetConstrainedDeltaVec()
		{
			return this.constrainedVecCur - this.constrainedVecPrev;
		}

		public Vector2 GetScroll()
		{
			return this.scrollVecCur;
		}

		public Vector2 GetScrollDelta()
		{
			return this.scrollVecCur - this.scrollVecPrev;
		}

		public float GetReleasedDurationRaw()
		{
			return this.relDur;
		}

		public void ForceSwipe()
		{
			if (!this.moved)
			{
				this.OnSwipeStart();
			}
		}

		public void BlockSwipe()
		{
			this.blockDrag = true;
			if (this.moved)
			{
				this.moved = false;
				this.justMoved = false;
			}
		}

		public void HoldDelayedEvents(bool holdThemForNow)
		{
			this.holdDelayedEvents = holdThemForNow;
		}

		public void CancelTap()
		{
			if (base.PressedRaw())
			{
				this.cancelTapFlag = true;
			}
			this.ResetTapState();
		}

		public void DisableTapUntilRelease()
		{
			this.disableTapUntilReleaseFlag = true;
			this.ResetTapState();
		}

		public void FlushRegisteredTaps()
		{
			this.flushTapsFlag = true;
		}

		public void MarkAsNonStatic()
		{
			this.nonStaticFlag = true;
		}

		public override void Update()
		{
			if (this.thresh != null)
			{
				this.thresh.Recalc(CFScreen.dpi);
			}
			this.justTapped = false;
			this.justMoved = false;
			this.justLongTapped = false;
			this.cleanPressPrev = this.cleanPressCur;
			this.holdPressPrev = this.holdPressCur;
			this.scrollVecPrev = this.scrollVecCur;
			this.constrainedVecPrev = this.constrainedVecCur;
			this.swipeDirState.BeginFrame();
			this.swipeDirState4.BeginFrame();
			this.swipeDirState8.BeginFrame();
			base.InternalUpdate();
			this.CheckTap(false);
			base.InternalPostUpdate();
			if (this.tapCandidateReleased)
			{
				this.elapsedSinceLastTapCandidate += CFUtils.realDeltaTime;
			}
		}

		protected override void OnRelease(Vector2 pos, bool cancel)
		{
			if (cancel)
			{
				this.CancelTap();
			}
			base.OnRelease(pos, cancel);
		}

		protected override void OnPress(Vector2 startPos, Vector2 pos, float delay, bool startedByMouse, bool isPressureSensitive, float pressure)
		{
			base.OnPress(startPos, pos, delay, startedByMouse, isPressureSensitive, pressure);
			this.blockDrag = false;
			this.moved = false;
			this.justMoved = false;
			this.nonStaticFlag = false;
			this.constrainedVecCur = (this.constrainedVecPrev = Vector2.zero);
			this.scrollVecCur = (this.scrollVecPrev = Vector2.zero);
			this.segmentOrigin = this.posCurSmooth;
			this.scrollConstraint = TouchGestureConfig.DirConstraint.None;
			this.swipeConstraint = TouchGestureConfig.DirConstraint.None;
			this.swipeDirConstraint = TouchGestureConfig.DirConstraint.None;
			if (this.config != null)
			{
				this.scrollConstraint = this.config.scrollConstraint;
				this.swipeConstraint = this.config.swipeConstraint;
				this.swipeDirConstraint = this.config.swipeDirConstraint;
			}
			this.swipeDirState.Reset();
			this.swipeDirState4.Reset();
			this.swipeDirState8.Reset();
		}

		private void OnSwipeStart()
		{
			if (!this.moved)
			{
				this.justMoved = true;
				this.moved = true;
			}
		}

		protected override void CheckMovement(bool itsFinalUpdate)
		{
			base.CheckMovement(itsFinalUpdate);
			if (this.thresh == null || this.config == null)
			{
				return;
			}
			if (!base.PressedRaw())
			{
				this.cleanPressCur = false;
				this.holdPressCur = false;
			}
			else
			{
				bool flag = false;
				if (this.holdPressCur)
				{
					if (this.config.endLongPressWhenMoved && (this.nonStaticFlag || base.Moved(this.thresh.tapMoveThreshPxSq)))
					{
						this.holdPressCur = false;
					}
					else if (this.config.endLongPressWhenSwiped && this.Swiped())
					{
						this.holdPressCur = false;
					}
				}
				else if (this.config.detectLongPress && !this.nonStaticFlag && !base.Moved(this.thresh.tapMoveThreshPxSq))
				{
					if (this.elapsedSincePress > this.thresh.longPressMinTime)
					{
						this.holdPressCur = true;
					}
					else
					{
						flag = true;
					}
				}
				if (!this.cleanPressCur)
				{
					if (!this.holdPressCur && !flag && !this.IsPotentialTap())
					{
						this.cleanPressCur = true;
					}
				}
			}
			Vector2 swipeVecRaw = base.GetSwipeVecRaw();
			if (this.scrollConstraint != TouchGestureConfig.DirConstraint.None)
			{
				if (this.scrollConstraint == TouchGestureConfig.DirConstraint.Auto)
				{
					if (Mathf.Abs(swipeVecRaw.x) > this.thresh.scrollThreshPx)
					{
						this.scrollConstraint = TouchGestureConfig.DirConstraint.Horizontal;
					}
					if (Mathf.Abs(swipeVecRaw.y) > this.thresh.scrollThreshPx && Mathf.Abs(swipeVecRaw.y) > Mathf.Abs(swipeVecRaw.x))
					{
						this.scrollConstraint = TouchGestureConfig.DirConstraint.Vertical;
					}
				}
				if (this.scrollConstraint == TouchGestureConfig.DirConstraint.Horizontal)
				{
					swipeVecRaw.y = 0f;
				}
				else if (this.scrollConstraint == TouchGestureConfig.DirConstraint.Vertical)
				{
					swipeVecRaw.x = 0f;
				}
			}
			for (int i = 0; i < 2; i++)
			{
				this.scrollVecCur[i] = (float)CFUtils.GetScrollValue(swipeVecRaw[i], (int)this.scrollVecCur[i], this.thresh.scrollThreshPx, this.thresh.scrollMagnetFactor);
			}
			this.constrainedVecCur = base.GetSwipeVecSmooth();
			if (this.swipeConstraint != TouchGestureConfig.DirConstraint.None)
			{
				if (this.swipeConstraint == TouchGestureConfig.DirConstraint.Auto)
				{
					if (Mathf.Abs(this.extremeDistPerAxisCur.x) > this.thresh.dragThreshPx)
					{
						this.swipeConstraint = TouchGestureConfig.DirConstraint.Horizontal;
					}
					if (Mathf.Abs(this.extremeDistPerAxisCur.y) > this.thresh.dragThreshPx && Mathf.Abs(this.extremeDistPerAxisCur.y) > Mathf.Abs(this.extremeDistPerAxisCur.x))
					{
						this.swipeConstraint = TouchGestureConfig.DirConstraint.Vertical;
					}
				}
				if (this.swipeConstraint == TouchGestureConfig.DirConstraint.Horizontal)
				{
					this.constrainedVecCur.y = 0f;
				}
				else if (this.swipeConstraint == TouchGestureConfig.DirConstraint.Vertical)
				{
					this.constrainedVecCur.x = 0f;
				}
				else
				{
					this.constrainedVecCur = Vector2.zero;
				}
			}
			if (!this.moved && !this.blockDrag && this.constrainedVecCur.sqrMagnitude > this.thresh.dragThreshPxSq)
			{
				this.OnSwipeStart();
			}
			Vector2 vec = this.posCurSmooth - this.segmentOrigin;
			if (this.swipeDirConstraint != TouchGestureConfig.DirConstraint.None)
			{
				if (this.swipeDirConstraint == TouchGestureConfig.DirConstraint.Auto)
				{
					if (Mathf.Abs(vec.x) > this.thresh.swipeSegLenPx)
					{
						this.swipeDirConstraint = TouchGestureConfig.DirConstraint.Horizontal;
					}
					if (Mathf.Abs(vec.y) > this.thresh.swipeSegLenPx && Mathf.Abs(vec.y) > Mathf.Abs(vec.x))
					{
						this.swipeDirConstraint = TouchGestureConfig.DirConstraint.Vertical;
					}
				}
				if (this.swipeDirConstraint == TouchGestureConfig.DirConstraint.Horizontal)
				{
					vec.y = 0f;
				}
				else if (this.swipeDirConstraint == TouchGestureConfig.DirConstraint.Vertical)
				{
					vec.x = 0f;
				}
				else
				{
					vec = Vector2.zero;
				}
			}
			float sqrMagnitude = vec.sqrMagnitude;
			if (sqrMagnitude > this.thresh.swipeSegLenPxSq)
			{
				vec.Normalize();
				this.swipeDirState4.SetDir(CFUtils.VecToDir(vec, this.swipeDirState4.GetCur(), 0.1f, false), this.config.swipeOriginalDirResetMode);
				this.swipeDirState8.SetDir(CFUtils.VecToDir(vec, this.swipeDirState8.GetCur(), 0.1f, true), this.config.swipeOriginalDirResetMode);
				this.swipeDirState.SetDir(((this.config.dirMode != TouchGestureConfig.DirMode.EightWay) ? this.swipeDirState4 : this.swipeDirState8).GetCur(), this.config.swipeOriginalDirResetMode);
				this.segmentOrigin = this.posCurSmooth;
			}
		}

		private void ResetTapState()
		{
			this.tapCandidateCount = 0;
			this.tapCandidateReleased = false;
			this.elapsedSinceLastTapCandidate = 0f;
			this.lastReportedTapCount = 0;
		}

		public bool IsPotentialTap()
		{
			return this.thresh != null && this.config != null && (base.PressedRaw() && !this.cancelTapFlag && !this.disableTapUntilReleaseFlag && this.config.maxTapCount > 0 && !this.nonStaticFlag && this.elapsedSincePress < this.thresh.tapMaxDur) && this.extremeDistCurSq < this.thresh.tapMoveThreshPxSq;
		}

		public bool IsPotentialLongPress()
		{
			return this.thresh != null && this.config != null && (base.PressedRaw() && !this.nonStaticFlag && this.config.detectLongPress && this.elapsedSincePress < this.thresh.longPressMinTime) && this.extremeDistCurSq < this.thresh.tapMoveThreshPxSq;
		}

		private void CheckTap(bool lastUpdate)
		{
			if (this.thresh == null || this.config == null)
			{
				return;
			}
			if (this.config.maxTapCount <= 0)
			{
				return;
			}
			if (this.flushTapsFlag)
			{
				this.ReportTap(true);
				this.flushTapsFlag = false;
			}
			if (!base.PressedRaw())
			{
				if (this.tapCandidateReleased && !this.holdDelayedEvents && this.elapsedSinceLastTapCandidate > this.thresh.multiTapMaxTimeGap)
				{
					this.ReportTap(true);
				}
			}
			else if (this.tapCandidateCount > 0 && ((!this.holdDelayedEvents && this.elapsedSincePress > this.thresh.tapMaxDur) || this.nonStaticFlag || this.extremeDistCurSq > this.thresh.tapMoveThreshPxSq))
			{
				if (this.config.cleanTapsOnly)
				{
					this.ResetTapState();
				}
				else
				{
					this.ReportTap(true);
				}
			}
			if (base.JustPressedRaw())
			{
				if (this.tapCandidateCount > 0 && (this.posCurRaw - this.tapFirstPos).sqrMagnitude > this.thresh.tapPosThreshPxSq)
				{
					if (this.config.cleanTapsOnly)
					{
						this.ResetTapState();
					}
					else
					{
						this.ReportTap(true);
					}
				}
				if (this.tapCandidateCount == 0)
				{
					this.tapFirstPos = this.posStart;
				}
			}
			if (base.JustReleasedRaw())
			{
				if (this.cancelTapFlag || this.disableTapUntilReleaseFlag)
				{
					this.ResetTapState();
				}
				else
				{
					if (this.config.detectLongTap && this.relDur > this.thresh.longPressMinTime && !this.nonStaticFlag && this.relDur <= this.thresh.longPressMinTime + this.thresh.longTapMaxDuration && this.relExtremeDistSq <= this.thresh.tapMoveThreshPxSq)
					{
						this.justLongTapped = true;
						this.tapPos = this.relStartPos;
					}
					if (this.relDur <= this.thresh.tapMaxDur && !this.nonStaticFlag && this.relExtremeDistSq <= this.thresh.tapMoveThreshPxSq)
					{
						this.tapCandidateCount++;
						if (this.tapCandidateCount >= this.config.maxTapCount)
						{
							this.ReportTap(true);
						}
						else
						{
							if (!this.config.cleanTapsOnly)
							{
								this.ReportTap(false);
							}
							this.tapCandidateReleased = true;
							this.elapsedSinceLastTapCandidate = 0f;
						}
					}
					else if (this.config.cleanTapsOnly)
					{
						this.ResetTapState();
					}
					else
					{
						this.ReportTap(true);
					}
				}
				this.disableTapUntilReleaseFlag = false;
				this.cancelTapFlag = false;
			}
		}

		private void ReportTap()
		{
			this.ReportTap(true);
		}

		private void ReportTap(bool reset)
		{
			if (this.tapCandidateCount > 0 && this.tapCandidateCount > this.lastReportedTapCount)
			{
				this.lastReportedTapCount = this.tapCandidateCount;
				this.justTapped = true;
				this.tapPos = this.tapFirstPos;
				this.tapCount = this.tapCandidateCount;
			}
			if (reset)
			{
				this.ResetTapState();
			}
		}

		private float elapsedSinceLastTapCandidate;

		private Vector2 segmentOrigin;

		private Vector2 constrainedVecCur;

		private Vector2 constrainedVecPrev;

		private Vector2 scrollVecCur;

		private Vector2 scrollVecPrev;

		private TouchGestureConfig.DirConstraint swipeConstraint;

		private TouchGestureConfig.DirConstraint swipeDirConstraint;

		private TouchGestureConfig.DirConstraint scrollConstraint;

		private bool cleanPressCur;

		private bool cleanPressPrev;

		private bool holdPressCur;

		private bool holdPressPrev;

		private Vector2 lastDirOrigin;

		public TouchGestureThresholds thresh;

		public TouchGestureConfig config;

		private bool moved;

		private bool justMoved;

		private bool nonStaticFlag;

		private bool cancelTapFlag;

		private bool disableTapUntilReleaseFlag;

		private bool flushTapsFlag;

		private bool holdDelayedEvents;

		private bool justTapped;

		private bool justLongTapped;

		private Vector2 tapPos;

		private int tapCount;

		private DirectionState swipeDirState;

		private DirectionState swipeDirState4;

		private DirectionState swipeDirState8;

		private bool blockDrag;

		private Vector2 tapFirstPos;

		private int tapCandidateCount;

		private int lastReportedTapCount;

		private bool tapCandidateReleased;
	}
}
