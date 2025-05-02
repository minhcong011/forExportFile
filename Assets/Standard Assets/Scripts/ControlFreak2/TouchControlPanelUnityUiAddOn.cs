// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: ControlFreak2.TouchControlPanelUnityUiAddOn
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ControlFreak2
{
	[ExecuteInEditMode]
	public class TouchControlPanelUnityUiAddOn : Graphic, IPointerDownHandler, IPointerUpHandler, IDragHandler, IEventSystemHandler
	{
		public TouchControlPanelUnityUiAddOn()
		{
			this.eventData = new TouchControlPanel.SystemTouchEventData();
		}

		public bool IsConnectedToPanel()
		{
			return this.panel != null;
		}

		private TouchControlPanel.SystemTouchEventData TranslateEvent(PointerEventData data)
		{
			this.eventData.id = data.pointerId;
			this.eventData.pos = data.position;
			this.eventData.isMouseEvent = (data.pointerId < 0);
			this.eventData.cam = data.pressEventCamera;
			return this.eventData;
		}

		protected override void Awake()
		{
			base.Awake();
			this.panel = base.GetComponent<TouchControlPanel>();
		}

		protected override void OnEnable()
		{
			base.OnEnable();
		}

		public override bool Raycast(Vector2 sp, Camera eventCamera)
		{
			return !(this.panel == null) && this.panel.Raycast(sp, eventCamera);
		}

		void IPointerDownHandler.OnPointerDown(PointerEventData data)
		{
			if (this.panel != null)
			{
				this.panel.OnSystemTouchStart(this.TranslateEvent(data));
			}
		}

		void IPointerUpHandler.OnPointerUp(PointerEventData data)
		{
			if (this.panel != null)
			{
				this.panel.OnSystemTouchEnd(this.TranslateEvent(data));
			}
		}

		void IDragHandler.OnDrag(PointerEventData data)
		{
			if (this.panel != null)
			{
				this.panel.OnSystemTouchMove(this.TranslateEvent(data));
			}
		}

		protected override void UpdateGeometry()
		{
			if (base.canvasRenderer != null)
			{
				base.canvasRenderer.Clear();
			}
		}

		private void OnDrawGizmos()
		{
			this.DrawGizmos(false);
		}

		private void OnDrawGizmosSelected()
		{
			this.DrawGizmos(true);
		}

		private void DrawGizmos(bool selected)
		{
			RectTransform rectTransform = base.transform as RectTransform;
			if (rectTransform == null)
			{
				return;
			}
			Gizmos.color = ((!selected) ? Color.white : Color.red);
			Gizmos.matrix = rectTransform.localToWorldMatrix;
			Gizmos.DrawWireCube(rectTransform.rect.center, rectTransform.rect.size);
		}

		private TouchControlPanel panel;

		private TouchControlPanel.SystemTouchEventData eventData;
	}
}
