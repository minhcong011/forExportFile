// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: ControlFreak2.UI.AutoPreferredSize
using System;
using UnityEngine;
using UnityEngine.UI;

namespace ControlFreak2.UI
{
	[ExecuteInEditMode]
	public class AutoPreferredSize : MonoBehaviour
	{
		private void OnEnable()
		{
			if (this.target == null)
			{
				this.target = base.GetComponent<LayoutElement>();
			}
		}

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
			if (this.autoHeight)
			{
				this.target.minHeight = this.source.rect.height + this.vertPadding;
			}
			if (this.autoWidth)
			{
				this.target.minWidth = this.source.rect.width + this.horzPadding;
			}
		}

		public LayoutElement target;

		public bool autoWidth;

		public bool autoHeight;

		public float horzPadding = 10f;

		public float vertPadding = 10f;

		public RectTransform source;
	}
}
