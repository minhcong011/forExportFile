// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: ControlFreak2.DebugUtils.AxisDebugger
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ControlFreak2.DebugUtils
{
	public class AxisDebugger : MonoBehaviour
	{
		private void Update()
		{
			if (UnityEngine.Input.GetKeyDown(this.deltaResetKey))
			{
				this.unScroll = Vector2.zero;
				this.cfScroll = Vector2.zero;
				this.cfMouseDelta = Vector2.zero;
				this.unMouseDelta = Vector2.zero;
			}
			this.cfScroll += CF2Input.mouseScrollDelta;
			this.unScroll.x = this.unScroll.x + Input.mouseScrollDelta.x;
			this.unScroll.y = this.unScroll.y + Input.mouseScrollDelta.y;
			if (CF2Input.activeRig != null)
			{
				this.cfMouseDelta.x = this.cfMouseDelta.x + CF2Input.GetAxis("Mouse X");
				this.cfMouseDelta.y = this.cfMouseDelta.y + CF2Input.GetAxis("Mouse Y");
			}
			this.unMouseDelta.x = this.unMouseDelta.x + this.GetUnityAxis("Mouse X", 0f);
			this.unMouseDelta.y = this.unMouseDelta.y + this.GetUnityAxis("Mouse Y", 0f);
		}

		private void OnGUI()
		{
			if (CF2Input.activeRig == null)
			{
				return;
			}
			GUI.skin = this.skin;
			GUILayout.BeginVertical((!(GUI.skin != null)) ? GUIStyle.none : GUI.skin.box, new GUILayoutOption[0]);
			GUILayout.Box("Active RIG: " + CF2Input.activeRig.name, new GUILayoutOption[0]);
			if (this.drawMouseStats)
			{
				GUILayout.Box(string.Concat(new object[]
				{
					"Scroll: cf:",
					this.cfScroll,
					" un:",
					this.unScroll,
					"\nMouse Delta: cf: ",
					this.cfMouseDelta,
					" un:",
					this.unMouseDelta
				}), new GUILayoutOption[0]);
			}
			List<InputRig.AxisConfig> list = CF2Input.activeRig.axes.list;
			for (int i = 0; i < list.Count; i++)
			{
				InputRig.AxisConfig axisConfig = list[i];
				float num = 0f;
				float num2 = 0f;
				bool flag = false;
				num = axisConfig.GetAnalog();
				try
				{
					num2 = UnityEngine.Input.GetAxis(axisConfig.name);
					flag = true;
				}
				catch (Exception)
				{
				}
				if (num != 0f || (this.drawUnityAxes && num2 != 0f))
				{
					GUILayout.BeginVertical(new GUILayoutOption[0]);
					GUILayout.Label(axisConfig.name, new GUILayoutOption[0]);
					GUI.color = ((num != 0f && num != 1f && num != -1f) ? Color.gray : Color.green);
					GUILayout.Label("CF : " + num.ToString("0.00000"), new GUILayoutOption[0]);
					GUILayout.Box(string.Empty, new GUILayoutOption[]
					{
						GUILayout.Width(Mathf.Abs(num) * 100f)
					});
					if (this.drawUnityAxes)
					{
						GUI.color = ((num2 != 0f && num2 != 1f && num2 != -1f) ? Color.gray : Color.green);
						GUILayout.Label("UN : " + ((!flag) ? "UNAVAILABLE!" : num2.ToString("0.00000")), new GUILayoutOption[0]);
						GUILayout.Box(string.Empty, new GUILayoutOption[]
						{
							GUILayout.Width(Mathf.Abs(num2) * 100f)
						});
					}
					GUILayout.EndVertical();
					GUI.color = Color.white;
					if (axisConfig.axisType == InputRig.AxisType.UnsignedAnalog)
					{
					}
				}
			}
			GUILayout.EndVertical();
		}

		private float GetUnityAxis(string name, float defaultVal = 0f)
		{
			float result = defaultVal;
			try
			{
				result = UnityEngine.Input.GetAxis(name);
			}
			catch (Exception)
			{
			}
			return result;
		}

		public bool drawGUI = true;

		public bool drawMouseStats;

		public bool drawUnityAxes;

		[Tooltip("When pressed, delta accumulators will be reset.")]
		public KeyCode deltaResetKey = KeyCode.F5;

		private Vector2 cfScroll;

		private Vector2 unScroll;

		private Vector2 cfMouseDelta;

		private Vector2 unMouseDelta;

		public GUISkin skin;
	}
}
