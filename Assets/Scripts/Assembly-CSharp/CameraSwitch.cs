using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000008 RID: 8
public class CameraSwitch : MonoBehaviour
{
	// Token: 0x06000013 RID: 19 RVA: 0x000024CF File Offset: 0x000006CF
	private void OnEnable()
	{
		this.text.text = this.objects[this.m_CurrentActiveObject].name;
	}

	// Token: 0x06000014 RID: 20 RVA: 0x000024F0 File Offset: 0x000006F0
	public void NextCamera()
	{
		int num = (this.m_CurrentActiveObject + 1 >= this.objects.Length) ? 0 : (this.m_CurrentActiveObject + 1);
		for (int i = 0; i < this.objects.Length; i++)
		{
			this.objects[i].SetActive(i == num);
		}
		this.m_CurrentActiveObject = num;
		this.text.text = this.objects[this.m_CurrentActiveObject].name;
	}

	// Token: 0x0400000C RID: 12
	public GameObject[] objects;

	// Token: 0x0400000D RID: 13
	public Text text;

	// Token: 0x0400000E RID: 14
	private int m_CurrentActiveObject;
}
