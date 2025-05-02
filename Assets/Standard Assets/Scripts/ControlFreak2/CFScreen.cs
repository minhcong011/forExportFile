// dnSpy decompiler from Assembly-CSharp-firstpass.dll class: ControlFreak2.CFScreen
using System;
using UnityEngine;

namespace ControlFreak2
{
	public static class CFScreen
	{
		public static float dpi
		{
			get
			{
				CFScreen.UpdateDpiIfNeeded();
				return CFScreen.mDpi;
			}
		}

		public static float dpcm
		{
			get
			{
				CFScreen.UpdateDpiIfNeeded();
				return CFScreen.mDpcm;
			}
		}

		public static float invDpi
		{
			get
			{
				CFScreen.UpdateDpiIfNeeded();
				return CFScreen.mInvDpi;
			}
		}

		public static float invDpcm
		{
			get
			{
				CFScreen.UpdateDpiIfNeeded();
				return CFScreen.mInvDpcm;
			}
		}

		public static float width
		{
			get
			{
				return (float)Screen.width;
			}
		}

		public static float height
		{
			get
			{
				return (float)Screen.height;
			}
		}

		public static bool lockCursor
		{
			get
			{
				return CFCursor.lockState == CursorLockMode.Locked;
			}
			set
			{
				CFCursor.lockState = ((!value) ? CursorLockMode.None : CursorLockMode.Locked);
			}
		}

		public static bool showCursor
		{
			get
			{
				return CFCursor.visible;
			}
			set
			{
				CFCursor.visible = value;
			}
		}

		public static bool fullScreen
		{
			get
			{
				return Screen.fullScreen;
			}
			set
			{
				CFScreen.UpdateDpi();
				Screen.fullScreen = value;
			}
		}

		public static void SetResolution(int width, int height, bool fullScreen, int refreshRate = 0)
		{
			CFScreen.UpdateDpi();
			Screen.SetResolution(width, height, fullScreen, refreshRate);
		}

		public static void ForceFallbackDpi(bool enableFallbackDpi)
		{
			CFScreen.mForceFallbackDpi = enableFallbackDpi;
			CFScreen.UpdateDpi();
		}

		public static void SetFallbackScreenDiameter(float diameterInInches)
		{
			CFScreen.mLastScreenWidth = -1f;
			CFScreen.mFallbackDiameter = Mathf.Max(1f, diameterInInches);
			CFScreen.UpdateDpi();
		}

		private static void UpdateDpiIfNeeded()
		{
			Resolution currentResolution = Screen.currentResolution;
			if (CFScreen.mLastScreenWidth != (float)Screen.width || CFScreen.mLastScreenHeight != (float)Screen.height || CFScreen.mLastScreenDpi != Screen.dpi || currentResolution.width != CFScreen.mLastScreenResolution.width || currentResolution.height != CFScreen.mLastScreenResolution.height)
			{
				CFScreen.UpdateDpi();
			}
		}

		public static void UpdateDpi()
		{
			Resolution currentResolution = Screen.currentResolution;
			if (CFScreen.mLastScreenDpi != Screen.dpi || CFScreen.mNativeScreenResolution.width <= 0 || CFScreen.mNativeScreenResolution.height <= 0)
			{
				CFScreen.mNativeScreenResolution = currentResolution;
				CFScreen.mNativeScreenDpi = Screen.dpi;
			}
			CFScreen.mLastScreenWidth = (float)Screen.width;
			CFScreen.mLastScreenHeight = (float)Screen.height;
			CFScreen.mLastScreenDpi = Screen.dpi;
			CFScreen.mFallbackDpi = Mathf.Sqrt(CFScreen.mLastScreenWidth * CFScreen.mLastScreenWidth + CFScreen.mLastScreenHeight * CFScreen.mLastScreenHeight) / CFScreen.mFallbackDiameter;
			CFScreen.mLastScreenResolution = currentResolution;
			float num = CFScreen.mLastScreenDpi;
			if (CFScreen.mNativeScreenDpi > 0f && CFScreen.mLastScreenDpi > 0f && CFScreen.mNativeScreenResolution.width > 0 && CFScreen.mNativeScreenResolution.height > 0 && currentResolution.width > 0 && currentResolution.height > 0)
			{
				num = CFScreen.mNativeScreenDpi * (Mathf.Sqrt((float)(currentResolution.width * currentResolution.width + currentResolution.height * currentResolution.height)) / Mathf.Sqrt((float)(CFScreen.mNativeScreenResolution.width * CFScreen.mNativeScreenResolution.width + CFScreen.mNativeScreenResolution.height * CFScreen.mNativeScreenResolution.height)));
			}
			if (num < 1f || CFScreen.mForceFallbackDpi)
			{
				CFScreen.mDpi = CFScreen.mFallbackDpi;
			}
			else
			{
				CFScreen.mDpi = num;
			}
			CFScreen.mDpcm = CFScreen.mDpi / 2.54f;
			CFScreen.mInvDpi = 1f / CFScreen.mDpi;
			CFScreen.mInvDpcm = 1f / CFScreen.mDpcm;
		}

		private const float DEFAULT_FALLBACK_SCREEN_DIAMETER = 7f;

		private static bool mForceFallbackDpi;

		private static Resolution mLastScreenResolution;

		private static Resolution mNativeScreenResolution;

		private static float mFallbackDpi = 96f;

		private static float mFallbackDiameter = 7f;

		private static float mLastScreenWidth = -1f;

		private static float mLastScreenHeight = -1f;

		private static float mLastScreenDpi = -1f;

		private static float mNativeScreenDpi = -1f;

		private static float mDpi = 100f;

		private static float mDpcm = 100f;

		private static float mInvDpi = 1f;

		private static float mInvDpcm = 1f;
	}
}
