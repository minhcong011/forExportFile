using System;
using UnityEngine;

// Token: 0x02000059 RID: 89
public class tabcode : MonoBehaviour
{
	// Token: 0x0600018F RID: 399 RVA: 0x0000A5D3 File Offset: 0x000087D3
	private void Start()
	{
		this.Player = GameObject.Find("FPSController");
	}

	// Token: 0x06000190 RID: 400 RVA: 0x0000A5E5 File Offset: 0x000087E5
	private void OnMouseDown()
	{
		if (Vector3.Distance(this.Player.transform.position, base.transform.position) <= 3f)
		{
			this.clickTime = Time.time;
		}
	}

	// Token: 0x06000191 RID: 401 RVA: 0x0000A61C File Offset: 0x0000881C
	private void OnMouseUp()
	{
		if (Time.time - this.clickTime <= 0.3f && Vector3.Distance(this.Player.transform.position, base.transform.position) <= 3f)
		{
			this.TakeObject();
		}
	}

	// Token: 0x06000192 RID: 402 RVA: 0x0000A669 File Offset: 0x00008869
	private void TakeObject()
	{
		VariblesGlobal.SelectObject = "Code";
		base.gameObject.SetActive(false);
	}

	// Token: 0x040001A3 RID: 419
	private GameObject Player;

	// Token: 0x040001A4 RID: 420
	private bool ClickUI;

	// Token: 0x040001A5 RID: 421
	private float clickTime;
}
