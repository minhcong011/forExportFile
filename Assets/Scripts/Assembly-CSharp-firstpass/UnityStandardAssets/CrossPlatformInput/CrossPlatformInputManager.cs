using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput.PlatformSpecific;

namespace UnityStandardAssets.CrossPlatformInput
{
	// Token: 0x02000030 RID: 48
	public static class CrossPlatformInputManager
	{
		// Token: 0x060000B9 RID: 185 RVA: 0x0000610D File Offset: 0x0000430D
		static CrossPlatformInputManager()
		{
			CrossPlatformInputManager.activeInput = CrossPlatformInputManager.s_TouchInput;
		}

		// Token: 0x060000BA RID: 186 RVA: 0x0000612D File Offset: 0x0000432D
		public static void SwitchActiveInputMethod(CrossPlatformInputManager.ActiveInputMethod activeInputMethod)
		{
			if (activeInputMethod == CrossPlatformInputManager.ActiveInputMethod.Hardware)
			{
				CrossPlatformInputManager.activeInput = CrossPlatformInputManager.s_HardwareInput;
				return;
			}
			if (activeInputMethod != CrossPlatformInputManager.ActiveInputMethod.Touch)
			{
				return;
			}
			CrossPlatformInputManager.activeInput = CrossPlatformInputManager.s_TouchInput;
		}

		// Token: 0x060000BB RID: 187 RVA: 0x0000614C File Offset: 0x0000434C
		public static bool AxisExists(string name)
		{
			return CrossPlatformInputManager.activeInput.AxisExists(name);
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00006159 File Offset: 0x00004359
		public static bool ButtonExists(string name)
		{
			return CrossPlatformInputManager.activeInput.ButtonExists(name);
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00006166 File Offset: 0x00004366
		public static void RegisterVirtualAxis(CrossPlatformInputManager.VirtualAxis axis)
		{
			CrossPlatformInputManager.activeInput.RegisterVirtualAxis(axis);
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00006173 File Offset: 0x00004373
		public static void RegisterVirtualButton(CrossPlatformInputManager.VirtualButton button)
		{
			CrossPlatformInputManager.activeInput.RegisterVirtualButton(button);
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00006180 File Offset: 0x00004380
		public static void UnRegisterVirtualAxis(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			CrossPlatformInputManager.activeInput.UnRegisterVirtualAxis(name);
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x0000619B File Offset: 0x0000439B
		public static void UnRegisterVirtualButton(string name)
		{
			CrossPlatformInputManager.activeInput.UnRegisterVirtualButton(name);
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x000061A8 File Offset: 0x000043A8
		public static CrossPlatformInputManager.VirtualAxis VirtualAxisReference(string name)
		{
			return CrossPlatformInputManager.activeInput.VirtualAxisReference(name);
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x000061B5 File Offset: 0x000043B5
		public static float GetAxis(string name)
		{
			return CrossPlatformInputManager.GetAxis(name, false);
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x000061BE File Offset: 0x000043BE
		public static float GetAxisRaw(string name)
		{
			return CrossPlatformInputManager.GetAxis(name, true);
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x000061C7 File Offset: 0x000043C7
		private static float GetAxis(string name, bool raw)
		{
			return CrossPlatformInputManager.activeInput.GetAxis(name, raw);
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x000061D5 File Offset: 0x000043D5
		public static bool GetButton(string name)
		{
			return CrossPlatformInputManager.activeInput.GetButton(name);
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x000061E2 File Offset: 0x000043E2
		public static bool GetButtonDown(string name)
		{
			return CrossPlatformInputManager.activeInput.GetButtonDown(name);
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x000061EF File Offset: 0x000043EF
		public static bool GetButtonUp(string name)
		{
			return CrossPlatformInputManager.activeInput.GetButtonUp(name);
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x000061FC File Offset: 0x000043FC
		public static void SetButtonDown(string name)
		{
			CrossPlatformInputManager.activeInput.SetButtonDown(name);
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00006209 File Offset: 0x00004409
		public static void SetButtonUp(string name)
		{
			CrossPlatformInputManager.activeInput.SetButtonUp(name);
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00006216 File Offset: 0x00004416
		public static void SetAxisPositive(string name)
		{
			CrossPlatformInputManager.activeInput.SetAxisPositive(name);
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00006223 File Offset: 0x00004423
		public static void SetAxisNegative(string name)
		{
			CrossPlatformInputManager.activeInput.SetAxisNegative(name);
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00006230 File Offset: 0x00004430
		public static void SetAxisZero(string name)
		{
			CrossPlatformInputManager.activeInput.SetAxisZero(name);
		}

		// Token: 0x060000CD RID: 205 RVA: 0x0000623D File Offset: 0x0000443D
		public static void SetAxis(string name, float value)
		{
			CrossPlatformInputManager.activeInput.SetAxis(name, value);
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x060000CE RID: 206 RVA: 0x0000624B File Offset: 0x0000444B
		public static Vector3 mousePosition
		{
			get
			{
				return CrossPlatformInputManager.activeInput.MousePosition();
			}
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00006257 File Offset: 0x00004457
		public static void SetVirtualMousePositionX(float f)
		{
			CrossPlatformInputManager.activeInput.SetVirtualMousePositionX(f);
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00006264 File Offset: 0x00004464
		public static void SetVirtualMousePositionY(float f)
		{
			CrossPlatformInputManager.activeInput.SetVirtualMousePositionY(f);
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00006271 File Offset: 0x00004471
		public static void SetVirtualMousePositionZ(float f)
		{
			CrossPlatformInputManager.activeInput.SetVirtualMousePositionZ(f);
		}

		// Token: 0x040000CC RID: 204
		private static VirtualInput activeInput;

		// Token: 0x040000CD RID: 205
		private static VirtualInput s_TouchInput = new MobileInput();

		// Token: 0x040000CE RID: 206
		private static VirtualInput s_HardwareInput = new StandaloneInput();

		// Token: 0x0200007C RID: 124
		public enum ActiveInputMethod
		{
			// Token: 0x040002CB RID: 715
			Hardware,
			// Token: 0x040002CC RID: 716
			Touch
		}

		// Token: 0x0200007D RID: 125
		public class VirtualAxis
		{
			// Token: 0x1700003D RID: 61
			// (get) Token: 0x06000270 RID: 624 RVA: 0x0000D408 File Offset: 0x0000B608
			// (set) Token: 0x06000271 RID: 625 RVA: 0x0000D410 File Offset: 0x0000B610
			public string name { get; private set; }

			// Token: 0x1700003E RID: 62
			// (get) Token: 0x06000272 RID: 626 RVA: 0x0000D419 File Offset: 0x0000B619
			// (set) Token: 0x06000273 RID: 627 RVA: 0x0000D421 File Offset: 0x0000B621
			public bool matchWithInputManager { get; private set; }

			// Token: 0x06000274 RID: 628 RVA: 0x0000D42A File Offset: 0x0000B62A
			public VirtualAxis(string name) : this(name, true)
			{
			}

			// Token: 0x06000275 RID: 629 RVA: 0x0000D434 File Offset: 0x0000B634
			public VirtualAxis(string name, bool matchToInputSettings)
			{
				this.name = name;
				this.matchWithInputManager = matchToInputSettings;
			}

			// Token: 0x06000276 RID: 630 RVA: 0x0000D44A File Offset: 0x0000B64A
			public void Remove()
			{
				CrossPlatformInputManager.UnRegisterVirtualAxis(this.name);
			}

			// Token: 0x06000277 RID: 631 RVA: 0x0000D457 File Offset: 0x0000B657
			public void Update(float value)
			{
				this.m_Value = value;
			}

			// Token: 0x1700003F RID: 63
			// (get) Token: 0x06000278 RID: 632 RVA: 0x0000D460 File Offset: 0x0000B660
			public float GetValue
			{
				get
				{
					return this.m_Value;
				}
			}

			// Token: 0x17000040 RID: 64
			// (get) Token: 0x06000279 RID: 633 RVA: 0x0000D460 File Offset: 0x0000B660
			public float GetValueRaw
			{
				get
				{
					return this.m_Value;
				}
			}

			// Token: 0x040002CE RID: 718
			private float m_Value;
		}

		// Token: 0x0200007E RID: 126
		public class VirtualButton
		{
			// Token: 0x17000041 RID: 65
			// (get) Token: 0x0600027A RID: 634 RVA: 0x0000D468 File Offset: 0x0000B668
			// (set) Token: 0x0600027B RID: 635 RVA: 0x0000D470 File Offset: 0x0000B670
			public string name { get; private set; }

			// Token: 0x17000042 RID: 66
			// (get) Token: 0x0600027C RID: 636 RVA: 0x0000D479 File Offset: 0x0000B679
			// (set) Token: 0x0600027D RID: 637 RVA: 0x0000D481 File Offset: 0x0000B681
			public bool matchWithInputManager { get; private set; }

			// Token: 0x0600027E RID: 638 RVA: 0x0000D48A File Offset: 0x0000B68A
			public VirtualButton(string name) : this(name, true)
			{
			}

			// Token: 0x0600027F RID: 639 RVA: 0x0000D494 File Offset: 0x0000B694
			public VirtualButton(string name, bool matchToInputSettings)
			{
				this.name = name;
				this.matchWithInputManager = matchToInputSettings;
			}

			// Token: 0x06000280 RID: 640 RVA: 0x0000D4BA File Offset: 0x0000B6BA
			public void Pressed()
			{
				if (this.m_Pressed)
				{
					return;
				}
				this.m_Pressed = true;
				this.m_LastPressedFrame = Time.frameCount;
			}

			// Token: 0x06000281 RID: 641 RVA: 0x0000D4D7 File Offset: 0x0000B6D7
			public void Released()
			{
				this.m_Pressed = false;
				this.m_ReleasedFrame = Time.frameCount;
			}

			// Token: 0x06000282 RID: 642 RVA: 0x0000D4EB File Offset: 0x0000B6EB
			public void Remove()
			{
				CrossPlatformInputManager.UnRegisterVirtualButton(this.name);
			}

			// Token: 0x17000043 RID: 67
			// (get) Token: 0x06000283 RID: 643 RVA: 0x0000D4F8 File Offset: 0x0000B6F8
			public bool GetButton
			{
				get
				{
					return this.m_Pressed;
				}
			}

			// Token: 0x17000044 RID: 68
			// (get) Token: 0x06000284 RID: 644 RVA: 0x0000D500 File Offset: 0x0000B700
			public bool GetButtonDown
			{
				get
				{
					return this.m_LastPressedFrame - Time.frameCount == -1;
				}
			}

			// Token: 0x17000045 RID: 69
			// (get) Token: 0x06000285 RID: 645 RVA: 0x0000D511 File Offset: 0x0000B711
			public bool GetButtonUp
			{
				get
				{
					return this.m_ReleasedFrame == Time.frameCount - 1;
				}
			}

			// Token: 0x040002D2 RID: 722
			private int m_LastPressedFrame = -5;

			// Token: 0x040002D3 RID: 723
			private int m_ReleasedFrame = -5;

			// Token: 0x040002D4 RID: 724
			private bool m_Pressed;
		}
	}
}
