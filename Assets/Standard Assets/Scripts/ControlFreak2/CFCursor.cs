// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: ControlFreak2.CFCursor
using System;
using System.Diagnostics;
using UnityEngine;

namespace ControlFreak2
{
	public static class CFCursor
	{
		//[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event Action onLockStateChange;

		public static CursorLockMode lockState
		{
			get
			{
				if (CFCursor.IsCursorLockingAllowed() && CFCursor.mLockState != CFCursor.InternalGetLockMode())
				{
					CFCursor.mLockState = CFCursor.InternalGetLockMode();
					if (CFCursor.onLockStateChange != null)
					{
						CFCursor.onLockStateChange();
					}
				}
				return CFCursor.mLockState;
			}
			set
			{
				CursorLockMode cursorLockMode = CFCursor.mLockState;
				CFCursor.mLockState = value;
				if (CFCursor.IsCursorLockingAllowed())
				{
					CFCursor.InternalSetLockMode(value);
					CFCursor.mLockState = CFCursor.InternalGetLockMode();
				}
				else
				{
					CFCursor.InternalSetLockMode(CursorLockMode.None);
				}
				if (cursorLockMode != CFCursor.mLockState && CFCursor.onLockStateChange != null)
				{
					CFCursor.onLockStateChange();
				}
			}
		}

		public static bool visible
		{
			get
			{
				return CFCursor.IsCursorLockingAllowed() ? (CFCursor.mVisible = CFCursor.InternalIsVisible()) : CFCursor.mVisible;
			}
			set
			{
				CFCursor.mVisible = value;
				if (CFCursor.IsCursorLockingAllowed())
				{
					CFCursor.InternalSetVisible(value);
				}
				else
				{
					CFCursor.InternalSetVisible(true);
				}
			}
		}

		private static bool IsCursorLockingAllowed()
		{
			return !(CF2Input.activeRig != null) || !CF2Input.IsInMobileMode();
		}

		public static void InternalRefresh()
		{
			if (!CFCursor.IsCursorLockingAllowed())
			{
				CFCursor.InternalSetLockMode(CursorLockMode.None);
				CFCursor.InternalSetVisible(true);
			}
			else
			{
				CursorLockMode cursorLockMode = CFCursor.InternalGetLockMode();
				bool flag = CFCursor.InternalIsVisible();
				CFCursor.InternalSetLockMode(CFCursor.mLockState);
				CFCursor.InternalSetVisible(CFCursor.mVisible);
				CFCursor.mLockState = CFCursor.InternalGetLockMode();
				CFCursor.mVisible = CFCursor.InternalIsVisible();
				if ((CFCursor.mLockState != cursorLockMode || CFCursor.mVisible != flag) && CFCursor.onLockStateChange != null)
				{
					CFCursor.onLockStateChange();
				}
			}
		}

		public static void SetCursor(Texture2D tex, Vector2 hotSpot, CursorMode mode)
		{
			Cursor.SetCursor(tex, hotSpot, mode);
		}

		private static CursorLockMode InternalGetLockMode()
		{
			return Cursor.lockState;
		}

		private static void InternalSetLockMode(CursorLockMode mode)
		{
			Cursor.lockState = mode;
		}

		private static bool InternalIsVisible()
		{
			return Cursor.visible;
		}

		private static void InternalSetVisible(bool visible)
		{
			Cursor.visible = visible;
		}

		private static CursorLockMode mLockState = CursorLockMode.None;

		private static bool mVisible = true;
	}
}
