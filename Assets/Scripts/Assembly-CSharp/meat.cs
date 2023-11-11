using System;
using UnityEngine;

// Token: 0x0200004D RID: 77
public class meat : MonoBehaviour
{
	// Token: 0x06000153 RID: 339 RVA: 0x00009745 File Offset: 0x00007945
	private void Start()
	{
		this.Player = GameObject.Find("FPSController");
	}

	// Token: 0x06000154 RID: 340 RVA: 0x00009757 File Offset: 0x00007957
	private void OnMouseDown()
	{
		if (Vector3.Distance(this.Player.transform.position, base.transform.position) <= 3f)
		{
			this.clickTime = Time.time;
		}
	}

	// Token: 0x06000155 RID: 341 RVA: 0x0000978C File Offset: 0x0000798C
	private void OnMouseUp()
	{
		if (Time.time - this.clickTime <= 0.3f && Vector3.Distance(this.Player.transform.position, base.transform.position) <= 3f)
		{
			this.TakeObject();
		}
	}

	// Token: 0x06000156 RID: 342 RVA: 0x000097D9 File Offset: 0x000079D9
	private void TakeObject()
	{
		VariblesGlobal.SelectObject = "meat";
	}

	// Token: 0x04000185 RID: 389
	private GameObject Player;

	// Token: 0x04000186 RID: 390
	private float clickTime;
}
