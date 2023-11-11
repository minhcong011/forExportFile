using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UnityStandardAssets.CrossPlatformInput
{
	// Token: 0x02000032 RID: 50
	public class Joystick : MonoBehaviour, IPointerDownHandler, IEventSystemHandler, IPointerUpHandler, IDragHandler
	{
		// Token: 0x060000D5 RID: 213 RVA: 0x00006298 File Offset: 0x00004498
		private void OnEnable()
		{
			this.CreateVirtualAxes();
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x000062A0 File Offset: 0x000044A0
		private void Start()
		{
			this.m_StartPos = base.transform.position;
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x000062B4 File Offset: 0x000044B4
		private void UpdateVirtualAxes(Vector3 value)
		{
			Vector3 vector = this.m_StartPos - value;
			vector.y = -vector.y;
			vector /= (float)this.MovementRange;
			if (this.m_UseX)
			{
				this.m_HorizontalVirtualAxis.Update(-vector.x);
			}
			if (this.m_UseY)
			{
				this.m_VerticalVirtualAxis.Update(vector.y);
			}
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00006320 File Offset: 0x00004520
		private void CreateVirtualAxes()
		{
			this.m_UseX = (this.axesToUse == Joystick.AxisOption.Both || this.axesToUse == Joystick.AxisOption.OnlyHorizontal);
			this.m_UseY = (this.axesToUse == Joystick.AxisOption.Both || this.axesToUse == Joystick.AxisOption.OnlyVertical);
			if (this.m_UseX)
			{
				this.m_HorizontalVirtualAxis = new CrossPlatformInputManager.VirtualAxis(this.horizontalAxisName);
				CrossPlatformInputManager.RegisterVirtualAxis(this.m_HorizontalVirtualAxis);
			}
			if (this.m_UseY)
			{
				this.m_VerticalVirtualAxis = new CrossPlatformInputManager.VirtualAxis(this.verticalAxisName);
				CrossPlatformInputManager.RegisterVirtualAxis(this.m_VerticalVirtualAxis);
			}
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x000063AC File Offset: 0x000045AC
		public void OnDrag(PointerEventData data)
		{
			Vector3 zero = Vector3.zero;
			if (this.m_UseX)
			{
				int num = (int)(data.position.x - this.m_StartPos.x);
				num = Mathf.Clamp(num, -this.MovementRange, this.MovementRange);
				zero.x = (float)num;
			}
			if (this.m_UseY)
			{
				int num2 = (int)(data.position.y - this.m_StartPos.y);
				num2 = Mathf.Clamp(num2, -this.MovementRange, this.MovementRange);
				zero.y = (float)num2;
			}
			base.transform.position = new Vector3(this.m_StartPos.x + zero.x, this.m_StartPos.y + zero.y, this.m_StartPos.z + zero.z);
			this.UpdateVirtualAxes(base.transform.position);
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00006492 File Offset: 0x00004692
		public void OnPointerUp(PointerEventData data)
		{
			base.transform.position = this.m_StartPos;
			this.UpdateVirtualAxes(this.m_StartPos);
		}

		// Token: 0x060000DB RID: 219 RVA: 0x000033A1 File Offset: 0x000015A1
		public void OnPointerDown(PointerEventData data)
		{
		}

		// Token: 0x060000DC RID: 220 RVA: 0x000064B1 File Offset: 0x000046B1
		private void OnDisable()
		{
			if (this.m_UseX)
			{
				this.m_HorizontalVirtualAxis.Remove();
			}
			if (this.m_UseY)
			{
				this.m_VerticalVirtualAxis.Remove();
			}
		}

		// Token: 0x040000D0 RID: 208
		public int MovementRange = 100;

		// Token: 0x040000D1 RID: 209
		public Joystick.AxisOption axesToUse;

		// Token: 0x040000D2 RID: 210
		public string horizontalAxisName = "Horizontal";

		// Token: 0x040000D3 RID: 211
		public string verticalAxisName = "Vertical";

		// Token: 0x040000D4 RID: 212
		private Vector3 m_StartPos;

		// Token: 0x040000D5 RID: 213
		private bool m_UseX;

		// Token: 0x040000D6 RID: 214
		private bool m_UseY;

		// Token: 0x040000D7 RID: 215
		private CrossPlatformInputManager.VirtualAxis m_HorizontalVirtualAxis;

		// Token: 0x040000D8 RID: 216
		private CrossPlatformInputManager.VirtualAxis m_VerticalVirtualAxis;

		// Token: 0x0200007F RID: 127
		public enum AxisOption
		{
			// Token: 0x040002D6 RID: 726
			Both,
			// Token: 0x040002D7 RID: 727
			OnlyHorizontal,
			// Token: 0x040002D8 RID: 728
			OnlyVertical
		}
	}
}
