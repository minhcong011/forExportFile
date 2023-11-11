using System;
using System.Collections.Generic;
using UnityEngine;

namespace UnityStandardAssets.CrossPlatformInput
{
	// Token: 0x02000036 RID: 54
	public abstract class VirtualInput
	{
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x060000F0 RID: 240 RVA: 0x0000698D File Offset: 0x00004B8D
		// (set) Token: 0x060000F1 RID: 241 RVA: 0x00006995 File Offset: 0x00004B95
		public Vector3 virtualMousePosition { get; private set; }

		// Token: 0x060000F2 RID: 242 RVA: 0x0000699E File Offset: 0x00004B9E
		public bool AxisExists(string name)
		{
			return this.m_VirtualAxes.ContainsKey(name);
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x000069AC File Offset: 0x00004BAC
		public bool ButtonExists(string name)
		{
			return this.m_VirtualButtons.ContainsKey(name);
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x000069BC File Offset: 0x00004BBC
		public void RegisterVirtualAxis(CrossPlatformInputManager.VirtualAxis axis)
		{
			if (this.m_VirtualAxes.ContainsKey(axis.name))
			{
				Debug.LogError("There is already a virtual axis named " + axis.name + " registered.");
				return;
			}
			this.m_VirtualAxes.Add(axis.name, axis);
			if (!axis.matchWithInputManager)
			{
				this.m_AlwaysUseVirtual.Add(axis.name);
			}
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00006A24 File Offset: 0x00004C24
		public void RegisterVirtualButton(CrossPlatformInputManager.VirtualButton button)
		{
			if (this.m_VirtualButtons.ContainsKey(button.name))
			{
				Debug.LogError("There is already a virtual button named " + button.name + " registered.");
				return;
			}
			this.m_VirtualButtons.Add(button.name, button);
			if (!button.matchWithInputManager)
			{
				this.m_AlwaysUseVirtual.Add(button.name);
			}
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00006A8A File Offset: 0x00004C8A
		public void UnRegisterVirtualAxis(string name)
		{
			if (this.m_VirtualAxes.ContainsKey(name))
			{
				this.m_VirtualAxes.Remove(name);
			}
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00006AA7 File Offset: 0x00004CA7
		public void UnRegisterVirtualButton(string name)
		{
			if (this.m_VirtualButtons.ContainsKey(name))
			{
				this.m_VirtualButtons.Remove(name);
			}
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00006AC4 File Offset: 0x00004CC4
		public CrossPlatformInputManager.VirtualAxis VirtualAxisReference(string name)
		{
			if (!this.m_VirtualAxes.ContainsKey(name))
			{
				return null;
			}
			return this.m_VirtualAxes[name];
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x00006AE2 File Offset: 0x00004CE2
		public void SetVirtualMousePositionX(float f)
		{
			this.virtualMousePosition = new Vector3(f, this.virtualMousePosition.y, this.virtualMousePosition.z);
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00006B06 File Offset: 0x00004D06
		public void SetVirtualMousePositionY(float f)
		{
			this.virtualMousePosition = new Vector3(this.virtualMousePosition.x, f, this.virtualMousePosition.z);
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00006B2A File Offset: 0x00004D2A
		public void SetVirtualMousePositionZ(float f)
		{
			this.virtualMousePosition = new Vector3(this.virtualMousePosition.x, this.virtualMousePosition.y, f);
		}

		// Token: 0x060000FC RID: 252
		public abstract float GetAxis(string name, bool raw);

		// Token: 0x060000FD RID: 253
		public abstract bool GetButton(string name);

		// Token: 0x060000FE RID: 254
		public abstract bool GetButtonDown(string name);

		// Token: 0x060000FF RID: 255
		public abstract bool GetButtonUp(string name);

		// Token: 0x06000100 RID: 256
		public abstract void SetButtonDown(string name);

		// Token: 0x06000101 RID: 257
		public abstract void SetButtonUp(string name);

		// Token: 0x06000102 RID: 258
		public abstract void SetAxisPositive(string name);

		// Token: 0x06000103 RID: 259
		public abstract void SetAxisNegative(string name);

		// Token: 0x06000104 RID: 260
		public abstract void SetAxisZero(string name);

		// Token: 0x06000105 RID: 261
		public abstract void SetAxis(string name, float value);

		// Token: 0x06000106 RID: 262
		public abstract Vector3 MousePosition();

		// Token: 0x040000F1 RID: 241
		protected Dictionary<string, CrossPlatformInputManager.VirtualAxis> m_VirtualAxes = new Dictionary<string, CrossPlatformInputManager.VirtualAxis>();

		// Token: 0x040000F2 RID: 242
		protected Dictionary<string, CrossPlatformInputManager.VirtualButton> m_VirtualButtons = new Dictionary<string, CrossPlatformInputManager.VirtualButton>();

		// Token: 0x040000F3 RID: 243
		protected List<string> m_AlwaysUseVirtual = new List<string>();
	}
}
