// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: ControlFreak2.UI.LeftRightMenu
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ControlFreak2.UI
{
	public class LeftRightMenu : Selectable
	{
		public override void OnSelect(BaseEventData data)
		{
			base.OnSelect(data);
		}

		public override void OnDeselect(BaseEventData data)
		{
			base.OnDeselect(data);
		}

		public override void OnMove(AxisEventData data)
		{
			if (data.moveDir == MoveDirection.Left || data.moveDir == MoveDirection.Right)
			{
				this.OnSwitch((data.moveDir != MoveDirection.Left) ? 1 : -1);
				data.Use();
			}
			else
			{
				base.OnMove(data);
			}
		}

		public int GetItemCount()
		{
			return this.optionItems.Length;
		}

		public void SetItemActive(int id)
		{
			for (int i = 0; i < this.optionItems.Length; i++)
			{
				RectTransform rectTransform = this.optionItems[i];
				if (!(rectTransform == null))
				{
					rectTransform.gameObject.SetActive(id == i);
				}
			}
		}

		public void SetTitle(string title)
		{
			if (this.titleText != null)
			{
				this.titleText.text = title;
			}
		}

		protected override void OnEnable()
		{
			base.OnEnable();
			this.buttonLeft.onClick.AddListener(new UnityAction(this.OnLeftPressed));
			this.buttonRight.onClick.AddListener(new UnityAction(this.OnRightPressed));
			this.buttonBack.onClick.AddListener(new UnityAction(this.OnBackPressed));
			for (int i = 0; i < this.optionItems.Length; i++)
			{
				RectTransform rectTransform = this.optionItems[i];
				if (!(rectTransform == null))
				{
					rectTransform.SetParent(this.contentPlaceholder, false);
					rectTransform.gameObject.SetActive(false);
				}
			}
		}

		protected override void OnDisable()
		{
			this.buttonLeft.onClick.RemoveAllListeners();
			this.buttonRight.onClick.RemoveAllListeners();
			this.buttonBack.onClick.RemoveAllListeners();
			base.OnDisable();
		}

		private void OnSwitch(int dir)
		{
			if (this.onOptionSwitch != null)
			{
				this.onOptionSwitch(dir);
			}
		}

		private void OnBackPressed()
		{
			if (this.onBackPressed != null)
			{
				this.onBackPressed();
			}
		}

		private void OnLeftPressed()
		{
			this.OnSwitch(-1);
		}

		private void OnRightPressed()
		{
			this.OnSwitch(1);
		}

		public Button buttonLeft;

		public Button buttonRight;

		public Button buttonBack;

		public RectTransform contentPlaceholder;

		public Text titleText;

		public Action onBackPressed;

		public LeftRightMenu.OptionSwitchCallback onOptionSwitch;

		public RectTransform[] optionItems;

		public delegate void OptionSwitchCallback(int dir);
	}
}
