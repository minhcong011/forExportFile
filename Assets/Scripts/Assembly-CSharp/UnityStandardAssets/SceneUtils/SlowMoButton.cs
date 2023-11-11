using System;
using UnityEngine;
using UnityEngine.UI;

namespace UnityStandardAssets.SceneUtils
{
	// Token: 0x020000B2 RID: 178
	public class SlowMoButton : MonoBehaviour
	{
		// Token: 0x06000339 RID: 825 RVA: 0x000141D3 File Offset: 0x000123D3
		private void Start()
		{
			this.m_SlowMo = false;
		}

		// Token: 0x0600033A RID: 826 RVA: 0x000141DC File Offset: 0x000123DC
		private void OnDestroy()
		{
			Time.timeScale = 1f;
		}

		// Token: 0x0600033B RID: 827 RVA: 0x000141E8 File Offset: 0x000123E8
		public void ChangeSpeed()
		{
			this.m_SlowMo = !this.m_SlowMo;
			Image image = this.button.targetGraphic as Image;
			if (image != null)
			{
				image.sprite = (this.m_SlowMo ? this.SlowSpeedTex : this.FullSpeedTex);
			}
			this.button.targetGraphic = image;
			Time.timeScale = (this.m_SlowMo ? this.slowSpeed : this.fullSpeed);
		}

		// Token: 0x0400037A RID: 890
		public Sprite FullSpeedTex;

		// Token: 0x0400037B RID: 891
		public Sprite SlowSpeedTex;

		// Token: 0x0400037C RID: 892
		public float fullSpeed = 1f;

		// Token: 0x0400037D RID: 893
		public float slowSpeed = 0.3f;

		// Token: 0x0400037E RID: 894
		public Button button;

		// Token: 0x0400037F RID: 895
		private bool m_SlowMo;
	}
}
