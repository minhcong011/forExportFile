// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: ControlFreak2.TouchControl
using System;
using System.Collections.Generic;
using ControlFreak2.Internal;
using UnityEngine;

namespace ControlFreak2
{
	[RequireComponent(typeof(RectTransform))]
	public abstract class TouchControl : ComponentBase, IBindingContainer
	{
		public TouchControl()
		{
			this.disablingConditions = new DisablingConditionSet(null);
			this.animatorList = new List<TouchControlAnimatorBase>(2);
			this.swipeOverTargetList = new List<TouchControl>(2);
		}

		public InputRig rig
		{
			get
			{
				return this._rig;
			}
			protected set
			{
				this._rig = value;
			}
		}

		public Canvas canvas
		{
			get
			{
				return this._canvas;
			}
			protected set
			{
				this._canvas = value;
			}
		}

		public TouchControlPanel panel
		{
			get
			{
				return this._panel;
			}
			protected set
			{
				this._panel = value;
			}
		}

		protected abstract void OnInitControl();

		protected abstract void OnUpdateControl();

		protected abstract void OnDestroyControl();

		public void GetSubBindingDescriptions(BindingDescriptionList descList, UnityEngine.Object undoObject, string parentMenuPath)
		{
			this.OnGetSubBindingDescriptions(descList, undoObject, parentMenuPath);
		}

		public bool IsBoundToAxis(string axisName, InputRig rig)
		{
			return this.OnIsBoundToAxis(axisName, rig);
		}

		public bool IsBoundToKey(KeyCode key, InputRig rig)
		{
			return this.OnIsBoundToKey(key, rig);
		}

		public bool IsEmulatingTouches()
		{
			return this.OnIsEmulatingTouches();
		}

		public bool IsEmulatingMousePosition()
		{
			return this.OnIsEmulatingMousePosition();
		}

		public virtual bool IsUsingKeyForEmulation(KeyCode key)
		{
			return false;
		}

		protected virtual void OnGetSubBindingDescriptions(BindingDescriptionList descList, UnityEngine.Object undoObject, string parentMenuPath)
		{
		}

		protected virtual bool OnIsBoundToAxis(string axisName, InputRig rig)
		{
			return false;
		}

		protected virtual bool OnIsBoundToKey(KeyCode key, InputRig rig)
		{
			return false;
		}

		protected virtual bool OnIsEmulatingTouches()
		{
			return false;
		}

		protected virtual bool OnIsEmulatingMousePosition()
		{
			return false;
		}

		public abstract void ResetControl();

		public abstract void ReleaseAllTouches();

		public abstract bool OnTouchStart(TouchObject touch, TouchControl sender, TouchControl.TouchStartType touchStartType);

		public abstract bool OnTouchEnd(TouchObject touch, TouchControl.TouchEndType touchEndType);

		public abstract bool OnTouchMove(TouchObject touch);

		public abstract bool OnTouchPressureChange(TouchObject touch);

		protected bool CheckSwipeOff(TouchObject touchObj, TouchControl.TouchStartType touchStartType)
		{
			if (touchObj == null || this.swipeOffMode == TouchControl.SwipeOffMode.Disabled || (this.swipeOffMode == TouchControl.SwipeOffMode.OnlyIfSwipedOver && touchStartType != TouchControl.TouchStartType.SwipeOver))
			{
				return false;
			}
			if (!this.RaycastScreen(touchObj.screenPosCur, touchObj.cam))
			{
				touchObj.ReleaseControl(this, TouchControl.TouchEndType.SwipeOff);
				return true;
			}
			return false;
		}

		public virtual bool CanShareTouchWith(TouchControl c)
		{
			return true;
		}

		public abstract bool CanBeSwipedOverFromNothing(TouchObject touchObj);

		protected bool CanBeSwipedOverFromNothingDefault(TouchObject touchObj)
		{
			return this.canBeSwipedOver && this.IsActive();
		}

		public abstract bool CanBeSwipedOverFromRestrictedList(TouchObject touchObj);

		protected bool CanBeSwipedOverFromRestrictedListDefault(TouchObject touchObj)
		{
			return this.IsActive();
		}

		public abstract bool CanSwipeOverOthers(TouchObject touchObj);

		protected bool CanSwipeOverOthersDefault(TouchObject touchObj, TouchObject myTouchObj, TouchControl.TouchStartType touchStartType)
		{
			return myTouchObj == touchObj && (this.swipeOverOthersMode == TouchControl.SwipeOverOthersMode.Enabled || (this.swipeOverOthersMode == TouchControl.SwipeOverOthersMode.OnlyIfTouchedDirectly && touchStartType != TouchControl.TouchStartType.SwipeOver));
		}

		public virtual bool CanBeTouchedDirectly(TouchObject touchObj)
		{
			return !this.cantBeControlledDirectly && this.IsActive();
		}

		public virtual bool CanBeActivatedByOtherControl(TouchControl c, TouchObject touchObj)
		{
			return this.IsActive();
		}

		public static int CompareByDepth(TouchControl a, TouchControl b)
		{
			if (a == null || b == null)
			{
				return (!(a == null) || !(b == null)) ? ((!(a == null)) ? -1 : 1) : 0;
			}
			float z = a.transform.position.z;
			float z2 = a.transform.position.z;
			return (Mathf.Abs(z - z2) >= 0.001f) ? ((z >= z2) ? 1 : -1) : 0;
		}

		public void ShowOrHideControl(bool show, bool noAnim = false)
		{
			this.SetHidingFlag(0, !show);
			this.SyncBaseAlphaToHidingConditions(noAnim);
		}

		public void ShowControl(bool noAnim = false)
		{
			this.ShowOrHideControl(true, noAnim);
		}

		public void HideControl(bool noAnim = false)
		{
			this.ShowOrHideControl(false, noAnim);
		}

		public bool IsHiddenManually()
		{
			return (this.hidingFlagsCur | 0) != 0;
		}

		public bool IsActiveButInvisible()
		{
			return this.IsActive() && this.GetAlpha() <= 0.0001f;
		}

		public bool IsActiveAndVisible()
		{
			return this.IsActive() && this.GetAlpha() > 0.0001f;
		}

		public bool IsActive()
		{
			return (this.hidingFlagsCur & -65) == 0;
		}

		public void SetHidingFlag(int flagBit, bool state)
		{
			if (flagBit < 0 || flagBit > 31)
			{
				return;
			}
			this.hidingFlagsCur = ((!state) ? (this.hidingFlagsCur & ~(1 << flagBit)) : (this.hidingFlagsCur | 1 << flagBit));
			if (!this.IsActive())
			{
				this.ReleaseAllTouches();
			}
		}

		public void SyncDisablingConditions(bool skipAnim)
		{
			this.SetHidingFlag(1, !base.enabled || !base.gameObject.activeInHierarchy);
			if (this.rig != null)
			{
				this.SetHidingFlag(4, this.disablingConditions.IsInEffect());
				this.SetHidingFlag(5, this.rig.AreTouchControlsHiddenManually());
			}
			this.SyncBaseAlphaToHidingConditions(skipAnim);
			if (skipAnim)
			{
				this.UpdateAnimators(skipAnim);
			}
		}

		public void AddAnimator(TouchControlAnimatorBase a)
		{
			if (this.animatorList.Contains(a))
			{
				return;
			}
			this.animatorList.Add(a);
		}

		public void RemoveAnimator(TouchControlAnimatorBase a)
		{
			if (!this.animatorList.Contains(a))
			{
				return;
			}
			this.animatorList.Remove(a);
		}

		public List<TouchControlAnimatorBase> GetAnimatorList()
		{
			return this.animatorList;
		}

		protected void UpdateAnimators(bool skipAnim)
		{
			for (int i = 0; i < this.animatorList.Count; i++)
			{
				this.animatorList[i].UpdateAnimator(skipAnim);
			}
		}

		private void SyncBaseAlphaToHidingConditions(bool noAnim)
		{
			float num = (float)((this.hidingFlagsCur != 0) ? 0 : 1);
			if (Mathf.Abs(num - ((!this.baseAlphaAnimOn) ? this.baseAlphaCur : this.baseAlphaEnd)) > 0.001f)
			{
				this.StartAlphaAnim(num, (!noAnim && !(this.rig == null)) ? this.rig.controlBaseAlphaAnimDuration : 0f);
			}
		}

		private void StartAlphaAnim(float targetAlpha, float duration)
		{
			this.baseAlphaStart = this.baseAlphaCur;
			this.baseAlphaEnd = targetAlpha;
			if (duration <= 0.0001f)
			{
				this.baseAlphaAnimOn = false;
				this.baseAlphaCur = this.baseAlphaEnd;
			}
			else
			{
				this.baseAlphaAnimOn = true;
				this.baseAlphaAnimElapsed = 0f;
				this.baseAlphaAnimDur = duration;
			}
		}

		private void UpdateBaseAlpha()
		{
			if (this.baseAlphaAnimOn)
			{
				this.baseAlphaAnimElapsed += CFUtils.realDeltaTime;
				if (this.baseAlphaAnimElapsed >= this.baseAlphaAnimDur)
				{
					this.baseAlphaCur = this.baseAlphaEnd;
					this.baseAlphaAnimOn = false;
				}
				else
				{
					this.baseAlphaCur = Mathf.Lerp(this.baseAlphaStart, this.baseAlphaEnd, this.baseAlphaAnimElapsed / this.baseAlphaAnimDur);
				}
			}
		}

		public float GetBaseAlpha()
		{
			return this.baseAlphaCur;
		}

		public virtual float GetAlpha()
		{
			return this.baseAlphaCur;
		}

		public Camera GetCamera()
		{
			return (!(this.canvas != null) || this.canvas.renderMode == RenderMode.ScreenSpaceOverlay) ? null : this.canvas.worldCamera;
		}

		public void SetRig(InputRig rig)
		{
			if (rig != null && !rig.CanBeUsed())
			{
				rig = null;
			}
			if (this.rig != rig)
			{
				if (this.rig != null)
				{
					this.rig.RemoveControl(this);
				}
				this.rig = rig;
				if (this.rig != null)
				{
					this.rig.AddControl(this);
				}
			}
			this.disablingConditions.SetRig(this.rig);
			this.SyncDisablingConditions(true);
		}

		public void SetTouchControlPanel(TouchControlPanel panel)
		{
			if (this.panel == panel)
			{
				return;
			}
			if (this.panel != null)
			{
				this.panel.RemoveControl(this);
			}
			if (panel != null && !panel.CanBeUsed())
			{
				panel = null;
			}
			this.panel = panel;
			if (this.panel != null)
			{
				this.panel.AddControl(this);
			}
		}

		public virtual void InvalidateHierarchy()
		{
			InputRig inputRig = null;
			TouchControlPanel touchControlPanel = null;
			Canvas canvas = null;
			Transform parent = base.transform.parent;
			while (parent != null)
			{
				TouchControlPanel component = parent.GetComponent<TouchControlPanel>();
				InputRig component2 = parent.GetComponent<InputRig>();
				Canvas component3 = parent.GetComponent<Canvas>();
				if (touchControlPanel == null && component != null)
				{
					touchControlPanel = component;
				}
				if (inputRig == null && component2 != null)
				{
					inputRig = component2;
				}
				if (canvas == null && component3 != null)
				{
					canvas = component3;
				}
				if (inputRig != null && touchControlPanel != null && canvas != null)
				{
					break;
				}
				parent = parent.parent;
			}
			this.canvas = canvas;
			if (this.rig != inputRig)
			{
				this.SetRig(inputRig);
			}
			if (this.panel != touchControlPanel)
			{
				this.SetTouchControlPanel(touchControlPanel);
			}
		}

		public Vector3 GetWorldPos()
		{
			return base.transform.position;
		}

		public virtual void SetWorldPos(Vector2 pos2D)
		{
			this.SetWorldPosRaw(pos2D);
		}

		protected void SetWorldPosRaw(Vector2 pos2D)
		{
			Transform transform = base.transform;
			transform.position = new Vector3(pos2D.x, pos2D.y, transform.position.z);
		}

		public virtual Rect GetLocalRect()
		{
			RectTransform rectTransform = base.transform as RectTransform;
			if (rectTransform == null)
			{
				return new Rect(0f, 0f, 1f, 1f);
			}
			Rect rect = rectTransform.rect;
			if (this.shape == TouchControl.Shape.Circle || this.shape == TouchControl.Shape.Square)
			{
				Vector2 center = rect.center;
				float num = Mathf.Min(rect.width, rect.height);
				return new Rect(center.x - num * 0.5f, center.y - num * 0.5f, num, num);
			}
			return rect;
		}

		protected Vector3 ScreenToWorldPos(Vector2 sp, Camera cam)
		{
			Transform transform = base.transform;
			Vector3 vector = sp;
			if (cam != null)
			{
				if (Mathf.Abs(Vector3.Dot(transform.forward, cam.transform.forward)) >= 0.99999f)
				{
					vector = cam.ScreenToWorldPoint(vector);
					vector.z = transform.position.z;
				}
				else
				{
					float d = 0f;
					Ray ray = cam.ScreenPointToRay(sp);
					Plane plane = new Plane(transform.forward, transform.position);
					if (plane.Raycast(ray, out d))
					{
						vector = ray.origin + ray.direction * d;
					}
					else
					{
						vector = cam.ScreenToWorldPoint(vector);
						vector.z = transform.position.z;
					}
				}
			}
			else
			{
				vector.z = transform.position.z;
			}
			return vector;
		}

		protected Vector2 WorldToLocalPos(Vector3 wp, Vector2 worldOffset)
		{
			return base.transform.worldToLocalMatrix.MultiplyPoint3x4(wp + (Vector3)worldOffset);
		}

		protected Vector2 WorldToLocalPos(Vector3 wp)
		{
			return this.WorldToLocalPos(wp, Vector2.zero);
		}

		protected Vector2 ScreenToLocalPos(Vector2 sp, Camera cam, Vector2 worldOffset)
		{
			return this.WorldToLocalPos(this.ScreenToWorldPos(sp, cam), worldOffset);
		}

		protected Vector2 ScreenToLocalPos(Vector2 sp, Camera cam)
		{
			return this.ScreenToLocalPos(sp, cam, Vector2.zero);
		}

		protected Vector2 ScreenToNormalizedPos(Vector2 sp, Camera cam, Vector2 worldOffset)
		{
			return this.LocalToNormalizedPos(this.WorldToLocalPos(this.ScreenToWorldPos(sp, cam), worldOffset));
		}

		protected Vector2 ScreenToNormalizedPos(Vector2 sp, Camera cam)
		{
			return this.ScreenToNormalizedPos(sp, cam, Vector2.zero);
		}

		protected Vector2 LocalToNormalizedPos(Vector2 lp)
		{
			Rect localRect = this.GetLocalRect();
			lp -= localRect.center;
			lp.x /= localRect.width * 0.5f;
			lp.y /= localRect.height * 0.5f;
			return lp;
		}

		protected Vector2 WorldToNormalizedPos(Vector2 wp, Vector2 worldOffset)
		{
			return this.LocalToNormalizedPos(this.WorldToLocalPos(wp, worldOffset));
		}

		protected Vector2 WorldToNormalizedPos(Vector2 wp)
		{
			return this.WorldToNormalizedPos(wp, Vector2.zero);
		}

		protected Vector2 NormalizedToLocalPos(Vector2 np)
		{
			Rect localRect = this.GetLocalRect();
			np.x *= localRect.width * 0.5f;
			np.y *= localRect.height * 0.5f;
			return np + localRect.center;
		}

		protected Vector2 NormalizedToLocalOffset(Vector2 np)
		{
			Rect localRect = this.GetLocalRect();
			np.x *= localRect.width * 0.5f;
			np.y *= localRect.height * 0.5f;
			return np;
		}

		protected Vector3 NormalizedToWorldPos(Vector2 np)
		{
			Vector2 v = this.NormalizedToLocalPos(np);
			return base.transform.localToWorldMatrix.MultiplyPoint3x4(v);
		}

		protected Vector2 NormalizedToWorldOffset(Vector2 np)
		{
			Vector2 v = this.NormalizedToLocalOffset(np);
			return base.transform.localToWorldMatrix.MultiplyVector(v);
		}

		protected Vector3 WorldToScreenPos(Vector3 wp, Camera cam)
		{
			return (!(cam != null)) ? wp : cam.WorldToScreenPoint(wp);
		}

		protected Vector2 LocalToScreenPos(Vector2 lp, Camera cam)
		{
			return (!(cam != null)) ? base.transform.localToWorldMatrix.MultiplyPoint3x4(lp) : cam.WorldToScreenPoint(base.transform.localToWorldMatrix.MultiplyPoint3x4(lp));
		}

		public Vector2 ScreenToOrientedPos(Vector2 sp, Camera cam)
		{
			Quaternion rotation = Quaternion.identity;
			rotation = Quaternion.Inverse(base.transform.rotation);
			return rotation * sp;
		}

		public Vector3 GetWorldSpaceCenter()
		{
			Vector2 center = this.GetLocalRect().center;
			if (center == Vector2.zero)
			{
				return base.transform.position;
			}
			return base.transform.localToWorldMatrix.MultiplyPoint3x4(center);
		}

		public Vector3 GetWorldSpaceSize()
		{
			return this.GetWorldSpaceAABB().size;
		}

		public Bounds GetWorldSpaceAABB()
		{
			Rect localRect = this.GetLocalRect();
			return CFUtils.TransformRectAsBounds(localRect, base.transform.localToWorldMatrix, this.shape == TouchControl.Shape.Circle || this.shape == TouchControl.Shape.Ellipse);
		}

		public Vector2 GetScreenSpaceCenter(Camera cam)
		{
			Vector2 vector = this.GetWorldSpaceCenter();
			return (!(cam == null)) ? cam.WorldToScreenPoint(vector) : (Vector3)vector;
		}

		public Matrix4x4 GetWorldToNormalizedMatrix()
		{
			Rect localRect = this.GetLocalRect();
			return Matrix4x4.Scale(new Vector3(2f / localRect.width, 2f / localRect.height, 1f)) * Matrix4x4.TRS(-localRect.center, Quaternion.identity, Vector3.one) * base.transform.worldToLocalMatrix;
		}

		public Matrix4x4 GetNormalizedToWorldMatrix()
		{
			Rect localRect = this.GetLocalRect();
			return base.transform.localToWorldMatrix * Matrix4x4.TRS(localRect.center, Quaternion.identity, new Vector3(localRect.width * 0.5f, localRect.height * 0.5f, 1f));
		}

		protected Vector3 GetFollowPos(Vector3 targetWorldPos, Vector2 worldOffset, out bool posWasOutside)
		{
			Vector3 v = this.WorldToNormalizedPos(targetWorldPos, worldOffset);
			if (this.shape == TouchControl.Shape.Circle || this.shape == TouchControl.Shape.Ellipse)
			{
				if (v.sqrMagnitude <= 1f)
				{
					posWasOutside = false;
					return targetWorldPos;
				}
				v = CFUtils.ClampInsideUnitCircle(v);
			}
			else
			{
				if (v.x >= -1f && v.x <= 1f && v.y >= -1f && v.y <= 1f)
				{
					posWasOutside = false;
					return targetWorldPos;
				}
				v = CFUtils.ClampInsideUnitSquare(v);
			}
			Vector3 a = this.NormalizedToWorldPos(v);
			Vector3 worldSpaceCenter = this.GetWorldSpaceCenter();
			posWasOutside = true;
			return targetWorldPos - (a - worldSpaceCenter);
		}

		protected Vector3 GetFollowPos(Vector3 targetWorldPos, Vector3 worldOffset)
		{
			bool flag;
			return this.GetFollowPos(targetWorldPos, worldOffset, out flag);
		}

		protected Vector3 ClampInsideCanvas(Vector3 targetWorldPos, Canvas limiterCanvas)
		{
			RectTransform rectTransform;
			if (limiterCanvas == null || (rectTransform = (limiterCanvas.transform as RectTransform)) == null)
			{
				return targetWorldPos;
			}
			Rect localRect = this.GetLocalRect();
			Rect rect = rectTransform.rect;
			Matrix4x4 tr = limiterCanvas.transform.worldToLocalMatrix * CFUtils.ChangeMatrixTranl(base.transform.localToWorldMatrix, targetWorldPos);
			bool flag = this.shape == TouchControl.Shape.Circle || this.shape == TouchControl.Shape.Ellipse;
			Rect rect2 = CFUtils.TransformRect(localRect, tr, flag);
			Vector2 vector = CFUtils.ClampRectInside(rect2, flag, rect, false);
			if (vector == Vector2.zero)
			{
				return targetWorldPos;
			}
			return targetWorldPos + limiterCanvas.transform.localToWorldMatrix.MultiplyVector(vector);
		}

		protected Vector3 ClampInsideOther(Vector3 targetWorldPos, TouchControl limiter)
		{
			Rect localRect = this.GetLocalRect();
			Rect localRect2 = limiter.GetLocalRect();
			Matrix4x4 tr = limiter.transform.worldToLocalMatrix * CFUtils.ChangeMatrixTranl(base.transform.localToWorldMatrix, targetWorldPos);
			bool flag = this.shape == TouchControl.Shape.Circle || this.shape == TouchControl.Shape.Ellipse;
			bool limiterIsRound = limiter.shape == TouchControl.Shape.Circle || limiter.shape == TouchControl.Shape.Ellipse;
			Rect rect = CFUtils.TransformRect(localRect, tr, flag);
			Vector2 vector = CFUtils.ClampRectInside(rect, flag, localRect2, limiterIsRound);
			if (vector == Vector2.zero)
			{
				return targetWorldPos;
			}
			return targetWorldPos + limiter.transform.localToWorldMatrix.MultiplyVector(vector);
		}

		public bool RaycastScreen(Vector2 screenPos, Camera cam)
		{
			return this.RaycastLocal(this.ScreenToLocalPos(screenPos, cam));
		}

		public bool RaycastLocal(Vector2 localPos)
		{
			Rect localRect = this.GetLocalRect();
			switch (this.shape)
			{
			case TouchControl.Shape.Rectangle:
			case TouchControl.Shape.Square:
				return localPos.x >= localRect.x && localPos.x <= localRect.xMax && localPos.y >= localRect.y && localPos.y <= localRect.yMax;
			case TouchControl.Shape.Circle:
			{
				float num = localRect.width * 0.5f;
				return (localPos - localRect.center).sqrMagnitude <= num * num;
			}
			case TouchControl.Shape.Ellipse:
			{
				Vector2 vector = localPos - localRect.center;
				vector.x /= localRect.width * 0.5f;
				vector.y /= localRect.height * 0.5f;
				return vector.sqrMagnitude <= 1f;
			}
			default:
				return false;
			}
		}

		public bool HitTest(Vector2 sp, Camera cam, float fingerRadPx, TouchControl.Hit hit)
		{
			hit.Reset();
			bool flag = this.ignoreFingerRadius || fingerRadPx < 0.001f;
			Vector2 vector = this.ScreenToLocalPos(sp, cam);
			Rect localRect = this.GetLocalRect();
			bool flag2 = false;
			Vector2 vector2 = Vector2.zero;
			Vector2 vector3 = vector - localRect.center;
			switch (this.shape)
			{
			case TouchControl.Shape.Rectangle:
			case TouchControl.Shape.Square:
				if (!(flag2 = (vector.x >= localRect.x && vector.x <= localRect.xMax && vector.y >= localRect.y && vector.y <= localRect.yMax)) && !flag)
				{
					vector2 = CFUtils.ClampInsideRect(vector, localRect);
				}
				break;
			case TouchControl.Shape.Circle:
			{
				float num = localRect.width * 0.5f;
				if (!(flag2 = (vector3.sqrMagnitude <= num * num)) && !flag)
				{
					vector2 = vector3.normalized * num;
				}
				break;
			}
			case TouchControl.Shape.Ellipse:
			{
				Vector2 vector4 = vector3;
				vector4.x /= localRect.width * 0.5f;
				vector4.y /= localRect.height * 0.5f;
				if (!(flag2 = (vector4.sqrMagnitude <= 1f)) && !flag && !flag)
				{
					vector2 = vector3.normalized;
					vector2.x *= localRect.width * 0.5f;
					vector2.y *= localRect.height * 0.5f;
				}
				break;
			}
			}
			if (flag && !flag2)
			{
				return false;
			}
			Vector2 vector5 = this.LocalToScreenPos(vector2, cam);
			bool flag3 = !flag2 && (sp - vector5).sqrMagnitude <= fingerRadPx * fingerRadPx;
			if (flag2 || flag3)
			{
				hit.c = this;
				hit.indirectHit = flag3;
				hit.depth = base.transform.position.z;
				hit.localPos = vector;
				hit.closestLocalPos = ((!flag2) ? vector2 : vector);
				hit.screenDistSqPx = (sp - this.LocalToScreenPos(localRect.center, cam)).sqrMagnitude;
				hit.screenPos = sp;
				hit.closestScreenPos = vector5;
				return true;
			}
			return false;
		}

		protected override void OnInitComponent()
		{
			this.StartAlphaAnim(1f, 0f);
			this.InvalidateHierarchy();
			this.OnInitControl();
			this.SyncDisablingConditions(true);
		}

		protected override void OnEnableComponent()
		{
			this.ResetControl();
			this.SyncDisablingConditions(true);
		}

		protected override void OnDisableComponent()
		{
			this.SyncDisablingConditions(true);
			this.ReleaseAllTouches();
			this.ResetControl();
		}

		protected override void OnDestroyComponent()
		{
			this.SetRig(null);
			this.SetTouchControlPanel(null);
			this.OnDestroyControl();
		}

		public void UpdateControl()
		{
			if (!base.CanBeUsed())
			{
				return;
			}
			this.UpdateBaseAlpha();
			this.OnUpdateControl();
			this.UpdateAnimators(false);
		}

		protected void DrawDefaultGizmo(bool drawFullRect)
		{
			this.DrawDefaultGizmo(drawFullRect, 0.33f);
		}

		protected void DrawDefaultGizmo(bool drawFullRect, float fullRectColorShade)
		{
		}

		protected virtual void DrawCustomGizmos(bool selected)
		{
			Color color = Gizmos.color;
			Gizmos.color = ((!selected) ? Color.white : Color.red);
			this.DrawDefaultGizmo(true);
			Gizmos.color = color;
		}

		private void OnDrawGizmos()
		{
			if (!base.IsInitialized)
			{
				return;
			}
			this.DrawCustomGizmos(false);
		}

		private void OnDrawGizmosSelected()
		{
			if (!base.IsInitialized)
			{
				return;
			}
			this.DrawCustomGizmos(true);
		}

		public bool ignoreFingerRadius;

		public bool cantBeControlledDirectly;

		public bool shareTouch;

		public bool dontAcceptSharedTouches;

		public bool canBeSwipedOver;

		public bool restictSwipeOverTargets;

		public TouchControl.SwipeOverOthersMode swipeOverOthersMode = TouchControl.SwipeOverOthersMode.OnlyIfTouchedDirectly;

		public TouchControl.SwipeOffMode swipeOffMode = TouchControl.SwipeOffMode.OnlyIfSwipedOver;

		public List<TouchControl> swipeOverTargetList;

		public TouchControl.Shape shape;

		[NonSerialized]
		protected List<TouchControlAnimatorBase> animatorList;

		public DisablingConditionSet disablingConditions;

		[NonSerialized]
		private int hidingFlagsCur;

		[NonSerialized]
		private InputRig _rig;

		[NonSerialized]
		private Canvas _canvas;

		[NonSerialized]
		private TouchControlPanel _panel;

		protected const int HIDDEN_BY_USER = 0;

		protected const int HIDDEN_BY_DISABLED_GO = 1;

		protected const int HIDDEN_BY_CONDITIONS = 4;

		protected const int HIDDEN_BY_RIG = 5;

		protected const int HIDDEN_DUE_TO_INACTIVITY = 6;

		protected const int HIDDEN_AND_DISABLED_MASK = -65;

		[NonSerialized]
		private bool isHidden;

		[NonSerialized]
		private bool baseAlphaAnimOn;

		[NonSerialized]
		private float baseAlphaStart;

		[NonSerialized]
		private float baseAlphaEnd;

		[NonSerialized]
		private float baseAlphaCur;

		[NonSerialized]
		private float baseAlphaAnimDur;

		[NonSerialized]
		private float baseAlphaAnimElapsed;

		public enum Shape
		{
			Rectangle,
			Square,
			Circle,
			Ellipse
		}

		public enum SwipeOffMode
		{
			Disabled,
			Enabled,
			OnlyIfSwipedOver
		}

		public enum SwipeOverOthersMode
		{
			Disabled,
			Enabled,
			OnlyIfTouchedDirectly
		}

		public enum TouchStartType
		{
			DirectPress,
			ProxyPress,
			SwipeOver
		}

		public enum TouchEndType
		{
			Release,
			Cancel,
			SwipeOff
		}

		public class Hit
		{
			public void Reset()
			{
				this.c = null;
			}

			public bool IsEmpty()
			{
				return this.c == null;
			}

			public void CopyFrom(TouchControl.Hit b)
			{
				this.c = b.c;
				this.depth = b.depth;
				this.indirectHit = b.indirectHit;
				this.localPos = b.localPos;
				this.closestLocalPos = b.closestLocalPos;
				this.screenPos = b.screenPos;
				this.closestScreenPos = b.closestScreenPos;
				this.screenDistSqPx = b.screenDistSqPx;
			}

			public bool IsHigherThan(TouchControl.Hit r)
			{
				if (this.c == null != (r.c == null))
				{
					return this.c != null;
				}
				if (Mathf.RoundToInt(this.depth) <= Mathf.RoundToInt(r.depth) && !(this.c is DynamicRegion) && r.c is DynamicRegion)
				{
					return true;
				}
				if (this.indirectHit != r.indirectHit)
				{
					return !this.indirectHit;
				}
				if (Mathf.RoundToInt(this.depth) != Mathf.RoundToInt(r.depth))
				{
					return this.depth < r.depth;
				}
				return this.screenDistSqPx != r.screenDistSqPx && this.screenDistSqPx < r.screenDistSqPx;
			}

			public TouchControl c;

			public float depth;

			public bool indirectHit;

			public Vector2 localPos;

			public Vector2 closestLocalPos;

			public Vector2 screenPos;

			public Vector2 closestScreenPos;

			public float screenDistSqPx;
		}

		public class HitPool : ObjectPoolBase<TouchControl.Hit>
		{
			public HitPool()
			{
				this.tempHit = new TouchControl.Hit();
			}

			public bool HitTestAny(List<TouchControl> controlList, Vector2 screenPos, Camera cam, float fingerRadPx = 0f, TouchControl.HitPool.TouchControlFilterFunc filter = null)
			{
				base.EnsureCapacity(1);
				base.Clear();
				for (int i = 0; i < controlList.Count; i++)
				{
					TouchControl touchControl = controlList[i];
					if (!(touchControl == null) && (filter == null || filter(touchControl)))
					{
						if (touchControl.HitTest(screenPos, cam, fingerRadPx, this.tempHit))
						{
							base.GetNewObject(-1).CopyFrom(this.tempHit);
							return true;
						}
					}
				}
				return false;
			}

			public int HitTest(List<TouchControl> controlList, Vector2 screenPos, Camera cam, int maxHits = 8, float fingerRadPx = 0f, TouchControl.HitPool.TouchControlFilterFunc filter = null)
			{
				if (maxHits < 1)
				{
					maxHits = 1;
				}
				base.Clear();
				base.EnsureCapacity(maxHits);
				for (int i = 0; i < controlList.Count; i++)
				{
					TouchControl touchControl = controlList[i];
					if (!(touchControl == null) && (filter == null || filter(touchControl)))
					{
						if (touchControl.HitTest(screenPos, cam, fingerRadPx, this.tempHit))
						{
							int num = -1;
							for (int j = 0; j < base.GetList().Count; j++)
							{
								if (this.tempHit.IsHigherThan(base.GetList()[j]))
								{
									num = j;
									break;
								}
							}
							if (base.GetUsedCount() < maxHits)
							{
								base.GetNewObject(num).CopyFrom(this.tempHit);
							}
							else if (num >= 0)
							{
								TouchControl.Hit hit = base.GetList()[maxHits - 1];
								base.GetList().RemoveAt(maxHits - 1);
								base.GetList().Insert(num, hit);
								hit.CopyFrom(this.tempHit);
							}
						}
					}
				}
				return base.GetUsedCount();
			}

			protected override TouchControl.Hit CreateInternalObject()
			{
				return new TouchControl.Hit();
			}

			private TouchControl.Hit tempHit;

			public delegate bool TouchControlFilterFunc(TouchControl c);
		}
	}
}
