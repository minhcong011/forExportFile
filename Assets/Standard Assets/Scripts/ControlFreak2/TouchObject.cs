// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: ControlFreak2.TouchObject
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ControlFreak2
{
	public class TouchObject
	{
		public TouchObject()
		{
			this.controls = new List<TouchControl>(8);
			this.swipeOverTargetList = new List<TouchControl>(16);
			this.isSwipeOverRestricted = false;
			this.isOn = false;
			this.isPressureSensitive = false;
			this.normalizedPressure = 1f;
		}

		public bool IsOn()
		{
			return this.isOn;
		}

		public bool IsMouse()
		{
			return this.isMouse;
		}

		public bool IsPressureSensitive()
		{
			return this.isPressureSensitive;
		}

		public float GetPressure()
		{
			return this.normalizedPressure;
		}

		public int GetControlCount()
		{
			return this.controls.Count;
		}

		public void Start(Vector2 screenPosStart, Vector2 screenPosCur, Camera cam, bool isMouse, bool isPressureSensitive, float pressure)
		{
			this.cam = cam;
			this.screenPosStart = screenPosStart;
			this.screenPosCur = screenPosCur;
			this.isSwipeOverRestricted = false;
			this.swipeOverTargetList.Clear();
			this.isMouse = isMouse;
			this.isPressureSensitive = isPressureSensitive;
			this.normalizedPressure = pressure;
			this.isOn = true;
			this.OnControlListChange();
		}

		public void MoveIfNeeded(Vector2 screenPos, Camera cam)
		{
			if (!object.ReferenceEquals(cam, this.cam) || screenPos != this.screenPosCur)
			{
				this.Move(screenPos, cam);
			}
		}

		public void Move(Vector2 screenPos, Camera cam)
		{
			this.cam = cam;
			this.screenPosCur = screenPos;
			for (int i = 0; i < this.controls.Count; i++)
			{
				TouchControl touchControl = this.controls[i];
				if (touchControl != null)
				{
					touchControl.OnTouchMove(this);
				}
			}
		}

		public void End(bool cancel)
		{
			this.isOn = false;
			for (int i = 0; i < this.controls.Count; i++)
			{
				this.controls[i].OnTouchEnd(this, (!cancel) ? TouchControl.TouchEndType.Release : TouchControl.TouchEndType.Cancel);
			}
			this.controls.Clear();
			this.swipeOverTargetList.Clear();
			this.OnControlListChange();
		}

		public void SetPressure(float rawPressure, float maxPressure)
		{
			this.isPressureSensitive = true;
			this.normalizedPressure = ((maxPressure >= 0.001f) ? (rawPressure / maxPressure) : 1f);
			for (int i = 0; i < this.controls.Count; i++)
			{
				TouchControl touchControl = this.controls[i];
				if (touchControl != null)
				{
					touchControl.OnTouchPressureChange(this);
				}
			}
		}

		public void ReleaseControl(TouchControl c, TouchControl.TouchEndType touchEndType)
		{
			int num = this.controls.IndexOf(c);
			if (num < 0)
			{
				return;
			}
			c.OnTouchEnd(this, touchEndType);
			this.controls.RemoveAt(num);
			this.OnControlListChange();
		}

		public void AddControl(TouchControl c)
		{
			if (c == null)
			{
				return;
			}
			if (this.controls.Contains(c))
			{
				return;
			}
			this.controls.Add(c);
			this.OnControlListChange();
		}

		protected void OnControlListChange()
		{
			this.isSwipeOverRestricted = false;
			this.swipeOverTargetList.Clear();
			for (int i = 0; i < this.controls.Count; i++)
			{
				TouchControl touchControl = this.controls[i];
				if (!(touchControl == null))
				{
					if (!touchControl.CanSwipeOverOthers(this))
					{
						this.isSwipeOverRestricted = true;
					}
					else if (touchControl.restictSwipeOverTargets)
					{
						this.isSwipeOverRestricted = true;
						for (int j = 0; j < touchControl.swipeOverTargetList.Count; j++)
						{
							TouchControl item = touchControl.swipeOverTargetList[j];
							if (touchControl != null && !this.controls.Contains(item) && !this.swipeOverTargetList.Contains(item))
							{
								this.swipeOverTargetList.Add(item);
							}
						}
					}
				}
			}
		}

		public bool CanAcceptControl(TouchControl c)
		{
			for (int i = 0; i < this.controls.Count; i++)
			{
				TouchControl touchControl = this.controls[i];
				if (touchControl != null && !touchControl.CanShareTouchWith(c))
				{
					return false;
				}
			}
			return true;
		}

		public List<TouchControl> GetRestrictedSwipeOverTargetList()
		{
			return (!this.isSwipeOverRestricted) ? null : this.swipeOverTargetList;
		}

		public bool SwipeOverFromNothingControlFilter(TouchControl c)
		{
			return c != null && c.CanBeSwipedOverFromNothing(this);
		}

		public bool DirectTouchControlFilter(TouchControl c)
		{
			return c != null && c.CanBeTouchedDirectly(this);
		}

		private bool isOn;

		private bool isMouse;

		private bool isPressureSensitive;

		private float normalizedPressure;

		private List<TouchControl> controls;

		public Vector2 screenPosCur;

		public Vector2 screenPosStart;

		public Camera cam;

		private bool isSwipeOverRestricted;

		private List<TouchControl> swipeOverTargetList;
	}
}
