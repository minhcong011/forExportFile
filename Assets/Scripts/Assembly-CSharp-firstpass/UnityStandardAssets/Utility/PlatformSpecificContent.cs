using System;
using UnityEngine;

namespace UnityStandardAssets.Utility
{
	// Token: 0x02000012 RID: 18
	public class PlatformSpecificContent : MonoBehaviour
	{
		// Token: 0x06000034 RID: 52 RVA: 0x00002E48 File Offset: 0x00001048
		private void OnEnable()
		{
			this.CheckEnableContent();
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002E50 File Offset: 0x00001050
		private void CheckEnableContent()
		{
			if (this.m_BuildTargetGroup == PlatformSpecificContent.BuildTargetGroup.Mobile)
			{
				this.EnableContent(true);
				return;
			}
			this.EnableContent(false);
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002E6C File Offset: 0x0000106C
		private void EnableContent(bool enabled)
		{
			if (this.m_Content.Length != 0)
			{
				foreach (GameObject gameObject in this.m_Content)
				{
					if (gameObject != null)
					{
						gameObject.SetActive(enabled);
					}
				}
			}
			if (this.m_ChildrenOfThisObject)
			{
				foreach (object obj in base.transform)
				{
					((Transform)obj).gameObject.SetActive(enabled);
				}
			}
			if (this.m_MonoBehaviours.Length != 0)
			{
				MonoBehaviour[] monoBehaviours = this.m_MonoBehaviours;
				for (int i = 0; i < monoBehaviours.Length; i++)
				{
					monoBehaviours[i].enabled = enabled;
				}
			}
		}

		// Token: 0x04000045 RID: 69
		[SerializeField]
		private PlatformSpecificContent.BuildTargetGroup m_BuildTargetGroup;

		// Token: 0x04000046 RID: 70
		[SerializeField]
		private GameObject[] m_Content = new GameObject[0];

		// Token: 0x04000047 RID: 71
		[SerializeField]
		private MonoBehaviour[] m_MonoBehaviours = new MonoBehaviour[0];

		// Token: 0x04000048 RID: 72
		[SerializeField]
		private bool m_ChildrenOfThisObject;

		// Token: 0x0200006E RID: 110
		private enum BuildTargetGroup
		{
			// Token: 0x0400029F RID: 671
			Standalone,
			// Token: 0x040002A0 RID: 672
			Mobile
		}
	}
}
