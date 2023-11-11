using UnityEngine;
using UnityEngine.UI;

namespace UnityStandardAssets.Utility
{
	// Token: 0x02000013 RID: 19
	public class SimpleActivatorMenu : MonoBehaviour
	{
		// Token: 0x06000038 RID: 56 RVA: 0x00002F50 File Offset: 0x00001150
		private void OnEnable()
		{
			this.m_CurrentActiveObject = 0;
			this.camSwitchButton.text = this.objects[this.m_CurrentActiveObject].name;
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002F78 File Offset: 0x00001178
		public void NextCamera()
		{
			int num = (this.m_CurrentActiveObject + 1 >= this.objects.Length) ? 0 : (this.m_CurrentActiveObject + 1);
			for (int i = 0; i < this.objects.Length; i++)
			{
				this.objects[i].SetActive(i == num);
			}
			this.m_CurrentActiveObject = num;
			this.camSwitchButton.text = this.objects[this.m_CurrentActiveObject].name;
		}

		// Token: 0x04000049 RID: 73
		public Text camSwitchButton;

		// Token: 0x0400004A RID: 74
		public GameObject[] objects;

		// Token: 0x0400004B RID: 75
		private int m_CurrentActiveObject;
	}
}
