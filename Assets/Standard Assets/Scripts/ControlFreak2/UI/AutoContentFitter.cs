// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: ControlFreak2.UI.AutoContentFitter
using System;
using UnityEngine;

namespace ControlFreak2.UI
{
	[ExecuteInEditMode]
	public class AutoContentFitter : MonoBehaviour
	{
		private void Update()
		{
			this.UpdatePreferredDimensions();
		}

		public void UpdatePreferredDimensions()
		{
			if (!this.autoWidth && !this.autoHeight)
			{
				return;
			}
			if (this.source == null)
			{
				return;
			}
			RectTransform rectTransform = (RectTransform)base.transform;
			if (this.autoHeight)
			{
				rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, this.source.rect.height + this.vertPadding);
			}
			if (this.autoWidth)
			{
				rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, this.source.rect.width + this.horzPadding);
			}
		}

		public RectTransform source;

		public bool autoWidth;

		public bool autoHeight;

		public float horzPadding = 10f;

		public float vertPadding = 10f;
	}
}
