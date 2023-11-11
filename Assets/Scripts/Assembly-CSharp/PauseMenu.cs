using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000006 RID: 6
public class PauseMenu : MonoBehaviour
{
	// Token: 0x0600000A RID: 10 RVA: 0x000023EF File Offset: 0x000005EF
	private void Awake()
	{
		this.m_MenuToggle = base.GetComponent<Toggle>();
	}

	// Token: 0x0600000B RID: 11 RVA: 0x000023FD File Offset: 0x000005FD
	private void MenuOn()
	{
		this.m_TimeScaleRef = Time.timeScale;
		Time.timeScale = 0f;
		this.m_VolumeRef = AudioListener.volume;
		AudioListener.volume = 0f;
		this.m_Paused = true;
	}

	// Token: 0x0600000C RID: 12 RVA: 0x00002430 File Offset: 0x00000630
	public void MenuOff()
	{
		Time.timeScale = this.m_TimeScaleRef;
		AudioListener.volume = this.m_VolumeRef;
		this.m_Paused = false;
	}

	// Token: 0x0600000D RID: 13 RVA: 0x0000244F File Offset: 0x0000064F
	public void OnMenuStatusChange()
	{
		if (this.m_MenuToggle.isOn && !this.m_Paused)
		{
			this.MenuOn();
			return;
		}
		if (!this.m_MenuToggle.isOn && this.m_Paused)
		{
			this.MenuOff();
		}
	}

	// Token: 0x04000007 RID: 7
	private Toggle m_MenuToggle;

	// Token: 0x04000008 RID: 8
	private float m_TimeScaleRef = 1f;

	// Token: 0x04000009 RID: 9
	private float m_VolumeRef = 1f;

	// Token: 0x0400000A RID: 10
	private bool m_Paused;
}
