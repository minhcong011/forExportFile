using System;
using UnityEngine;

// Token: 0x0200005D RID: 93
public class WoodGM : MonoBehaviour
{
	// Token: 0x060001A3 RID: 419 RVA: 0x0000A9DE File Offset: 0x00008BDE
	private void Start()
	{
		this.Player = GameObject.Find("FPSController");
	}

	// Token: 0x060001A4 RID: 420 RVA: 0x0000A9F0 File Offset: 0x00008BF0
	private void OnMouseDown()
	{
		if (Vector3.Distance(this.Player.transform.position, base.transform.position) <= 5f)
		{
			this.clickTime = Time.time;
		}
	}

	// Token: 0x060001A5 RID: 421 RVA: 0x0000AA24 File Offset: 0x00008C24
	private void OnMouseUp()
	{
		if (Time.time - this.clickTime <= 0.3f && Vector3.Distance(this.Player.transform.position, base.transform.position) <= 5f)
		{
			this.TakeObject();
		}
	}

	// Token: 0x060001A6 RID: 422 RVA: 0x0000AA71 File Offset: 0x00008C71
	private void TakeObject()
	{
		VariblesGlobal.SelectObject = "Wood";
	}

	// Token: 0x040001AD RID: 429
	private GameObject Player;

	// Token: 0x040001AE RID: 430
	private float clickTime;
}
