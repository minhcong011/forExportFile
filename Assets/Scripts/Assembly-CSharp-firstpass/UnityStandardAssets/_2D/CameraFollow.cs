using System;
using UnityEngine;

namespace UnityStandardAssets._2D
{
	// Token: 0x02000060 RID: 96
	public class CameraFollow : MonoBehaviour
	{
		// Token: 0x06000210 RID: 528 RVA: 0x0000C2A1 File Offset: 0x0000A4A1
		private void Awake()
		{
			this.m_Player = GameObject.FindGameObjectWithTag("Player").transform;
		}

		// Token: 0x06000211 RID: 529 RVA: 0x0000C2B8 File Offset: 0x0000A4B8
		private bool CheckXMargin()
		{
			return Mathf.Abs(base.transform.position.x - this.m_Player.position.x) > this.xMargin;
		}

		// Token: 0x06000212 RID: 530 RVA: 0x0000C2E8 File Offset: 0x0000A4E8
		private bool CheckYMargin()
		{
			return Mathf.Abs(base.transform.position.y - this.m_Player.position.y) > this.yMargin;
		}

		// Token: 0x06000213 RID: 531 RVA: 0x0000C318 File Offset: 0x0000A518
		private void Update()
		{
			this.TrackPlayer();
		}

		// Token: 0x06000214 RID: 532 RVA: 0x0000C320 File Offset: 0x0000A520
		private void TrackPlayer()
		{
			float num = base.transform.position.x;
			float num2 = base.transform.position.y;
			if (this.CheckXMargin())
			{
				num = Mathf.Lerp(base.transform.position.x, this.m_Player.position.x, this.xSmooth * Time.deltaTime);
			}
			if (this.CheckYMargin())
			{
				num2 = Mathf.Lerp(base.transform.position.y, this.m_Player.position.y, this.ySmooth * Time.deltaTime);
			}
			num = Mathf.Clamp(num, this.minXAndY.x, this.maxXAndY.x);
			num2 = Mathf.Clamp(num2, this.minXAndY.y, this.maxXAndY.y);
			base.transform.position = new Vector3(num, num2, base.transform.position.z);
		}

		// Token: 0x04000260 RID: 608
		public float xMargin = 1f;

		// Token: 0x04000261 RID: 609
		public float yMargin = 1f;

		// Token: 0x04000262 RID: 610
		public float xSmooth = 8f;

		// Token: 0x04000263 RID: 611
		public float ySmooth = 8f;

		// Token: 0x04000264 RID: 612
		public Vector2 maxXAndY;

		// Token: 0x04000265 RID: 613
		public Vector2 minXAndY;

		// Token: 0x04000266 RID: 614
		private Transform m_Player;
	}
}
