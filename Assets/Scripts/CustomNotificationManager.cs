// dnSpy decompiler from Assembly-CSharp.dll class: CustomNotificationManager
using System;
using UnityEngine;

public class CustomNotificationManager : MonoBehaviour
{
	private void Awake()
	{
		if (!CustomNotificationManager.isCreated)
		{
			Singleton<GameController>.Instance.customNotificationManager = this;
			CustomNotificationManager.isCreated = true;
			UnityEngine.Object.DontDestroyOnLoad(this);
			this.setNotificationWithId(Constants.QUARTER_NOTIFICATION_ID, 10800, "Champ!!!", "Are you scared to defend country assets.??", false);
		}
		else
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	private void Start()
	{
		if (PlayerPrefs.GetInt("Notification1", 0) == 0)
		{
			this.setNotificationWithId(Constants.ALLTIME_NOTIFICATION_ID, 43200, Constants.GAME_NAME, "Get ready for exciting surprise", true);
			PlayerPrefs.SetInt("Notification1", 1);
		}
	}

	public void setNotificationWithId(int id, int delay, string title, string desc, bool isRepeating)
	{
		if (isRepeating)
		{
			LocalNotification.SendRepeatingNotification(id, (long)delay, (long)delay, title, desc, new Color32(byte.MaxValue, 68, 68, byte.MaxValue), true, true, true, string.Empty);
		}
		else
		{
			LocalNotification.SendNotification(id, (long)delay, title, desc, new Color32(byte.MaxValue, 68, 68, byte.MaxValue), true, true, true, string.Empty, LocalNotification.NotificationExecuteMode.ExactAndAllowWhileIdle);
		}
	}

	private static bool isCreated;
}
