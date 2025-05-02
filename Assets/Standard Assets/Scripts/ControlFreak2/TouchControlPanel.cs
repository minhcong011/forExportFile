// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: ControlFreak2.TouchControlPanel
using System;
using System.Collections.Generic;
using ControlFreak2.Internal;
using UnityEngine;

namespace ControlFreak2
{
	[ExecuteInEditMode]
	public class TouchControlPanel : ComponentBase
	{
		public TouchControlPanel()
		{
			this.controls = new List<TouchControl>(16);
			this.hitPool = new TouchControl.HitPool();
			this.hitPool.EnsureCapacity(8);
			this.touchList = new List<TouchControlPanel.SystemTouch>(16);
			for (int i = 0; i < 16; i++)
			{
				TouchControlPanel.SystemTouch item = new TouchControlPanel.SystemTouch(this);
				this.touchList.Add(item);
			}
		}

		protected override void OnInitComponent()
		{
			this.InvalidateHierarchy();
		}

		protected override void OnDestroyComponent()
		{
			this.ReleaseAll(true);
			if (this.controls != null)
			{
				foreach (TouchControl touchControl in this.controls)
				{
					if (touchControl != null)
					{
						touchControl.SetTouchControlPanel(null);
					}
				}
			}
		}

		protected override void OnEnableComponent()
		{
		}

		protected override void OnDisableComponent()
		{
			this.ReleaseAll(true);
		}

		private void Update()
		{
			this.UpdatePanel();
		}

		public void UpdatePanel()
		{
			this.UpdateTouches();
		}

		public void InvalidateHierarchy()
		{
			if (this.autoConnectToRig || this.rig == null)
			{
				this.rig = base.GetComponent<InputRig>();
				if (this.rig == null)
				{
					this.rig = base.GetComponentInParent<InputRig>();
				}
			}
		}

		public void AddControl(TouchControl c)
		{
			if (!base.CanBeUsed())
			{
				return;
			}
			if (this.controls.Contains(c))
			{
				return;
			}
			this.controls.Add(c);
		}

		public void RemoveControl(TouchControl c)
		{
			if (!base.CanBeUsed())
			{
				return;
			}
			if (this.controls != null)
			{
				this.controls.Remove(c);
			}
		}

		public List<TouchControl> GetControlList()
		{
			return this.controls;
		}

		private void Prepare()
		{
			this.fingerRadInPx = ((!(this.rig != null)) ? 0.1f : this.rig.fingerRadiusInCm) * CFScreen.dpcm;
		}

		public bool Raycast(Vector2 sp, Camera eventCamera)
		{
			if (this.rig != null)
			{
				if (this.rig.AreTouchControlsHiddenManually())
				{
					return false;
				}
				if (this.rig.AreTouchControlsSleeping())
				{
					return true;
				}
				if (this.rig.swipeOverFromNothing)
				{
					return true;
				}
			}
			this.Prepare();
			return this.hitPool.HitTestAny(this.controls, sp, eventCamera, this.fingerRadInPx, new TouchControl.HitPool.TouchControlFilterFunc(this.RaycastControlFilter));
		}

		private bool RaycastControlFilter(TouchControl c)
		{
			return c != null && c.CanBeTouchedDirectly(null);
		}

		public void OnSystemTouchStart(TouchControlPanel.SystemTouchEventData data)
		{
			if (!base.IsInitialized)
			{
				return;
			}
			if (this.rig != null)
			{
				this.rig.WakeTouchControlsUp();
			}
			TouchControlPanel.SystemTouch systemTouch = this.StartNewTouch(data);
			if (systemTouch == null)
			{
				return;
			}
			this.Prepare();
			if (this.hitPool.HitTest(this.controls, data.pos, data.cam, 8, this.fingerRadInPx, new TouchControl.HitPool.TouchControlFilterFunc(systemTouch.touch.DirectTouchControlFilter)) > 0)
			{
				for (int i = 0; i < this.hitPool.GetList().Count; i++)
				{
					TouchControl.Hit hit = this.hitPool.GetList()[i];
					if (hit != null && !(hit.c == null))
					{
						if (!hit.c.dontAcceptSharedTouches || systemTouch.touch.GetControlCount() <= 0)
						{
							if (hit.c.OnTouchStart(systemTouch.touch, null, TouchControl.TouchStartType.DirectPress))
							{
								if (!hit.c.shareTouch)
								{
									break;
								}
							}
						}
					}
				}
			}
		}

		public void OnSystemTouchEnd(TouchControlPanel.SystemTouchEventData data)
		{
			if (!base.IsInitialized)
			{
				return;
			}
			if (this.rig != null)
			{
				this.rig.WakeTouchControlsUp();
			}
			TouchControlPanel.SystemTouch systemTouch = this.FindTouch(data.id);
			if (systemTouch == null)
			{
				return;
			}
			systemTouch.touch.End(false);
		}

		public void OnSystemTouchMove(TouchControlPanel.SystemTouchEventData data)
		{
			if (!base.IsInitialized)
			{
				return;
			}
			if (this.rig != null)
			{
				this.rig.WakeTouchControlsUp();
			}
			TouchControlPanel.SystemTouch systemTouch = this.FindTouch(data.id);
			if (systemTouch == null)
			{
				return;
			}
			systemTouch.WakeUp();
			Vector2 pos = data.pos;
			systemTouch.touch.Move(pos, data.cam);
			List<TouchControl> restrictedSwipeOverTargetList = systemTouch.touch.GetRestrictedSwipeOverTargetList();
			List<TouchControl> list = (restrictedSwipeOverTargetList == null) ? this.controls : restrictedSwipeOverTargetList;
			if (list.Count > 0 && this.hitPool.HitTest(list, systemTouch.touch.screenPosCur, systemTouch.touch.cam, 8, 0f, null) > 0)
			{
				for (int i = 0; i < this.hitPool.GetList().Count; i++)
				{
					TouchControl c = this.hitPool.GetList()[i].c;
					if (((restrictedSwipeOverTargetList != null) ? c.CanBeSwipedOverFromRestrictedList(systemTouch.touch) : c.CanBeSwipedOverFromNothing(systemTouch.touch)) && c.OnTouchStart(systemTouch.touch, null, TouchControl.TouchStartType.SwipeOver) && !c.shareTouch)
					{
						break;
					}
				}
			}
		}

		public void UpdateTouches()
		{
			for (int i = 0; i < this.touchList.Count; i++)
			{
				this.touchList[i].Update();
			}
			this.UpdateTouchPressure();
		}

		private bool IsTouchPressureSensitive(int touchId, out float pressureOut)
		{
			pressureOut = 1f;
			if (Input.touchPressureSupported)
			{
				for (int i = 0; i < UnityEngine.Input.touchCount; i++)
				{
					Touch touch = UnityEngine.Input.GetTouch(i);
					if (touch.phase != TouchPhase.Canceled && touch.phase != TouchPhase.Ended)
					{
						if (touch.fingerId == touchId)
						{
							pressureOut = touch.pressure / touch.maximumPossiblePressure;
							return true;
						}
					}
				}
			}
			return false;
		}

		private void UpdateTouchPressure()
		{
			if (Input.touchPressureSupported)
			{
				for (int i = 0; i < UnityEngine.Input.touchCount; i++)
				{
					Touch touch = UnityEngine.Input.GetTouch(i);
					if (touch.phase != TouchPhase.Canceled && touch.phase != TouchPhase.Ended)
					{
						TouchControlPanel.SystemTouch systemTouch = this.FindTouch(touch.fingerId);
						if (systemTouch != null)
						{
							systemTouch.touch.SetPressure(touch.pressure, touch.maximumPossiblePressure);
						}
					}
				}
			}
		}

		private void ReleaseAll(bool cancel)
		{
			for (int i = 0; i < this.touchList.Count; i++)
			{
				this.touchList[i].touch.End(cancel);
			}
		}

		private TouchControlPanel.SystemTouch FindTouch(int hwId)
		{
			for (int i = 0; i < this.touchList.Count; i++)
			{
				TouchControlPanel.SystemTouch systemTouch = this.touchList[i];
				if (systemTouch.touch.IsOn() && systemTouch.hwId == hwId)
				{
					return systemTouch;
				}
			}
			return null;
		}

		private TouchControlPanel.SystemTouch StartNewTouch(TouchControlPanel.SystemTouchEventData data)
		{
			TouchControlPanel.SystemTouch systemTouch = null;
			for (int i = 0; i < this.touchList.Count; i++)
			{
				TouchControlPanel.SystemTouch systemTouch2 = this.touchList[i];
				if (!systemTouch2.touch.IsOn())
				{
					systemTouch = systemTouch2;
				}
				else if (systemTouch2.hwId == data.id)
				{
					systemTouch2.touch.End(true);
				}
			}
			if (systemTouch != null)
			{
				systemTouch.elapsedSinceLastAction = 0f;
				systemTouch.hwId = data.id;
				systemTouch.startFrame = Time.frameCount;
				float pressure = 1f;
				bool isPressureSensitive = !data.isMouseEvent && this.IsTouchPressureSensitive(systemTouch.hwId, out pressure);
				systemTouch.touch.Start(data.pos, data.pos, data.cam, data.isMouseEvent, isPressureSensitive, pressure);
				return systemTouch;
			}
			return null;
		}

		public int GetActiveTouchCount()
		{
			int num = 0;
			for (int i = 0; i < this.touchList.Count; i++)
			{
				if (this.touchList[i].touch.IsOn())
				{
					num++;
				}
			}
			return num;
		}

		private const float staticFingerTimeout = 2f;

		public bool autoConnectToRig = true;

		public InputRig rig;

		[NonSerialized]
		protected List<TouchControl> controls;

		[NonSerialized]
		private List<TouchControlPanel.SystemTouch> touchList;

		[NonSerialized]
		private TouchControl.HitPool hitPool;

		private const int MAX_SYSTEM_TOUCHES = 16;

		private const int MAX_RAYCAST_HITS = 8;

		private float fingerRadInPx;

		public class SystemTouchEventData
		{
			public Vector2 pos;

			public Camera cam;

			public int id;

			public bool isMouseEvent;

			public int touchId;
		}

		private class SystemTouch
		{
			public SystemTouch(TouchControlPanel panel)
			{
				this.touch = new TouchObject();
				this.hwId = 0;
				this.elapsedSinceLastAction = 0f;
			}

			public void WakeUp()
			{
				this.elapsedSinceLastAction = 0f;
			}

			public void Update()
			{
				if (!this.touch.IsOn())
				{
					return;
				}
				if ((this.elapsedSinceLastAction += Time.unscaledDeltaTime) > 2f && !Input.GetMouseButton(0) && !Input.GetMouseButton(1) && !Input.GetMouseButton(2) && UnityEngine.Input.touchCount == 0)
				{
					this.touch.End(true);
				}
			}

			public TouchObject touch;

			public int hwId;

			public float elapsedSinceLastAction;

			public int startFrame;
		}
	}
}
