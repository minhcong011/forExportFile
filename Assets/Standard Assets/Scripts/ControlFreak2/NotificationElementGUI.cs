// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: ControlFreak2.NotificationElementGUI
using System;
using UnityEngine;
using UnityEngine.UI;

namespace ControlFreak2
{
	public class NotificationElementGUI : MonoBehaviour
	{
		public void End()
		{
			base.gameObject.SetActive(false);
		}

		public void Show(string msg, Sprite icon, RectTransform parent, float duration)
		{
			RectTransform rectTransform = (RectTransform)base.transform;
			rectTransform.SetParent(parent, false);
			rectTransform.SetSiblingIndex(0);
			base.gameObject.SetActive(true);
			this.duration = duration;
			this.elapsed = 0f;
			this.targetText.text = msg;
			if (this.targetIcon != null)
			{
				bool flag = icon != null;
				this.targetIcon.enabled = flag;
				if (flag)
				{
					this.targetIcon.sprite = icon;
				}
			}
		}

		public void UpdateNotification(float dt)
		{
			if (!this.IsActive())
			{
				return;
			}
			this.elapsed += dt;
			if (this.elapsed > this.duration)
			{
				base.gameObject.SetActive(false);
			}
			this.OnUpdate();
		}

		public float GetTimeElapsed()
		{
			return this.elapsed;
		}

		protected virtual void OnUpdate()
		{
			float num = this.elapsed / this.duration;
			base.transform.localScale = Vector3.one * Mathf.Clamp01((1f - num) * 8f);
		}

		public bool IsActive()
		{
			return base.gameObject.activeSelf;
		}

		public static int Compare(NotificationElementGUI a, NotificationElementGUI b)
		{
			if (a == null || b == null)
			{
				return 0;
			}
			if (a.IsActive() != b.IsActive())
			{
				return (!a.IsActive()) ? 1 : -1;
			}
			if (!a.IsActive())
			{
				return 0;
			}
			return (a.elapsed != b.elapsed) ? ((a.elapsed >= b.elapsed) ? 1 : -1) : 0;
		}

		public Text targetText;

		public Image targetIcon;

		private float duration;

		private float elapsed;
	}
}
