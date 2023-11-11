using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UnityStandardAssets.CrossPlatformInput
{
	// Token: 0x02000035 RID: 53
	[RequireComponent(typeof(Image))]
	public class TouchPad : MonoBehaviour, IPointerDownHandler, IEventSystemHandler, IPointerUpHandler
	{
		// Token: 0x060000E7 RID: 231 RVA: 0x000066E3 File Offset: 0x000048E3
		private void OnEnable()
		{
			this.CreateVirtualAxes();
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x000066EB File Offset: 0x000048EB
		private void Start()
		{
			this.m_Image = base.GetComponent<Image>();
			this.m_Center = this.m_Image.transform.position;
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00006710 File Offset: 0x00004910
		private void CreateVirtualAxes()
		{
			this.m_UseX = (this.axesToUse == TouchPad.AxisOption.Both || this.axesToUse == TouchPad.AxisOption.OnlyHorizontal);
			this.m_UseY = (this.axesToUse == TouchPad.AxisOption.Both || this.axesToUse == TouchPad.AxisOption.OnlyVertical);
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

		// Token: 0x060000EA RID: 234 RVA: 0x00006799 File Offset: 0x00004999
		private void UpdateVirtualAxes(Vector3 value)
		{
			value = value.normalized;
			if (this.m_UseX)
			{
				this.m_HorizontalVirtualAxis.Update(value.x);
			}
			if (this.m_UseY)
			{
				this.m_VerticalVirtualAxis.Update(value.y);
			}
		}

		// Token: 0x060000EB RID: 235 RVA: 0x000067D6 File Offset: 0x000049D6
		public void OnPointerDown(PointerEventData data)
		{
			this.m_Dragging = true;
			this.m_Id = data.pointerId;
			if (this.controlStyle != TouchPad.ControlStyle.Absolute)
			{
				this.m_Center = data.position;
			}
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00006804 File Offset: 0x00004A04
		private void Update()
		{
			if (!this.m_Dragging)
			{
				return;
			}
			if (Input.touchCount >= this.m_Id + 1 && this.m_Id != -1)
			{
				if (this.controlStyle == TouchPad.ControlStyle.Swipe)
				{
					this.m_Center = this.m_PreviousTouchPos;
					this.m_PreviousTouchPos = Input.touches[this.m_Id].position;
				}
				Vector2 normalized = new Vector2(Input.touches[this.m_Id].position.x - this.m_Center.x, Input.touches[this.m_Id].position.y - this.m_Center.y).normalized;
				normalized.x *= this.Xsensitivity;
				normalized.y *= this.Ysensitivity;
				this.UpdateVirtualAxes(new Vector3(normalized.x, normalized.y, 0f));
			}
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00006905 File Offset: 0x00004B05
		public void OnPointerUp(PointerEventData data)
		{
			this.m_Dragging = false;
			this.m_Id = -1;
			this.UpdateVirtualAxes(Vector3.zero);
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00006920 File Offset: 0x00004B20
		private void OnDisable()
		{
			if (CrossPlatformInputManager.AxisExists(this.horizontalAxisName))
			{
				CrossPlatformInputManager.UnRegisterVirtualAxis(this.horizontalAxisName);
			}
			if (CrossPlatformInputManager.AxisExists(this.verticalAxisName))
			{
				CrossPlatformInputManager.UnRegisterVirtualAxis(this.verticalAxisName);
			}
		}

		// Token: 0x040000DE RID: 222
		public TouchPad.AxisOption axesToUse;

		// Token: 0x040000DF RID: 223
		public TouchPad.ControlStyle controlStyle;

		// Token: 0x040000E0 RID: 224
		public string horizontalAxisName = "Horizontal";

		// Token: 0x040000E1 RID: 225
		public string verticalAxisName = "Vertical";

		// Token: 0x040000E2 RID: 226
		public float Xsensitivity = 1f;

		// Token: 0x040000E3 RID: 227
		public float Ysensitivity = 1f;

		// Token: 0x040000E4 RID: 228
		private Vector3 m_StartPos;

		// Token: 0x040000E5 RID: 229
		private Vector2 m_PreviousDelta;

		// Token: 0x040000E6 RID: 230
		private Vector3 m_JoytickOutput;

		// Token: 0x040000E7 RID: 231
		private bool m_UseX;

		// Token: 0x040000E8 RID: 232
		private bool m_UseY;

		// Token: 0x040000E9 RID: 233
		private CrossPlatformInputManager.VirtualAxis m_HorizontalVirtualAxis;

		// Token: 0x040000EA RID: 234
		private CrossPlatformInputManager.VirtualAxis m_VerticalVirtualAxis;

		// Token: 0x040000EB RID: 235
		private bool m_Dragging;

		// Token: 0x040000EC RID: 236
		private int m_Id = -1;

		// Token: 0x040000ED RID: 237
		private Vector2 m_PreviousTouchPos;

		// Token: 0x040000EE RID: 238
		private Vector3 m_Center;

		// Token: 0x040000EF RID: 239
		private Image m_Image;

		// Token: 0x02000082 RID: 130
		public enum AxisOption
		{
			// Token: 0x040002DF RID: 735
			Both,
			// Token: 0x040002E0 RID: 736
			OnlyHorizontal,
			// Token: 0x040002E1 RID: 737
			OnlyVertical
		}

		// Token: 0x02000083 RID: 131
		public enum ControlStyle
		{
			// Token: 0x040002E3 RID: 739
			Absolute,
			// Token: 0x040002E4 RID: 740
			Relative,
			// Token: 0x040002E5 RID: 741
			Swipe
		}
	}
}
