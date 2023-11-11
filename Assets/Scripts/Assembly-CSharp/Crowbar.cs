using System;
using UnityEngine;

// Token: 0x02000043 RID: 67
public class Crowbar : MonoBehaviour
{
	// Token: 0x06000121 RID: 289 RVA: 0x00008E39 File Offset: 0x00007039
	private void Start()
	{
		this.Player = GameObject.Find("FPSController");
	}

	// Token: 0x06000122 RID: 290 RVA: 0x00008E4B File Offset: 0x0000704B
	private void OnMouseDown()
	{
		if (Vector3.Distance(this.Player.transform.position, base.transform.position) <= 3f)
		{
			this.clickTime = Time.time;
		}
	}

	// Token: 0x06000123 RID: 291 RVA: 0x00008E80 File Offset: 0x00007080
	private void OnMouseUp()
	{
		if (Time.time - this.clickTime <= 0.3f && Vector3.Distance(this.Player.transform.position, base.transform.position) <= 3f)
		{
			this.TakeObject();
		}
	}

	// Token: 0x06000124 RID: 292 RVA: 0x00008ECD File Offset: 0x000070CD
	private void TakeObject()
	{
		VariblesGlobal.SelectObject = "Crowbar";
		base.gameObject.SetActive(false);
	}

	// Token: 0x0400016C RID: 364
	private GameObject Player;

	// Token: 0x0400016D RID: 365
	private float clickTime;
}
