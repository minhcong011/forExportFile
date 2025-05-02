// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: ControlFreak2.GamepadNotifier
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace ControlFreak2
{
	public class GamepadNotifier : MonoBehaviour
	{
		public GamepadNotifier()
		{
			this.notificationList = new List<NotificationElementGUI>(8);
		}

		private void OnEnable()
		{
			if (CFUtils.editorStopped)
			{
				return;
			}
			this.maxNotifications = Mathf.Max(this.maxNotifications, 1);
			if (this.notificationTemplate == null)
			{
				return;
			}
			this.notificationTemplate.gameObject.SetActive(false);
			while (this.notificationList.Count < this.maxNotifications)
			{
				this.notificationList.Add(null);
			}
			for (int i = 0; i < this.notificationList.Count; i++)
			{
				if (this.notificationList[i] == null)
				{
					this.notificationList[i] = UnityEngine.Object.Instantiate<NotificationElementGUI>(this.notificationTemplate);
				}
				this.notificationList[i].End();
				this.notificationList[i].transform.SetParent(this.notificationListBox, false);
			}
			GamepadManager.onGamepadActivated += this.OnGamepadActivated;
			GamepadManager.onGamepadConnected += this.OnGamepadConnected;
			GamepadManager.onGamepadDisconnected += this.OnGamepadDisconnected;
			GamepadManager.onGamepadDisactivated += this.OnGamepadDisactivated;
		}

		private void OnDisable()
		{
			if (CFUtils.editorStopped)
			{
				return;
			}
			GamepadManager.onGamepadActivated -= this.OnGamepadActivated;
			GamepadManager.onGamepadConnected -= this.OnGamepadConnected;
			GamepadManager.onGamepadDisconnected -= this.OnGamepadDisconnected;
			GamepadManager.onGamepadDisactivated -= this.OnGamepadDisactivated;
		}

		private void Update()
		{
			if (GamepadManager.activeManager != null)
			{
			}
			float realDeltaTimeClamped = CFUtils.realDeltaTimeClamped;
			for (int i = 0; i < this.notificationList.Count; i++)
			{
				if (this.notificationList[i] != null)
				{
					this.notificationList[i].UpdateNotification(realDeltaTimeClamped);
				}
			}
		}

		private void OnGamepadConnected(GamepadManager.Gamepad gamepad)
		{
			string profileName = gamepad.GetProfileName();
			if (string.IsNullOrEmpty(profileName))
			{
				this.AddNotification(string.Format(this.msgUnknownGamepadConnected, gamepad.GetInternalJoyName()), this.iconUnknownGamepadConnected);
			}
			else
			{
				this.AddNotification(string.Format(this.msgKnownGamepadConnected, gamepad.GetProfileName()), this.iconKnownGamepadConnected);
			}
		}

		private void OnGamepadActivated(GamepadManager.Gamepad gamepad)
		{
			this.AddNotification(string.Format(this.msgGamepadActivated, gamepad.GetProfileName(), gamepad.GetSlot()), this.iconGamepadDisactivated);
		}

		private void OnGamepadDisconnected(GamepadManager.Gamepad gamepad, GamepadManager.DisconnectionReason reason)
		{
			this.AddNotification(string.Format(this.msgGamepadDisconnected, gamepad.GetProfileName(), gamepad.GetSlot()), this.iconGamepadDisconnected);
		}

		private void OnGamepadDisactivated(GamepadManager.Gamepad gamepad, GamepadManager.DisconnectionReason reason)
		{
		}

		private void AddNotification(string msg, Sprite icon)
		{
			if (string.IsNullOrEmpty(msg))
			{
				return;
			}
			NotificationElementGUI notificationElementGUI = null;
			for (int i = 0; i < this.notificationList.Count; i++)
			{
				NotificationElementGUI notificationElementGUI2 = this.notificationList[i];
				if (!(notificationElementGUI2 == null))
				{
					if (!notificationElementGUI2.IsActive())
					{
						notificationElementGUI = notificationElementGUI2;
						break;
					}
					if (notificationElementGUI == null || notificationElementGUI2.GetTimeElapsed() > notificationElementGUI.GetTimeElapsed())
					{
						notificationElementGUI = notificationElementGUI2;
					}
				}
			}
			if (notificationElementGUI == null)
			{
				return;
			}
			notificationElementGUI.End();
			notificationElementGUI.Show(msg, icon, this.notificationListBox, this.msgDuration);
			this.SortNotifications();
		}

		private void SortNotifications()
		{
			List<NotificationElementGUI> list = this.notificationList;
			if (GamepadNotifier._003C_003Ef__mg_0024cache0 == null)
			{
				GamepadNotifier._003C_003Ef__mg_0024cache0 = new Comparison<NotificationElementGUI>(NotificationElementGUI.Compare);
			}
			list.Sort(GamepadNotifier._003C_003Ef__mg_0024cache0);
		}

		public int maxNotifications = 8;

		public float msgDuration = 5f;

		public NotificationElementGUI notificationTemplate;

		public RectTransform notificationListBox;

		private List<NotificationElementGUI> notificationList;

		public string msgUnknownGamepadConnected = "Unknown gamepad {0} has been connected!";

		public string msgKnownGamepadConnected = "Gamepad {0} has been connected!";

		public string msgGamepadDisconnected = "Gamepad {0} at slot {1} has been disconnected.";

		public string msgGamepadActivated = "Gamepad {0} activated at slot {1}.";

		public string msgGamepadDisactivated = "Gamepad {0} disactivated at slot {1}.";

		public Sprite iconUnknownGamepadConnected;

		public Sprite iconKnownGamepadConnected;

		public Sprite iconGamepadDisconnected;

		public Sprite iconGamepadActivated;

		public Sprite iconGamepadDisactivated;

		[CompilerGenerated]
		private static Comparison<NotificationElementGUI> _003C_003Ef__mg_0024cache0;
	}
}
