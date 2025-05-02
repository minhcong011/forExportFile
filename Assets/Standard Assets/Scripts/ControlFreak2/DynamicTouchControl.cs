// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: ControlFreak2.DynamicTouchControl
using System;
using ControlFreak2.Internal;
using UnityEngine;

namespace ControlFreak2
{
	public abstract class DynamicTouchControl : TouchControl
	{
		public DynamicTouchControl()
		{
			this.touchStateScreen = new TouchGestureBasicState();
			this.touchStateWorld = new TouchGestureBasicState();
			this.touchStateOriented = new TouchGestureBasicState();
			this.directInitialVector = Vector2.zero;
			this.indirectInitialVector = Vector2.zero;
			this.startFadedOut = true;
			this.fadeOutWhenReleased = true;
			this.touchSmoothing = 0.1f;
		}

		protected override void OnInitControl()
		{
			this.SetTargetDynamicRegion(this.targetDynamicRegion);
			this.SetTouchSmoothing(this.touchSmoothing);
			this.StoreDefaultPos();
		}

		public override void ResetControl()
		{
			if (this.CanFadeOut() && this.startFadedOut && !CFUtils.editorStopped)
			{
				this.DynamicFadeOut(false);
			}
			else
			{
				this.DynamicWakeUp(false);
			}
		}

		public override void InvalidateHierarchy()
		{
			base.InvalidateHierarchy();
			this.StoreDefaultPos();
		}

		public bool Pressed()
		{
			return this.touchStateWorld.PressedRaw();
		}

		public bool JustPressed()
		{
			return this.touchStateWorld.JustPressedRaw();
		}

		public bool JustReleased()
		{
			return this.touchStateWorld.JustReleasedRaw();
		}

		public bool IsTouchPressureSensitive()
		{
			return this.touchStateWorld.PressedRaw() && this.touchStateWorld.IsPressureSensitive();
		}

		public float GetTouchPressure()
		{
			return (!this.touchStateWorld.PressedRaw()) ? 0f : this.touchStateWorld.GetPressure();
		}

		public void SetTouchSmoothing(float smTime)
		{
			this.touchSmoothing = Mathf.Clamp01(smTime);
			this.touchStateWorld.SetSmoothingTime(this.touchSmoothing * 0.1f);
			this.touchStateOriented.SetSmoothingTime(this.touchSmoothing * 0.1f);
			this.touchStateScreen.SetSmoothingTime(this.touchSmoothing * 0.1f);
		}

		public void SetTargetDynamicRegion(DynamicRegion targetDynamicRegion)
		{
			this.targetDynamicRegion = targetDynamicRegion;
			if (targetDynamicRegion != null && targetDynamicRegion.CanBeUsed())
			{
				targetDynamicRegion.SetTargetControl(this);
			}
		}

		public void OnLinkToDynamicRegion(DynamicRegion dynRegion)
		{
			this.linkedToDynamicRegion = (dynRegion != null && dynRegion == this.targetDynamicRegion);
		}

		public DynamicRegion GetDynamicRegion()
		{
			return (!this.linkedToDynamicRegion) ? null : this.targetDynamicRegion;
		}

		public bool IsInDynamicMode()
		{
			return this.GetDynamicRegion() != null;
		}

		public float GetDynamicAlpha()
		{
			return this.dynamicAlphaCur;
		}

		public override float GetAlpha()
		{
			return this.GetDynamicAlpha() * base.GetAlpha();
		}

		private void SetDynamicAlpha(float alpha, float animDur)
		{
			if (animDur > 0.001f)
			{
				this.dynamicAlphaAnimDur = animDur;
				this.dynamicAlphaStart = this.dynamicAlphaCur;
				this.dynamicAlphaTarget = alpha;
				this.dynamicAlphaAnimElapsed = 0f;
				this.dynamicAlphaAnimOn = true;
			}
			else
			{
				this.dynamicAlphaAnimOn = false;
				this.dynamicAlphaAnimElapsed = 0f;
				this.dynamicAlphaStart = alpha;
				this.dynamicAlphaTarget = alpha;
				this.dynamicAlphaCur = alpha;
			}
		}

		private void DynamicWakeUp(bool animate)
		{
			this.dynamicIsFadingOut = false;
			this.dynamicWaitingToFadeOut = false;
			this.SetDynamicAlpha(1f, (!animate) ? 0f : this.fadeInDuration);
		}

		private void DynamicFadeOut(bool animate)
		{
			if (!animate)
			{
				this.dynamicIsFadingOut = true;
				this.dynamicWaitingToFadeOut = false;
				this.dynamicFadeOutDelayElapsed = 0f;
				this.SetDynamicAlpha(this.fadeOutTargetAlpha, 0f);
			}
			else
			{
				if (this.dynamicIsFadingOut)
				{
					return;
				}
				this.dynamicIsFadingOut = true;
				this.dynamicWaitingToFadeOut = true;
				this.dynamicFadeOutDelayElapsed = 0f;
			}
		}

		private bool CanFadeOut()
		{
			return this.fadeOutWhenReleased && this.IsInDynamicMode();
		}

		private void UpdateDynamicAlpha()
		{
			if (this.dynamicAlphaAnimOn)
			{
				this.dynamicAlphaAnimElapsed += CFUtils.realDeltaTime;
				if (this.dynamicAlphaAnimElapsed > this.dynamicAlphaAnimDur)
				{
					this.dynamicAlphaAnimOn = false;
					this.dynamicAlphaCur = this.dynamicAlphaTarget;
				}
				else
				{
					this.dynamicAlphaCur = Mathf.Lerp(this.dynamicAlphaStart, this.dynamicAlphaTarget, this.dynamicAlphaAnimElapsed / this.dynamicAlphaAnimDur);
				}
			}
			if (this.dynamicIsFadingOut)
			{
				if (this.dynamicWaitingToFadeOut)
				{
					this.dynamicFadeOutDelayElapsed += CFUtils.realDeltaTime;
					if (this.dynamicFadeOutDelayElapsed >= this.fadeOutDelay)
					{
						this.dynamicWaitingToFadeOut = false;
						this.SetDynamicAlpha(this.fadeOutTargetAlpha, this.fadeOutDuration);
					}
				}
			}
		}

		public override void SetWorldPos(Vector2 pos2D)
		{
			this.SetOriginPos(pos2D, false);
			this.StoreDefaultPos();
		}

		protected void SetOriginPos(Vector3 pos, bool animate)
		{
			this.originPos = pos;
			if (animate)
			{
				this.originStartPos = base.GetWorldPos();
				this.originAnimOn = true;
				this.originAnimElapsed = 0f;
			}
			else
			{
				base.SetWorldPosRaw(this.originPos);
				this.originStartPos = this.originPos;
				this.originAnimOn = false;
			}
		}

		protected void SetOriginPos(Vector3 pos)
		{
			this.SetOriginPos(pos, true);
		}

		protected Vector2 GetOriginOffset()
		{
			return base.transform.position - this.originPos;
		}

		protected void UpdateOriginAnimation()
		{
			if (this.originAnimOn)
			{
				this.originAnimElapsed += CFUtils.realDeltaTime;
				if (this.originAnimElapsed >= this.originSmoothTime * 0.2f)
				{
					this.originAnimOn = false;
					base.SetWorldPosRaw(this.originPos);
				}
				else
				{
					base.SetWorldPosRaw(Vector3.Lerp(this.originStartPos, this.originPos, this.originAnimElapsed / (this.originSmoothTime * 0.2f)));
				}
			}
		}

		protected void StoreDefaultPos()
		{
			if (CFUtils.editorStopped)
			{
				return;
			}
			if (this.initialRectCopyGo == null)
			{
				this.initialRectCopyGo = new GameObject(base.name + "_INITIAL_POS", new Type[]
				{
					typeof(InitialPosPlaceholder)
				});
			}
			RectTransform component = base.GetComponent<RectTransform>();
			this.initialRectCopyGo.transform.SetParent(component.parent, false);
			this.initialRectCopy = this.initialRectCopyGo.GetComponent<RectTransform>();
			this.initialRectCopyGo.hideFlags = HideFlags.DontSave;
			this.initialAnchorMin = component.anchorMin;
			this.initialAnchorMax = component.anchorMax;
			this.initialOffsetMin = component.offsetMin;
			this.initialOffsetMax = component.offsetMax;
			this.initialAnchoredPosition3D = component.anchoredPosition3D;
			this.initialPivot = component.pivot;
			this.SetupInitialRectPosition();
		}

		private void SetupInitialRectPosition()
		{
			if (this.initialRectCopy == null)
			{
				return;
			}
			this.initialRectCopy.anchoredPosition3D = this.initialAnchoredPosition3D;
			this.initialRectCopy.anchorMin = this.initialAnchorMin;
			this.initialRectCopy.anchorMax = this.initialAnchorMax;
			this.initialRectCopy.offsetMin = this.initialOffsetMin;
			this.initialRectCopy.offsetMax = this.initialOffsetMax;
			this.initialRectCopy.pivot = this.initialPivot;
		}

		protected Vector3 GetDefaultPos()
		{
			return (!(this.initialRectCopy == null)) ? this.initialRectCopy.position : base.transform.position;
		}

		protected override void OnDestroyControl()
		{
			this.ResetControl();
			if (this.initialRectCopyGo != null)
			{
				UnityEngine.Object.Destroy(this.initialRectCopyGo);
			}
		}

		protected override void OnUpdateControl()
		{
			if (this.touchObj != null && base.rig != null)
			{
				base.rig.WakeTouchControlsUp();
			}
			this.UpdateDynamicAlpha();
			this.touchStateWorld.Update();
			this.touchStateScreen.Update();
			this.touchStateOriented.Update();
			if (this.touchStateScreen.JustPressedRaw())
			{
				this.DynamicWakeUp(true);
				if (!this.IsInDynamicMode())
				{
					this.SetOriginPos(base.GetWorldPos(), false);
				}
				else
				{
					bool flag = this.GetDynamicAlpha() < 0.001f;
					if (!this.centerOnDirectTouch && !this.touchStartedByRegion)
					{
						this.SetOriginPos(base.GetWorldPos(), false);
					}
					else
					{
						Vector2 vector = this.touchStateWorld.GetStartPos();
						if (this.touchStartedByRegion)
						{
							if (this.indirectInitialVector != Vector2.zero)
							{
								vector -= base.NormalizedToWorldOffset(this.indirectInitialVector);
							}
						}
						else if (this.directInitialVector != Vector2.zero)
						{
							vector -= base.NormalizedToWorldOffset(this.directInitialVector);
						}
						if (!flag && !this.centerOnIndirectTouch)
						{
							vector = base.GetFollowPos(vector, Vector2.zero);
						}
						if (this.clampInsideRegion && this.GetDynamicRegion() != null)
						{
							vector = base.ClampInsideOther(vector, this.GetDynamicRegion());
						}
						if (this.clampInsideCanvas && base.canvas != null)
						{
							vector = base.ClampInsideCanvas(vector, base.canvas);
						}
						this.SetOriginPos(vector, !flag);
					}
				}
			}
			if (this.touchStateWorld.JustReleasedRaw() && (!this.IsInDynamicMode() || this.returnToStartingPosition))
			{
				this.SetOriginPos(this.GetDefaultPos(), true);
			}
			if (this.IsInDynamicMode() && this.fadeOutWhenReleased && !this.touchStateWorld.PressedRaw())
			{
				this.DynamicFadeOut(true);
			}
			this.touchStartedByRegion = false;
			if (this.touchStateWorld.PressedRaw() && this.stickyMode && (this.swipeOffMode == TouchControl.SwipeOffMode.Disabled || (this.swipeOffMode == TouchControl.SwipeOffMode.OnlyIfSwipedOver && this.touchStartType != TouchControl.TouchStartType.SwipeOver)))
			{
				bool flag2 = true;
				Vector3 targetWorldPos = this.touchStateWorld.GetCurPosSmooth();
				if (!this.centerWhenFollowing)
				{
					flag2 = false;
					targetWorldPos = base.GetFollowPos(targetWorldPos, this.GetOriginOffset(), out flag2);
				}
				if (flag2)
				{
					if (this.clampInsideRegion && this.GetDynamicRegion() != null)
					{
						targetWorldPos = base.ClampInsideOther(targetWorldPos, this.GetDynamicRegion());
					}
					if (this.clampInsideCanvas && base.canvas != null)
					{
						targetWorldPos = base.ClampInsideCanvas(targetWorldPos, base.canvas);
					}
					this.SetOriginPos(targetWorldPos);
				}
			}
			this.UpdateOriginAnimation();
		}

		public override bool OnTouchStart(TouchObject touch, TouchControl sender, TouchControl.TouchStartType touchStartType)
		{
			if (this.touchObj != null)
			{
				return false;
			}
			this.touchObj = touch;
			this.touchObj.AddControl(this);
			Vector3 v = (touchStartType != TouchControl.TouchStartType.DirectPress) ? touch.screenPosCur : touch.screenPosStart;
			Vector3 v2 = touch.screenPosCur;
			Vector3 v3 = base.ScreenToOrientedPos(v, touch.cam);
			Vector3 v4 = base.ScreenToOrientedPos(v2, touch.cam);
			Vector3 v5 = base.ScreenToWorldPos(v, touch.cam);
			Vector3 v6 = base.ScreenToWorldPos(v2, touch.cam);
			this.touchStateWorld.OnTouchStart(v5, v6, 0f, this.touchObj);
			this.touchStateScreen.OnTouchStart(v, v2, 0f, this.touchObj);
			this.touchStateOriented.OnTouchStart(v3, v4, 0f, this.touchObj);
			this.touchStartedByRegion = (sender != null && sender != this);
			this.touchStartType = touchStartType;
			return true;
		}

		public override bool OnTouchEnd(TouchObject touch, TouchControl.TouchEndType touchEndType)
		{
			if (touch != this.touchObj || this.touchObj == null)
			{
				return false;
			}
			this.touchObj = null;
			this.touchStateWorld.OnTouchEnd(touchEndType != TouchControl.TouchEndType.Release);
			this.touchStateScreen.OnTouchEnd(touchEndType != TouchControl.TouchEndType.Release);
			this.touchStateOriented.OnTouchEnd(touchEndType != TouchControl.TouchEndType.Release);
			return true;
		}

		public override bool OnTouchMove(TouchObject touch)
		{
			if (touch != this.touchObj || this.touchObj == null)
			{
				return false;
			}
			Vector3 v = touch.screenPosCur;
			Vector3 v2 = base.ScreenToWorldPos(touch.screenPosCur, touch.cam);
			Vector3 v3 = base.ScreenToOrientedPos(touch.screenPosCur, touch.cam);
			this.touchStateWorld.OnTouchMove(v2);
			this.touchStateScreen.OnTouchMove(v);
			this.touchStateOriented.OnTouchMove(v3);
			base.CheckSwipeOff(touch, this.touchStartType);
			return true;
		}

		public override bool OnTouchPressureChange(TouchObject touch)
		{
			if (touch != this.touchObj || this.touchObj == null)
			{
				return false;
			}
			this.touchStateWorld.OnTouchPressureChange(touch.GetPressure());
			this.touchStateScreen.OnTouchPressureChange(touch.GetPressure());
			this.touchStateOriented.OnTouchPressureChange(touch.GetPressure());
			return true;
		}

		public override void ReleaseAllTouches()
		{
			if (this.touchObj != null)
			{
				this.touchObj.ReleaseControl(this, TouchControl.TouchEndType.Cancel);
				this.touchObj = null;
			}
			this.touchStateWorld.OnTouchEnd(true);
			this.touchStateOriented.OnTouchEnd(true);
			this.touchStateScreen.OnTouchEnd(true);
		}

		public override bool CanBeTouchedDirectly(TouchObject touchObj)
		{
			return base.CanBeTouchedDirectly(touchObj) && this.touchObj == null;
		}

		public override bool CanBeSwipedOverFromNothing(TouchObject touchObj)
		{
			return base.CanBeSwipedOverFromNothingDefault(touchObj) && this.touchObj == null && base.IsActiveAndVisible();
		}

		public override bool CanBeSwipedOverFromRestrictedList(TouchObject touchObj)
		{
			return base.CanBeSwipedOverFromRestrictedListDefault(touchObj) && this.touchObj == null && base.IsActiveAndVisible();
		}

		public override bool CanSwipeOverOthers(TouchObject touchObj)
		{
			return base.CanSwipeOverOthersDefault(touchObj, this.touchObj, this.touchStartType);
		}

		public virtual bool CanBeActivatedByDynamicRegion()
		{
			return this.touchObj == null && base.IsActive();
		}

		public override bool CanBeActivatedByOtherControl(TouchControl c, TouchObject touchObj)
		{
			return base.CanBeActivatedByOtherControl(c, touchObj) && this.touchObj == null;
		}

		public bool fadeOutWhenReleased = true;

		public bool startFadedOut;

		public float fadeOutTargetAlpha;

		public float fadeInDuration = 0.2f;

		public float fadeOutDelay = 0.5f;

		public float fadeOutDuration = 0.5f;

		public bool centerOnDirectTouch = true;

		public bool centerOnIndirectTouch = true;

		public bool centerWhenFollowing;

		public bool stickyMode;

		public bool clampInsideRegion = true;

		public bool clampInsideCanvas = true;

		public bool returnToStartingPosition;

		public Vector2 directInitialVector;

		public Vector2 indirectInitialVector;

		public float touchSmoothing;

		[Tooltip("Stick's origin smoothing - the higher the value, the slower the movement.")]
		[Range(0f, 1f)]
		public float originSmoothTime = 0.5f;

		private const float ORIGIN_ANIM_MAX_TIME = 0.2f;

		public DynamicRegion targetDynamicRegion;

		protected bool linkedToDynamicRegion;

		protected bool touchStartedByRegion;

		protected TouchControl.TouchStartType touchStartType;

		protected TouchGestureBasicState touchStateOriented;

		protected TouchGestureBasicState touchStateScreen;

		protected TouchGestureBasicState touchStateWorld;

		protected TouchObject touchObj;

		private RectTransform initialRectCopy;

		private Vector3 originPos;

		private Vector3 originStartPos;

		private bool originAnimOn;

		private float originAnimElapsed;

		private bool dynamicIsFadingOut;

		private bool dynamicAlphaAnimOn;

		private bool dynamicWaitingToFadeOut;

		private float dynamicAlphaAnimDur;

		private float dynamicAlphaAnimElapsed;

		private float dynamicFadeOutDelayElapsed;

		private float dynamicAlphaCur;

		private float dynamicAlphaStart;

		private float dynamicAlphaTarget;

		private GameObject initialRectCopyGo;

		private Vector2 initialAnchorMax;

		private Vector2 initialAnchorMin;

		private Vector2 initialOffsetMin;

		private Vector2 initialOffsetMax;

		private Vector2 initialPivot;

		private Vector3 initialAnchoredPosition3D;
	}
}
