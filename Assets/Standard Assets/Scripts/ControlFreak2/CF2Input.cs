// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: ControlFreak2.CF2Input
using System;
using System.Diagnostics;
using UnityEngine;

namespace ControlFreak2
{
	public static class CF2Input
	{
		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event Action onMobileModeChange;

		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event Action onActiveRigChange;

		public static CF2Input.MobileMode GetMobileMode()
		{
			return CF2Input.mMobileMode;
		}

		public static void SetMobileMode(CF2Input.MobileMode mode)
		{
			CF2Input.mMobileMode = mode;
			if (CF2Input.onMobileModeChange != null)
			{
				CF2Input.onMobileModeChange();
			}
			CFCursor.InternalRefresh();
		}

		public static bool IsInMobileMode()
		{
			return CF2Input.mMobileMode == CF2Input.MobileMode.Enabled || (CF2Input.mMobileMode == CF2Input.MobileMode.Auto && CF2Input.IsMobilePlatform());
		}

		public static bool IsMobilePlatform()
		{
			return Application.isMobilePlatform;
		}

		[Obsolete("Use .IsInMobileMode()")]
		public static bool ControllerActive()
		{
			return CF2Input.IsInMobileMode();
		}

		public static InputRig activeRig
		{
			get
			{
				return CF2Input.mRig;
			}
			set
			{
				if (value == CF2Input.mRig)
				{
					return;
				}
				if (CF2Input.mRig != null)
				{
					CF2Input.mRig.OnDisactivateRig();
				}
				else
				{
					CF2Input.mSimulateMouseWithTouches = Input.simulateMouseWithTouches;
				}
				CF2Input.mRig = value;
				if (CF2Input.mRig != null)
				{
					CF2Input.mRig.OnActivateRig();
					Input.simulateMouseWithTouches = false;
				}
				else
				{
					Input.simulateMouseWithTouches = CF2Input.mSimulateMouseWithTouches;
				}
				if (CF2Input.onActiveRigChange != null)
				{
					CF2Input.onActiveRigChange();
				}
				CFCursor.InternalRefresh();
			}
		}

		public static void ResetInputAxes()
		{
			if (CF2Input.mRig != null)
			{
				CF2Input.mRig.ResetInputAxes();
			}
			Input.ResetInputAxes();
		}

		public static float GetAxis(string axisName)
		{
			return (!(CF2Input.mRig != null)) ? UnityEngine.Input.GetAxis(axisName) : CF2Input.mRig.GetAxis(axisName);
		}

		public static float GetAxis(string axisName, ref int cachedId)
		{
			return (!(CF2Input.mRig != null)) ? UnityEngine.Input.GetAxis(axisName) : CF2Input.mRig.GetAxis(axisName, ref cachedId);
		}

		public static float GetAxisRaw(string axisName)
		{
			return (!(CF2Input.mRig != null)) ? UnityEngine.Input.GetAxisRaw(axisName) : CF2Input.mRig.GetAxisRaw(axisName);
		}

		public static float GetAxisRaw(string axisName, ref int cachedId)
		{
			return (!(CF2Input.mRig != null)) ? UnityEngine.Input.GetAxisRaw(axisName) : CF2Input.mRig.GetAxisRaw(axisName, ref cachedId);
		}

		public static bool GetButton(string axisName)
		{
			return (!(CF2Input.mRig != null)) ? Input.GetButton(axisName) : CF2Input.mRig.GetButton(axisName);
		}

		public static bool GetButton(string axisName, ref int cachedId)
		{
			return (!(CF2Input.mRig != null)) ? Input.GetButton(axisName) : CF2Input.mRig.GetButton(axisName, ref cachedId);
		}

		public static bool GetButtonDown(string axisName)
		{
			return (!(CF2Input.mRig != null)) ? Input.GetButtonDown(axisName) : CF2Input.mRig.GetButtonDown(axisName);
		}

		public static bool GetButtonDown(string axisName, ref int cachedId)
		{
			return (!(CF2Input.mRig != null)) ? Input.GetButtonDown(axisName) : CF2Input.mRig.GetButtonDown(axisName, ref cachedId);
		}

		public static bool GetButtonUp(string axisName)
		{
			return (!(CF2Input.mRig != null)) ? Input.GetButtonUp(axisName) : CF2Input.mRig.GetButtonUp(axisName);
		}

		public static bool GetButtonUp(string axisName, ref int cachedId)
		{
			return (!(CF2Input.mRig != null)) ? Input.GetButtonUp(axisName) : CF2Input.mRig.GetButtonUp(axisName, ref cachedId);
		}

		public static bool GetKey(KeyCode keyCode)
		{
			return (!(CF2Input.mRig != null)) ? UnityEngine.Input.GetKey(keyCode) : CF2Input.mRig.GetKey(keyCode);
		}

		public static bool GetKeyDown(KeyCode keyCode)
		{
			return (!(CF2Input.mRig != null)) ? UnityEngine.Input.GetKeyDown(keyCode) : CF2Input.mRig.GetKeyDown(keyCode);
		}

		public static bool GetKeyUp(KeyCode keyCode)
		{
			return (!(CF2Input.mRig != null)) ? UnityEngine.Input.GetKeyUp(keyCode) : CF2Input.mRig.GetKeyUp(keyCode);
		}

		[Obsolete("Please, use GetKey(KeyCode) version instead!")]
		public static bool GetKey(string keyName)
		{
			return (!(CF2Input.mRig != null)) ? UnityEngine.Input.GetKey(keyName) : CF2Input.mRig.GetKey(keyName);
		}

		[Obsolete("Please, use GetKeyDown(KeyCode) version instead!")]
		public static bool GetKeyDown(string keyName)
		{
			return (!(CF2Input.mRig != null)) ? UnityEngine.Input.GetKeyDown(keyName) : CF2Input.mRig.GetKeyDown(keyName);
		}

		[Obsolete("Please, use GetKeyUp(KeyCode) version instead!")]
		public static bool GetKeyUp(string keyName)
		{
			return (!(CF2Input.mRig != null)) ? UnityEngine.Input.GetKeyUp(keyName) : CF2Input.mRig.GetKeyUp(keyName);
		}

		public static bool anyKey
		{
			get
			{
				return (!(CF2Input.mRig != null)) ? Input.anyKey : CF2Input.mRig.AnyKey();
			}
		}

		public static bool anyKeyDown
		{
			get
			{
				return (!(CF2Input.mRig != null)) ? Input.anyKeyDown : CF2Input.mRig.AnyKeyDown();
			}
		}

		public static int touchCount
		{
			get
			{
				return (!(CF2Input.mRig != null)) ? UnityEngine.Input.touchCount : CF2Input.mRig.GetEmuTouchCount();
			}
		}

		public static InputRig.Touch[] touches
		{
			get
			{
				return (!(CF2Input.mRig != null)) ? InputRig.Touch.TranslateUnityTouches(Input.touches) : CF2Input.mRig.GetEmuTouchArray();
			}
		}

		public static InputRig.Touch GetTouch(int i)
		{
			return (!(CF2Input.mRig != null)) ? new InputRig.Touch(UnityEngine.Input.GetTouch(i)) : CF2Input.mRig.GetEmuTouch(i);
		}

		public static bool GetMouseButton(int mouseButton)
		{
			return (!(CF2Input.mRig != null)) ? Input.GetMouseButton(mouseButton) : CF2Input.mRig.GetMouseButton(mouseButton);
		}

		public static bool GetMouseButtonDown(int mouseButton)
		{
			return (!(CF2Input.mRig != null)) ? Input.GetMouseButtonDown(mouseButton) : CF2Input.mRig.GetMouseButtonDown(mouseButton);
		}

		public static bool GetMouseButtonUp(int mouseButton)
		{
			return (!(CF2Input.mRig != null)) ? Input.GetMouseButtonUp(mouseButton) : CF2Input.mRig.GetMouseButtonUp(mouseButton);
		}

		public static Vector3 mousePosition
		{
			get
			{
				return (!(CF2Input.mRig == null)) ? CF2Input.mRig.mouseConfig.GetPosition() : UnityEngine.Input.mousePosition;
			}
		}

		public static Vector2 mouseScrollDelta
		{
			get
			{
				return (!(CF2Input.mRig == null)) ? CF2Input.mRig.scrollWheel.GetDelta() : new Vector2(Input.mouseScrollDelta.x, Input.mouseScrollDelta.y);
			}
		}

		public static bool simulateMouseWithTouches
		{
			get
			{
				return (!(CF2Input.mRig == null)) ? CF2Input.mSimulateMouseWithTouches : Input.simulateMouseWithTouches;
			}
			set
			{
				CF2Input.mSimulateMouseWithTouches = value;
				if (CF2Input.mRig == null)
				{
					Input.simulateMouseWithTouches = CF2Input.mSimulateMouseWithTouches;
				}
				else
				{
					Input.simulateMouseWithTouches = false;
				}
			}
		}

		public static void CalibrateTilt()
		{
			if (CF2Input.activeRig != null)
			{
				CF2Input.activeRig.CalibrateTilt();
			}
		}

		private const bool simulateMouseWithTouchesWhenRigIsActive = false;

		private static CF2Input.MobileMode mMobileMode = CF2Input.MobileMode.Enabled;

		private static bool mSimulateMouseWithTouches;

		private static InputRig mRig;

		public enum MobileMode
		{
			Auto,
			Enabled,
			Disabled
		}
	}
}
