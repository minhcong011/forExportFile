using System;
using UnityEngine;

// Token: 0x0200004F RID: 79
public class Molotok : MonoBehaviour
{
	// Token: 0x0600015D RID: 349 RVA: 0x0000995C File Offset: 0x00007B5C
	private void Start()
	{
		this.Player = GameObject.Find("FPSController");
	}

	// Token: 0x0600015E RID: 350 RVA: 0x0000996E File Offset: 0x00007B6E
	private void OnMouseDown()
	{
		if (Vector3.Distance(this.Player.transform.position, base.transform.position) <= 3f)
		{
			this.clickTime = Time.time;
		}
	}

	// Token: 0x0600015F RID: 351 RVA: 0x000099A4 File Offset: 0x00007BA4
	private void OnMouseUp()
	{
		if (Time.time - this.clickTime <= 0.3f && Vector3.Distance(this.Player.transform.position, base.transform.position) <= 3f)
		{
			this.TakeObject();
		}
	}

	// Token: 0x06000160 RID: 352 RVA: 0x000099F1 File Offset: 0x00007BF1
	private void TakeObject()
	{
		VariblesGlobal.SelectObject = "Molotok";
		base.gameObject.SetActive(false);
	}

	// Token: 0x04000189 RID: 393
	private GameObject Player;

	// Token: 0x0400018A RID: 394
	private float clickTime;
}
