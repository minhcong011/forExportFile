using System;
using UnityEngine;
using UnityEngine.UI;

namespace UnityStandardAssets.Utility
{
	// Token: 0x0200000E RID: 14
	[RequireComponent(typeof(Text))]
	public class FPSCounter : MonoBehaviour
	{
		// Token: 0x06000027 RID: 39 RVA: 0x00002CED File Offset: 0x00000EED
		private void Start()
		{
			this.m_FpsNextPeriod = Time.realtimeSinceStartup + 0.5f;
			this.m_Text = base.GetComponent<Text>();
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002D0C File Offset: 0x00000F0C
		private void Update()
		{
			this.m_FpsAccumulator++;
			if (Time.realtimeSinceStartup > this.m_FpsNextPeriod)
			{
				this.m_CurrentFps = (int)((float)this.m_FpsAccumulator / 0.5f);
				this.m_FpsAccumulator = 0;
				this.m_FpsNextPeriod += 0.5f;
				this.m_Text.text = string.Format("{0} FPS", this.m_CurrentFps);
			}
		}

		// Token: 0x04000034 RID: 52
		private const float fpsMeasurePeriod = 0.5f;

		// Token: 0x04000035 RID: 53
		private int m_FpsAccumulator;

		// Token: 0x04000036 RID: 54
		private float m_FpsNextPeriod;

		// Token: 0x04000037 RID: 55
		private int m_CurrentFps;

		// Token: 0x04000038 RID: 56
		private const string display = "{0} FPS";

		// Token: 0x04000039 RID: 57
		private Text m_Text;
	}
}
