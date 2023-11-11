using UnityEngine;
using UnityEngine.EventSystems;

namespace UnityStandardAssets.CrossPlatformInput
{
	// Token: 0x02000033 RID: 51
	[ExecuteInEditMode]
	public class MobileControlRig : MonoBehaviour
	{
		// Token: 0x060000DE RID: 222 RVA: 0x000064FF File Offset: 0x000046FF
		private void OnEnable()
		{
			this.CheckEnableControlRig();
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00006507 File Offset: 0x00004707
		private void Start()
		{
			if (Object.FindObjectOfType<EventSystem>() == null)
			{
				GameObject gameObject = new GameObject("EventSystem");
				gameObject.AddComponent<EventSystem>();
				gameObject.AddComponent<StandaloneInputModule>();
			}
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x0000652D File Offset: 0x0000472D
		private void CheckEnableControlRig()
		{
			this.EnableControlRig(true);
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00006538 File Offset: 0x00004738
		private void EnableControlRig(bool enabled)
		{
			foreach (object obj in base.transform)
			{
				((Transform)obj).gameObject.SetActive(enabled);
			}
		}
	}
}
